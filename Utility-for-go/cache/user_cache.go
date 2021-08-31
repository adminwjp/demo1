package cache

import "user/models"

var BuyerCaches map[int64]models.Buyer= make(map[int64]models.Buyer,10000*10)
var SellerCaches map[int64]models.Seller= make(map[int64]models.Seller,10000*10)
var BuyerAddrCaches map[int64][]models.BuyerAddr= make(map[int64][]models.BuyerAddr,10000*10)
var SellerAddrCaches map[int64][]models.SellerAddr= make(map[int64][]models.SellerAddr,10000*10)

var BuyerToken map[int64]string= make(map[int64]string,10000*10)
var SellerToken map[int64]string= make(map[int64]string,10000*10)

func Register(account string,pwd string)  {

}

