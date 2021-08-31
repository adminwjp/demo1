package services

import (
	"log"
	"strconv"
	"strings"
	"time"
	"news/shop/models"

	"github.com/jinzhu/gorm"
)

type AccountType int32

const (
	Email AccountType = 0
	Phone AccountType = 1
)

func BaseRegister(id int64, account string, pwd string, accountType AccountType, tableName string) bool {
	sql := "insert into " + tableName + "(id,email,password) values(?,?,?);"
	if accountType == Phone {
		sql = "insert into " + tableName + "(id,phone,password) values(?,?,?);"
	}
	db := Db
	return db.Exec(sql, id, account, pwd).Error != nil
}

func BaseExistsAccount(account string, tableName string, accountType AccountType) (bool, error) {
	sql := "select count(id) " + tableName + "where email=?;"
	if accountType == Phone {
		sql = "select count(id) " + tableName + "where phone=?;"
	}
	db := Db
	db = db.Raw(sql, account)
	if db.Error != nil {
		return false, db.Error
	}
	rows, err := db.Rows()
	if err != nil {
		return false, err
	}
	var count int64
	defer rows.Close()
	for rows.Next() {
		rows.Scan(&count)
	}
	return count > 0, nil
}

func BaseUpdateAccount(id int64, account string, newAccount string, accountType AccountType, tableName string) bool {
	sql := "update  " + tableName + "set email=? where id=? ;"
	if accountType == Phone {
		sql = "update  " + tableName + "set phone=? where id=? ;"
	}
	db := Db
	return db.Exec(sql, newAccount, id).Error != nil
}

func BaseUpdateAccountByAccount(oldAccount string, newAccount string, accountType AccountType, tableName string) bool {
	sql := "update  " + tableName + "set email=? where email=? ;"
	if accountType == Phone {
		sql = "update  " + tableName + "set phone=? where phone=? ;"
	}
	db := Db
	return db.Exec(sql, newAccount, oldAccount).Error != nil
}

func BaseUpdatePwdByAccount(account string, pwd string, accountType AccountType, tableName string) bool {
	sql := "update  " + tableName + "set password=? where email=? ;"
	if accountType == Phone {
		sql = "update  " + tableName + "set password=? where phone=? ;"
	}
	db := Db
	return db.Exec(sql, pwd, account).Error != nil
}

func BaseUpdatePwd(id int64, pwd string, newPwd string, tableName string) bool {
	sql := "update  " + tableName + "set password=? where id=? ;"
	db := Db
	return db.Exec(sql, id, newPwd).Error != nil
}

//dubbo thrift go thrift https://www.cnblogs.com/tqlin/p/12125517.html
func BaseAddFriend(id int64, friends string, tableName string, isBuyer bool, addOrUpdate bool, idName string) (bool, error) {
	ids, err := getFriendIds(friends)
	if ids == nil {
		return false, err
	}
	db := Db
	tx := db.Begin()
	add := false
	for i := range ids {
		sql := "select count(id) from " + tableName + " where " + idName + "=? and id=?"
		if !addOrUpdate {
			sql = "select status from " + tableName + " where " + idName + "=? and id=?"
		}
		tx = tx.Raw(sql, ids[i], id)
		res, err := tx.Rows()
		if err != nil {
			tx = tx.Rollback()
			if err != nil {
				return false, err
			}
			return false, err
		}

		if res.Next() {
			var count int64
			err := res.Scan(&count)
			if count < 1 {
				sql = "insert into " + tableName + " (id," + idName + ",add_date) values(?,?,?)"
				if !addOrUpdate {
					sql = "update " + tableName + " set status=1,agree_date=? where id=? and " + idName + "=? "
					tx = tx.Exec(sql, time.Local, id, ids[i])
				} else {
					tx = tx.Exec(sql, id, ids[i], time.Local)
				}
				err = tx.Error
				if tx.Error != nil {
					tx = tx.Rollback()
					if tx.Error != nil {
						return false, tx.Error
					}
					return false, err
				}
				add = true
			} else {
				log.Println("find " + tableName + " friend ,id:" + strconv.FormatInt(id, 10) + "," + idName + ":" + strconv.FormatInt(ids[i], 10))
				return false, err
			}
		}
	}
	if add {
		tx = tx.Commit()
		if tx.Error != nil {
			return false, tx.Error
		}
		return true, nil
	} else {
		err := tx.Error
		tx = tx.Rollback()
		if tx.Error != nil {
			return false, tx.Error
		}
		return false, err
	}
}

