package dto
type CreateSellerCouponInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
CatagoryIds         int64  `json:"catagory_ids" form:"catagory_ids"  xml:"catagory_ids"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type UpdateSellerCouponInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
CatagoryIds         int64  `json:"catagory_ids" form:"catagory_ids"  xml:"catagory_ids"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QuerySellerCouponInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
CatagoryIds         int64  `json:"catagory_ids" form:"catagory_ids"  xml:"catagory_ids"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QuerySellerCouponOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
CatagoryIds         int64  `json:"catagory_ids" form:"catagory_ids"  xml:"catagory_ids"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type CreateSellerCouponSettsingInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CosFee         int64  `json:"cos_fee" form:"cos_fee"  xml:"cos_fee"  `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type UpdateSellerCouponSettsingInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CosFee         int64  `json:"cos_fee" form:"cos_fee"  xml:"cos_fee"  `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QuerySellerCouponSettsingInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CosFee         int64  `json:"cos_fee" form:"cos_fee"  xml:"cos_fee"  `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QuerySellerCouponSettsingOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CosFee         int64  `json:"cos_fee" form:"cos_fee"  xml:"cos_fee"  `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type CreateBuyerFullReductionInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Total         int64  `json:"total" form:"total"  xml:"total"  `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type UpdateBuyerFullReductionInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Total         int64  `json:"total" form:"total"  xml:"total"  `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QueryBuyerFullReductionInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Total         int64  `json:"total" form:"total"  xml:"total"  `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QueryBuyerFullReductionOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Total         int64  `json:"total" form:"total"  xml:"total"  `
CouponFee         int64  `json:"coupon_fee" form:"coupon_fee"  xml:"coupon_fee"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type CreateGiftInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
}
type UpdateGiftInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
}
type QueryGiftInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
}
type QueryGiftOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
}
type CreateBuyerIntegralInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SingnId         int64  `json:"singn_id" form:"singn_id"  xml:"singn_id"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
}
type UpdateBuyerIntegralInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SingnId         int64  `json:"singn_id" form:"singn_id"  xml:"singn_id"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
}
type QueryBuyerIntegralInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SingnId         int64  `json:"singn_id" form:"singn_id"  xml:"singn_id"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
}
type QueryBuyerIntegralOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SingnId         int64  `json:"singn_id" form:"singn_id"  xml:"singn_id"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
}
type CreateSellerIntegralSettingInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
}
type UpdateSellerIntegralSettingInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
}
type QuerySellerIntegralSettingInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
}
type QuerySellerIntegralSettingOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
}
type CreateBuyerPrizeLogInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SellerIntegralSettingId         int64  `json:"seller_integral_setting_id" form:"seller_integral_setting_id"  xml:"seller_integral_setting_id"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type UpdateBuyerPrizeLogInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SellerIntegralSettingId         int64  `json:"seller_integral_setting_id" form:"seller_integral_setting_id"  xml:"seller_integral_setting_id"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QueryBuyerPrizeLogInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SellerIntegralSettingId         int64  `json:"seller_integral_setting_id" form:"seller_integral_setting_id"  xml:"seller_integral_setting_id"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QueryBuyerPrizeLogOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SellerIntegralSettingId         int64  `json:"seller_integral_setting_id" form:"seller_integral_setting_id"  xml:"seller_integral_setting_id"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type CreateSellerPrizeInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
SellerPrizeSettingId         int64  `json:"seller_prize_setting_id" form:"seller_prize_setting_id"  xml:"seller_prize_setting_id"  `
}
type UpdateSellerPrizeInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
SellerPrizeSettingId         int64  `json:"seller_prize_setting_id" form:"seller_prize_setting_id"  xml:"seller_prize_setting_id"  `
}
type QuerySellerPrizeInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
SellerPrizeSettingId         int64  `json:"seller_prize_setting_id" form:"seller_prize_setting_id"  xml:"seller_prize_setting_id"  `
}
type QuerySellerPrizeOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
CosIntegral         int64  `json:"cos_integral" form:"cos_integral"  xml:"cos_integral"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
SellerPrizeSettingId         int64  `json:"seller_prize_setting_id" form:"seller_prize_setting_id"  xml:"seller_prize_setting_id"  `
}
type CreateSellerPrizeSettingInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type UpdateSellerPrizeSettingInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QuerySellerPrizeSettingInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QuerySellerPrizeSettingOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type CreateAuditInput struct {
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key"  `
ItemName         int64  `json:"item_name" form:"item_name"  xml:"item_name"  `
DisplayOrder         int64  `json:"display_order" form:"display_order"  xml:"display_order"  `
Description         int64  `json:"description" form:"description"  xml:"description"  `
}
type UpdateAuditInput struct {
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key"  `
ItemName         int64  `json:"item_name" form:"item_name"  xml:"item_name"  `
DisplayOrder         int64  `json:"display_order" form:"display_order"  xml:"display_order"  `
Description         int64  `json:"description" form:"description"  xml:"description"  `
}
type QueryAuditInput struct {
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key"  `
ItemName         int64  `json:"item_name" form:"item_name"  xml:"item_name"  `
DisplayOrder         int64  `json:"display_order" form:"display_order"  xml:"display_order"  `
Description         int64  `json:"description" form:"description"  xml:"description"  `
}
type QueryAuditOutput struct {
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key"  `
ItemName         int64  `json:"item_name" form:"item_name"  xml:"item_name"  `
DisplayOrder         int64  `json:"display_order" form:"display_order"  xml:"display_order"  `
Description         int64  `json:"description" form:"description"  xml:"description"  `
}
type CreateAuditInRoleInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
RoleId         int64  `json:"role_id" form:"role_id"  xml:"role_id"  `
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key"  `
StrictDegree         int64  `json:"strict_degree" form:"strict_degree"  xml:"strict_degree"  `
IsLocked         int64  `json:"is_locked" form:"is_locked"  xml:"is_locked"  `
}
type UpdateAuditInRoleInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
RoleId         int64  `json:"role_id" form:"role_id"  xml:"role_id"  `
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key"  `
StrictDegree         int64  `json:"strict_degree" form:"strict_degree"  xml:"strict_degree"  `
IsLocked         int64  `json:"is_locked" form:"is_locked"  xml:"is_locked"  `
}
type QueryAuditInRoleInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
RoleId         int64  `json:"role_id" form:"role_id"  xml:"role_id"  `
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key"  `
StrictDegree         int64  `json:"strict_degree" form:"strict_degree"  xml:"strict_degree"  `
IsLocked         int64  `json:"is_locked" form:"is_locked"  xml:"is_locked"  `
}
type QueryAuditInRoleOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
RoleId         int64  `json:"role_id" form:"role_id"  xml:"role_id"  `
ItemKey         int64  `json:"item_key" form:"item_key"  xml:"item_key"  `
StrictDegree         int64  `json:"strict_degree" form:"strict_degree"  xml:"strict_degree"  `
IsLocked         int64  `json:"is_locked" form:"is_locked"  xml:"is_locked"  `
}
type CreateClassInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type UpdateClassInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryClassInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryClassOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type CreateEmailInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Emails         int64  `json:"emails" form:"emails"  xml:"emails"  `
Scrept         int64  `json:"scrept" form:"scrept"  xml:"scrept"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
}
type UpdateEmailInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Emails         int64  `json:"emails" form:"emails"  xml:"emails"  `
Scrept         int64  `json:"scrept" form:"scrept"  xml:"scrept"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
}
type QueryEmailInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Emails         int64  `json:"emails" form:"emails"  xml:"emails"  `
Scrept         int64  `json:"scrept" form:"scrept"  xml:"scrept"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
}
type QueryEmailOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Emails         int64  `json:"emails" form:"emails"  xml:"emails"  `
Scrept         int64  `json:"scrept" form:"scrept"  xml:"scrept"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
}
type CreateEmailNotifyProductInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Notifydate         int64  `json:"notifydate" form:"notifydate"  xml:"notifydate"  `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name"  `
ReceiveEmail         int64  `json:"receive_email" form:"receive_email"  xml:"receive_email"  `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d"  `
SendFailureCount         int64  `json:"send_failure_count" form:"send_failure_count"  xml:"send_failure_count"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
}
type UpdateEmailNotifyProductInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Notifydate         int64  `json:"notifydate" form:"notifydate"  xml:"notifydate"  `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name"  `
ReceiveEmail         int64  `json:"receive_email" form:"receive_email"  xml:"receive_email"  `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d"  `
SendFailureCount         int64  `json:"send_failure_count" form:"send_failure_count"  xml:"send_failure_count"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
}
type QueryEmailNotifyProductInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Notifydate         int64  `json:"notifydate" form:"notifydate"  xml:"notifydate"  `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name"  `
ReceiveEmail         int64  `json:"receive_email" form:"receive_email"  xml:"receive_email"  `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d"  `
SendFailureCount         int64  `json:"send_failure_count" form:"send_failure_count"  xml:"send_failure_count"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
}
type QueryEmailNotifyProductOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Notifydate         int64  `json:"notifydate" form:"notifydate"  xml:"notifydate"  `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name"  `
ReceiveEmail         int64  `json:"receive_email" form:"receive_email"  xml:"receive_email"  `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d"  `
SendFailureCount         int64  `json:"send_failure_count" form:"send_failure_count"  xml:"send_failure_count"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
SellerId         int64  `json:"seller_id" form:"seller_id"  xml:"seller_id"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
}
type CreateBuyerFavouriteInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductId         int64  `json:"product_id" form:"product_id"  xml:"product_id"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
}
type UpdateBuyerFavouriteInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductId         int64  `json:"product_id" form:"product_id"  xml:"product_id"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
}
type QueryBuyerFavouriteInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductId         int64  `json:"product_id" form:"product_id"  xml:"product_id"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
}
type QueryBuyerFavouriteOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductId         int64  `json:"product_id" form:"product_id"  xml:"product_id"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
}
type CreateMenuInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Href         int64  `json:"href" form:"href"  xml:"href"  `
Icon         int64  `json:"icon" form:"icon"  xml:"icon"  `
Menus         int64  `json:"menus" form:"menus"  xml:"menus"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
Parent         int64  `json:"parent" form:"parent"  xml:"parent"  `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id"  `
Children         int64  `json:"children" form:"children"  xml:"children"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type UpdateMenuInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Href         int64  `json:"href" form:"href"  xml:"href"  `
Icon         int64  `json:"icon" form:"icon"  xml:"icon"  `
Menus         int64  `json:"menus" form:"menus"  xml:"menus"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
Parent         int64  `json:"parent" form:"parent"  xml:"parent"  `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id"  `
Children         int64  `json:"children" form:"children"  xml:"children"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryMenuInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Href         int64  `json:"href" form:"href"  xml:"href"  `
Icon         int64  `json:"icon" form:"icon"  xml:"icon"  `
Menus         int64  `json:"menus" form:"menus"  xml:"menus"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
Parent         int64  `json:"parent" form:"parent"  xml:"parent"  `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id"  `
Children         int64  `json:"children" form:"children"  xml:"children"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryMenuOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Href         int64  `json:"href" form:"href"  xml:"href"  `
Icon         int64  `json:"icon" form:"icon"  xml:"icon"  `
Menus         int64  `json:"menus" form:"menus"  xml:"menus"  `
Flag         int64  `json:"flag" form:"flag"  xml:"flag"  `
Parent         int64  `json:"parent" form:"parent"  xml:"parent"  `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id"  `
Children         int64  `json:"children" form:"children"  xml:"children"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type CreateProductMsgInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
Title         int64  `json:"title" form:"title"  xml:"title"  `
Msg         int64  `json:"msg" form:"msg"  xml:"msg"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
End         int64  `json:"end" form:"end"  xml:"end"  `
Pic         int64  `json:"pic" form:"pic"  xml:"pic"  `
Times         int64  `json:"times" form:"times"  xml:"times"  `
}
type UpdateProductMsgInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
Title         int64  `json:"title" form:"title"  xml:"title"  `
Msg         int64  `json:"msg" form:"msg"  xml:"msg"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
End         int64  `json:"end" form:"end"  xml:"end"  `
Pic         int64  `json:"pic" form:"pic"  xml:"pic"  `
Times         int64  `json:"times" form:"times"  xml:"times"  `
}
type QueryProductMsgInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
Title         int64  `json:"title" form:"title"  xml:"title"  `
Msg         int64  `json:"msg" form:"msg"  xml:"msg"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
End         int64  `json:"end" form:"end"  xml:"end"  `
Pic         int64  `json:"pic" form:"pic"  xml:"pic"  `
Times         int64  `json:"times" form:"times"  xml:"times"  `
}
type QueryProductMsgOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
Title         int64  `json:"title" form:"title"  xml:"title"  `
Msg         int64  `json:"msg" form:"msg"  xml:"msg"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
End         int64  `json:"end" form:"end"  xml:"end"  `
Pic         int64  `json:"pic" form:"pic"  xml:"pic"  `
Times         int64  `json:"times" form:"times"  xml:"times"  `
}
type CreateCartInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type UpdateCartInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QueryCartInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type QueryCartOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductIds         int64  `json:"product_ids" form:"product_ids"  xml:"product_ids"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
}
type CreateOrderInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CartId         int64  `json:"cart_id" form:"cart_id"  xml:"cart_id"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
Ptotal         int64  `json:"ptotal" form:"ptotal"  xml:"ptotal"  `
UpdateAmount         int64  `json:"update_amount" form:"update_amount"  xml:"update_amount"  `
ExpressCompanyName         int64  `json:"express_company_name" form:"express_company_name"  xml:"express_company_name"  `
PayType         int64  `json:"pay_type" form:"pay_type"  xml:"pay_type"  `
Rebate         int64  `json:"rebate" form:"rebate"  xml:"rebate"  `
Carry         int64  `json:"carry" form:"carry"  xml:"carry"  `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks"  `
RefundStatus         int64  `json:"refund_status" form:"refund_status"  xml:"refund_status"  `
Fee         int64  `json:"fee" form:"fee"  xml:"fee"  `
OtherRequirement         int64  `json:"other_requirement" form:"other_requirement"  xml:"other_requirement"  `
Paystatus         int64  `json:"paystatus" form:"paystatus"  xml:"paystatus"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
ClosedComment         int64  `json:"closed_comment" form:"closed_comment"  xml:"closed_comment"  `
ExpressNo         int64  `json:"express_no" form:"express_no"  xml:"express_no"  `
ExpressName         int64  `json:"express_name" form:"express_name"  xml:"express_name"  `
Quantity         int64  `json:"quantity" form:"quantity"  xml:"quantity"  `
ConfirmSendProductRemark         int64  `json:"confirm_send_product_remark" form:"confirm_send_product_remark"  xml:"confirm_send_product_remark"  `
ExpressCode         int64  `json:"express_code" form:"express_code"  xml:"express_code"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
Amount         int64  `json:"amount" form:"amount"  xml:"amount"  `
}
type UpdateOrderInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CartId         int64  `json:"cart_id" form:"cart_id"  xml:"cart_id"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
Ptotal         int64  `json:"ptotal" form:"ptotal"  xml:"ptotal"  `
UpdateAmount         int64  `json:"update_amount" form:"update_amount"  xml:"update_amount"  `
ExpressCompanyName         int64  `json:"express_company_name" form:"express_company_name"  xml:"express_company_name"  `
PayType         int64  `json:"pay_type" form:"pay_type"  xml:"pay_type"  `
Rebate         int64  `json:"rebate" form:"rebate"  xml:"rebate"  `
Carry         int64  `json:"carry" form:"carry"  xml:"carry"  `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks"  `
RefundStatus         int64  `json:"refund_status" form:"refund_status"  xml:"refund_status"  `
Fee         int64  `json:"fee" form:"fee"  xml:"fee"  `
OtherRequirement         int64  `json:"other_requirement" form:"other_requirement"  xml:"other_requirement"  `
Paystatus         int64  `json:"paystatus" form:"paystatus"  xml:"paystatus"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
ClosedComment         int64  `json:"closed_comment" form:"closed_comment"  xml:"closed_comment"  `
ExpressNo         int64  `json:"express_no" form:"express_no"  xml:"express_no"  `
ExpressName         int64  `json:"express_name" form:"express_name"  xml:"express_name"  `
Quantity         int64  `json:"quantity" form:"quantity"  xml:"quantity"  `
ConfirmSendProductRemark         int64  `json:"confirm_send_product_remark" form:"confirm_send_product_remark"  xml:"confirm_send_product_remark"  `
ExpressCode         int64  `json:"express_code" form:"express_code"  xml:"express_code"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
Amount         int64  `json:"amount" form:"amount"  xml:"amount"  `
}
type QueryOrderInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CartId         int64  `json:"cart_id" form:"cart_id"  xml:"cart_id"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
Ptotal         int64  `json:"ptotal" form:"ptotal"  xml:"ptotal"  `
UpdateAmount         int64  `json:"update_amount" form:"update_amount"  xml:"update_amount"  `
ExpressCompanyName         int64  `json:"express_company_name" form:"express_company_name"  xml:"express_company_name"  `
PayType         int64  `json:"pay_type" form:"pay_type"  xml:"pay_type"  `
Rebate         int64  `json:"rebate" form:"rebate"  xml:"rebate"  `
Carry         int64  `json:"carry" form:"carry"  xml:"carry"  `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks"  `
RefundStatus         int64  `json:"refund_status" form:"refund_status"  xml:"refund_status"  `
Fee         int64  `json:"fee" form:"fee"  xml:"fee"  `
OtherRequirement         int64  `json:"other_requirement" form:"other_requirement"  xml:"other_requirement"  `
Paystatus         int64  `json:"paystatus" form:"paystatus"  xml:"paystatus"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
ClosedComment         int64  `json:"closed_comment" form:"closed_comment"  xml:"closed_comment"  `
ExpressNo         int64  `json:"express_no" form:"express_no"  xml:"express_no"  `
ExpressName         int64  `json:"express_name" form:"express_name"  xml:"express_name"  `
Quantity         int64  `json:"quantity" form:"quantity"  xml:"quantity"  `
ConfirmSendProductRemark         int64  `json:"confirm_send_product_remark" form:"confirm_send_product_remark"  xml:"confirm_send_product_remark"  `
ExpressCode         int64  `json:"express_code" form:"express_code"  xml:"express_code"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
Amount         int64  `json:"amount" form:"amount"  xml:"amount"  `
}
type QueryOrderOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CartId         int64  `json:"cart_id" form:"cart_id"  xml:"cart_id"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
Ptotal         int64  `json:"ptotal" form:"ptotal"  xml:"ptotal"  `
UpdateAmount         int64  `json:"update_amount" form:"update_amount"  xml:"update_amount"  `
ExpressCompanyName         int64  `json:"express_company_name" form:"express_company_name"  xml:"express_company_name"  `
PayType         int64  `json:"pay_type" form:"pay_type"  xml:"pay_type"  `
Rebate         int64  `json:"rebate" form:"rebate"  xml:"rebate"  `
Carry         int64  `json:"carry" form:"carry"  xml:"carry"  `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks"  `
RefundStatus         int64  `json:"refund_status" form:"refund_status"  xml:"refund_status"  `
Fee         int64  `json:"fee" form:"fee"  xml:"fee"  `
OtherRequirement         int64  `json:"other_requirement" form:"other_requirement"  xml:"other_requirement"  `
Paystatus         int64  `json:"paystatus" form:"paystatus"  xml:"paystatus"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
ClosedComment         int64  `json:"closed_comment" form:"closed_comment"  xml:"closed_comment"  `
ExpressNo         int64  `json:"express_no" form:"express_no"  xml:"express_no"  `
ExpressName         int64  `json:"express_name" form:"express_name"  xml:"express_name"  `
Quantity         int64  `json:"quantity" form:"quantity"  xml:"quantity"  `
ConfirmSendProductRemark         int64  `json:"confirm_send_product_remark" form:"confirm_send_product_remark"  xml:"confirm_send_product_remark"  `
ExpressCode         int64  `json:"express_code" form:"express_code"  xml:"express_code"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
BuyerId         int64  `json:"buyer_id" form:"buyer_id"  xml:"buyer_id"  `
Amount         int64  `json:"amount" form:"amount"  xml:"amount"  `
}
type CreateOrderDetailInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d"  `
Fee         int64  `json:"fee" form:"fee"  xml:"fee"  `
IsComment         int64  `json:"is_comment" form:"is_comment"  xml:"is_comment"  `
SellerID         int64  `json:"seller_i_d" form:"seller_i_d"  xml:"seller_i_d"  `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name"  `
GiftID         int64  `json:"gift_i_d" form:"gift_i_d"  xml:"gift_i_d"  `
OrderID         int64  `json:"order_i_d" form:"order_i_d"  xml:"order_i_d"  `
SpecInfo         int64  `json:"spec_info" form:"spec_info"  xml:"spec_info"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
Number         int64  `json:"number" form:"number"  xml:"number"  `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
Total         int64  `json:"total" form:"total"  xml:"total"  `
}
type UpdateOrderDetailInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d"  `
Fee         int64  `json:"fee" form:"fee"  xml:"fee"  `
IsComment         int64  `json:"is_comment" form:"is_comment"  xml:"is_comment"  `
SellerID         int64  `json:"seller_i_d" form:"seller_i_d"  xml:"seller_i_d"  `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name"  `
GiftID         int64  `json:"gift_i_d" form:"gift_i_d"  xml:"gift_i_d"  `
OrderID         int64  `json:"order_i_d" form:"order_i_d"  xml:"order_i_d"  `
SpecInfo         int64  `json:"spec_info" form:"spec_info"  xml:"spec_info"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
Number         int64  `json:"number" form:"number"  xml:"number"  `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
Total         int64  `json:"total" form:"total"  xml:"total"  `
}
type QueryOrderDetailInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d"  `
Fee         int64  `json:"fee" form:"fee"  xml:"fee"  `
IsComment         int64  `json:"is_comment" form:"is_comment"  xml:"is_comment"  `
SellerID         int64  `json:"seller_i_d" form:"seller_i_d"  xml:"seller_i_d"  `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name"  `
GiftID         int64  `json:"gift_i_d" form:"gift_i_d"  xml:"gift_i_d"  `
OrderID         int64  `json:"order_i_d" form:"order_i_d"  xml:"order_i_d"  `
SpecInfo         int64  `json:"spec_info" form:"spec_info"  xml:"spec_info"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
Number         int64  `json:"number" form:"number"  xml:"number"  `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
Total         int64  `json:"total" form:"total"  xml:"total"  `
}
type QueryOrderDetailOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ProductID         int64  `json:"product_i_d" form:"product_i_d"  xml:"product_i_d"  `
Fee         int64  `json:"fee" form:"fee"  xml:"fee"  `
IsComment         int64  `json:"is_comment" form:"is_comment"  xml:"is_comment"  `
SellerID         int64  `json:"seller_i_d" form:"seller_i_d"  xml:"seller_i_d"  `
ProductName         int64  `json:"product_name" form:"product_name"  xml:"product_name"  `
GiftID         int64  `json:"gift_i_d" form:"gift_i_d"  xml:"gift_i_d"  `
OrderID         int64  `json:"order_i_d" form:"order_i_d"  xml:"order_i_d"  `
SpecInfo         int64  `json:"spec_info" form:"spec_info"  xml:"spec_info"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
Number         int64  `json:"number" form:"number"  xml:"number"  `
LowStocks         int64  `json:"low_stocks" form:"low_stocks"  xml:"low_stocks"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
Total         int64  `json:"total" form:"total"  xml:"total"  `
}
type CreateOrderPayInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
ConfirmDate         int64  `json:"confirm_date" form:"confirm_date"  xml:"confirm_date"  `
TradeNo         int64  `json:"trade_no" form:"trade_no"  xml:"trade_no"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
PayMethod         int64  `json:"pay_method" form:"pay_method"  xml:"pay_method"  `
PayAmount         int64  `json:"pay_amount" form:"pay_amount"  xml:"pay_amount"  `
ConfirmUser         int64  `json:"confirm_user" form:"confirm_user"  xml:"confirm_user"  `
PayStatus         int64  `json:"pay_status" form:"pay_status"  xml:"pay_status"  `
}
type UpdateOrderPayInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
ConfirmDate         int64  `json:"confirm_date" form:"confirm_date"  xml:"confirm_date"  `
TradeNo         int64  `json:"trade_no" form:"trade_no"  xml:"trade_no"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
PayMethod         int64  `json:"pay_method" form:"pay_method"  xml:"pay_method"  `
PayAmount         int64  `json:"pay_amount" form:"pay_amount"  xml:"pay_amount"  `
ConfirmUser         int64  `json:"confirm_user" form:"confirm_user"  xml:"confirm_user"  `
PayStatus         int64  `json:"pay_status" form:"pay_status"  xml:"pay_status"  `
}
type QueryOrderPayInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
ConfirmDate         int64  `json:"confirm_date" form:"confirm_date"  xml:"confirm_date"  `
TradeNo         int64  `json:"trade_no" form:"trade_no"  xml:"trade_no"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
PayMethod         int64  `json:"pay_method" form:"pay_method"  xml:"pay_method"  `
PayAmount         int64  `json:"pay_amount" form:"pay_amount"  xml:"pay_amount"  `
ConfirmUser         int64  `json:"confirm_user" form:"confirm_user"  xml:"confirm_user"  `
PayStatus         int64  `json:"pay_status" form:"pay_status"  xml:"pay_status"  `
}
type QueryOrderPayOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
ConfirmDate         int64  `json:"confirm_date" form:"confirm_date"  xml:"confirm_date"  `
TradeNo         int64  `json:"trade_no" form:"trade_no"  xml:"trade_no"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
PayMethod         int64  `json:"pay_method" form:"pay_method"  xml:"pay_method"  `
PayAmount         int64  `json:"pay_amount" form:"pay_amount"  xml:"pay_amount"  `
ConfirmUser         int64  `json:"confirm_user" form:"confirm_user"  xml:"confirm_user"  `
PayStatus         int64  `json:"pay_status" form:"pay_status"  xml:"pay_status"  `
}
type CreateOrderLogInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
Content         int64  `json:"content" form:"content"  xml:"content"  `
AccountType         int64  `json:"account_type" form:"account_type"  xml:"account_type"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
}
type UpdateOrderLogInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
Content         int64  `json:"content" form:"content"  xml:"content"  `
AccountType         int64  `json:"account_type" form:"account_type"  xml:"account_type"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
}
type QueryOrderLogInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
Content         int64  `json:"content" form:"content"  xml:"content"  `
AccountType         int64  `json:"account_type" form:"account_type"  xml:"account_type"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
}
type QueryOrderLogOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
Content         int64  `json:"content" form:"content"  xml:"content"  `
AccountType         int64  `json:"account_type" form:"account_type"  xml:"account_type"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
}
type CreateOrderShipInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Tel         int64  `json:"tel" form:"tel"  xml:"tel"  `
CityCode         int64  `json:"city_code" form:"city_code"  xml:"city_code"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
AreaCode         int64  `json:"area_code" form:"area_code"  xml:"area_code"  `
Zip         int64  `json:"zip" form:"zip"  xml:"zip"  `
Shipname         int64  `json:"shipname" form:"shipname"  xml:"shipname"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
ProvinceCode         int64  `json:"province_code" form:"province_code"  xml:"province_code"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Shipaddress         int64  `json:"shipaddress" form:"shipaddress"  xml:"shipaddress"  `
Area         int64  `json:"area" form:"area"  xml:"area"  `
}
type UpdateOrderShipInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Tel         int64  `json:"tel" form:"tel"  xml:"tel"  `
CityCode         int64  `json:"city_code" form:"city_code"  xml:"city_code"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
AreaCode         int64  `json:"area_code" form:"area_code"  xml:"area_code"  `
Zip         int64  `json:"zip" form:"zip"  xml:"zip"  `
Shipname         int64  `json:"shipname" form:"shipname"  xml:"shipname"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
ProvinceCode         int64  `json:"province_code" form:"province_code"  xml:"province_code"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Shipaddress         int64  `json:"shipaddress" form:"shipaddress"  xml:"shipaddress"  `
Area         int64  `json:"area" form:"area"  xml:"area"  `
}
type QueryOrderShipInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Tel         int64  `json:"tel" form:"tel"  xml:"tel"  `
CityCode         int64  `json:"city_code" form:"city_code"  xml:"city_code"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
AreaCode         int64  `json:"area_code" form:"area_code"  xml:"area_code"  `
Zip         int64  `json:"zip" form:"zip"  xml:"zip"  `
Shipname         int64  `json:"shipname" form:"shipname"  xml:"shipname"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
ProvinceCode         int64  `json:"province_code" form:"province_code"  xml:"province_code"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Shipaddress         int64  `json:"shipaddress" form:"shipaddress"  xml:"shipaddress"  `
Area         int64  `json:"area" form:"area"  xml:"area"  `
}
type QueryOrderShipOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Tel         int64  `json:"tel" form:"tel"  xml:"tel"  `
CityCode         int64  `json:"city_code" form:"city_code"  xml:"city_code"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
AreaCode         int64  `json:"area_code" form:"area_code"  xml:"area_code"  `
Zip         int64  `json:"zip" form:"zip"  xml:"zip"  `
Shipname         int64  `json:"shipname" form:"shipname"  xml:"shipname"  `
OrderId         int64  `json:"order_id" form:"order_id"  xml:"order_id"  `
ProvinceCode         int64  `json:"province_code" form:"province_code"  xml:"province_code"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Shipaddress         int64  `json:"shipaddress" form:"shipaddress"  xml:"shipaddress"  `
Area         int64  `json:"area" form:"area"  xml:"area"  `
}
type CreateProductInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CatagoryID         int64  `json:"catagory_i_d" form:"catagory_i_d"  xml:"catagory_i_d"  `
SellCount         int64  `json:"sell_count" form:"sell_count"  xml:"sell_count"  `
Stock         int64  `json:"stock" form:"stock"  xml:"stock"  `
Keywords         int64  `json:"keywords" form:"keywords"  xml:"keywords"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
CreateAccount         int64  `json:"create_account" form:"create_account"  xml:"create_account"  `
Hit         int64  `json:"hit" form:"hit"  xml:"hit"  `
Title         int64  `json:"title" form:"title"  xml:"title"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account"  `
ActivityID         int64  `json:"activity_i_d" form:"activity_i_d"  xml:"activity_i_d"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
IsTimePromotion         int64  `json:"is_time_promotion" form:"is_time_promotion"  xml:"is_time_promotion"  `
ProductHTML         int64  `json:"product_h_t_m_l" form:"product_h_t_m_l"  xml:"product_h_t_m_l"  `
Isnew         int64  `json:"isnew" form:"isnew"  xml:"isnew"  `
Introduce         int64  `json:"introduce" form:"introduce"  xml:"introduce"  `
SearchKey         int64  `json:"search_key" form:"search_key"  xml:"search_key"  `
Images         int64  `json:"images" form:"images"  xml:"images"  `
NowPrice         int64  `json:"now_price" form:"now_price"  xml:"now_price"  `
MaxPicture         int64  `json:"max_picture" form:"max_picture"  xml:"max_picture"  `
Description         int64  `json:"description" form:"description"  xml:"description"  `
Unit         int64  `json:"unit" form:"unit"  xml:"unit"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Sale         int64  `json:"sale" form:"sale"  xml:"sale"  `
}
type UpdateProductInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CatagoryID         int64  `json:"catagory_i_d" form:"catagory_i_d"  xml:"catagory_i_d"  `
SellCount         int64  `json:"sell_count" form:"sell_count"  xml:"sell_count"  `
Stock         int64  `json:"stock" form:"stock"  xml:"stock"  `
Keywords         int64  `json:"keywords" form:"keywords"  xml:"keywords"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
CreateAccount         int64  `json:"create_account" form:"create_account"  xml:"create_account"  `
Hit         int64  `json:"hit" form:"hit"  xml:"hit"  `
Title         int64  `json:"title" form:"title"  xml:"title"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account"  `
ActivityID         int64  `json:"activity_i_d" form:"activity_i_d"  xml:"activity_i_d"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
IsTimePromotion         int64  `json:"is_time_promotion" form:"is_time_promotion"  xml:"is_time_promotion"  `
ProductHTML         int64  `json:"product_h_t_m_l" form:"product_h_t_m_l"  xml:"product_h_t_m_l"  `
Isnew         int64  `json:"isnew" form:"isnew"  xml:"isnew"  `
Introduce         int64  `json:"introduce" form:"introduce"  xml:"introduce"  `
SearchKey         int64  `json:"search_key" form:"search_key"  xml:"search_key"  `
Images         int64  `json:"images" form:"images"  xml:"images"  `
NowPrice         int64  `json:"now_price" form:"now_price"  xml:"now_price"  `
MaxPicture         int64  `json:"max_picture" form:"max_picture"  xml:"max_picture"  `
Description         int64  `json:"description" form:"description"  xml:"description"  `
Unit         int64  `json:"unit" form:"unit"  xml:"unit"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Sale         int64  `json:"sale" form:"sale"  xml:"sale"  `
}
type QueryProductInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CatagoryID         int64  `json:"catagory_i_d" form:"catagory_i_d"  xml:"catagory_i_d"  `
SellCount         int64  `json:"sell_count" form:"sell_count"  xml:"sell_count"  `
Stock         int64  `json:"stock" form:"stock"  xml:"stock"  `
Keywords         int64  `json:"keywords" form:"keywords"  xml:"keywords"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
CreateAccount         int64  `json:"create_account" form:"create_account"  xml:"create_account"  `
Hit         int64  `json:"hit" form:"hit"  xml:"hit"  `
Title         int64  `json:"title" form:"title"  xml:"title"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account"  `
ActivityID         int64  `json:"activity_i_d" form:"activity_i_d"  xml:"activity_i_d"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
IsTimePromotion         int64  `json:"is_time_promotion" form:"is_time_promotion"  xml:"is_time_promotion"  `
ProductHTML         int64  `json:"product_h_t_m_l" form:"product_h_t_m_l"  xml:"product_h_t_m_l"  `
Isnew         int64  `json:"isnew" form:"isnew"  xml:"isnew"  `
Introduce         int64  `json:"introduce" form:"introduce"  xml:"introduce"  `
SearchKey         int64  `json:"search_key" form:"search_key"  xml:"search_key"  `
Images         int64  `json:"images" form:"images"  xml:"images"  `
NowPrice         int64  `json:"now_price" form:"now_price"  xml:"now_price"  `
MaxPicture         int64  `json:"max_picture" form:"max_picture"  xml:"max_picture"  `
Description         int64  `json:"description" form:"description"  xml:"description"  `
Unit         int64  `json:"unit" form:"unit"  xml:"unit"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Sale         int64  `json:"sale" form:"sale"  xml:"sale"  `
}
type QueryProductOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
CatagoryID         int64  `json:"catagory_i_d" form:"catagory_i_d"  xml:"catagory_i_d"  `
SellCount         int64  `json:"sell_count" form:"sell_count"  xml:"sell_count"  `
Stock         int64  `json:"stock" form:"stock"  xml:"stock"  `
Keywords         int64  `json:"keywords" form:"keywords"  xml:"keywords"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
CreateAccount         int64  `json:"create_account" form:"create_account"  xml:"create_account"  `
Hit         int64  `json:"hit" form:"hit"  xml:"hit"  `
Title         int64  `json:"title" form:"title"  xml:"title"  `
Price         int64  `json:"price" form:"price"  xml:"price"  `
UpdateAccount         int64  `json:"update_account" form:"update_account"  xml:"update_account"  `
ActivityID         int64  `json:"activity_i_d" form:"activity_i_d"  xml:"activity_i_d"  `
Status         int64  `json:"status" form:"status"  xml:"status"  `
IsTimePromotion         int64  `json:"is_time_promotion" form:"is_time_promotion"  xml:"is_time_promotion"  `
ProductHTML         int64  `json:"product_h_t_m_l" form:"product_h_t_m_l"  xml:"product_h_t_m_l"  `
Isnew         int64  `json:"isnew" form:"isnew"  xml:"isnew"  `
Introduce         int64  `json:"introduce" form:"introduce"  xml:"introduce"  `
SearchKey         int64  `json:"search_key" form:"search_key"  xml:"search_key"  `
Images         int64  `json:"images" form:"images"  xml:"images"  `
NowPrice         int64  `json:"now_price" form:"now_price"  xml:"now_price"  `
MaxPicture         int64  `json:"max_picture" form:"max_picture"  xml:"max_picture"  `
Description         int64  `json:"description" form:"description"  xml:"description"  `
Unit         int64  `json:"unit" form:"unit"  xml:"unit"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Sale         int64  `json:"sale" form:"sale"  xml:"sale"  `
}
type CreateProductCatagoryInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Code         int64  `json:"code" form:"code"  xml:"code"  `
Href         int64  `json:"href" form:"href"  xml:"href"  `
Orders         int64  `json:"orders" form:"orders"  xml:"orders"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id"  `
Parent         int64  `json:"parent" form:"parent"  xml:"parent"  `
Children         int64  `json:"children" form:"children"  xml:"children"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type UpdateProductCatagoryInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Code         int64  `json:"code" form:"code"  xml:"code"  `
Href         int64  `json:"href" form:"href"  xml:"href"  `
Orders         int64  `json:"orders" form:"orders"  xml:"orders"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id"  `
Parent         int64  `json:"parent" form:"parent"  xml:"parent"  `
Children         int64  `json:"children" form:"children"  xml:"children"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryProductCatagoryInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Code         int64  `json:"code" form:"code"  xml:"code"  `
Href         int64  `json:"href" form:"href"  xml:"href"  `
Orders         int64  `json:"orders" form:"orders"  xml:"orders"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id"  `
Parent         int64  `json:"parent" form:"parent"  xml:"parent"  `
Children         int64  `json:"children" form:"children"  xml:"children"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryProductCatagoryOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Code         int64  `json:"code" form:"code"  xml:"code"  `
Href         int64  `json:"href" form:"href"  xml:"href"  `
Orders         int64  `json:"orders" form:"orders"  xml:"orders"  `
Picture         int64  `json:"picture" form:"picture"  xml:"picture"  `
parent_id         int64  `json:"parent_id" form:"parent_id"  xml:"parent_id"  `
Parent         int64  `json:"parent" form:"parent"  xml:"parent"  `
Children         int64  `json:"children" form:"children"  xml:"children"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type CreateStudentInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
StudentId         int64  `json:"student_id" form:"student_id"  xml:"student_id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
class_id         int64  `json:"class_id" form:"class_id"  xml:"class_id"  `
Class         int64  `json:"class" form:"class"  xml:"class"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type UpdateStudentInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
StudentId         int64  `json:"student_id" form:"student_id"  xml:"student_id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
class_id         int64  `json:"class_id" form:"class_id"  xml:"class_id"  `
Class         int64  `json:"class" form:"class"  xml:"class"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryStudentInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
StudentId         int64  `json:"student_id" form:"student_id"  xml:"student_id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
class_id         int64  `json:"class_id" form:"class_id"  xml:"class_id"  `
Class         int64  `json:"class" form:"class"  xml:"class"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryStudentOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
StudentId         int64  `json:"student_id" form:"student_id"  xml:"student_id"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
class_id         int64  `json:"class_id" form:"class_id"  xml:"class_id"  `
Class         int64  `json:"class" form:"class"  xml:"class"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type CreateBuyerInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Realname         int64  `json:"realname" form:"realname"  xml:"realname"  `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl"  `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id"  `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq"  `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id"  `
Language         int64  `json:"language" form:"language"  xml:"language"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe"  `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time"  `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip"  `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token"  `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in"  `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date"  `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Email         int64  `json:"email" form:"email"  xml:"email"  `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
Bir         int64  `json:"bir" form:"bir"  xml:"bir"  `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode"  `
Balance         int64  `json:"balance" form:"balance"  xml:"balance"  `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip"  `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip"  `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc"  `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag"  `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app"  `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag"  `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program"  `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower"  `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date"  `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type UpdateBuyerInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Realname         int64  `json:"realname" form:"realname"  xml:"realname"  `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl"  `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id"  `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq"  `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id"  `
Language         int64  `json:"language" form:"language"  xml:"language"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe"  `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time"  `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip"  `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token"  `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in"  `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date"  `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Email         int64  `json:"email" form:"email"  xml:"email"  `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
Bir         int64  `json:"bir" form:"bir"  xml:"bir"  `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode"  `
Balance         int64  `json:"balance" form:"balance"  xml:"balance"  `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip"  `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip"  `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc"  `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag"  `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app"  `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag"  `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program"  `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower"  `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date"  `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryBuyerInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Realname         int64  `json:"realname" form:"realname"  xml:"realname"  `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl"  `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id"  `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq"  `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id"  `
Language         int64  `json:"language" form:"language"  xml:"language"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe"  `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time"  `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip"  `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token"  `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in"  `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date"  `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Email         int64  `json:"email" form:"email"  xml:"email"  `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
Bir         int64  `json:"bir" form:"bir"  xml:"bir"  `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode"  `
Balance         int64  `json:"balance" form:"balance"  xml:"balance"  `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip"  `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip"  `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc"  `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag"  `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app"  `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag"  `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program"  `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower"  `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date"  `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryBuyerOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
Realname         int64  `json:"realname" form:"realname"  xml:"realname"  `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl"  `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id"  `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq"  `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id"  `
Language         int64  `json:"language" form:"language"  xml:"language"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe"  `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time"  `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip"  `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token"  `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in"  `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date"  `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Email         int64  `json:"email" form:"email"  xml:"email"  `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
Bir         int64  `json:"bir" form:"bir"  xml:"bir"  `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode"  `
Balance         int64  `json:"balance" form:"balance"  xml:"balance"  `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip"  `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip"  `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc"  `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag"  `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app"  `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag"  `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program"  `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower"  `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date"  `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type CreateSellerInput struct {
UserLevel         int64  `json:"user_level" form:"user_level"  xml:"user_level"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
VersionNo         int64  `json:"version_no" form:"version_no"  xml:"version_no"  `
LoginFailureCount         int64  `json:"login_failure_count" form:"login_failure_count"  xml:"login_failure_count"  `
Id         int64  `json:"id" form:"id"  xml:"id"  `
Realname         int64  `json:"realname" form:"realname"  xml:"realname"  `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl"  `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id"  `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq"  `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id"  `
Language         int64  `json:"language" form:"language"  xml:"language"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe"  `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time"  `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip"  `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token"  `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in"  `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date"  `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Email         int64  `json:"email" form:"email"  xml:"email"  `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
Bir         int64  `json:"bir" form:"bir"  xml:"bir"  `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode"  `
Balance         int64  `json:"balance" form:"balance"  xml:"balance"  `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip"  `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip"  `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc"  `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag"  `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app"  `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag"  `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program"  `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower"  `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date"  `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type UpdateSellerInput struct {
UserLevel         int64  `json:"user_level" form:"user_level"  xml:"user_level"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
VersionNo         int64  `json:"version_no" form:"version_no"  xml:"version_no"  `
LoginFailureCount         int64  `json:"login_failure_count" form:"login_failure_count"  xml:"login_failure_count"  `
Id         int64  `json:"id" form:"id"  xml:"id"  `
Realname         int64  `json:"realname" form:"realname"  xml:"realname"  `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl"  `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id"  `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq"  `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id"  `
Language         int64  `json:"language" form:"language"  xml:"language"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe"  `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time"  `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip"  `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token"  `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in"  `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date"  `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Email         int64  `json:"email" form:"email"  xml:"email"  `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
Bir         int64  `json:"bir" form:"bir"  xml:"bir"  `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode"  `
Balance         int64  `json:"balance" form:"balance"  xml:"balance"  `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip"  `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip"  `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc"  `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag"  `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app"  `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag"  `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program"  `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower"  `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date"  `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QuerySellerInput struct {
UserLevel         int64  `json:"user_level" form:"user_level"  xml:"user_level"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
VersionNo         int64  `json:"version_no" form:"version_no"  xml:"version_no"  `
LoginFailureCount         int64  `json:"login_failure_count" form:"login_failure_count"  xml:"login_failure_count"  `
Id         int64  `json:"id" form:"id"  xml:"id"  `
Realname         int64  `json:"realname" form:"realname"  xml:"realname"  `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl"  `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id"  `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq"  `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id"  `
Language         int64  `json:"language" form:"language"  xml:"language"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe"  `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time"  `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip"  `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token"  `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in"  `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date"  `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Email         int64  `json:"email" form:"email"  xml:"email"  `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
Bir         int64  `json:"bir" form:"bir"  xml:"bir"  `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode"  `
Balance         int64  `json:"balance" form:"balance"  xml:"balance"  `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip"  `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip"  `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc"  `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag"  `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app"  `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag"  `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program"  `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower"  `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date"  `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QuerySellerOutput struct {
UserLevel         int64  `json:"user_level" form:"user_level"  xml:"user_level"  `
StartDate         int64  `json:"start_date" form:"start_date"  xml:"start_date"  `
EndDate         int64  `json:"end_date" form:"end_date"  xml:"end_date"  `
VersionNo         int64  `json:"version_no" form:"version_no"  xml:"version_no"  `
LoginFailureCount         int64  `json:"login_failure_count" form:"login_failure_count"  xml:"login_failure_count"  `
Id         int64  `json:"id" form:"id"  xml:"id"  `
Realname         int64  `json:"realname" form:"realname"  xml:"realname"  `
Headimgurl         int64  `json:"headimgurl" form:"headimgurl"  xml:"headimgurl"  `
OpenId         int64  `json:"open_id" form:"open_id"  xml:"open_id"  `
ImQq         int64  `json:"im_qq" form:"im_qq"  xml:"im_qq"  `
AuthAppId         int64  `json:"auth_app_id" form:"auth_app_id"  xml:"auth_app_id"  `
Language         int64  `json:"language" form:"language"  xml:"language"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Subscribe         int64  `json:"subscribe" form:"subscribe"  xml:"subscribe"  `
SubscribeTime         int64  `json:"subscribe_time" form:"subscribe_time"  xml:"subscribe_time"  `
Groupid         int64  `json:"groupid" form:"groupid"  xml:"groupid"  `
Remark         int64  `json:"remark" form:"remark"  xml:"remark"  `
AccessIp         int64  `json:"access_ip" form:"access_ip"  xml:"access_ip"  `
RefreshToken         int64  `json:"refresh_token" form:"refresh_token"  xml:"refresh_token"  `
TokenExpiresIn         int64  `json:"token_expires_in" form:"token_expires_in"  xml:"token_expires_in"  `
LastLoginDate         int64  `json:"last_login_date" form:"last_login_date"  xml:"last_login_date"  `
Unionid         int64  `json:"unionid" form:"unionid"  xml:"unionid"  `
Score         int64  `json:"score" form:"score"  xml:"score"  `
IsReceiver         int64  `json:"is_receiver" form:"is_receiver"  xml:"is_receiver"  `
Account         int64  `json:"account" form:"account"  xml:"account"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
Email         int64  `json:"email" form:"email"  xml:"email"  `
Pwd         int64  `json:"pwd" form:"pwd"  xml:"pwd"  `
Name         int64  `json:"name" form:"name"  xml:"name"  `
Age         int64  `json:"age" form:"age"  xml:"age"  `
Sex         int64  `json:"sex" form:"sex"  xml:"sex"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
Bir         int64  `json:"bir" form:"bir"  xml:"bir"  `
Postcode         int64  `json:"postcode" form:"postcode"  xml:"postcode"  `
Balance         int64  `json:"balance" form:"balance"  xml:"balance"  `
LoginIp         int64  `json:"login_ip" form:"login_ip"  xml:"login_ip"  `
RegisterIp         int64  `json:"register_ip" form:"register_ip"  xml:"register_ip"  `
IsLoginPc         int64  `json:"is_login_pc" form:"is_login_pc"  xml:"is_login_pc"  `
PcFlag         int64  `json:"pc_flag" form:"pc_flag"  xml:"pc_flag"  `
IsLoginApp         int64  `json:"is_login_app" form:"is_login_app"  xml:"is_login_app"  `
AppFlag         int64  `json:"app_flag" form:"app_flag"  xml:"app_flag"  `
IsLoginWxProgram         int64  `json:"is_login_wx_program" form:"is_login_wx_program"  xml:"is_login_wx_program"  `
IsLoginBrower         int64  `json:"is_login_brower" form:"is_login_brower"  xml:"is_login_brower"  `
RegDate         int64  `json:"reg_date" form:"reg_date"  xml:"reg_date"  `
LoginDate         int64  `json:"login_date" form:"login_date"  xml:"login_date"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type CreateBuyerAddressInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Memo         int64  `json:"memo" form:"memo"  xml:"memo"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type UpdateBuyerAddressInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Memo         int64  `json:"memo" form:"memo"  xml:"memo"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryBuyerAddressInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Memo         int64  `json:"memo" form:"memo"  xml:"memo"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QueryBuyerAddressOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Memo         int64  `json:"memo" form:"memo"  xml:"memo"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type CreateSellerAddressInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Memo         int64  `json:"memo" form:"memo"  xml:"memo"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type UpdateSellerAddressInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Memo         int64  `json:"memo" form:"memo"  xml:"memo"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QuerySellerAddressInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Memo         int64  `json:"memo" form:"memo"  xml:"memo"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type QuerySellerAddressOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
ContactName         int64  `json:"contact_name" form:"contact_name"  xml:"contact_name"  `
Address         int64  `json:"address" form:"address"  xml:"address"  `
City         int64  `json:"city" form:"city"  xml:"city"  `
Province         int64  `json:"province" form:"province"  xml:"province"  `
Country         int64  `json:"country" form:"country"  xml:"country"  `
Memo         int64  `json:"memo" form:"memo"  xml:"memo"  `
Phone         int64  `json:"phone" form:"phone"  xml:"phone"  `
PostCode         int64  `json:"post_code" form:"post_code"  xml:"post_code"  `
IsDefault         int64  `json:"is_default" form:"is_default"  xml:"is_default"  `
UserId         int64  `json:"user_id" form:"user_id"  xml:"user_id"  `
CreationTime         int64  `json:"creation_time" form:"creation_time"  xml:"creation_time"  `
LastModificationTime         int64  `json:"last_modification_time" form:"last_modification_time"  xml:"last_modification_time"  `
DeletionTime         int64  `json:"deletion_time" form:"deletion_time"  xml:"deletion_time"  `
IsDeleted         int64  `json:"is_deleted" form:"is_deleted"  xml:"is_deleted"  `
}
type CreateSmsInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
AppId         int64  `json:"app_id" form:"app_id"  xml:"app_id"  `
Secrt         int64  `json:"secrt" form:"secrt"  xml:"secrt"  `
}
type UpdateSmsInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
AppId         int64  `json:"app_id" form:"app_id"  xml:"app_id"  `
Secrt         int64  `json:"secrt" form:"secrt"  xml:"secrt"  `
}
type QuerySmsInput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
AppId         int64  `json:"app_id" form:"app_id"  xml:"app_id"  `
Secrt         int64  `json:"secrt" form:"secrt"  xml:"secrt"  `
}
type QuerySmsOutput struct {
Id         int64  `json:"id" form:"id"  xml:"id"  `
AppId         int64  `json:"app_id" form:"app_id"  xml:"app_id"  `
Secrt         int64  `json:"secrt" form:"secrt"  xml:"secrt"  `
}
