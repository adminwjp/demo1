package services

import (
	"database/sql"
	"log"
	"time"
	"sign/models"

	"github.com/beego/beego/v2/client/orm"
)

//为多个内容项添加相同标签
func AddItemsToTag(itemIds []int64, tenantTypeId string, tagName string) int64 {
	if itemIds == nil && len(itemIds) == 0 {
		return 0
	}

	o := orm.NewOrm()
	tx, err := o.Begin()
	if err != nil {
		return 0
	}
	//sql = "insert into tn_Tags (tenant_type_id,tag_name,date_created) values(?,?,?)"
	c, err := o.Insert(models.Tag{TenantTypeId: tenantTypeId, TagName: tagName, DateCreated: time.Now().Unix()})
	if err != nil {
		tx.Rollback()
		return 0
	}
	rows := c
	for i := 0; i < len(itemIds); i++ {
		c, err := o.Insert(models.ItemInTag{TagName: tagName, TenantTypeId: tenantTypeId, ItemId: itemIds[i]})
		if err != nil {
			tx.Rollback()
			return 0
		}
		rows += c
	}
	res, err := o.Raw("update tn_Tags set item_count = item_count + ? Where item_count > 0 and tag_name = ? and tenant_type_id = ?", rows, tagName, tenantTypeId).Exec()
	return tagsFromItemHandler(tx, func() {
		tx.Commit()
	}, func() { tx.Rollback() }, res, err)
}

//为内容项批量设置标签
func AddTagsToItem(tagNames []string, tenantTypeId string, itemId int64) int64 {
	if tagNames == nil && len(tagNames) == 0 {
		return 0
	}
	o := orm.NewOrm()
	tx, err := o.Begin()
	if err != nil {
		return 0
	}
	var rows int64 = 0
	for i := range tagNames {
		if tagNames[i] == "" {
			continue
		}
		var count int64 = 0
		count, err := o.QueryTable(models.Tag{}).Filter("tag_name", tagNames[i]).Count()
		if err != nil {
			tx.Rollback()
			return 0
		}
		if count == 0 {
			c, err := o.Insert(models.Tag{TenantTypeId: tenantTypeId, TagName: tagNames[i], DateCreated: time.Now().Unix()})
			if err != nil {
				tx.Rollback()
				return 0
			}
			rows += c
		}
		count, err = o.QueryTable(models.ItemInTag{}).Filter("tenant_type_id", tenantTypeId).Filter("item_id", itemId).Count()
		if err != nil {
			tx.Rollback()
			return 0
		}
		if count == 0 {
			c, err := o.Insert(models.ItemInTag{TagName: tagNames[i], TenantTypeId: tenantTypeId, ItemId: itemId})
			if err != nil {
				tx.Rollback()
				return 0
			}
			rows += c
			c = updateTagsFromItem(tenantTypeId, tagNames[i], o, tx, empty, func() {
				tx.Rollback()
			})
			if c == 0 {
				return 0
			}
		}
	}
	if rows > 0 {
		tx.Commit()
	} else {
		tx.Rollback()
	}
	return rows
}

//删除标签与成员的关系实体
//entity 待处理的实体
func DeleteItemInTag(entity *models.ItemInTag) int64 {
	if entity == nil {
		return 0
	}
	o := orm.NewOrm()
	tx, err := o.Begin()
	if err != nil {
		return 0
	}
	c, err := o.Delete(entity)
	if err != nil || c < 1 {
		tx.Rollback()
		return 0
	}
	count := deleteTagsFromItem(entity.ItemId, entity.TenantTypeId, o, tx, func() {
		//tx.Commit()
	}, func() { tx.Rollback() })
	if count > 0 {
		count = updateTagsFromItem(entity.TenantTypeId, entity.TagName, o, tx, func() {
			tx.Commit()
		}, func() { tx.Rollback() })
	}
	return count
}

func deleteTagsFromItem(itemId int64, tenantTypeId string, o orm.Ormer, tx orm.TxOrmer, commit func(), roll func()) int64 {
	res, err := o.Raw("delete  From tn_ItemsInTags Where tenant_type_id=? and item_id=? ", tenantTypeId, itemId).Exec()
	return tagsFromItemHandler(tx, commit, roll, res, err)
}

