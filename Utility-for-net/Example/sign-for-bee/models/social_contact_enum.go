package models


//附件媒体类型 MediaType
const (
	Image_MediaType      = 1 //图片
	Video_MediaType         //视频
	Flash_MediaType		 //Flash
	Audio_MediaType        //音乐
	Document_MediaType        //文档
	Compressed_MediaType        //压缩包
	Other_MediaType =99       //其他类型
)


//图片尺寸大小 IconSize
const (
	Download_IconSize      = 1 //下载附件
	Read_IconSize         //浏览附件
)

//附件记录访问类型 AccessType
const (
	Small_AccessType      = 10 //小尺寸
	Medium_AccessType     =20    //中等尺寸
	Big_AccessType        =30    //大尺寸
)

//文档转换状态 ConvertStatus
const (
	Waiting_ConvertStatus      = 0 //等待转换
	Converting_ConvertStatus    //转换中
	Complete_ConvertStatus           //已转换
	Fail_ConvertStatus =10          //转换失败
	Other_ConvertStatus  =254         //不可转换
	All_ConvertStatus           //大尺寸
)

//附件展示位置 AttachmentPosition
const (
	NotSet_AttachmentPosition      = 0 //未设置
	Featured_AttachmentPosition     //转换中
	Complete_AttachmentPosition  =10   //特别的附件 (例如:标题图)
	Cover_AttachmentPosition          //封面图 (例如:视频封面图)
	Body_AttachmentPosition  =20         //主内容区域 (例如:Html编辑器正文中的图片)
	AttachmentList_AttachmentPosition=30  //附件列表
)

//审核严格程度 AuditStrictDegree
const (
	NotSet_AuditStrictDegree      = iota //未设置
	None_AuditStrictDegree     //不审核
	Create_AuditStrictDegree     //创建时审核
	Update_AuditStrictDegree          //需再审核 更新时也审核
)

//审核状态 AuditStatus
const (
	Fail_AuditStatus      = 10 //未通过
	Pending_AuditStatus	=20     //待审核
	Again_AuditStatus  =30    //需再审核
	Success_AuditStatus   =40        //需再审核 通过审核
)

//用于显示的审核状态 PubliclyAuditStatus
const (
	Fail_PubliclyAuditStatus      = 10 //未通过
	Pending_GreaterThanOrEqual_PubliclyAuditStatus	=19     //待审核、需再次审核、通过审核
	Pending_PubliclyAuditStatus  =20    //待审核
	Again_GreaterThanOrEqual_PubliclyAuditStatus   =29        //需再次审核、通过审核
	Again_PubliclyAuditStatus   =30        //需再次审核
	Success_PubliclyAuditStatus   =30        //通过审核
)

//评论排序字段 SortBy_Comment
const (
	DateCreated_SortBy_Comment      = iota //未通过发布日期
	DateCreatedDesc_SortBy_Comment	   //发布日期倒序
)

//贴吧排序依据 SortBy_BarSection
const (
	DateCreated_Desc_SortBy_BarSection      = iota //创建时间倒序
	ThreadCount_SortBy_BarSection	   //主题贴数
	ThreadAndPostCount_SortBy_BarSection	   //主题贴和回贴总数
	StageThreadAndPostCount_SortBy_BarSection	   //阶段主题贴和回贴总数
	FollowedCount_SortBy_BarSection	   //被关注数
)

//贴子排序依据 SortBy_BarThread
const (
	DateCreated_Desc_SortBy_BarThread      = iota //发布时间倒序
	LastModified_Desc_SortBy_BarThrea	   //更新时间倒序
	HitTimes_SortBy_BarThrea	   //浏览数
	StageHitTimes_SortBy_BarThrea	   //阶段浏览数
)

//贴子时间排序依据 SortBy_BarDateThread
const (
	All_SortBy_BarDateThread      = iota //全部时间
	ThreeDay_SortBy_BarDateThread	   //近三天
	SevenDay_SortBy_BarDateThread	   //近一个周
	AMonth_SortBy_BarDateThread	   //近一个月
)

//回贴排序依据 SortBy_BarPost
const (
	DateCreated_SortBy_BarPost      = iota //创建时间
	DateCreated_Desc_SortBy_BarPost	   //创建时间倒序
)

