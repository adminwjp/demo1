package example

//用户举报
type baseReport struct {
	Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
	UserId     int64  `json:"user_id"`
	IsBuyer    bool   `json:"is_buyer"`
	GoodId     int64  `json:"good_id"`
	StroeId    int64  `json:"store_id"`
	Title      string `json:"title"`
	Content    string `json:"content"`
	Aduit      int    `json:"aduit"`
	ReportDate int64  `json:"report_date"`
	UpdateDate int64  `json:"update_date"`
}

//买家 举报
type BuyerReport struct {
	BuyerId int64 `json:"buyer_id"`
	baseReport
}

func (BuyerReport) TableName() string {
	return "t_buyer_report"
}

//卖家 举报
type SellerReport struct {
	SellerId int64 `json:"seller_id"`
	baseReport
}

func (SellerReport) TableName() string {
	return "t_seller_report"
}
