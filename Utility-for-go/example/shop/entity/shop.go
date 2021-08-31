package entity
type SellerCoupon struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids" gorm:"column:product_ids;" `
CatagoryIds         int64  `json:"catagory_ids" form:"catagory_ids"  xml:"catagory_ids" gorm:"column:catagory_ids;" `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id" gorm:"column:user_id;" `
}
func (SellerCoupon) TableName() string {
	return "t_seller_coupon"
}
type SellerCouponSettsing struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
CosFee         int64  `json:"cos_fee" form:"cos_fee"  xml:"cos_fee" gorm:"column:cos_fee;" `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee" gorm:"column:coupon_fee;" `
Flag         int64  `json:"flag" form:"flag"  xml:"flag" gorm:"column:flag;" `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id" gorm:"column:user_id;" `
}
func (SellerCouponSettsing) TableName() string {
	return "t_seller_coupon_settsing"
}
type BuyerFullReduction struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Total         int64  `json:"total" form:"total"  xml:"total" gorm:"column:total;" `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee" gorm:"column:coupon_fee;" `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id" gorm:"column:user_id;" `
}
func (BuyerFullReduction) TableName() string {
	return "t_buyer_full_reduction"
}
type Gift struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account" gorm:"column:update_account;" `
Status         int64  `json:"status" form:"status"  xml:"status" gorm:"column:status;" `
Picture         int64  `json:"picture" form:"picture"  xml:"picture" gorm:"column:picture;" `
Account         int64  `json:"account" form:"account"  xml:"account" gorm:"column:account;" `
Price         int64  `json:"price" form:"price"  xml:"price" gorm:"column:price;" `
Name         int64  `json:"name" form:"name"  xml:"name" gorm:"column:name;" `
}
func (Gift) TableName() string {
	return "t_gift"
}
type BuyerIntegral struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
SingnId         int64  `json:"singn_id" form:"singn_id"  xml:"singn_id" gorm:"column:singn_id;" `
Score         int64  `json:"score" form:"score"  xml:"score" gorm:"column:score;" `
}
func (BuyerIntegral) TableName() string {
	return "t_buyer_integral"
}
type SellerIntegralSetting struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Flag         int64  `json:"flag" form:"flag"  xml:"flag" gorm:"column:flag;" `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date" gorm:"column:start_date;" `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date" gorm:"column:end_date;" `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral" gorm:"column:cos_integral;" `
}
func (SellerIntegralSetting) TableName() string {
	return "t_seller_integral_setting"
}
type BuyerPrizeLog struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
SellerIntegralSettingId         int64  `json:"seller_integral_setting_id" form:"seller_integral_setting_id"  xml:"seller_integral_setting_id" gorm:"column:seller_integral_setting_id;" `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral" gorm:"column:cos_integral;" `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id" gorm:"column:user_id;" `
}
func (BuyerPrizeLog) TableName() string {
	return "t_buyer_prize_log"
}
type SellerPrize struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date" gorm:"column:start_date;" `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date" gorm:"column:end_date;" `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral" gorm:"column:cos_integral;" `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id" gorm:"column:user_id;" `
SellerPrizeSettingId         int64  `json:"seller_prize_setting_id" form:"seller_prize_setting_id"  xml:"seller_prize_setting_id" gorm:"column:seller_prize_setting_id;" `
}
func (SellerPrize) TableName() string {
	return "t_seller_prize"
}
type SellerPrizeSetting struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Name         int64  `json:"name" form:"name"  xml:"name" gorm:"column:name;" `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id" gorm:"column:user_id;" `
}
func (SellerPrizeSetting) TableName() string {
	return "t_seller_prize_setting"
}
type Audit struct {
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key" gorm:"column:item_key;" `
ItemName         int64  `json:"item_name" form:"item_name"  xml:"item_name" gorm:"column:item_name;" `
DisplayOrder         int64  `json:"display_order" form:"display_order"  xml:"display_order" gorm:"column:display_order;" `
Description         int64  `json:"description" form:"description"  xml:"description" gorm:"column:description;" `
}
func (Audit) TableName() string {
	return "t_audit"
}
type AuditInRole struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
RoleId         int64  `json:"role_id" form:"role_id"  xml:"role_id" gorm:"column:role_id;" `
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key" gorm:"column:item_key;" `
StrictDegree         int64  `json:"strict_degree" form:"strict_degree"  xml:"strict_degree" gorm:"column:strict_degree;" `
IsLocked         int64  `json:"is_locked" form:"is_locked"  xml:"is_locked" gorm:"column:is_locked;" `
}
func (AuditInRole) TableName() string {
	return "t_audit_in_role"
}
type Class struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Name         int64  `json:"name" form:"name"  xml:"name" gorm:"column:name;" `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time" gorm:"column:creation_time;" `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time" gorm:"column:last_modification_time;" `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time" gorm:"column:deletion_time;" `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted" gorm:"column:is_deleted;" `
}
func (Class) TableName() string {
	return "t_class"
}
type Email struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id" gorm:"column:seller_id;" `
Account         int64  `json:"account" form:"account"  xml:"account" gorm:"column:account;" `
Emails         int64  `json:"emails" form:"emails"  xml:"emails" gorm:"column:emails;" `
Scrept         int64  `json:"scrept" form:"scrept"  xml:"scrept" gorm:"column:scrept;" `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default" gorm:"column:is_default;" `
}
func (Email) TableName() string {
	return "t_email"
}
type EmailNotifyProduct struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Status         int64  `json:"status" form:"status"  xml:"status" gorm:"column:status;" `
Notifydate         int64  `json:"notifydate" form:"notifydate"  xml:"notifydate" gorm:"column:notifydate;" `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name" gorm:"column:product_name;" `
ReceiveEmail         int64  `json:"receive_email" form:"receive_email"  xml:"receive_email" gorm:"column:receive_email;" `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d" gorm:"column:product_i_d;" `
SendFailureCount         int64  `json:"send_failure_count" form:"send_failure_count"  xml:"send_failure_count" gorm:"column:send_failure_count;" `
Account         int64  `json:"account" form:"account"  xml:"account" gorm:"column:account;" `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id" gorm:"column:seller_id;" `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id" gorm:"column:buyer_id;" `
}
func (EmailNotifyProduct) TableName() string {
	return "t_email_notify_product"
}
type BuyerFavourite struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
ProductId         int64  `json:"product_id" form:"product_id"  xml:"product_id" gorm:"column:product_id;" `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id" gorm:"column:buyer_id;" `
}
func (BuyerFavourite) TableName() string {
	return "t_buyer_favourite"
}
type Menu struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Name         int64  `json:"name" form:"name"  xml:"name" gorm:"column:name;" `
Href         int64  `json:"href" form:"href"  xml:"href" gorm:"column:href;" `
Icon         int64  `json:"icon" form:"icon"  xml:"icon" gorm:"column:icon;" `
Menus         int64  `json:"menus" form:"menus"  xml:"menus" gorm:"column:menus;" `
Flag         int64  `json:"flag" form:"flag"  xml:"flag" gorm:"column:flag;" `
Parent         int64  `json:"parent" form:"parent"  xml:"parent" gorm:"column:parent;" `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id" gorm:"column:parent_id;" `
Children         int64  `json:"children" form:"children"  xml:"children" gorm:"column:children;" `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time" gorm:"column:deletion_time;" `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted" gorm:"column:is_deleted;" `
}
func (Menu) TableName() string {
	return "t_menu"
}
type ProductMsg struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids" gorm:"column:product_ids;" `
Title         int64  `json:"title" form:"title"  xml:"title" gorm:"column:title;" `
Msg         int64  `json:"msg" form:"msg"  xml:"msg" gorm:"column:msg;" `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date" gorm:"column:start_date;" `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date" gorm:"column:end_date;" `
End         int64  `json:"end" form:"end"  xml:"end" gorm:"column:end;" `
Pic         int64  `json:"pic" form:"pic"  xml:"pic" gorm:"column:pic;" `
Times         int64  `json:"times" form:"times"  xml:"times" gorm:"column:times;" `
}
func (ProductMsg) TableName() string {
	return "t_product_msg"
}
type Cart struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids" gorm:"column:product_ids;" `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id" gorm:"column:user_id;" `
}
func (Cart) TableName() string {
	return "t_cart"
}
type Order struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
CartId         int64  `json:"cart_id" form:"cart_id"  xml:"cart_id" gorm:"column:cart_id;" `
Status         int64  `json:"status" form:"status"  xml:"status" gorm:"column:status;" `
Score         int64  `json:"score" form:"score"  xml:"score" gorm:"column:score;" `
Ptotal         int64  `json:"ptotal" form:"ptotal"  xml:"ptotal" gorm:"column:ptotal;" `
UpdateAmount         int64  `json:"update_amount" form:"update_amount"  xml:"update_amount" gorm:"column:update_amount;" `
ExpressCompanyName         int64  `json:"express_company_name" form:"express_company_name"  xml:"express_company_name" gorm:"column:express_company_name;" `
PayType         int64  `json:"pay_type" form:"pay_type"  xml:"pay_type" gorm:"column:pay_type;" `
Rebate         int64  `json:"rebate" form:"rebate"  xml:"rebate" gorm:"column:rebate;" `
Carry         int64  `json:"carry" form:"carry"  xml:"carry" gorm:"column:carry;" `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks" gorm:"column:low_stocks;" `
RefundStatus         int64  `json:"refund_status" form:"refund_status"  xml:"refund_status" gorm:"column:refund_status;" `
Fee         int64  `json:"fee" form:"fee"  xml:"fee" gorm:"column:fee;" `
OtherRequirement         int64  `json:"other_requirement" form:"other_requirement"  xml:"other_requirement" gorm:"column:other_requirement;" `
Paystatus         int64  `json:"paystatus" form:"paystatus"  xml:"paystatus" gorm:"column:paystatus;" `
Remark         int64  `json:"remark" form:"remark"  xml:"remark" gorm:"column:remark;" `
ClosedComment         int64  `json:"closed_comment" form:"closed_comment"  xml:"closed_comment" gorm:"column:closed_comment;" `
ExpressNo         int64  `json:"express_no" form:"express_no"  xml:"express_no" gorm:"column:express_no;" `
ExpressName         int64  `json:"express_name" form:"express_name"  xml:"express_name" gorm:"column:express_name;" `
Quantity         int64  `json:"quantity" form:"quantity"  xml:"quantity" gorm:"column:quantity;" `
ConfirmSendProductRemark         int64  `json:"confirm_send_product_remark" form:"confirm_send_product_remark"  xml:"confirm_send_product_remark" gorm:"column:confirm_send_product_remark;" `
ExpressCode         int64  `json:"express_code" form:"express_code"  xml:"express_code" gorm:"column:express_code;" `
Account         int64  `json:"account" form:"account"  xml:"account" gorm:"column:account;" `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id" gorm:"column:buyer_id;" `
Amount         int64  `json:"amount" form:"amount"  xml:"amount" gorm:"column:amount;" `
}
func (Order) TableName() string {
	return "t_order"
}
type OrderDetail struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d" gorm:"column:product_i_d;" `
Fee         int64  `json:"fee" form:"fee"  xml:"fee" gorm:"column:fee;" `
IsComment         int64  `json:"is_comment" form:"is_comment"  xml:"is_comment" gorm:"column:is_comment;" `
SellerID         int64  `json:"seller_i_d" form:"seller_i_d"  xml:"seller_i_d" gorm:"column:seller_i_d;" `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name" gorm:"column:product_name;" `
GiftID         int64  `json:"gift_i_d" form:"gift_i_d"  xml:"gift_i_d" gorm:"column:gift_i_d;" `
OrderID         int64  `json:"order_i_d" form:"order_i_d"  xml:"order_i_d" gorm:"column:order_i_d;" `
SpecInfo         int64  `json:"spec_info" form:"spec_info"  xml:"spec_info" gorm:"column:spec_info;" `
Score         int64  `json:"score" form:"score"  xml:"score" gorm:"column:score;" `
Number         int64  `json:"number" form:"number"  xml:"number" gorm:"column:number;" `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks" gorm:"column:low_stocks;" `
Price         int64  `json:"price" form:"price"  xml:"price" gorm:"column:price;" `
Total         int64  `json:"total" form:"total"  xml:"total" gorm:"column:total;" `
}
func (OrderDetail) TableName() string {
	return "t_order_detail"
}
type OrderPay struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id" gorm:"column:order_id;" `
ConfirmDate         int64  `json:"confirm_date" form:"confirm_date"  xml:"confirm_date" gorm:"column:confirm_date;" `
TradeNo         int64  `json:"trade_no" form:"trade_no"  xml:"trade_no" gorm:"column:trade_no;" `
Remark         int64  `json:"remark" form:"remark"  xml:"remark" gorm:"column:remark;" `
PayMethod         int64  `json:"pay_method" form:"pay_method"  xml:"pay_method" gorm:"column:pay_method;" `
PayAmount         int64  `json:"pay_amount" form:"pay_amount"  xml:"pay_amount" gorm:"column:pay_amount;" `
ConfirmUser         int64  `json:"confirm_user" form:"confirm_user"  xml:"confirm_user" gorm:"column:confirm_user;" `
PayStatus         int64  `json:"pay_status" form:"pay_status"  xml:"pay_status" gorm:"column:pay_status;" `
}
func (OrderPay) TableName() string {
	return "t_order_pay"
}
type OrderLog struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id" gorm:"column:order_id;" `
Content         int64  `json:"content" form:"content"  xml:"content" gorm:"column:content;" `
AccountType         int64  `json:"account_type" form:"account_type"  xml:"account_type" gorm:"column:account_type;" `
Account         int64  `json:"account" form:"account"  xml:"account" gorm:"column:account;" `
}
func (OrderLog) TableName() string {
	return "t_order_log"
}
type OrderShip struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Tel         int64  `json:"tel" form:"tel"  xml:"tel" gorm:"column:tel;" `
CityCode         int64  `json:"city_code" form:"city_code"  xml:"city_code" gorm:"column:city_code;" `
City         int64  `json:"city" form:"city"  xml:"city" gorm:"column:city;" `
Province         int64  `json:"province" form:"province"  xml:"province" gorm:"column:province;" `
Sex         int64  `json:"sex" form:"sex"  xml:"sex" gorm:"column:sex;" `
AreaCode         int64  `json:"area_code" form:"area_code"  xml:"area_code" gorm:"column:area_code;" `
Zip         int64  `json:"zip" form:"zip"  xml:"zip" gorm:"column:zip;" `
Shipname         int64  `json:"shipname" form:"shipname"  xml:"shipname" gorm:"column:shipname;" `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id" gorm:"column:order_id;" `
ProvinceCode         int64  `json:"province_code" form:"province_code"  xml:"province_code" gorm:"column:province_code;" `
Remark         int64  `json:"remark" form:"remark"  xml:"remark" gorm:"column:remark;" `
Phone         int64  `json:"phone" form:"phone"  xml:"phone" gorm:"column:phone;" `
Shipaddress         int64  `json:"shipaddress" form:"shipaddress"  xml:"shipaddress" gorm:"column:shipaddress;" `
Area         int64  `json:"area" form:"area"  xml:"area" gorm:"column:area;" `
}
func (OrderShip) TableName() string {
	return "t_order_ship"
}
type Product struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
CatagoryID         int64  `json:"catagory_i_d" form:"catagory_i_d"  xml:"catagory_i_d" gorm:"column:catagory_i_d;" `
SellCount         int64  `json:"sell_count" form:"sell_count"  xml:"sell_count" gorm:"column:sell_count;" `
Stock         int64  `json:"stock" form:"stock"  xml:"stock" gorm:"column:stock;" `
Keywords         int64  `json:"keywords" form:"keywords"  xml:"keywords" gorm:"column:keywords;" `
Score         int64  `json:"score" form:"score"  xml:"score" gorm:"column:score;" `
CreateAccount         int64  `json:"create_account" form:"create_account"  xml:"create_account" gorm:"column:create_account;" `
Hit         int64  `json:"hit" form:"hit"  xml:"hit" gorm:"column:hit;" `
Title         int64  `json:"title" form:"title"  xml:"title" gorm:"column:title;" `
Price         int64  `json:"price" form:"price"  xml:"price" gorm:"column:price;" `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account" gorm:"column:update_account;" `
ActivityID         int64  `json:"activity_i_d" form:"activity_i_d"  xml:"activity_i_d" gorm:"column:activity_i_d;" `
Status         int64  `json:"status" form:"status"  xml:"status" gorm:"column:status;" `
IsTimePromotion         int64  `json:"is_time_promotion" form:"is_time_promotion"  xml:"is_time_promotion" gorm:"column:is_time_promotion;" `
ProductHTML         int64  `json:"product_h_t_m_l" form:"product_h_t_m_l"  xml:"product_h_t_m_l" gorm:"column:product_h_t_m_l;" `
Isnew         int64  `json:"isnew" form:"isnew"  xml:"isnew" gorm:"column:isnew;" `
Introduce         int64  `json:"introduce" form:"introduce"  xml:"introduce" gorm:"column:introduce;" `
SearchKey         int64  `json:"search_key" form:"search_key"  xml:"search_key" gorm:"column:search_key;" `
Images         int64  `json:"images" form:"images"  xml:"images" gorm:"column:images;" `
NowPrice         int64  `json:"now_price" form:"now_price"  xml:"now_price" gorm:"column:now_price;" `
MaxPicture         int64  `json:"max_picture" form:"max_picture"  xml:"max_picture" gorm:"column:max_picture;" `
Description         int64  `json:"description" form:"description"  xml:"description" gorm:"column:description;" `
Unit         int64  `json:"unit" form:"unit"  xml:"unit" gorm:"column:unit;" `
Picture         int64  `json:"picture" form:"picture"  xml:"picture" gorm:"column:picture;" `
Name         int64  `json:"name" form:"name"  xml:"name" gorm:"column:name;" `
Sale         int64  `json:"sale" form:"sale"  xml:"sale" gorm:"column:sale;" `
}
func (Product) TableName() string {
	return "t_product"
}
type ProductCatagory struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Name         int64  `json:"name" form:"name"  xml:"name" gorm:"column:name;" `
Code         int64  `json:"code" form:"code"  xml:"code" gorm:"column:code;" `
Href         int64  `json:"href" form:"href"  xml:"href" gorm:"column:href;" `
Orders         int64  `json:"orders" form:"orders"  xml:"orders" gorm:"column:orders;" `
Picture         int64  `json:"picture" form:"picture"  xml:"picture" gorm:"column:picture;" `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id" gorm:"column:parent_id;" `
Parent         int64  `json:"parent" form:"parent"  xml:"parent" gorm:"column:parent;" `
Children         int64  `json:"children" form:"children"  xml:"children" gorm:"column:children;" `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time" gorm:"column:creation_time;" `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time" gorm:"column:last_modification_time;" `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time" gorm:"column:deletion_time;" `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted" gorm:"column:is_deleted;" `
}
func (ProductCatagory) TableName() string {
	return "t_product_catagory"
}
type Student struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
StudentId         int64  `json:"student_id" form:"student_id"  xml:"student_id" gorm:"column:student_id;" `
Name         int64  `json:"name" form:"name"  xml:"name" gorm:"column:name;" `
Age         int64  `json:"age" form:"age"  xml:"age" gorm:"column:age;" `
Sex         int64  `json:"sex" form:"sex"  xml:"sex" gorm:"column:sex;" `
class_id         int64  `json:"class_id" form:"class_id"  xml:"class_id" gorm:"column:class_id;" `
Class         int64  `json:"class" form:"class"  xml:"class" gorm:"column:class;" `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time" gorm:"column:creation_time;" `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time" gorm:"column:last_modification_time;" `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time" gorm:"column:deletion_time;" `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted" gorm:"column:is_deleted;" `
}
func (Student) TableName() string {
	return "t_student"
}
type Buyer struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Realname         int64  `json:"realname" form:"realname"  xml:"realname" gorm:"column:realname;" `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl" gorm:"column:headimgurl;" `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id" gorm:"column:open_id;" `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq" gorm:"column:im_qq;" `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id" gorm:"column:auth_app_id;" `
Language         int64  `json:"language" form:"language"  xml:"language" gorm:"column:language;" `
City         int64  `json:"city" form:"city"  xml:"city" gorm:"column:city;" `
Province         int64  `json:"province" form:"province"  xml:"province" gorm:"column:province;" `
Country         int64  `json:"country" form:"country"  xml:"country" gorm:"column:country;" `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe" gorm:"column:subscribe;" `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time" gorm:"column:subscribe_time;" `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid" gorm:"column:groupid;" `
Remark         int64  `json:"remark" form:"remark"  xml:"remark" gorm:"column:remark;" `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip" gorm:"column:access_ip;" `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token" gorm:"column:refresh_token;" `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in" gorm:"column:token_expires_in;" `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date" gorm:"column:last_login_date;" `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid" gorm:"column:unionid;" `
Score         int64  `json:"score" form:"score"  xml:"score" gorm:"column:score;" `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver" gorm:"column:is_receiver;" `
Account         int64  `json:"account" form:"account"  xml:"account" gorm:"column:account;" `
Phone         int64  `json:"phone" form:"phone"  xml:"phone" gorm:"column:phone;" `
Email         int64  `json:"email" form:"email"  xml:"email" gorm:"column:email;" `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd" gorm:"column:pwd;" `
Name         int64  `json:"name" form:"name"  xml:"name" gorm:"column:name;" `
Age         int64  `json:"age" form:"age"  xml:"age" gorm:"column:age;" `
Sex         int64  `json:"sex" form:"sex"  xml:"sex" gorm:"column:sex;" `
Address         int64  `json:"address" form:"address"  xml:"address" gorm:"column:address;" `
Bir         int64  `json:"bir" form:"bir"  xml:"bir" gorm:"column:bir;" `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode" gorm:"column:postcode;" `
Balance         int64  `json:"balance" form:"balance"  xml:"balance" gorm:"column:balance;" `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip" gorm:"column:login_ip;" `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip" gorm:"column:register_ip;" `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc" gorm:"column:is_login_pc;" `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag" gorm:"column:pc_flag;" `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app" gorm:"column:is_login_app;" `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag" gorm:"column:app_flag;" `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program" gorm:"column:is_login_wx_program;" `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower" gorm:"column:is_login_brower;" `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date" gorm:"column:reg_date;" `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date" gorm:"column:login_date;" `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time" gorm:"column:deletion_time;" `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted" gorm:"column:is_deleted;" `
}
func (Buyer) TableName() string {
	return "t_buyer"
}
type Seller struct {
UserLevel         int64  `json:"user_level" form:"user_level"  xml:"user_level" gorm:"column:user_level;" `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date" gorm:"column:start_date;" `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date" gorm:"column:end_date;" `
VersionNo         int64  `json:"version_no" form:"version_no"  xml:"version_no" gorm:"column:version_no;" `
LoginFailureCount         int64  `json:"login_failure_count" form:"login_failure_count"  xml:"login_failure_count" gorm:"column:login_failure_count;" `
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
Realname         int64  `json:"realname" form:"realname"  xml:"realname" gorm:"column:realname;" `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl" gorm:"column:headimgurl;" `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id" gorm:"column:open_id;" `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq" gorm:"column:im_qq;" `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id" gorm:"column:auth_app_id;" `
Language         int64  `json:"language" form:"language"  xml:"language" gorm:"column:language;" `
City         int64  `json:"city" form:"city"  xml:"city" gorm:"column:city;" `
Province         int64  `json:"province" form:"province"  xml:"province" gorm:"column:province;" `
Country         int64  `json:"country" form:"country"  xml:"country" gorm:"column:country;" `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe" gorm:"column:subscribe;" `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time" gorm:"column:subscribe_time;" `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid" gorm:"column:groupid;" `
Remark         int64  `json:"remark" form:"remark"  xml:"remark" gorm:"column:remark;" `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip" gorm:"column:access_ip;" `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token" gorm:"column:refresh_token;" `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in" gorm:"column:token_expires_in;" `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date" gorm:"column:last_login_date;" `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid" gorm:"column:unionid;" `
Score         int64  `json:"score" form:"score"  xml:"score" gorm:"column:score;" `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver" gorm:"column:is_receiver;" `
Account         int64  `json:"account" form:"account"  xml:"account" gorm:"column:account;" `
Phone         int64  `json:"phone" form:"phone"  xml:"phone" gorm:"column:phone;" `
Email         int64  `json:"email" form:"email"  xml:"email" gorm:"column:email;" `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd" gorm:"column:pwd;" `
Name         int64  `json:"name" form:"name"  xml:"name" gorm:"column:name;" `
Age         int64  `json:"age" form:"age"  xml:"age" gorm:"column:age;" `
Sex         int64  `json:"sex" form:"sex"  xml:"sex" gorm:"column:sex;" `
Address         int64  `json:"address" form:"address"  xml:"address" gorm:"column:address;" `
Bir         int64  `json:"bir" form:"bir"  xml:"bir" gorm:"column:bir;" `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode" gorm:"column:postcode;" `
Balance         int64  `json:"balance" form:"balance"  xml:"balance" gorm:"column:balance;" `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip" gorm:"column:login_ip;" `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip" gorm:"column:register_ip;" `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc" gorm:"column:is_login_pc;" `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag" gorm:"column:pc_flag;" `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app" gorm:"column:is_login_app;" `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag" gorm:"column:app_flag;" `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program" gorm:"column:is_login_wx_program;" `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower" gorm:"column:is_login_brower;" `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date" gorm:"column:reg_date;" `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date" gorm:"column:login_date;" `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time" gorm:"column:deletion_time;" `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted" gorm:"column:is_deleted;" `
}
func (Seller) TableName() string {
	return "t_seller"
}
type BuyerAddress struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name" gorm:"column:contact_name;" `
Address         int64  `json:"address" form:"address"  xml:"address" gorm:"column:address;" `
City         int64  `json:"city" form:"city"  xml:"city" gorm:"column:city;" `
Province         int64  `json:"province" form:"province"  xml:"province" gorm:"column:province;" `
Country         int64  `json:"country" form:"country"  xml:"country" gorm:"column:country;" `
Memo         int64  `json:"memo" form:"memo"  xml:"memo" gorm:"column:memo;" `
Phone         int64  `json:"phone" form:"phone"  xml:"phone" gorm:"column:phone;" `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code" gorm:"column:post_code;" `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default" gorm:"column:is_default;" `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id" gorm:"column:user_id;" `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time" gorm:"column:creation_time;" `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time" gorm:"column:last_modification_time;" `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time" gorm:"column:deletion_time;" `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted" gorm:"column:is_deleted;" `
}
func (BuyerAddress) TableName() string {
	return "t_buyer_address"
}
type SellerAddress struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name" gorm:"column:contact_name;" `
Address         int64  `json:"address" form:"address"  xml:"address" gorm:"column:address;" `
City         int64  `json:"city" form:"city"  xml:"city" gorm:"column:city;" `
Province         int64  `json:"province" form:"province"  xml:"province" gorm:"column:province;" `
Country         int64  `json:"country" form:"country"  xml:"country" gorm:"column:country;" `
Memo         int64  `json:"memo" form:"memo"  xml:"memo" gorm:"column:memo;" `
Phone         int64  `json:"phone" form:"phone"  xml:"phone" gorm:"column:phone;" `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code" gorm:"column:post_code;" `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default" gorm:"column:is_default;" `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id" gorm:"column:user_id;" `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time" gorm:"column:creation_time;" `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time" gorm:"column:last_modification_time;" `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time" gorm:"column:deletion_time;" `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted" gorm:"column:is_deleted;" `
}
func (SellerAddress) TableName() string {
	return "t_seller_address"
}
type Sms struct {
Id         int64  `json:"id" form:"id"  xml:"id" gorm:"primary_key" `
AppId         int64  `json:"app_id" form:"app_id"  xml:"app_id" gorm:"column:app_id;" `
Secrt         int64  `json:"secrt" form:"secrt"  xml:"secrt" gorm:"column:secrt;" `
}
func (Sms) TableName() string {
	return "t_sms"
}
