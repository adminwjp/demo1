package router

import (
	"shop/controllers"

	"github.com/gin-gonic/gin"
)

func InitChatRouter(router *gin.Engine) {
	//检查手机是否被注册
	router.POST("user/check_phone_available", controllers.CheckPhoneAvailable)
	//发送验证码
	router.POST("user/send_code", controllers.SendCode)
	//验证验证码是否正确(必选先用手机号码调sendcode)
	router.POST("user/verify_code", controllers.VerifyCode)
	//注册
	router.POST("user/register", controllers.Register)
	//登录
	router.POST("user/login", controllers.Login)
	//获取 token 前置条件需要登录   502 坏的网关 测试环境用户已达上限
	router.GET("user/get_token", controllers.GetToken)
	//设置自己的昵称
	router.POST("user/set_nickname", controllers.SetName)
	//设置用户头像
	router.POST("user/set_portrait_uri", controllers.SetPortrait)
	//当前登录用户通过旧密码设置新密码  前置条件需要登录才能访问
	router.POST("user/change_password", controllers.ChangePassword)
	//通过手机验证码重置密码
	router.POST("user/reset_password", controllers.RestPassword)
	//根据 id 去服务端查询用户信息
	router.GET("user/:userid", controllers.GetUserInfoById)
	//通过国家码和手机号查询用户信息
	router.GET("user/find/:region/:phone", controllers.GetUserInfoFromPhone)
	//发送好友邀请
	router.POST("friendship/invite", controllers.SendFriendInvitation)
	//获取发生过用户关系的列表
	router.POST("friendship/all", controllers.GetAllUserRelationship)
	//根据userId去服务器查询好友信息
	router.POST("friendship/:userid/profile", controllers.GetFriendInfoByID)
	//同意对方好友邀请
	router.POST("friendship/agree", controllers.AgreeFriends)
	//删除好友
	router.POST("friendship/delete", controllers.DeleteFriend)
	//设置好友的备注名称
	router.POST("friendship/set_display_name", controllers.SetFriendDisplayName)
	//获取黑名单
	router.GET("friendship/blacklist", controllers.GetBlackList)
	//加入黑名单
	router.POST("friendship/add_to_blacklist", controllers.AddToBlackList)
	//移除黑名单
	router.POST("friendship/remove_from_blacklist", controllers.RemoveFromBlackList)
	//创建群组
	router.POST("group/create", controllers.CreateGroup)
	//创建者设置群组头像
	router.POST("group/set_portrait_uri", controllers.SetGroupPortrait)
	//获取当前用户所属群组列表
	router.GET("group/groups", controllers.GetGroups)
	//根据 群组id 查询该群组信息   403 群组成员才能看
	router.GET("group/:groupId", controllers.GetGroupInfo)
	//根据群id获取群组成员
	router.GET("group/:groupId/members", controllers.GetGroupInfo)
	//当前用户添加群组成员
	router.POST("group/add", controllers.AddGroupMember)
	//创建者将群组成员提出群组
	router.POST("group/kick", controllers.DeleGroupMember)
	//创建者更改群组昵称
	router.POST("group/rename", controllers.SetGroupName)
	//用户自行退出群组
	router.POST("group/quit", controllers.QuitGroup)
	//创建者解散群组
	router.POST("group/dismiss", controllers.DissmissGroup)
	//修改自己的当前的群昵称
	router.POST("group/set_display_name", controllers.SetGroupDisplayName)
	//当前用户加入某群组
	router.POST("group/join", controllers.JoinGroup)
	//获取默认群组 和 聊天室
	router.GET("group/demo_square", controllers.GetDefaultConversation)
	//根据一组ids 获取 一组用户信息
	router.GET("user/batch", controllers.GetUserInfos)
	//得到七牛的token
	router.GET("user/get_image_token", controllers.GetQiNiuToken)
	//获取版本信息

	router.GET("misc/client_version", controllers.GetClientVersion)
	router.GET("user/sync/:version", controllers.SyncTotalData)
	//下载图片
	router.GET("/", controllers.DownloadPic)

}
