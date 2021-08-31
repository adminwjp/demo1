package services

import (
	"github.com/beego/beego/v2/client/orm"
	"log"
	"reflect"
	"strconv"
	"time"
	 "sign/models"
	"sign/util"
)

//根据用户ID查询用户的签到统计
//userId 用户id
//用户签到统计实体
func GetUserSignInByUserId(userId int64) *models.UserSignIn{
	var userSignIns []*models.UserSignIn
	o := orm.NewOrm()
	o.QueryTable(models.UserSignIn{}).Filter("user_id",userId).Limit(1).All(&userSignIns)
	if userSignIns==nil||len(userSignIns)==0{
		return  nil
	}
	return  userSignIns[0]
}



//获取用户签到数据
//keyword 关键字（支持昵称，姓名，手机号，邮箱）
//flag  SignCount_Desc MonthSignCount_Desc
func  GetUserSignInsByKeywordAndSort(keyword string,flag models.UserSignInOrder,page int,size int) []*models.UserSignIn {
	var sql="select tn_UserSignIns.* From tn_UserSignIns "
	var where=""
	if keyword!=""{
		where+="Inner Join tn_Users on tn_Users.user_id=tn_UserSignIns.user_id "
		where+="Where tn_Users.user_name like ? or  tn_Users.true_name like ? or tn_Users.account_mobile like ? or tn_Users.account_email like ? "
	}
	sql+=where
	switch flag{
		case models.SignCount_Desc:sql+=" Order By sign_count desc"
		case models.MonthSignCount_Desc:sql+=" Order By month_sign_count desc"
	}
	var userSignIns []*models.UserSignIn
	o := orm.NewOrm()
	var row int64
	var err error
	if keyword!=""{
		k:="%" + keyword + "%"
		row,err=o.Raw(sql,k,k,k,k).QueryRows(&userSignIns)
	}else{
		row,err=o.Raw(sql).QueryRows(&userSignIns)
	}
	if err!=nil{
		log.Println("GetUserSignInsByKeywordAndSort find fail,error:"+err.Error())
	}else{
		log.Println("GetUserSignInsByKeywordAndSort find success,rows:"+strconv.FormatInt(row,10))
	}
	return  userSignIns
}

//更新每日连签,月累计签
func  UpdateUserSignInTask()  bool{
	t,err:=time.ParseDuration("-24h")
	if err!=nil{
		return false
	}
	sql:="update tn_UserSignIns set continued_sign_count = 0 where last_signed_in <?;"
	if time.Now().Day()==1{
		sql+="update tn_UserSignIns set month_sign_count = 0"
	}
	o := orm.NewOrm()
	tx,err:=o.Begin()
	if err!=nil{
		return false
	}
	res,err:=o.Raw(sql,util.GetTeimByEndYMD().Add(t)).Exec()
	if err!=nil{
		tx.Rollback()
		return  false
	}
	count,err:=res.RowsAffected()
	if count>0{
		tx.Commit()
		return  true
	}else{
		tx.Rollback()
		return  false
	}
}

//删除用户时候删除签到记录 用户不存在 也 删除
func  DeleteUserSignInTrashDatas()bool{
	o := orm.NewOrm()
	tx,err:=o.Begin()
	if err!=nil{
		log.Println("DeleteUserSignInTrashDatas tx begin fail,error:"+err.Error())
		return false
	}
	sql:="delete  from tn_UserSignIns where not exists (select 1 from tn_Users where  tn_UserSignIns.user_id = tn_Users.user_id AND tn_Users.Status>-1);"
	sql+="delete from tn_UserSignInDetails where not exists (select 1 from tn_Users where  tn_UserSignInDetails.user_id = tn_Users.user_id AND tn_Users.Status>-1)"

	res,err:=o.Raw(sql).Exec()
	if err!=nil{
		tx.Rollback()
		log.Println("DeleteUserSignInTrashDatas fail,error:"+err.Error())
		return false
	}
	count,err:=res.RowsAffected()
	if count>0{
		tx.Commit()
		return  true
	}
	tx.Rollback()
	return  false
}

