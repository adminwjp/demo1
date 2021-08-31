package services

import (
	"github.com/beego/beego/v2/client/orm"
	"log"
	"reflect"
	"sign/models"
	"sign/util"
	"strconv"
	"time"
)

func BuyerSign(buyerId int64,update bool) (bool,error){
	o := orm.NewOrm()
	sign,err:=IsBuyerSignIn(o,buyerId,true)
	if err!=nil{
		return false,err
	}
	t:=time.Now()
	var buyerSignInRecord models.BuyerSignInRecord=models.BuyerSignInRecord{
		BuyerId:buyerId,SignDate:t.Unix(),SignMonth:int32(t.Month()),
	}
	if sign {
		//upddate
		if update{
			tx,err:=o.Begin()
			if err!=nil{
				return false, err
			}
			var buyerSignInRecordLog models.BuyerSignInRecordLog=models.NewBuyerSignInRecordLog(buyerSignInRecord)
			rows1,err:=tx.Insert(buyerSignInRecordLog)
			if err!=nil||rows1<1{
				return rows1>0,err
			}
			start:=time.Date(t.Year(),t.Month(),t.Day(),0,0,0,0,time.Local).Unix()
			end:=time.Date(t.Year(),t.Month(),t.Day(),23,59,59,0,time.Local).Unix()

			res,err:=tx.Raw("update tn_buyser_sign_in_record set sign_date=? Where buyer_id=? and sign_date>=? and sign_date<=?",buyerSignInRecord.SignDate,buyerId,start,end).Exec()
			if err!=nil{
				rows1,err=  res.RowsAffected()
				if err!=nil||rows1<1{
					tx.Rollback()
					return rows1>0,err
				}
			}
			err=tx.Commit()
			return true,err
		}

		return true,err
	}

	rows,err:=o.Insert(buyerSignInRecord)
	return rows>0,err
}

//根据用户ID查询用户的签到统计
//buyerId 用户id
func GetListBuyerSignInRecordsByBuyerId(buyerId int64,page int,size int) ([]*models.BuyerSignInRecord,error){
	var buyerSignInRecords []*models.BuyerSignInRecord
	o := orm.NewOrm()
	_,err :=o.QueryTable(models.BuyerSignInRecord{}).Filter("buyer_id",buyerId).Limit(size).Offset((page-1)*size).All(&buyerSignInRecords)
	return  buyerSignInRecords,err
}

func GetListBuyerSignInLogsByBuyerId(buyerId int64,page int,size int) ([]*models.BuyerSignInRecord,error){
	return nil,nil
}


//更新每日连签,月累计签
func  UpdateBuyerSignInTask()  bool{
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


//获取今天签到总人数
func  GetBuyerSignInTodayCount(buyerId int64,)(int64,error)  {
	o := orm.NewOrm()
	qs:=o.QueryTable(models.BuyerSignInRecord{})
	n:=time.Now()
	qs=qs.Filter("sign_date__lt",time.Date(n.Year(),n.Month(),n.Day(),23,59,59,999,time.Local))
	if buyerId>0{
		qs=qs.Filter("buyer_id",buyerId)
	}
	count,err:= qs.Count()
	if err!=nil{
		return 0,err
	}
	return  count,nil
}

//获取用户签到历史明细 recentMonths不填为当前月
//recentMonths 历史前几个月的
func  GetBuyerSignInRecords(buyerId int64,recentMonths int,isMobile bool,need bool,cols ...string) ([]*models.BuyerSignInRecord,error) {
	if recentMonths>12{
		return nil,nil //最多查询 最近一年数据
	}
	o := orm.NewOrm()
	qs:=o.QueryTable(models.BuyerSignInRecord{}).Filter("buyer_id",buyerId)
	n:=time.Now()
	start:=time.Date(n.Year(),n.Month(),n.Day(),0,0,0,0,time.Local)

	if isMobile{
		qs=qs.Filter("sign_date__gte",start.Unix())
	}else{
		n:=time.Now()
		if recentMonths>0{
			//历史 记录时间 到 当前 时间
			qs=qs.Filter("sign_date__gte",getHistoryDate(-recentMonths,n).Unix())
		}else{
			//月初 到当前 时间
			qs=qs.Filter("sign_date__gte",time.Date(n.Year(),n.Month(),1,0,0,0,0,time.Local))
		}
		qs=qs.Filter("sign_date__lt",time.Date(n.Year(),n.Month(),n.Day(),23,59,59,999,time.Local))
	}
	var err error
	var buyerSignInRecords []*models.BuyerSignInRecord
	if need{
		_,err=qs.All(&buyerSignInRecords,cols...)
	}else{
		_,err=qs.All(&buyerSignInRecords)
	}
	return  buyerSignInRecords,err
}

//获取用户今天是否签到
func IsBuyerSignIn(o orm.Ormer,buyerId int64,origianSql bool)(bool,error)  {
	t:=time.Now()
	//time.Utc -8h
	start:=time.Date(t.Year(),t.Month(),t.Day(),0,0,0,0,time.Local).Unix()
	end:=time.Date(t.Year(),t.Month(),t.Day(),23,59,59,0,time.Local).Unix()
	if !origianSql{
		//tn_UserSignInDetails tn__UserSignInDetails table not exists
		qs:=o.QueryTable(models.BuyerSignInRecord{}).Filter("buyer_id",buyerId)
		qs=qs.Filter("sign_date__gte",start).Filter("sign_date__lt",end)
		return 	qs.Exist(),nil
	}
	sql:="Select count(id) c From tn_buyser_sign_in_record Where buyer_id=? and sign_date>=? and sign_date<?"
	var container []orm.Params
	l,err:=o.Raw(sql,[]interface{}{buyerId,start,end}).Values(&container)
	if err!=nil||l==0{
		return  false,err
	}
	c,err:=  strconv.ParseInt(container[0]["c"].(string),10,64)
	return  c>0,err
}

//获取用户某天的签到时间
func GetSignDateByBuyerId(o orm.Ormer,buyerId int64,taskTime int64 )(int64,error)  {
	sql:="Select sign_date From tn_buyser_sign_in_record Where buyer_id=? and sign_date>=? and sign_date<?"
	var container []orm.Params
	rows,err:=o.Raw(sql,buyerId,
		taskTime,
		taskTime+util.DayUnixTime,
	).Values(&container)
	if err!=nil{
		logErrorOutput1(err,"GetSignDateByBuyerId find")
		return 0,nil
	}else{
		log.Println("GetSignDateByBuyerId success,rows: "+strconv.FormatInt(rows,10))
		if len(container)>0{
			val:=container[0]["date_created"]
			if reflect.TypeOf(val).Kind()==reflect.String{
				log.Println(container[0]["sign_date"])
				c,err:= strconv.ParseInt(container[0]["sign_date"].(string),10,64)
				 return  c,err
			}else{
				return container[0]["sign_date"].(int64),nil
			}
		}
		return  0,nil
	}
}
