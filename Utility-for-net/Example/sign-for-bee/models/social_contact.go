package models

//附件实体
//模型层 必须 按 规则 来 不然 不创建
// 外键 其实 创建 了 只不过没关联
//default is to use 'id' if not set
// 手字母小写 不会创建
type Attachment struct {
	AttachmentId           int64  `orm:"pk" `                                                //Id
	AssociateId            int64  `orm:"column(associate_id);size(100)" json:"associate_id"` //附件关联Id（例如：博文Id、贴子Id）
	OwnerId                int64  //拥有者Id
	TenantTypeId           string //租户类型Id
	UserId                 int64  //附件上传人UserId
	UserDisplayName        string //附件上传人名称
	FileName               string //实际存储文件名称
	FriendlyFileName       string //文件显示名称
	ContentType            string //附件MIME类型
	FileLength             int64  //文件大小
	Price                  int    //售价（积分）
	ConvertStatus          int    //10=转换失败，0=待转化，1=转化中，2=已转换
	Discription            string //附件描述
	IsShowInAttachmentList bool   //是否在文章中的附件列表显示
	DateCreated            int64  //创建日期
	MediaType              int    //附件类型 MediaType
}

//审核项实体类
type AuditItem struct {
	ItemKey      int64  `orm:"pk" ` //审核项目标识
	ItemName     string //项目名称
	DisplayOrder string //排序序号
	Description  string //描述
}

//审核项目与角色实体类
type AuditItemInUserRole struct {
	Id           int64  //Id
	RoleId       int64  //角色名称
	ItemKey      string //审核项目标识
	StrictDegree int    //严格程度
	IsLocked     bool   //是否锁定
}

//分类实体类
type Category struct {
	CategoryId        int64  `orm:"pk"` // 类别Id
	ParentId          int64  //父评论Id（顶级ParentId=0）
	OwnerId           string //拥有者Id
	TenantTypeId      string //租户类型Id
	CategoryName      string //类别名称
	Description       string //类别描述
	DisplayOrder      int64  //排序序号
	Depth             int    //类别深度 顶级类别 Depth=0
	ChildCount        int    //子类别数目
	ItemCount         int    //内容项数目
	ImageAttachmentId int64  //特征图片
	LastModified      int64  //最后更新日期
	DateCreated       int64  //创建日期
	// parent_id
	//Parent   *Category   `orm:"null;rel(fk)" json:"Parent"`         // 设置一对一反向关系(可选)
	//Children []*Category `orm:"null;reverse(many)" json:"Children"` //设置多对多反向关系
}

//分类与内容的关联项实体
type ItemInCategory struct {
	Id         int64 // 类别Id
	CategoryId int64 //类别Id
	ItemId     int64 //内容项Id
}

//栏目管理员（用于贴吧、资讯栏目等）
type CategoryManager struct {
	Id                  int64  // 类别Id
	CategoryId          int64  //所属栏目Id（或贴吧Id）
	TenantTypeId        string //租户类型Id
	ContentModelKeys    string //
	ReferenceCategoryId int64  //从哪个栏目继承权限
	UserId              int64  //用户Id
}

//栏目实体
type ContentCategory struct {
	Id                  int64  `orm:"pk" ` // 类别Id
	CategoryId          int64  //所属栏目Id（或贴吧Id）
	CategoryName        string //栏目名称
	Description         string //栏目描述
	UserId              int64  //用户Id
	ParentId            int64  //ParentId
	ParentIdList        string //所有父级CatetoryId
	ChildCount          int    //子栏目数目
	Depth               int    //深度(从0开始)
	IsEnabled           bool   //是否启用
	ContentCount        int    //内容计数
	DateCreated         int64  //创建日期
	ContentModelKeys    string //内容模型Key集合(多个用英文逗号隔开)
	ProcessDefinitionId int64  //流程定义Id
	DisplayOrder        int64  //排列顺序，默认和CategoryId一致
}

