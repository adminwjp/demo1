package controllers

import (
	beego "github.com/beego/beego/v2/server/web"
	"os"
)
import "github.com/tidwall/gjson"

type AttachmentController struct {
	beego.Controller
}


func (this *AttachmentController) Get() {
	this.Ctx.WriteString("test")
}
type UserController struct {
	beego.Controller
}
func (this *UserController) Get() {
	this.Data["is_cms_detail"] = is_cms_detail
	placeholder := gjson.Get(RegisterTypeJson, "register_type.email_or_mobile")
	this.Data["placeholder"] = placeholder
	this.Data["is_thrid_login"]=true
	this.Data["thrid_login"] = getThridLogin()
	this.TplName = "user/login.tpl"
}
func getThridLogin()  string {
	qq1 := gjson.Get(RegisterTypeJson, "thrid_login.qq")
	weixin1 := gjson.Get(RegisterTypeJson, "thrid_login.wechat")
	weibo1 := gjson.Get(RegisterTypeJson, "thrid_login.weibo")
	alipay1 := gjson.Get(RegisterTypeJson, "thrid_login.alipay")
	str:=""
	if qq1.Bool()==true{
		str+=qq
	}
	if weixin1.Bool()==true{
		str+=weixin
	}
	if weibo1.Bool()==true{
		str+=weibo
	}
	if alipay1.Bool()==true{
		str+=alipay
	}
	return str
}
func getRegisterType(registerType string)  {

}
func initCofig(){
	buffer,err:=os.ReadFile("conf/config.json")
	if err!=nil{
		return
	}
	RegisterTypeJson=string(buffer)
}
var RegisterTypeJson string




var is_cms_detail=`            <div class="jh-position-fixed">
                <ul class="list-unstyled jh-share-style">
                    <li id="topid"><a href="#"><i class="fa fa-chevron-up "></i><br>顶部</a></li>
                </ul>
            </div>`

var qq=` <li><button type="button" class="btn btn-link" onclick="window.location=''"><i class="fa fa-qq"></i></button></li>`
var weixin=`<li><button type="button" class="btn btn-link layerwechat"><i class="fa fa-weixin"></i></button></li>`
var weibo=`<li><button type="button" class="btn btn-link" onclick="window.location=''"><i class="fa fa-weibo"></i></button></li>`
var alipay=`<li><button type="button" class="btn btn-link" onclick="window.location=''"><img src="~/img/alipay-icon.png" width="20" height="20" /></button></li>`