//主题分类状态 ThreadCategoryStatus
const (
	Disabled_ThreadCategoryStatus      = iota //0=禁用；1=启用（不强制）；2=启用（强制）
	NotForceEnabled_ThreadCategoryStatus	   //启用（不强制）
	ForceEnabled_ThreadCategoryStatus	   //启用（强制）
)

//贴子类型 ThreadType
const (
	Ordinary_ThreadType      = iota //普通贴
	Event_ThreadType	   //活动贴
	Vote_ThreadType	   //投票贴
)

//贴吧发言设置 SectionPostSetting
const (
	Default_SectionPostSetting      = iota //默认
	OwnerAndManagers_SectionPostSetting	   //仅吧主和管理员
)

//注册方式 RegistrationMode
const (
	All_RegistrationMode      = 1 //允许注册 允许所有途径的注册
	Invitation_RegistrationMode	   //仅邀请注册  仅允许通过邀请注册
	Disabled_RegistrationMode =4	   //禁止注册
)

//帐号激活方式 AccountActivation
const (
	Automatic_AccountActivation      = iota //自动激活 用户注册时自动激活
	Email_AccountActivation	   //Email激活  通过验证Email激活
	SMS_AccountActivation 	   //短信激活 通过手机短信激活
	Administrator_AccountActivation =9	   //管理员激活
)

//用户密码存储格式 UserPasswordFormat
const (
	Clear_UserPasswordFormat      = iota //密码未加密
	MD5_UserPasswordFormat	   //MD5加密
	RSA_UserPasswordFormat	   //RSA加密
	TEA_UserPasswordFormat	   //TEA加密
	SHA_UserPasswordFormat	   //SHA加密
)

//用什么名称作为用户的DisplayName对外显示 DisplayNameType
const (
	UserNameFirst_DisplayNameType      = 1 //采用昵称作为DisplayName
	TrueNameFirst_DisplayNameType	   //首先采用真实姓名作为DisplayName，如果真实姓名不存在则用昵称作为DisplayName
)

//用于创建用户帐号时的返回值 UserCreateStatus
const (
	UnknownFailure_UserCreateStatus      = iota //未知错误
	Created_UserCreateStatus	   //创建成功
	DuplicateUsername_UserCreateStatus	   //用户名重复
	DuplicateEmailAddress_UserCreateStatus	   //Email重复
	DuplicateMobile_UserCreateStatus	   //手机号重复
	DisallowedUsername_UserCreateStatus	   //不允许的用户名
	Updated_UserCreateStatus	   //更新成功
	InvalidQuestionAnswer_UserCreateStatus	   //不合法的密码提示问题/答案
	InvalidPassword_UserCreateStatus	   //不合法的密码
)

//删除用户时的返回状态 UserDeleteStatus
const (
	Deleted_UserDeleteStatus      = 1 //删除成功
	InvalidTakeOverUsername_UserDeleteStatus	   //接管被删除用户内容的用户名不存在
	DeletingUserNotFound_UserDeleteStatus	   //待删除的用户不存在
	UnknownFailure_UserDeleteStatus	   //未知错误
)

//用户登录状态 UserLoginStatus
const (
	Success_UserLoginStatus      = 1 //通过身份验证，登录成功
	InvalidCredentials_UserLoginStatus	   //用户名、密码不匹配
	NotActivated_UserLoginStatus	   //帐户未激活
	Banned_UserLoginStatus	   //帐户被封禁
	NoMobile_UserLoginStatus	   //不允许手机登录
	NoEmail_UserLoginStatus	   //不允许邮箱登录
	UnknownError_UserLoginStatus 	=100	   //未知错误
	InvalidAccount_UserLoginStatus	=500   //无效账号
)

//用户激活状态 UserStatus
const (
	Delete_UserStatus      = -1 //已删除 用户已删除
	IsActivated_UserStatus	=1	   //已激活
	NoActivated_UserStatus	=0	   //未激活
)

//性别类型 GenderType
const (
	NotSet_GenderType      = iota //未设置
	Male_GenderType		   //男
	FeMale_GenderType		   //女
)

//学历类型 DegreeType
const (
	PrimarySchool      = 7 //未设置
	MiddleSchool		  = 6   //初中
	VocationalSchool	  = 5	   //中专/技校
	HighSchool	  = 4	   //高中
	CommunityCollege  = 3		   //大专
	Undergraduate  = 2		   //本科
	Master	  = 1	   //硕士
	Doctor	  = 0	   //博士
)