//内容模型
type ContentItem struct {
	ContentItemId             int64                  `orm:"pk" `
	ContentCategoryId         int64                  //栏目Id
	ContentModelId            int64                  //内容模型Id
	Subject                   string                 //标题允许录入及修改
	FeaturedImageAttachmentId int64                  //标题图Id
	DepartmentGuid            string                 //发布部门Id
	Points                    int                    //发布资讯获得的积分(备用)
	UserId                    int64                  //发布者UserId
	Author                    string                 //发布者DisplayName
	Body                      string                 //内容
	Summary                   string                 //摘要
	ApprovalStatus            int                    //审批状态(-1=已删除(看需求是否需要)，0 草稿状态，10未通过审核，20=待审核，30=需再审核，40=已通过审核)
	IsLocked                  bool                   //是否锁定
	IsSticky                  bool                   //是否置顶
	IP                        string                 //IP地址
	DatePublished             int64                  //发布时间
	DateCreated               int64                  //创建时间
	LastModified              int64                  //最后更新时间
	attachmentIdsFinal        []int64                //所有的附件Id集合
	isAllowMobileEdit         bool                   //是否允许手机端编辑
	isComment                 bool                   //是否允许评论
	isDraft                   bool                   //编辑前是否为草稿
	isVisible                 bool                   //是否允许内容末尾显示附件列表
	additionalProperties      map[string]interface{} //附表中的字段
}

//内容模型
type ContentModel struct {
	ModelId             int64  `orm:"pk"` //模型Id
	ModelName           string //模型名称
	ModelKey            string //英文标识
	IsBuiltIn           bool   //是不是内建模型（内建模型不允许删除）
	DisplayOrder        int64  //排序序号
	PageNew             string //发布页面UrlRouteName
	PageEdit            string //修改页面UrlRouteName
	PageManage          string //列表管理页面UrlRouteName
	PageList            string //列表页面UrlRouteName
	PageDetail          string //详细显示页面UrlRouteName
	IsEnabled           bool   //是否启用
	EnableComment       bool   //是否启用评论
	AdditionalTableName string //附加的数据库表名
}

func AdditionalTableForeignKey() string {
	return "ContentItemId"
}

//内容模型额外字段
type ContentModelAdditionalFields struct {
	FieIdId      int64  `orm:"pk"` //模型Id
	ModelId      int64  //模型Id
	FieldName    string //字段名称
	FieldLabel   string //字段标签
	DataType     string //字段对应的C#类型，可选值：int,long,float,decimal,string,datetime,bool
	DefaultValue string //默认值
}

//评论实体
type Comment struct {
	Id                int64  //Id
	ParentIds         string //所有父级评论Id集合
	ParentId          int64  //父评论Id（一级ParentId等于0）
	CommentedObjectId int64  //被评论对象Id
	TenantTypeId      string //租户类型Id（4位ApplicationId+2位顺序号）
	OwnerId           int64  //拥有者Id
	UserId            int64  //评论人UserId
	CommentType       string //评论类别
	ChildrenCount     int    //子回复个数
	Author            string //评论人名称
	Subject           string //标题
	Body              string //评论内容
	ApprovalStatus    int    //审核状态
	IsAnonymous       bool   //是否匿名评论
	IsPrivate         bool   //是否悄悄话
	IP                string //评论人IP
	DateCreated       int64  //评论人IP
}

//计数实体
type CountEntity struct {
	CountId         int64  `orm:"pk" ` //id
	OwnerId         int64  //拥有者id
	TenantTypeId    string //租户类型Id
	ObjectId        int64  //计数对象id
	CountType       string //计数类型
	StatisticsCount int64  //计数
}

