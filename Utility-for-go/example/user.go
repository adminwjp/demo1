package example

//"github.com/jinzhu/gorm"

//代理商
//"github.com/beego/beego/v2/client/orm"
//orm:"pk" orm:"column(agent_name);size(100)"
//gorm:"primary_key;AUTO_INCREMENT"
//http://www.topgoer.com/%E6%95%B0%E6%8D%AE%E5%BA%93%E6%93%8D%E4%BD%9C/gorm/%E5%85%A5%E9%97%A8%E6%8C%87%E5%8D%97/%E6%A8%A1%E5%9E%8B%E5%AE%9A%E4%B9%89.html

type Agent struct {
	Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
	SellerId   int64  `json:"seller_id"`
	BuyerId    int64  `json:"buyer_id"`
	ParentId   int64  `json:"parent_id"`
	AgentName  string `json:"agent_name" gorm:"column:agent_name;size:100"`
	AgentPhone string `json:"agent_phone" gorm:"size:11"`
	AgentAddr  string `json:"agent_addr" gorm:"size:200"`
	ExpireDate int64  `json:"expire_date"`
	AuditDate  int64  `json:"audit_date"`
	Status     int32  `json:"status"`
	baseDate
}

func (Agent) TableName() string {
	return "t_agent"
}

//代理商 等级
type AgentRank struct {
	Id              int64   `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
	SellerId        int64   `json:"seller_id"`
	RankName        string  `json:"rank_name"`
	RankWeight      int64   `json:"rank_weight"`
	FirstRate       float64 `json:"first_rate"`
	SecondRate      float64 `json:"second_rate"`
	ThirdRate       float64 `json:"third_rate"`
	RewardValue     int64   `json:"reward_value"`
	GetCashTime     int64   `json:"get_cash_time"`
	GetCashLimit    int64   `json:"get_cash_limit"`
	ChildrenCount   int64   `json:"children_count"`
	TotalCommission float64 `json:"total_commission"`
	baseDate
}

func (AgentRank) TableName() string {
	return "t_agent_rank"
}

//代理商 佣金
type AgentCommission struct {
	Id              int64   `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
	AgentId         int64   `json:"agent_id"`
	OrderId         int64   `json:"order_id"`
	CommissionValue float64 `json:"commission_value"`
	baseDate
}

func (AgentCommission) TableName() string {
	return "t_agent_commission"
}

//代理商 审核 日志
type AgentAduitLog struct {
	Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
	AgentId    int64  `json:"agent_id"`
	AduitOpter int64  `json:"aduit_opter"`
	Content    string `json:"content"`
	Status     string `json:"status"`
	baseDate
}

func (AgentAduitLog) TableName() string {
	return "t_agent_aduit_log"
}

//"github.com/jinzhu/gorm"
type baseUser struct {
	Id             int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
	Nickname       string `json:"nickname"`
	Realname       string `json:"realname"`
	Phone          string `json:"phone"`
	Password       string `json:"password"`
	HeadimgurlId   string `json:"headimgurl_id"`
	Headimgurl     string `json:"headimgurl"`
	OpenId         string `json:"open_id"`
	Email          string `json:"email"`
	Gender         int    `json:"gender"`
	Birthday       int64  `json:"birthday"`
	ImQq           int64  `json:"im_qq"`
	AuthAppId      int64  `json:"auth_app_id"`
	Language       string `json:"language"`
	City           string `json:"city"`
	Province       string `json:"province"`
	Country        string `json:"country"`
	Subscribe      int32  `json:"subscribe"`
	SubscribeTime  int64  `json:"subscribe_time"`
	Groupid        int64  `json:"groupid"`
	Remark         string `json:"remark"`
	AccessIp       int64  `json:"access_ip"`
	AccessToken    string `json:"access_token"`
	RefreshToken   string `json:"refresh_token"`
	TokenExpiresIn string `json:"token_expires_in"`
	LastLoginDate  string `json:"last_login_date"`
	Unionid        string `json:"unionid"`
	Score          int32  `json:"score"`
	//Buyer -1 0 未回复 1 回复
	IsReceiver int32 `json:"is_receiver"`
	baseDate
	UserLevel             int32 `json:"user_level"`
	LoginDate             int64 `json:"login_date"`
	StartDate             int64 `json:"start_date"`
	EndDate               int64 `json:"end_date"`
	VersionNo             int32 `json:"version_no"`
	LoginIp               int64 `json:"login_ip"`
	AdmiLoginFailureCount int32 `json:"admi_login_failure_count"`
}

type Admin struct {
	baseUser
	RoleId int64 `json:"role_id"`
}

func (Admin) TableName() string {
	return "t_admin"
}

//买家
type Buyer struct {
	baseUser
	//买家 即是 卖家
	SellerId int64 `json:"seller_id"`
}

func (Buyer) TableName() string {
	return "t_buyer"
}

//卖家
type Seller struct {
	baseUser
}

func (Seller) TableName() string {
	return "t_seller"
}

//社交账号登录
type SocilWay struct {
	Id     int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
	Name   string `json:"name"`
	Enable bool   `json:"enable"`
	UserId int64  `json:"user_id"`
	baseDate
}

func (SocilWay) TableName() string {
	return "t_socil"
}

type BaseFriend struct {
	Id      int64 `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
	IsBuyer bool  `json:"is_buyer"`

	Status    int32 `json:"status"`
	AddDate   int64 `json:"add_date"`
	AgreeDate int64 `json:"agree_date"`
}

//买家好友
type BuyerFriend struct {
	BaseFriend

	SellerId int64 `json:"seller_id"`
	BuyerId  int64 `json:"buyer_id"`
}

func (BuyerFriend) TableName() string {
	return "t_buyer_friend"
}

//卖家好友
type SellerFriend struct {
	BaseFriend
	BuyerId  int64 `json:"buyer_id"`
	SellerId int64 `json:"seller_id"`
}

func (SellerFriend) TableName() string {
	return "t_seller_friend"
}

//c2m 厂家
type Manufacturer struct {
	baseUser
}

func (Manufacturer) TableName() string {
	return "t_manufacturer"
}

type ManufacturerFriend struct {
	BaseFriend
	BuyerId        int64 `json:"buyer_id"`
	SellerId       int64 `json:"seller_id"`
	ManufacturerId int64 `json:"manufacturer_id"`
}

func (ManufacturerFriend) TableName() string {
	return "t_manufacturer_friend"
}

type AgentFriend struct {
	BaseFriend
	BuyerId  int64 `json:"buyer_id"`
	SellerId int64 `json:"seller_id"`
	AgentId  int64 `json:"agent_id"`
}

func (AgentFriend) TableName() string {
	return "t_agent_friend"
}