//证件类型 CertificateType
type  CertificateType int
const (
	Residentcard  CertificateType    =iota //居民身份证
	SergeantsCard	CertificateType=1	   //军官证
	StudentCard	  CertificateType=2	   //学生证
	DriverCard	 CertificateType=3 	   //驾驶证
	passport  	 CertificateType=4  //护照
	HongKongPermit  	CertificateType=5	   //港澳通行证
)

//头像尺寸类型 AvatarSizeType
type  AvatarSizeType int
const (
	Original  AvatarSizeType    =iota //原始尺寸
	Big		  AvatarSizeType=1	   //大头像
	Medium	  AvatarSizeType=2	  	   //中头像
	Small  AvatarSizeType=3	  	   //小头像
	Micro  	AvatarSizeType=4	  	   //微头像
)

//用户资料完整度有关项目 ProfileIntegrityItems
type  ProfileIntegrityItems int
const (
	Avatar    ProfileIntegrityItems  =iota //头像
	Birthday	ProfileIntegrityItems=1	  	   //大头像
	NowArea	  ProfileIntegrityItems=2	 	   //所在地
	HomeArea  ProfileIntegrityItems=3	 	   //家乡
	IM  	ProfileIntegrityItems=4	 	   //即时通讯帐号
	Mobile  	ProfileIntegrityItems=5	 	   //手机号码
	EducationExperience  ProfileIntegrityItems=6	 		   //教育经历
	WorkExperience  ProfileIntegrityItems=7	 		   //工作经历
	Introduction  	ProfileIntegrityItems=8 	   //自我介绍
)

//用户激活、管制、封禁数 UserManageableCountType
type  UserManageableCountType int
const (
	IsActivated   UserManageableCountType   =1 //激活
	IsBanned	UserManageableCountType=2	  	   //封禁
	IsModerated	  UserManageableCountType=3	   //管制
	IsAll  	 UserManageableCountType=4  //总用户数
	IsLast24  	UserManageableCountType=5	   //24小时新增数
)

//用户排序字段 SortBy_User
type  SortBy_User int
const (
	FollowerCount SortBy_User =iota    //粉丝数
	ReputationPoints SortBy_User=1		  	   //威望
	PreWeekReputationPoints	  SortBy_User= 2	   //每周威望
	HitTimes   SortBy_User=3	   //浏览量
	TradePoints   SortBy_User=4		   //积分
	PreWeekHitTimes   SortBy_User=5		   //每周浏览量
	Rank  	SortBy_User=6	   //等级
	DateCreated   SortBy_User=7		   //创建日期
)

//附件媒体类型 AdvertisingType
type  AdvertisingType int
const (
	Script  AdvertisingType =iota    //代码
	Text	 AdvertisingType=1 	  	   //文字
	Image	  AdvertisingType =2  	   //图片
	Flash  	 AdvertisingType =3   //Flash
)

//广告状态 AdvertisingStatus
type  AdvertisingStatus int
const (
	Disabled  AdvertisingStatus =iota    //未启用
	NotServing AdvertisingStatus =1		  	   //未投放
	Serving	  AdvertisingStatus =2	   //已投放
	OutOfDate AdvertisingStatus  =3	   //已过期
	Enabled   AdvertisingStatus	  =4  //已启用
)

//顶踩排序字段 SortBy_Attitude
type  SortBy_Attitude int
const (
	Comprehensive_Desc SortBy_Attitude  =iota    //根据综合评价
	SupportCount_Desc	 SortBy_Attitude =1	  	   //根据顶的统计数
)

//顶踩的模式 AttitudeMode
type  AttitudeMode int
const (
	Unidirection  AttitudeMode =iota    //单向操作（用于仅存在顶操作）
	Bidirection	 AttitudeMode=1	  	   //双向操作（用于顶踩操作都存在）
)

//顶踩样式 AttitudeStyle
type  AttitudeStyle int
const (
	Like AttitudeStyle  =iota    //喜欢（心的形状）
	Support	 AttitudeStyle =1	  	   // 双向顶和踩（向上向下的手的形状）
	SupportOppose AttitudeStyle =2		  	   // 双向顶和踩（向上向下的手的形状）
	UpDown	 AttitudeStyle=3	  	   // 双向顶和踩（向上向下箭头的形状）
)


//签到排序 UserSignInOrder
type  UserSignInOrder int
const (
	SignCount_Desc UserSignInOrder  =iota    //喜欢（心的形状）
	MonthSignCount_Desc UserSignInOrder=1
)