//贴吧
type Section struct {
	SectionId                 int64  `orm:"pk" ` //id
	OwnerId                   int64  //拥有者id
	TenantTypeId              string //贴吧租户类型Id
	UserId                    int64  //吧主用户Id（若是活动/群组，则对应活动/群组创建者Id）
	Name                      string //贴吧名称
	Description               string //贴吧描述
	FeaturedImageAttachmentId int64  //标题图Id
	IsEnabled                 bool   //是否启用
	ThreadCategorySettings    int    //主题分类状态 0=禁用；1=启用（不强制）；2=启用（强制）
	DisplayOrder              int64  //排序序号
	DateCreated               int64  //创建时间
}

//贴子
type Thread struct {
	ThreadId       int64  `orm:"pk" ` //id
	SectionId      int64  //所属贴吧Id
	TenantTypeId   string //租户类型Id
	OwnerId        int64  //所属贴吧拥有者Id（例如：群组Id）
	UserId         int64  //主题作者用户Id
	Author         string //主题作者
	Subject        string //贴子标题
	Body           int64  //贴子内容
	IsLocked       bool   //是否锁定
	IsSticky       bool   //是否置顶
	ApprovalStatus int    //审批状态
	IP             string //发贴人IP
	DateCreated    int64  //创建时间
	LastModified   int64  //最后更新日期（被回复时也需要更新时间）
	ThreadType     int    //贴子类型
	AssociateId    int64  //关联 Id(活动,投票)
}

//租户数据实体类
type TenantType struct {
	TenantTypeId int64  `orm:"pk"` //租户类型Id
	Name         int64  //租户类型名称
	ClassType    string //类型
}

//第三方帐号类型
type AccountType struct {
	AccountTypeKey              string `orm:"pk;" ` //第三方帐号类型标识
	ThirdAccountGetterClassType int64  //第三方帐号获取器实现类Type值（如：Spacebuilder.Group.GroupConfig,Spacebuilder.Group）
	AppKey                      string //网站接入应用标识
	AppSecret                   string //网站接入应用加密串
	IsEnabled                   bool   //是否启用
}

//帐号绑定实体类
type AccountBinding struct {
	Id             int64  `orm:"pk;column(id)" json:"id"` //主键标识
	UserId         int64  //用户Id
	AccountTypeKey string //第三方帐号类型
	AccessToken    string //oauth授权凭证加密串
	ExpiredDate    int64  //过期时间
}

//在线用户实体类
type OnlineUser struct {
	Id               int64  `orm:"pk;c` //主键标识
	UserId           int64  //用户Id
	UserName         string //用户名
	DisplayName      string //对外显示的名称
	LastActivityTime int64  //上次活动时间
	LastAction       string //上次操作
	Ip               string //IP
	DateCreated      int64  //创建时间
}

//积分类型实体类
type PointCategory struct {
	CategoryKey  string `orm:"pk;" ` //积分类型标识
	CategoryName string //类型名称
	Unit         string //单位名称
	QuotaPerDay  int    //每人每日该类限额（0表示无限制）
	Description  string //描述
	DisplayOrder int64  //排序序号
}
type BasePoint struct {
	ExperiencePoints int //经验积分值
	ReputationPoints int //威望积分值
	TradePoints      int //交易积分值
	TradePoints1     int //交易积分值1
	TradePoints2     int //交易积分值2
	TradePoints3     int //交易积分值3
	TradePoints4     int //交易积分值4
}

//积分项目实体类
type PointItem struct {
	ItemKey      string `orm:"pk;" ` //积分项目标识
	TenantTypeId string //租户Id
	ItemName     string //项目名称
	DisplayOrder int64  //排序序号
	BasePoint
	Description string //描述
}

//积分纪录实体类
type PointRecord struct {
	RecordId       int64  `orm:"pk;" ` //RecordId
	UserId         int64  //用户Id
	OperatorUserId int64  //操作者用户Id
	PointItemName  string //积分项目名称
	Description    string //描述
	BasePoint
	DateCreated int64 //创建时间
}

