package routers

import (
	beego "github.com/beego/beego/v2/server/web"
	"sign/controllers"
)

var shop bool=true

func init()  {
	RegisterRouter()
}
func RegisterRouter() {
	beego.Router("/", &controllers.MainController{})
	beego.Router("/user/login", &controllers.UserController{})
	beego.Router("/test", &controllers.TestController{})
	beego.Router("/music", &controllers.MusicController{})
	beego.Router("/music/list", &controllers.GetMusicController{})
	beego.Router("/music/:page:int/:size:int", &controllers.GetMusicController{})
	if shop{
		beego.Router("/sign", &controllers.BuyerSignCtrl{})
		beego.Router("/sign/get", &controllers.GetBuyerSignCtrl{})
		beego.Router("/sign/records/:page:int/:size:int", &controllers.GetBuyerSignRecordCtrl{})
		beego.Router("/sign/logs/:page:int/:size:int", &controllers.GetBuyerSignLogCtrl{})
	}else{
		beego.Router("/sign/count", &controllers.SignController{})
		beego.Router("/sign/count", &controllers.SignController{})
		beego.Router("/sign/count", &controllers.SignController{})
		beego.Router("/sign/count", &controllers.SignController{})
		beego.Router("/sign/count", &controllers.SignController{})
	}

}
