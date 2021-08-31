package example

// sigin =========
// beego orm
func (*UserSignIn) TableName() string {
	return "tn_UserSignIns"
}

func (*SellerSignInConfig) TableName() string {
	return "tn_seller_sign_in_config"
}
func (*BuyerSignInRecord) TableName() string {
	return "tn_buyser_sign_in_record"
}
func (*BuyerSignInRecordLog) TableName() string {
	return "tn_buyser_sign_in_record_log"
}
func (*UserSignInDetail) TableName() string {
	return "t_user_sign_in_detail"
}
func (*BuyerSignInDetail) TableName() string {
	return "t_buyer_sign_in_detail"
}

// sigin =========