//用户角色
type Role struct {
	RoleId                int64  `orm:"pk;" ` //RoleId
	RoleName              string //角色友好名称（用于对外显示）
	IsBuiltIn             bool   //是否是系统内置的
	ConnectToUser         bool   //是否直接关联到用户（例如：版主、注册用户 无需直接赋给用户）
	IsPublic              bool   //是否对外显示
	Description           string //描述
	RoleImageAttachmentId int64  //角色标识图片Id
}

//用户和角色的关联关系
type UserInRole struct {
	Id     int64 `orm:"pk;" ` //Id
	UserId int64 //用户ID
	RoleId int64 //角色ID
}

//用户等级实体
type UserRank struct {
	Rank       int64  `orm:"pk;" ` //Rank
	PointLower int64  //积分下限
	RankName   string //等级名称
}

//用户帐号
type User struct {
	UserId            int64  `orm:"pk;" ` //UserId
	UserGuid          string //用户GUID/OpenId
	UserName          string //用户名（昵称）
	Password          string //密码
	PasswordFormat    int    //0=Clear（明文）1=标准MD5
	AccountEmail      string //帐号邮箱
	IsEmailVerified   bool   //帐号邮箱是否通过验证
	AccountMobile     string //手机号码
	IsMobileVerified  bool   //帐号手机是否通过验证
	TrueName          string //个人姓名 或 企业名称
	ForceLogin        bool   //是否强制用户登录
	Status            int    //用户账号状态(-1=已删除,1=已激活,0=未激活)
	DateCreated       int64  //创建时间
	IpCreated         string //创建用户时的ip
	UserType          int    //用户类别
	LastActivityTime  int64  //上次活动时间
	LastAction        string //上次操作
	IpLastActivity    string //上次活动时的ip
	IsBanned          string //是否封禁
	BanReason         string //封禁原因
	BanDeadline       int64  //封禁截止日期
	IsModerated       bool   //强制用户管制（不会自动解除）
	IsForceModerated  bool   //强制用户管制（不会自动解除）
	HasAvatar         int    //头像 是否存在
	HasCover          int    //封面图 是否存在
	DatabaseQuota     int    //磁盘配额
	DatabaseQuotaUsed int    //已用磁盘空间
	FollowedCount     int    //关注用户数
	FollowerCount     int    //粉丝数
	BasePoint
	Rank              int //用户等级
	FrozenTradePoints int //冻结的交易积分
}

//广告实体
type Advertising struct {
	AdvertisingId     int64  `orm:"pk;"` //AdvertisingId
	Name              string //广告名称
	AdvertisingType   int    //呈现方式
	Body              string //广告内容
	ImageAttachmentId int64  //图片附件Id
	LinkUrl           string //广告链接地址
	IsEnable          bool   //是否启用
	DisplayOrder      int64  //排序
	TargetBlank       int64  //是否新开窗口
	StartDate         int64  //开始时间
	EndDate           int64  //结束时间
	DateCreated       int64  //创建日期
}

//地区实体类
type Area struct {
	AreaCode     string `orm:"pk;" ` //AreaCode
	ParentCode   string //父级地区编码
	Name         string //地区名称
	PostCode     string //邮政编码
	DisplayOrder int64  //排序序号
	Depth        int    //深度
	ChildCount   int    //子地区个数
}

//顶踩实体
type Attitude struct {
	Id           int64  `orm:"pk;" ` //Id
	ObjectId     int64  //操作对象Id
	SupportCount int64  //点赞数
	TenantTypeId string //租户类型Id
}

//顶踩记录实体
type AttitudeRecord struct {
	Id           int64  `orm:"pk;" ` //Id
	ObjectId     int64  //操作对象Id
	UserId       int64  //用户Id
	TenantTypeId string //租户类型Id
}

//用户关联实体类
type AtUserEntity struct {
	Id           int64  `orm:"pk;" ` //Id
	ObjectId     int64  //操作对象Id
	UserId       int64  //用户Id
	TenantTypeId string //租户类型Id
	AssociateId  int64  //关联项Id
}

