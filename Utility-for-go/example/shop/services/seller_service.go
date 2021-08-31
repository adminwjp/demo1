package services

import (
	"news/shop/models"
)

func AddSeller(seller models.Seller) bool {
	seller.Id = Id.GetIdByStruct(seller)
	res, _ := add(seller)
	return res
}

func SellerList(seller models.Seller, page int, size int) ([]*models.Seller, int64) {
	var sellers []*models.Seller
	data, count, _ := selectList(seller, page, size, sellers)
	return data.([]*models.Seller), count
}

func AddSellerAddr(sellerAddr models.SellerAddr) bool {
	sellerAddr.Id = Id.GetIdByStruct(sellerAddr)
	res, _ := add(sellerAddr)
	return res
}
