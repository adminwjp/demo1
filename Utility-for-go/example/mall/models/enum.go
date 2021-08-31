package models

//账号状态
const  (
	Disabled = 0 //禁用
	Normal = 1 //正常/注册
	Blacklist //黑名单
)

//账号类型
const  (
	ShopAdmin = 1 //超级管理员
	ShopJustSoSoAdmin = 2 //普通管理员
)

//认证方式
const  (
	AutoApprove = 1 //自动认证
	PersonApprove = 2 //人工认证
)


//错误码
const  (
	Unauthorized = 403 //未授权
	SystemError = 503 //系统错误
	ReLogin = 10001 //请重新登录
	InvalidToken = 10002 //非法token
	ErrorSign = 10003 //sign 签名非法
	ErrorUserNameOrPass  //用户名或密码有误
	NotFound  //不存在
	Forbidden  //禁止
	InvalidPassword  //无效密码
	AccountDisabled  //账户禁用
	InvalidData  //非法数据
	HasValued = 20001 //数据已存在
)

//上传文件类别枚举
const  (
	Head = 1 //头像
	IdCardFace  //身份证正面照片
	IdCardBack  //身份证反面照片
	Feedbacks  //意见反馈
	Shop  //店铺
	Message  //消息
	Description  //其他
)

//来源类型
const  (
	Unknown = 0 //未知
	Web  //网站
	WeChat  //微信
	Android  //Android
	IOS  //iOS
	WeChatApp  //小程序
)