//用户收藏实体类
type FavoriteEntity struct {
	Id           int64  `orm:"pk;" ` //Id
	ObjectId     int64  //操作对象Id
	UserId       int64  //用户Id
	TenantTypeId string //租户类型Id
}

//关注用户实体类
type FollowEntity struct {
	Id             int64  `orm:"pk;" ` //Id
	FollowedUserId int64  //被关注用户Id
	UserId         int64  //用户Id
	NoteName       string //备注名称
	IsMutual       bool   //是否为互相关注
	DateCreated    int64  //创建日期
}

//用户举报实体
type ImpeachReport struct {
	Id             int64  `orm:"pk;" ` //Id
	UserId         int64  //用户Id
	TenantTypeId   string //租户类型Id
	Reporter       string //用户名称
	Reason         int    //举报原因
	Title          string //举报对象标题
	ReportObjectId string //被举报相关对象Id
	Status         bool   //Status
	DateCreated    int64  //创建日期
}

//邀请码
type InvitationCode struct {
	Code        string `orm:"pk;"` //Id
	UserId      int64  //用户Id
	IsMultiple  bool   //是否可以多次使用
	ExpiredDate int64  //过期日期
	DateCreated int64  //创建日期
}

//邀请好友的记录实体
type InviteFriendRecord struct {
	Id            int64  `orm:"pk;" ` //Id
	UserId        int64  //用户Id
	InvitedUserId bool   //受邀人
	Code          string //邀请码
	IsRewarded    bool   //邀请用户是否得到了奖励
	DateCreated   int64  //创建日期
}

//友情链接实体
type LinkEntity struct {
	LinkId            int64  `orm:"pk;"` //LinkId
	LinkName          string //链接名称
	LinkUrl           bool   //链接地址
	ImageAttachmentId int64  //图片附件 Id
	Description       string //链接说明
	IsEnabled         bool   //是否启用
	DisplayOrder      int64  //排序，默认与主键相同
	DateCreated       int64  //创建日期
}

//列表管理实体
type ListEntity struct {
	Code             string `orm:"pk;"` //Code
	Name             string //名称
	Description      string //描述
	IsMultilevel     int    //是否多层级
	AllowAddOrDelete int    //是否允许添加或删除
}

//列表项管理实体
type ListItem struct {
	Id            int64  `orm:"pk;"` //Id
	ItemCode      string //项编码（同一ListCode内唯一）
	ListCode      string //列表编码
	ParentCode    string //父级编码（根级为空字符串）
	Name          string //名称
	ChildrenCount int    //子级数目
	Depth         int    //深度（从0开始）
	DisplayOrder  int    //排列顺序
}

//勋章实体
type Medal struct {
	MedalId           int64  `orm:"pk;"` //MedalId
	MedalName         string //勋章名
	AwardStatus       int    //授予状态（可以授予、停止授予）
	AwardType         int    //授予方式（自主申请、人工授予）
	ImageAttachmentId int64  //勋章标题图ID
	Description       string //描述
	DisplayOrder      int64  //排列顺序
	DateCreated       int64  //创建日期
	GroupId           int64  `orm:"-" json:"-"` //互斥组Id
	GroupIdBefore     int64  `orm:"-" json:"-"` //之前互斥组Id
	Conditions        string `orm:"-" json:"-"` //申请条件Id
	ConditionValues   string `orm:"-" json:"-"` //申请条件最小值
}

//勋章条件实体
type MedalCondition struct {
	ConditionId   int64  `orm:"pk;"` //ConditionId
	ConditionName string //条件名
	DisplayOrder  int64  //排列顺序
	MethodName    string //验证方法名（接收最小条件值和验证用户id）
	MinCondition  int    `orm:"-" json:"-"` //最小条件值
}