func getFriendIds(friends string) ([]int64, error) {
	if friends == "" {
		return nil, nil
	}
	idStrs := strings.Split(friends, ",")
	if idStrs == nil || len(ids) == 0 {
		return nil, nil
	}
	ids := make([]int64, len(idStrs))
	for j := range idStrs {
		i, err := strconv.ParseInt(idStrs[j], 10, 64)
		if err != nil {
			return nil, err
		}
		ids[j] = i
	}
	return ids, nil
}

func BaseAgreeFriend(id int64, friends string, tableName string, isBuyer bool, idName string) (bool, error) {
	return BaseAddFriend(id, friends, tableName, isBuyer, false, idName)
}

//根据 条件 查询 买家 信息
func BuyerList(buyer models.Buyer, page int, size int) ([]*models.Buyer, int64) {
	var buyers []*models.Buyer
	count, _ := getList(buyer, page, size,
		func(db *gorm.DB) *gorm.DB {
			return getBuyerWhere(buyer, db)
		}, func(db *gorm.DB) interface{} {
			db = db.Scan(&buyers)
			return buyers
		}, false)
	return buyers, count
}

func getBuyerWhere(buyer models.Buyer, db *gorm.DB) *gorm.DB {
	if buyer.Id > 0 {
		db = db.Where("id=?", buyer.Id)
	}
	if buyer.Nickname != "" {
		db = db.Where("nickname like ?", "%"+buyer.Nickname+"%")
	}
	if buyer.Active != 0 {
		db = db.Where("active = ?", buyer.Active)
	}
	if buyer.SellerId > 0 {
		db = db.Where("seller_id = ?", buyer.SellerId)
	}
	if buyer.AuthAppId != 0 {
		db = db.Where("auth_app_id = ?", buyer.AuthAppId)
	}
	db = db.Order("created_date desc")
	return db
}

func GetBuyer(id int64) models.Buyer {
	db := Db
	var buyer models.Buyer
	// 通过主键进行查询 (仅适用于主键是数字类型)
	db = db.Model(new(models.Buyer)).First(&buyer, id)
	if db.Error != nil {
		log.Println("GetBuyer fail,err:" + db.Error.Error())
	}
	return buyer
}
func SetBuyerActive(ids []int64, active int32) bool {
	db := Db
	// 通过主键进行查询 (仅适用于主键是数字类型)
	db = db.Model(&models.Buyer{}).Where("id in (?)", ids).Update("active", active)
	if db.Error != nil {
		log.Println("SetBuyerActive fail,err:" + db.Error.Error())
		return false
	}
	return true
}

func SetBuyerIsReceiver(ids []int64, isReceiver int32) bool {
	db := Db
	// 通过主键进行查询 (仅适用于主键是数字类型)
	db = db.Model(&models.Buyer{}).Where("id in (?)", ids).Update("is_receiver", isReceiver)
	if db.Error != nil {
		log.Println("SetBuyerIsReceiver fail,err:" + db.Error.Error())
		return false
	}
	return true
}

func AddBuyer(buyer models.Buyer) bool {
	buyer.Id = Id.GetIdByStruct(buyer)
	res, _ := add(buyer)
	return res
}

func AddBuyerAddr(buyerAddr models.BuyerAddr) bool {
	buyerAddr.Id = Id.GetIdByStruct(buyerAddr)
	res, _ := add(buyerAddr)
	return res
}

func AddBuyerFriend(buyerFriend models.BuyerFriend) bool {
	buyerFriend.Id = Id.GetIdByStruct(buyerFriend)
	res, _ := add(buyerFriend)
	return res
}

func BuyerFriendList(buyerFriend models.BuyerFriend, page int, size int) ([]*models.BuyerFriend, int64) {
	var buyerFriends []*models.BuyerFriend
	data, count, _ := selectList(buyerFriend, page, size, buyerFriends)
	return data.([]*models.BuyerFriend), count
}
