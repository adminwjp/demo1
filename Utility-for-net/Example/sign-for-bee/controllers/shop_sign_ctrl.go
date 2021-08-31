package controllers

import (
	"github.com/beego/beego/v2/client/orm"
	beego "github.com/beego/beego/v2/server/web"
	"sign/services"
	"sign/util"
	"strconv"
)

type BuyerSignCtrl struct {
	beego.Controller
}

func (this *BuyerSignCtrl) Get() {
	buyer_id,err:=this.GetInt64("buyer_id",0)
	if err!=nil{
		this.Ctx.WriteString("false")
		return
	}
	if buyer_id==0{
		this.Ctx.WriteString("false")
		return
	}
	sign,err:=services.BuyerSign(buyer_id,true)
	if !sign||err!=nil{
		this.Ctx.WriteString("false")
		return
	}
	this.Ctx.WriteString("true")
}


type GetBuyerSignCtrl struct {
	beego.Controller
}
func (this *GetBuyerSignCtrl) Get() {
	buyer_id,err:=this.GetInt64("buyer_id",0)
	if err!=nil{
		this.Ctx.WriteString("false")
		return
	}
	if buyer_id==0{
		this.Ctx.WriteString("false")
		return
	}
	o := orm.NewOrm()
	signDate,err:=services.GetSignDateByBuyerId(o,buyer_id,util.GetTeimByStartYMD().Unix())
	if signDate==0|| err!=nil{
		this.Ctx.WriteString("false")
		return
	}
	count,err:=services.GetBuyerSignInTodayCount(buyer_id)
	if err!=nil{
		this.Ctx.WriteString("false")
		return
	}
	this.Ctx.WriteString("{\"sign_date\":"+strconv.FormatInt(signDate,10)+",\"count\":"+strconv.FormatInt(count,10)+"}")
}

type GetBuyerSignRecordCtrl struct {
	beego.Controller
}
func (this *GetBuyerSignCtrl) Post() {
	buyer_id,err:=this.GetInt64("buyer_id",0)
	if err!=nil{
		this.Ctx.WriteString("false")
		return
	}
	if buyer_id==0{
		this.Ctx.WriteString("false")
		return
	}
	page,err:=this.GetInt("page",1)
	if err!=nil{

	}
	size,err:=this.GetInt("size",10)
	if page<1&&page>999{
		page=1
	}
	if size<1&&size>99{
		size=10
	}
	data,err:=services.GetListBuyerSignInRecordsByBuyerId(buyer_id,page,size)
	if err!=nil{
		this.Ctx.WriteString("false")
	}
	this.Data["data"]=data
}

type TestCtrl struct {
	beego.Controller
}
func (this *TestCtrl) Get(){
	this.Ctx.WriteString("test")
}
type GetBuyerSignLogCtrl struct {
	beego.Controller
}



func (this *GetBuyerSignLogCtrl) Post() {
	buyer_id,err:=this.GetInt64("buyer_id",0)
	if err!=nil{
		this.Ctx.WriteString("false")
		return
	}
	if buyer_id==0{
		this.Ctx.WriteString("false")
		return
	}
	page,err:=this.GetInt("page",1)
	if err!=nil{

	}
	size,err:=this.GetInt("size",10)
	if page<1&&page>999{
		page=1
	}
	if size<1&&size>99{
		size=10
	}
	data,err:=services.GetListBuyerSignInLogsByBuyerId(buyer_id,page,size)
	if err!=nil{
		this.Ctx.WriteString("false")
	}
	this.Data["data"]=data
}