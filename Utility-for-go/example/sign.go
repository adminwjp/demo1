package example

type baseDate struct {
	//Buyer -1 0 未激活 1 激活
	Active      int32 `json:"active"`
	CreatedDate int32 `json:"created_date"`
	UpdatedDate int32 `json:"updated_date"`
}

//bee orm
//用户签到统计 废弃
type UserSignIn struct {
	Id                 int64 `json:"id" orm:"pk;" ` //标签Id
	UserId             int64 `json:"user_id"`       //用户ID
	TradePointSum      int   //累计奖励金币
	ExperiencePointSum int   //累计奖励积分
	ContinuedSignCount int   //连续签到次数
	MonthSignCount     int   //月累计签到次数
	SignCount          int   //累计签到次数
	LastSignedIn       int64 //最后签到时间
}

//管理员 设置 签到 活动
type AdminSignIn struct {
	Id       int64  `json:"id" orm:"pk;" ` //标签Id
	SellerId int64  `json:"seller_id"`     //买家ID
	Name     string `json:"name"`          //名称

	StartDate       int64  `json:"start_date"`
	EndDate         int64  `json:"end_date"`
	Memo            string `json:"memo"`
	ActivityType    int32  `json:"activity_type"`
	WirlessUrl      string `json:"wirless_url"`
	QrCode          string `json:"qr_code"`
	BackImg         string `json:"back_img"`
	NeedCollectShop int32  `json:"need_collect_shop"`
	Type            int32  `json:"type"`
	AsSignPrize     int32  `json:"as_sign_prize"`
	baseDate
}

//卖家 设置 签到 活动
type SellerSignIn struct {
	Id       int64  `json:"id" orm:"pk;" ` //标签Id
	SellerId int64  `json:"seller_id"`     //买家ID
	Name     string `json:"name"`          //名称

	StartDate       int64  `json:"start_date"`
	EndDate         int64  `json:"end_date"`
	Memo            string `json:"memo"`
	ActivityType    int32  `json:"activity_type"`
	WirlessUrl      string `json:"wirless_url"`
	QrCode          string `json:"qr_code"`
	BackImg         string `json:"back_img"`
	NeedCollectShop int32  `json:"need_collect_shop"`
	Type            int32  `json:"type"`
	AsSignPrize     int32  `json:"as_sign_prize"`
	baseDate
}

//卖家 设置 签到 活动 配置
type SellerSignInConfig struct {
	Id        int64  `json:"id" orm:"pk;" ` //标签Id
	SignId    int64  `json:"sign_id"`       //签到ID
	SignType  int32  `json:"sign_type"`
	SignDay   int32  `json:"sign_day"`
	PrizeId   int64  `json:"prize_id"`
	PrizeName string `json:"prize_name"`
	baseDate
}

//买家  签到 记录
type BuyerSignInRecord struct {
	Id        int64 `json:"id" orm:"pk;" ` //标签Id
	SignId    int64 `json:"sign_id"`       //签到ID
	BuyerId   int64 `json:"buyer_id"`      //买家ID
	SignDate  int64 `json:"sign_date"`
	SignMonth int32 `json:"sign_month"`
}

type BuyerSignInRecordLog struct {
	BuyerSignInRecord
}

func NewBuyerSignInRecordLog(buyerSignInRecord BuyerSignInRecord) BuyerSignInRecordLog {
	buyerSignInRecordLog := BuyerSignInRecordLog{}
	buyerSignInRecordLog.Id = buyerSignInRecord.Id
	buyerSignInRecordLog.SignId = buyerSignInRecord.SignId
	buyerSignInRecordLog.BuyerId = buyerSignInRecord.BuyerId
	buyerSignInRecordLog.SignDate = buyerSignInRecord.SignDate
	buyerSignInRecordLog.SignMonth = buyerSignInRecord.SignMonth
	return buyerSignInRecordLog
}

//用户签到明细
type signInDetail struct {
	TradePoints      int   //签到奖励金币
	ExperiencePoints int   //签到奖励积分
	DateCreated      int64 //签到时间
}

//用户签到明细
type UserSignInDetail struct {
	Id int64 `orm:"pk;"` //标签Id
	signInDetail
	UserId int64 //用户ID
}

//买家签到明细
type BuyerSignInDetail struct {
	Id int64 `orm:"pk;"` //标签Id
	signInDetail
	BuyerId int64 //买家ID
}
