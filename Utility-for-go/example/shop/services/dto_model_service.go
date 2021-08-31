package services

import (
	"news/shop/models"
)

//包不能互相嵌套
//这个不支持 泛型 怎么解决了 只支持接口 得写接口转换 接口 不支持属性 ？
func CopyUserAddressOutput(user *models.BaseUserAddr) *UserAddressOutput {
	userAddressOutput := &UserAddressOutput{}
	userAddressOutput.AreaId = user.AreaId
	userAddressOutput.ContactName = user.ContactName
	userAddressOutput.Addr = user.Addr
	userAddressOutput.City = user.City
	userAddressOutput.Province = user.Province
	userAddressOutput.Country = user.Country
	userAddressOutput.Memo = user.Memo
	userAddressOutput.Phone = user.Phone
	userAddressOutput.PostCode = user.PostCode
	userAddressOutput.IsDefault = user.IsDefault
	return userAddressOutput
}

func CopyUserAddressOutputByBuyer(user *models.BuyerAddr) *UserAddressOutput {
	userAddressOutput := &UserAddressOutput{}
	userAddressOutput.AreaId = user.AreaId
	userAddressOutput.ContactName = user.ContactName
	userAddressOutput.Addr = user.Addr
	userAddressOutput.City = user.City
	userAddressOutput.Province = user.Province
	userAddressOutput.Country = user.Country
	userAddressOutput.Memo = user.Memo
	userAddressOutput.Phone = user.Phone
	userAddressOutput.PostCode = user.PostCode
	userAddressOutput.IsDefault = user.IsDefault
	return userAddressOutput
}

func CopyUserAddressOutputBySeller(user *models.SellerAddr) *UserAddressOutput {
	userAddressOutput := &UserAddressOutput{}
	userAddressOutput.AreaId = user.AreaId
	userAddressOutput.ContactName = user.ContactName
	userAddressOutput.Addr = user.Addr
	userAddressOutput.City = user.City
	userAddressOutput.Province = user.Province
	userAddressOutput.Country = user.Country
	userAddressOutput.Memo = user.Memo
	userAddressOutput.Phone = user.Phone
	userAddressOutput.PostCode = user.PostCode
	userAddressOutput.IsDefault = user.IsDefault
	return userAddressOutput
}

func CopyUserAddressOutputByAgent(user *models.AgentAddr) *UserAddressOutput {
	userAddressOutput := &UserAddressOutput{}
	userAddressOutput.AreaId = user.AreaId
	userAddressOutput.ContactName = user.ContactName
	userAddressOutput.Addr = user.Addr
	userAddressOutput.City = user.City
	userAddressOutput.Province = user.Province
	userAddressOutput.Country = user.Country
	userAddressOutput.Memo = user.Memo
	userAddressOutput.Phone = user.Phone
	userAddressOutput.PostCode = user.PostCode
	userAddressOutput.IsDefault = user.IsDefault
	return userAddressOutput
}

func CopyUserAddressOutputByManufacturer(user *models.ManufacturerAddr) *UserAddressOutput {
	userAddressOutput := &UserAddressOutput{}
	userAddressOutput.AreaId = user.AreaId
	userAddressOutput.ContactName = user.ContactName
	userAddressOutput.Addr = user.Addr
	userAddressOutput.City = user.City
	userAddressOutput.Province = user.Province
	userAddressOutput.Country = user.Country
	userAddressOutput.Memo = user.Memo
	userAddressOutput.Phone = user.Phone
	userAddressOutput.PostCode = user.PostCode
	userAddressOutput.IsDefault = user.IsDefault
	return userAddressOutput
}

func CopyBuyerAddr(user *UserAddressOutput) *models.BuyerAddr {
	userAddressOutput := &models.BuyerAddr{}
	userAddressOutput.AreaId = user.AreaId
	userAddressOutput.ContactName = user.ContactName
	userAddressOutput.Addr = user.Addr
	userAddressOutput.City = user.City
	userAddressOutput.Province = user.Province
	userAddressOutput.Country = user.Country
	userAddressOutput.Memo = user.Memo
	userAddressOutput.Phone = user.Phone
	userAddressOutput.PostCode = user.PostCode
	userAddressOutput.IsDefault = user.IsDefault
	return userAddressOutput
}

func CopySellerAddr(user *UserAddressOutput) *models.SellerAddr {
	userAddressOutput := &models.SellerAddr{}
	userAddressOutput.AreaId = user.AreaId
	userAddressOutput.ContactName = user.ContactName
	userAddressOutput.Addr = user.Addr
	userAddressOutput.City = user.City
	userAddressOutput.Province = user.Province
	userAddressOutput.Country = user.Country
	userAddressOutput.Memo = user.Memo
	userAddressOutput.Phone = user.Phone
	userAddressOutput.PostCode = user.PostCode
	userAddressOutput.IsDefault = user.IsDefault
	return userAddressOutput
}

func CopyAgentAddr(user *UserAddressOutput) *models.AgentAddr {
	userAddressOutput := &models.AgentAddr{}
	userAddressOutput.AreaId = user.AreaId
	userAddressOutput.ContactName = user.ContactName
	userAddressOutput.Addr = user.Addr
	userAddressOutput.City = user.City
	userAddressOutput.Province = user.Province
	userAddressOutput.Country = user.Country
	userAddressOutput.Memo = user.Memo
	userAddressOutput.Phone = user.Phone
	userAddressOutput.PostCode = user.PostCode
	userAddressOutput.IsDefault = user.IsDefault
	return userAddressOutput
}

func CopyManufacturerAddr(user *UserAddressOutput) *models.ManufacturerAddr {
	userAddressOutput := &models.ManufacturerAddr{}
	userAddressOutput.AreaId = user.AreaId
	userAddressOutput.ContactName = user.ContactName
	userAddressOutput.Addr = user.Addr
	userAddressOutput.City = user.City
	userAddressOutput.Province = user.Province
	userAddressOutput.Country = user.Country
	userAddressOutput.Memo = user.Memo
	userAddressOutput.Phone = user.Phone
	userAddressOutput.PostCode = user.PostCode
	userAddressOutput.IsDefault = user.IsDefault
	return userAddressOutput
}