//勋章条件关联实体
type MedalInCondition struct {
	Id           int64 `orm:"pk;"`
	MedalId      int64 //勋章Id
	ConditionId  int64 //条件Id
	MinCondition int64 //最小条件值
}

//勋章实体
type MedalInGroup struct {
	Id      int64 `orm:"pk;"`
	MedalId int64 //勋章Id
	GroupId int64 //互斥组Id
}

//勋章实体
type MedalToUser struct {
	MedalInGroup
	UserDisplayName       string //用户名
	ManagerId             int64  //管理者Id
	UserAwardStatus       int    //用户授予状态（已授予、已收回、已拒绝、申请中、已放弃）
	DateCreated           int64  //创建日期
	UserAwardStatusBefore int    `orm:"-" json:"-"` //用户授予状态（已授予、已收回、已拒绝、申请中、已放弃）
}

//私信实体
type Message struct {
	MessageId      int64  `orm:"pk;"`
	SenderUserId   int64  //发件人UserId
	Sender         string //发件人的DisplayName
	ReceiverUserId int64  //收件人UserId
	Receiver       string //收件人DisplayName
	Subject        string //私信标题
	Body           string //私信内容
	IsRead         bool   //是否已读
	IP             string //私信来源IP
	DateCreated    int64  //创建日期
	MessageType    int    `orm:"-" json:"-"` //私信类型
	AsAnonymous    bool   `orm:"-" json:"-"` //会话是否匿名
}

//私信的会话
type MessageSession struct {
	SessionId          int64  `orm:"pk;"`
	UserId             int64  //会话拥有者UserId
	OtherUserId        int64  //会话参与人UserId
	LastMessageId      int64  //会话中最新的私信MessageId
	MessageCount       int64  //信息数统计
	UnreadMessageCount string //未读信息数统计（用来显示未读私信统计数和和标示会话的阅读状态）
	MessageType        int    //消息类型
	LastModified       int64  //最后回复日期
	IP                 string //私信来源IP
	AsAnonymous        string //作为匿名用户
	SenderSessionId    int64  //附表ID
}

//通知的实体类
type Notice struct {
	Id                 int64  `orm:"pk;"`
	NoticeTypeKey      int64  //通知类型 Key
	ReceiverId         int64  //通知接收人
	LeadingActorUserId int64  //主角 UserId
	LeadingActor       int64  //主角
	RelativeObjectName string //相关项对象名称
	RelativeObjectId   int64  //相关项对象 Id
	RelativeObjectUrl  string //相关项对象链接地址
	ObjectId           int64  //触发通知的对象Id
	Body               string //内容
	Status             int    //处理状态   0=Unhandled:未处理;1=Readed  知道了;  2=Accepted 接受；3=Refused 拒绝；
	DateCreated        int64  //创建日期
	LastSendDate       int64  //上次发送时间
	Times              int    //通知发送次数
}

//通知 类型Key
type NoticeType struct {
	NoticeTypeKey string `orm:"pk;"` //通知类型 Key
	Name          string //类型名称
	Description   string //类型描述
}

//通知设置类
type NoticeTypeSettings struct {
	Id            int64  `orm:"pk;"` //Id
	NoticeTypeKey string //通知类型 Key
	Time          int    //第几次通知
	Interval      int    //距离上次通知的时间间隔(秒)
	SendMode      int    //发送方式（0=站内，1=Email，2=手机短信）
}

//操作日志实体
type OperationLog struct {
	Id                  int64  `orm:"pk;"` //Id
	TenantTypeId        string //租户Id
	OperationType       string //操作类型标识
	OperationObjectName string //操作对象名称
	OperationObjectId   int    //OperationObjectId
	Description         string //操作描述
	OperationUserId     int64  //操作者UserId
	OperationUserRole   int64  //操作者角色
	Operator            int64  //操作者名称
	OperatorIP          string //操作者IP
	AccessUrl           string //操作访问的url
	DateCreated         int64  //创建日期
}