func updateTagsFromItem(tenantTypeId string, tagName string, o orm.Ormer, tx orm.TxOrmer, commit func(), roll func()) int64 {
	res, err := o.Raw("update tn_Tags set item_count = item_count - 1 Where item_count > 0 and tag_name = ? and tenant_type_id = ?", tagName, tenantTypeId).Exec()
	return tagsFromItemHandler(tx, commit, roll, res, err)
}

func tagsFromItemHandler(tx orm.TxOrmer, commit func(), roll func(), res sql.Result, err error) int64 {
	if err != nil {
		tx.Rollback()
		return 0
	}
	count, err := res.RowsAffected()
	if count > 0 {
		commit()
	}
	roll()
	return count
}
func empty() {
}

//清除内容项的所有标签
func ClearTagsFromItem(itemId int64, tenantTypeId string) int64 {
	o := orm.NewOrm()
	tx, err := o.Begin()
	if err != nil {
		tx.Rollback()
		return 0
	}
	sql := "Select tag_name From tn_ItemsInTags Where tenant_type_id=? and item_id=? "
	var names []orm.Params
	row, err := o.Raw(sql, tenantTypeId, itemId).Values(&names)
	if err != nil {
		tx.Rollback()
		return 0
	}
	if err != nil {
		return 0
	}
	count := deleteTagsFromItem(itemId, tenantTypeId, o, tx, empty, empty)
	if count < 1 {
		tx.Rollback()
		return 0
	}
	if names != nil && len(names) > 0 {
		for i := 0; i < len(names); i++ {
			updateTagsFromItem(tenantTypeId, names[i]["tag_name"].(string), o, tx, empty, empty)
		}
	}
	tx.Commit()
	return row
}

//获取标签的所有内容项集合
func GetTagOfItemIds(tagName string, tenantTypeId string) []int64 {
	return GetItemIds(tagName, tenantTypeId, 1, 1, false)
}

func getItemInTagsWhere(o orm.Ormer, tagName string, tenantTypeId string) orm.QuerySeter {
	qs := o.QueryTable(models.ItemInTag{})
	if tenantTypeId != "" {
		qs = qs.Filter("tenant_type_id", tenantTypeId)
	}
	if tagName != "" {
		qs = qs.Filter("tag_name", tagName)
	}
	return qs
}

//获取标签的内容项集合
func GetItemInTags(tagName string, tenantTypeId string, page int, size int) []models.ItemInTag {
	o := orm.NewOrm()
	var data []models.ItemInTag
	getItemInTagsWhere(o, tagName, tenantTypeId).Offset((page - 1) * size).Limit(size).All(&data)
	return data
}

func GetItemIds(tagName string, tenantTypeId string, page int, size int, isPage bool) []int64 {
	o := orm.NewOrm()
	var data []models.ItemInTag
	qs := getItemInTagsWhere(o, tagName, tenantTypeId)
	if isPage {
		qs = qs.Offset((page - 1) * size).Limit(size)
	}
	qs.All(&data, "item_id")
	if data != nil && len(data) > 0 {
		var ids []int64
		ids = make([]int64, len(data))
		for i := 0; i < len(data); i++ {
			ids[i] = data[i].ItemId
		}
		return ids
	}
	return nil
}

//根据用户ID列表获取用户tag
func GetTagNamesOfUsers(userIds []int64) []map[string]interface{} {
	o := orm.NewOrm()
	var data []models.ItemInTag
	qs := o.QueryTable(models.ItemInTag{}).Filter("item_id__in", userIds)
	qs.All(&data, "item_id", "tag_name")
	if data != nil && len(data) > 0 {
		var users []map[string]interface{}
		users = make([]map[string]interface{}, len(data))
		for i := 0; i < len(data); i++ {
			users[i] = map[string]interface{}{"user_id": data[i].ItemId, "tag_name": data[i].TagName}
		}
		return users
	}
	return nil
}

func logErrorOutput(rows int64, err error, methodName string) {

}

func logErrorOutput1(err error, methodName string) {
	log.Println(methodName + " fail,error: " + err.Error())
}
