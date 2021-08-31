package example

//"github.com/jinzhu/gorm"

//买家 退货
type BuyerRecharge struct {
	Id            int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
	BuyerId       int64  `json:"buyer_id"`
	Recharge      string `json:"recharge"`
	OutTradeId    string `json:"out_trade_id"`
	TransactionId string `json:"transaction_id"`
	Status        int32  `json:"status"`
	CreatedDate   int64  `json:"created_date"`
	UpdatedDate   int64  `json:"updated_date"`
}

func (BuyerRecharge) TableName() string {
	return "t_buyer_recharge"
}
