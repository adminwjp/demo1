package controllers

import (
	beego "github.com/beego/beego/v2/server/web"
	"strconv"
	"sign/services"
	"time"
	"sign/models"
)


type SignController struct {
	beego.Controller
}
func (this *SignController) Get() {
	userId,err:=this.GetInt64("user_id",0)
	if err!=nil{
		this.Ctx.WriteString("false")
		return
	}
	if userId==0{
		this.Ctx.WriteString("false")
		return
	}
	sign:=services.IsSignIn(userId,true)
	if sign{
		this.Ctx.WriteString("false")
		return
	}
	services.Add(models.UserSignInDetail{})
	this.Ctx.WriteString("true")
}



type SignInTodayCountController struct {
	beego.Controller
}
func (this *SignInTodayCountController) Get() {
	count:=services.GetSignInTodayCount()
	this.Ctx.WriteString(strconv.FormatInt(count,10))
}

type SignByKeywordController struct {
	beego.Controller
}
func (this *SignByKeywordController) Get() {
	keyword:=this.GetString("keyword","")
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
	data:=services.GetUserSignInsByKeywordAndSort(keyword,models.SignCount_Desc,page,size)
	this.Data["data"]=data
}

type SignByUserIdController struct {
	beego.Controller
}
func (this *SignByUserIdController) Get() {
	userId,err:=this.GetInt64("user_id",0)
	if err!=nil{
		this.Ctx.WriteString("{\"data\":[]}")
		return
	}
	if userId==0{
		this.Ctx.WriteString("{\"data\":[]}")
		return
	}
	data:=services.GetUserSignInByUserId(userId)
	this.Data["data"]=data
}

type SignHistorDetailsDaysByUserIdAndRecentMonthsController struct {
	beego.Controller
}
func (this *SignHistorDetailsDaysByUserIdAndRecentMonthsController) Get() {
	userId,err:=this.GetInt64("user_id",0)
	if err!=nil{
		this.Ctx.WriteString("{\"sign_days\":[],\"sign_total_day\":0,\"code\":-1}")
		return
	}
	if userId==0{
		this.Ctx.WriteString("{\"sign_days\":[],\"sign_total_day\":0,\"code\":-1}")
		return
	}
	recentMonths,err:=this.GetInt("recent_months",1)
	if err!=nil{

	}
	data:=services.GetUserHistorDetails(userId,recentMonths,false,true,"date_created")
	if data==nil{
		this.Ctx.WriteString("{\"sign_days\":[],\"sign_total_day\":0,\"code\":0}")
		return
	}
	var days []int=make([]int,len(data))
	for i:=0;i<len(data);i++{
		t:=time.Unix(data[i].DateCreated, 0)
		days[i]=t.Day()
	}
	this.Data["sign_days"]=days
	this.Data["sign_total_day"]=len(data)
	this.Data["code"]=1
}