package test

import (
	model "sign/models"
	"sign/services"
	"github.com/beego/beego/v2/client/orm"
	"log"
	"math/rand"
	"strconv"
	"sync/atomic"
	"testing"
	"time"
)

var o orm.Ormer

func init() {
	//sql show
	orm.Debug=true
	o=orm.NewOrm()
	ids= map[string]int64{}
	ids[userSignInKey]=0
	ids[userSignInDetailKey]=0
	ids[relatedTagKey]=0
	ids[tagKey]=0
	ids[itemInTagKey]=0
}
var ids map[string]int64
var idStrs map[string]string
var userSignInKey="UserSignIn"
var userSignInDetailKey="UserSignInDetail"
var relatedTagKey="RelatedTag"
var tagKey="Tag"
var itemInTagKey="ItemInTag"
func Test(t *testing.T)  {
	//delete all
	//只能 根据 id 删除
	//rows,err:=o.QueryTable(model.UserSignIn{}).Delete() //ex
	//if err!=nil{
	//	t.Log("UserSignIn delete fail,rows:"+strconv.FormatInt(rows,10)+", error:"+err.Error())
	//}else {
	//	t.Log("UserSignIn delete success,rows:"+strconv.FormatInt(rows,10))
	//}
	//syntax error [truncate table tn_UserSignIns | truncate  tn_UserSignIns]
	sql:="truncate table tn_UserSignIns;truncate table  tn_UserSignInDetails;"
	sql+="truncate table tn_Tags; truncate table tn_RelatedTags; truncate  table tn_ItemInTags;"

	sql="delete from  tn_UserSignIns;delete from   tn_UserSignInDetails;"
	sql+="delete from  tn_Tags; delete from  tn_RelatedTags; delete from  tn_ItemInTags;"
	res,err:=o.Raw(sql).Exec()
	if err!=nil{
		t.Log("UserSignIn delete(truncate) fail, error:"+err.Error())
	}
	rows,err:=res.RowsAffected()
	if err==nil{
		t.Log("UserSignIn delete (truncate) success,rows:"+strconv.FormatInt(rows,10))
	}
	//重复 插入  需要 逻辑 处理 不会 报错
	initData(100,t)
	n:=time.Now()
	//sqlite  不支持 时间 查询 最好 统一 成 时间戳 处理 后期 迁移 nosql 或其他 方便 不然 后期 统一 不了
	signInTime:=services.GetSignInTime(1,time.Date(n.Year(),n.Month(),n.Day(),0,0,0,0,time.UTC).Unix())
	if signInTime>0{
		log.Println("find signInTime success,signInTime:"+strconv.FormatInt(signInTime,10))
	}else{
		log.Println("find  signInTime fail,signInTime:")
	}
	//sign:=services.IsSignIn(1,false)
	sign:=services.IsSignIn(1,true)
	log.Println("IsSignIn  :"+strconv.FormatBool(sign))

	signHistorys:=services.GetUserHistorDetails(1,1,false)
	if signHistorys!=nil{
		log.Println("GetUserHistorDetails success,rows:"+strconv.Itoa(len(signHistorys)))
	}else{
		log.Println("GetUserHistorDetails fail,rows:0")
	}

	signCount:=services.GetSignInTodayCount()
	log.Println("GetSignInTodayCount rows:"+strconv.FormatInt(signCount,10))

	deleteUserSignInTrashDatas:=services.DeleteUserSignInTrashDatas()
	log.Println("DeleteUserSignInTrashDatas  :"+strconv.FormatBool(deleteUserSignInTrashDatas))

	updateUserSignInTask:=services.UpdateUserSignInTask()
	log.Println("UpdateUserSignInTask  :"+strconv.FormatBool(updateUserSignInTask))

	getUserSignInsByKeywordAndSort:=services.GetUserSignInsByKeywordAndSort("",model.SignCount_Desc,1,10)
	if getUserSignInsByKeywordAndSort!=nil{
		log.Println("GetUserSignInsByKeywordAndSort success,rows:"+strconv.Itoa(len(getUserSignInsByKeywordAndSort)))
	}else{
		log.Println("GetUserSignInsByKeywordAndSort fail,rows:0")
	}

	getUserSignInByUserId:=services.GetUserSignInByUserId(1)
	if getUserSignInByUserId!=nil{
		log.Println("GetUserSignInByUserId success,id:"+strconv.FormatInt(getUserSignInByUserId.Id,10))
	}else{
		log.Println("GetUserSignInByUserId fail,rows:0")
	}

	getItemIds:=services.GetItemIds("","",1,10,true)
	if getItemIds!=nil{
		log.Println("GetItemIds success,rows:"+strconv.Itoa(len(getItemIds)))
	}else{
		log.Println("GetItemIds fail,rows:0")
	}

	clearTagsFromItem:=services.ClearTagsFromItem(1,"")
	log.Println("ClearTagsFromItem ,rows:"+strconv.FormatInt(clearTagsFromItem,10))
}