//获取今天签到总人数
func  GetSignInTodayCount()int64  {
	o := orm.NewOrm()
	qs:=o.QueryTable(models.UserSignInDetail{})
	n:=time.Now()
	qs=qs.Filter("date_created__lt",time.Date(n.Year(),n.Month(),n.Day(),23,59,59,999,time.Local))
	count,err:= qs.Count()
	if err!=nil{
		return 0
	}
	return  count
}

//获取用户签到历史明细 recentMonths不填为当前月
//recentMonths 历史前几个月的
func  GetUserHistorDetails(userId int64,recentMonths int,isMobile bool,need bool,cols ...string) []*models.UserSignInDetail {
	if recentMonths>12{
		return nil //最多查询 最近一年数据
	}
	o := orm.NewOrm()
	qs:=o.QueryTable(models.UserSignInDetail{}).Filter("user_id",userId)
	n:=time.Now()
	start:=time.Date(n.Year(),n.Month(),n.Day(),0,0,0,0,time.Local)

	if isMobile{
		qs=qs.Filter("date_created__gte",start.Unix())
	}else{
		n:=time.Now()
		if recentMonths>0{
			//历史 记录时间 到 当前 时间
			qs=qs.Filter("date_created__gte",getHistoryDate(-recentMonths,n).Unix())
		}else{
			//月初 到当前 时间
			qs=qs.Filter("date_created__gte",time.Date(n.Year(),n.Month(),1,0,0,0,0,time.Local))
		}
		qs=qs.Filter("date_created__lt",time.Date(n.Year(),n.Month(),n.Day(),23,59,59,999,time.Local))
	}

	var userSignInDetails []*models.UserSignInDetail
	if need{
		qs.All(&userSignInDetails,cols...)
	}else{
		qs.All(&userSignInDetails)
	}
	return  userSignInDetails
}

func getHistoryDate(month int,n time.Time)time.Time  {
	y,m :=n.Year(),int(n.Month())
	if m+month>12{
		y+=1
		m=m+month-12
	}else{
		if m+month<0{
			y-=1
			m=12+(m+month)
		}else{
			m+=month
		}
	}
	historyDate:=time.Date(y,time.Month(m),n.Day(),0,0,0,0,time.Local)
	return historyDate
}

//获取用户今天是否签到
func IsSignIn(userId int64,origianSql bool)bool  {
	t:=time.Now()
	o := orm.NewOrm()
	//time.Utc -8h
	start:=time.Date(t.Year(),t.Month(),t.Day(),0,0,0,0,time.Local).Unix()
	end:=time.Date(t.Year(),t.Month(),t.Day(),23,59,59,0,time.Local).Unix()
	if !origianSql{
		//tn_UserSignInDetails tn__UserSignInDetails table not exists
		qs:=o.QueryTable(models.UserSignInDetail{}).Filter("user_id",userId)
		qs=qs.Filter("date_created__gte",start).Filter("date_created__lt",end)
		return 	qs.Exist()
	}
	sql:="Select count(id) c From tn_UserSignInDetails Where user_id=? and date_created>=? and date_created<?"
	var container []orm.Params
	l,err:=o.Raw(sql,[]interface{}{userId,start,end}).Values(&container)
	if err!=nil||l==0{
		return  false
	}
	c,err:=  strconv.ParseInt(container[0]["c"].(string),10,64)
	return  c>0
}

//获取用户某天的签到时间
func GetSignInTime(userId int32,taskTime int64 )int64  {
	sql:="Select date_created From tn_UserSignInDetails Where user_id=? and date_created>=? and date_created<?"
	o := orm.NewOrm()
	var container []orm.Params
	rows,err:=o.Raw(sql,userId,
		taskTime,
		taskTime+util.DayUnixTime,
	).Values(&container)
	if err!=nil{
		logErrorOutput1(err,"GetSignInTime find")
		return 0
	}else{
		log.Println("GetSignInTime success,rows: "+strconv.FormatInt(rows,10))
		if len(container)>0{
			val:=container[0]["date_created"]
			if reflect.TypeOf(val).Kind()==reflect.String{
				log.Println(container[0]["date_created"])
				c,err:= strconv.ParseInt(container[0]["date_created"].(string),15,64)
				if err!=nil{
					logErrorOutput1(err,"GetSignInTime find success, parse string to int64 ")
					return  0
				}
				return  c
			}else{
				return container[0]["date_created"].(int64)
			}

		}
		return  0
	}
}