//短网址实体
type ParsedMedia struct {
	Alias         string `orm:"pk;"` //Url别名
	Url           string //网址
	MediaType     int    //多媒体类型
	Name          string //多媒体名称
	Description   string //操作描述
	ThumbnailUrl  string //缩略图地址
	PlayerUrl     string //播放器地址
	SourceFileUrl string //源文件地址
	DateCreated   int64  //创建日期
}

//权限项目与角色关联
type Permission struct {
	Id                int64  `orm:"pk;"` //Id
	PermissionItemKey string //权限项目标识
	OwnerId           int64  //被授权对象Id
	OwnerType         int64  //被授权对象类型（用户=1、角色=11）
	IsLocked          bool   //是否锁定
}

//权限实体类
type PermissionItem struct {
	ItemKey      string `orm:"pk;"` //权限项目标志
	ItemName     string //权限项目名称
	DisplayOrder int64  //排序序号
	Discription  string //权限项目描述
}

//权限实体类
type PointRechargeOrder struct {
	Id             int64   `orm:"pk;"` //订单号
	UserId         int64   //用户Id
	TradePoints    int     //积分
	TotalPrice     float64 //金额
	Buyway         int     //支付方式
	PayMediaType   int     //支付媒介类型
	Discription    string  //描述
	TradingAccount string  //交易账号
	Status         int     //订单状态
	TradeNo        string  //流水账号
	DateCreated    int64   //创建日期
}

//积分任务实体
type PointTask struct {
	TaskId        int64  `orm:"pk;"` //积分任务Id
	TypeId        int64  //积分任务种类Id
	TaskName      string //积分任务名
	Description   string //描述
	AwardPoints   int    //奖励积分值
	AwardGolds    int    //奖励金币数
	MinUserRank   int    //申请所需最小用户等级
	MinCondition  int    //最小条件（人工审核任务时为0）
	TasksSettings string //任务内容（人工审核任务、分享任务url）（多个任务内容json存储）
	Deadline      int64  //截止时间
	Status        int    //任务状态（正常、已禁用、已超期）
	DateCreated   int64  //创建日期
}

//积分任务实体
type PointTaskRecord struct {
	RecordId        int64  `orm:"pk;"` //领取记录Id
	TaskId          int64  //积分任务Id
	UserId          int64  //用户Id
	UserDisplayName string //用户名
	Status          int    //任务进行状态（进行中、申请完成中、已完成、未通过、已放弃）
	ResultContent   string //用户针对内容项提交的内容（多个任务内容json存储）
	Feedback        string //反馈意见
	DateCreated     int64  //创建日期
}

//积分任务实体
type PointTaskType struct {
	TypeId            int64  `orm:"pk;"` //积分任务Id
	TypeName          int64  //积分任务名
	IsShowProgressBar bool   //是否显示进度条
	IsSetDeadline     bool   //是否可设截止日期
	CanAddTask        bool   //能否添加任务
	Description       bool   //完成规则说明
	TaskUrl           string //点击去做任务跳转的URL
	TaskUrlType       int    //手机端跳转类别
	CheckMethodName   string //检验任务是否完成的方法名（如果不填则表示需要人工审核）service中添加
	RouteName         string //请求编辑时的action名。添加新的任务类型时需1、添加任务类型表记录2、添加一个编辑分布页3、添加control中对应的action方法
	ImageUrl          string //图标URL
}

//用户资料
type PointTaskUserInfo struct {
	UserId        int64  `orm:"pk;"` //UserId
	Gender        int    //性别1=男,2=女,0=未设置
	BirthdayType  int    //生日类型1=公历,2=阴历
	Birthday      int64  //公历生日
	LunarBirthday int64  //阴历生日
	NowAreaCode   string //所在地
	QQ            string //QQ
	CardType      int    //证件类型
	CardID        string //证件号码
	Introduction  string //自我介绍
	Integrity     int    //资料完整度（0至100）
}

