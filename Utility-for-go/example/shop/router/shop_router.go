package router

import (
	"github.com/gin-gonic/gin"
	"shop/controllers"
)
/**
用户： 	买家,卖家,厂家,代理商
或 买家 (申请开通卖家)
用户注册：
用户登录：
修改邮箱：
修改密码：
修改手机号:
找回密码：邮箱或手机号

用户地址：用户地址列表,默认用户地址(只有一个),修改默认用户地址,添加用户地址,修改用户地址
用户好友(聊天):添加用户好友(对方同意),用户好友列表
用户举报:举报,撤销举报,修改举报原因,举报记录,举报审核日志
*/
func InitShopRouter(r *gin.Engine)  {

	r.GET("/check", controllers.Check)

	r.GET("/buyer/list", controllers.BuyerList)
	r.POST("/buyer/list", controllers.BuyerList)

	r.POST("/buyer/register", controllers.RegisterBuyer)
	r.POST("/buyer/login", controllers.LoginBuyer)
	r.POST("/buyer/update_email", controllers.UpdateEmailByBuyer)
	r.POST("/buyer/update_phone", controllers.UpdatePhoneByBuyer)
	r.POST("/buyer/update_pwd_by_email", controllers.UpdatePwdByEmailByBuyer)
	r.POST("/buyer/update_pwd_by_phone", controllers.UpdatePwdByPhoneByBuyer)

	r.GET("/buyer_addr/list", controllers.BuyerAddressList)
	r.POST("/buyer_addr/list", controllers.BuyerAddressList)
	r.POST("/buyer_addr/insert", controllers.AddAddressByBuyer)

	r.POST("/seller/register", controllers.RegisterSeller)
	r.POST("/seller/login", controllers.LoginSeller)
	r.POST("/seller/update_email", controllers.UpdateEmailBySeller)
	r.POST("/seller/update_phone", controllers.UpdatePhoneBySeller)
	r.POST("/seller/update_pwd_by_email", controllers.UpdatePwdByEmailBySeller)
	r.POST("/seller/update_pwd_by_phone", controllers.UpdatePwdByPhoneBySeller)

	r.GET("/seller_addr/list", controllers.BuyerAddressList)
	r.POST("/seller_addr/list", controllers.BuyerAddressList)
	r.POST("/seller_addr/insert", controllers.AddAddressBySeller)
	r.GET("/seller_addr/default", controllers.SetDefaultAddressByBuyer)

	r.GET("/agent/list", controllers.AgentList)
	r.POST("/agent/list", controllers.AgentList)

	r.POST("/agent/register", controllers.RegisterAgent)
	r.POST("/agent/login", controllers.LoginAgent)
	r.POST("/agent/update_email", controllers.UpdateEmailByAgent)
	r.POST("/agent/update_phone", controllers.UpdatePhoneByAgent)
	r.POST("/agent/update_pwd_by_email", controllers.UpdatePwdByEmailByAgent)
	r.POST("/agent/update_pwd_by_phone", controllers.UpdatePwdByPhoneByAgent)

	r.GET("/agent_addr/list", controllers.AgentAddressList)
	r.POST("/agent_addr/list", controllers.AgentAddressList)
	r.POST("/agent_addr/insert", controllers.AddAddressByAgent)
	r.GET("/agent_addr/default", controllers.SetDefaultAddressByAgent)

	r.GET("/agent_rank/list", controllers.AgentRankList)
	r.POST("/agent_rank/list", controllers.AgentRankList)
	r.POST("/agent_rank/insert", controllers.AddAgentRank)

	r.GET("/agent_commission/list", controllers.AgentCommissionList)
	r.POST("/agent_commission/list", controllers.AgentCommissionList)
	r.POST("/agent_commission/insert", controllers.AddAgentCommission)

	r.POST("/manufacturer/register", controllers.RegisterManufacturer)
	r.POST("/manufacturer/login", controllers.LoginManufacturer)
	r.POST("/manufacturer/update_email", controllers.UpdateEmailByManufacturer)
	r.POST("/manufacturer/update_phone", controllers.UpdatePhoneByManufacturer)
	r.POST("/manufacturer/update_pwd_by_email", controllers.UpdatePwdByEmailByManufacturer)
	r.POST("/manufacturer/update_pwd_by_phone", controllers.UpdatePwdByPhoneByManufacturer)

	r.GET("/manufacturer_addr/list", controllers.ManufacturerAddressList)
	r.POST("/manufacturer_addr/list", controllers.ManufacturerAddressList)
	r.POST("/manufacturer_addr/insert", controllers.AddAddressByManufacturer)
	r.GET("/manufacturer_addr/default", controllers.SetDefaultAddressByManufacturer)
}