func initData(rows int,t *testing.T) {
	signs := make([]model.UserSignIn, rows)
	for i := 0; i < rows; i++ {
		sign := model.UserSignIn{Id: getId(userSignInKey), UserId: 1,
			LastSignedIn: time.Now().AddDate(0, i+1, i+1).Unix()}
		signs[i] = sign
	}
	logOutput(t,"UserSignIn",rows,signs)

	signDetails := make([]model.UserSignInDetail, rows)
	for i := 0; i < rows; i++ {
		//time 2020-04-19 17:20:00 time 2020-05-21 09:20:00 time.Now().Unix()-8*60*60*1000
		//time 2020-04-19 17:20:00 time 2020-05-18 09:20:00 time.Now().Unix()+int64(rand.Intn(1*60*60*1000))
		signDetail := model.UserSignInDetail{Id: getId(userSignInDetailKey), UserId: 1,
			DateCreated: time.Now().Unix()+int64(rand.Intn(1*60*60))}
			//DateCreated: time.Now().Unix()}
		signDetails[i] = signDetail
	}
	logOutput(t,"UserSignInDetail",rows,signDetails)

	tags := make([]model.Tag, rows)
	for i := 0; i < rows; i++ {
		tag := model.Tag{TagId: getId(tagKey), TagName: "test" + strconv.Itoa(i),
			DateCreated: time.Now().AddDate(0, i+1, i+1).Unix()}
		tags[i] = tag
	}
	logOutput(t,"Tag",rows,tags)

	itemInTags := make([]model.ItemInTag, rows)
	for i := 0; i < rows; i++ {
		itemInTag := model.ItemInTag{Id: getId(itemInTagKey),TagName: "test" + strconv.Itoa(i)}
		itemInTags[i] = itemInTag
	}
	logOutput(t,"ItemInTag",rows,itemInTags)

	relatedTags := make([]model.RelatedTag, rows)
	for i := 0; i < rows; i++ {
		r:=rand.Int63n(int64(rows))
		relatedTag := model.RelatedTag{Id: getId(relatedTagKey), TagId: r,RelatedTagId: rand.Int63n(int64(rows))}
		relatedTags[i] = relatedTag
	}
	logOutput(t,"RelatedTag",rows,relatedTags)

}
func logOutput(t *testing.T,name string,rows int,datas interface{}) {
	row, err := o.InsertMulti(rows, datas)
	if err != nil {
		t.Log(name + " init data fail, rows:" + strconv.FormatInt(row, 10) + " ,error: " + err.Error())
	} else {
		t.Log(name + " init data success, rows:" + strconv.FormatInt(row, 10))
	}
}

func getId(name string) int64  {
	id:=ids[name]
	atomic.AddInt64(&id,1)
	ids[name]=id
	return id
}