//评价实体
type Review struct {
	Id           int64  `orm:"pk;"` //Id
	TenantTypeId string //租户Id
	ParentId     int64  //父级
	OwnerId      int64  //拥有者Id
	UserId       int64  //评价人UserId
	NowAreaCode  string //所在地
	Author       string //评论人名称
	Body         int    //评价 内容
	RateNumber   int    //星级评价评分
	ReviewRank   int    //好中差评
	IsAnonymous  int    //是否匿名
	IP           string //评论人Ip
	DateCreated  int64  //创建日期
}

//评价汇总实体
type ReviewSummary struct {
	Id                  int64  `orm:"pk;"` //Id
	TenantTypeId        string //租户Id
	ReviewedObjectId    int64  //被评论对象
	OwnerId             int64  //拥有者Id
	RateNumber          int    //星级评价评分
	RateCount           int    //星级评价人数
	PositiveReivewCount int    //好评数
	ModerateReivewCount int    //中评数
	NegativeReivewCount int    //差评数
}

//搜索热词实体
type SearchWord struct {
	Id                     int64  `orm:"pk;"` //Id
	Word                   string //搜索词
	SearchTypeCode         string //搜索词类型
	IsAddedByAdministrator bool   //是否由管理员添加
	DateCreated            int64  //创建日期
	LastModified           int64  //最后使用时间
}

//短网址实体
type ShortUrlEntity struct {
	Alias         string `orm:"pk;"` //Url别名
	Url           string //实际的Url地址
	OtherShortUrl string //第三方服务处理后的短网址
	DateCreated   int64  //创建日期
}

//推荐的内容
type SpecialContentItem struct {
	Id                        int64  `orm:"pk;"` //推荐id
	TypeId                    int64  //推荐类别Id
	TenantTypeId              string //租户类型ID
	RegionId                  int64  //推荐内容所在区域Id（可能是版块、栏目也可能是自定义的数字）
	ItemId                    int64  //内容实体ID
	ItemName                  string //推荐标题（默认为内容名称或标题，允许推荐人修改）
	FeaturedImageAttachmentId int64  //标题图ID
	Recommender               string //推荐人 DisplayName
	RecommenderUserId         int64  //推荐人用户 Id
	DateCreated               int64  //推荐日期
	ExpiredDate               int64  //截止期限
	DisplayOrder              int64  //排序顺序（默认和Id一致）
}

//推荐的类别
type SpecialContentType struct {
	TypeId                  int64  `orm:"pk;"` //类型ID（创建后不允许修改）
	Name                    int64  //推荐类型名称
	TenantTypeId            string //租户ID
	Description             string //推荐类型描述
	RequireExpiredDate      bool   //是否需要截止日期
	RequireFeaturedImage    bool   //是否包含标题图
	AllowExternalLink       bool   //是否允许添加外链
	IsBuiltIn               bool   //是否系统内置
	FeaturedImageDescrption string //标题图说明
}

//标签与内容的关联项实体
type ItemInTag struct {
	Id           int64  `orm:"pk;"` //类型ID（创建后不允许修改）
	TagName      string //标签名称
	ItemId       int64  //内容项Id
	TenantTypeId string //租户类型Id
}

//相关标签实体
type RelatedTag struct {
	Id           int64 `orm:"pk;"` //类型ID（创建后不允许修改）
	TagId        int64 //标签Id
	RelatedTagId int64 //相关标签Id
}

//标签实体类
type Tag struct {
	TagId             int64  `orm:"pk;"` //标签Id
	TenantTypeId      string //租户类型Id
	TagName           string //标签名称
	Description       string //描述
	ImageAttachmentId int64  //标签标题图Id
	IsFeatured        bool   //是否为特色标签
	ItemCount         bool   //内容项数目
	DateCreated       int64  //创建日期
}
