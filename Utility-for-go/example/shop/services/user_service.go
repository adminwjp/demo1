package services

import (
	"news/shop/models"

	"github.com/jinzhu/gorm"
)

type RegisterResult int32

const (
	RegisterSuccess  RegisterResult = 0
	RegisterFail     RegisterResult = 1
	AccountExists    RegisterResult = 2
	AccountNotExists RegisterResult = 3
)

type UserType int

const (
	BuyerType        UserType = 0
	SellerType       UserType = 1
	AgentType        UserType = 2
	ManufacturerType UserType = 3
)

func RegUser(model models.BaseModel, user CreateUserInput) RegisterResult {
	registerType := Phone
	if user.Type == 1 {
		registerType = Email
	}
	//判断是否存在账号
	extsis, _ := BaseExistsAccount(user.Account, model.TableName(), registerType)
	if extsis {
		return AccountExists
	}
	//注册
	id := Id.GetIdByStruct(model)
	registerResult := BaseRegister(id, user.Account, user.Pwd, registerType, model.TableName())
	if registerResult {
		return RegisterSuccess
	} else {
		return RegisterFail
	}
}

func UserLogin(model models.BaseModel, user CreateUserInput) RegisterResult {
	registerType := Phone
	if user.Type == 1 {
		registerType = Email
	}
	//判断是否存在账号
	extsis, _ := BaseExistsAccount(user.Account, model.TableName(), registerType)
	if extsis {
		return AccountExists
	}
	//登录 返回 用户信息
	return RegisterFail
}

func ExistsAccount(model models.BaseModel, user CreateUserInput) RegisterResult {
	registerType := Phone
	if user.Type == 1 {
		registerType = Email
	}
	//判断是否存在账号
	extsis, _ := BaseExistsAccount(user.Account, model.TableName(), registerType)
	if extsis {
		return AccountExists
	}
	return AccountNotExists
}

func UpdateEmail(model models.BaseModel, user UpdateUserEmailInput) bool {
	registerType := Email
	return BaseUpdateAccountByAccount(user.Email, user.NewEmail, registerType, model.TableName())

}

func UpdatePhone(model models.BaseModel, user UpdateUserPhoneInput) bool {
	registerType := Phone
	return BaseUpdateAccountByAccount(user.Phone, user.NewPhone, registerType, model.TableName())
}

func UpdatePwdByEmail(model models.BaseModel, user UpdateUserPwdByEmailInput) bool {
	registerType := Email
	return BaseUpdatePwdByAccount(user.Email, user.Pwd, registerType, model.TableName())
}

func UpdatePwdByPhone(model models.BaseModel, user UpdateUserPwdByPhoneInput) bool {
	registerType := Phone
	return BaseUpdatePwdByAccount(user.Phone, user.Pwd, registerType, model.TableName())
}

func BuyerAddrList(page int, size int) ([]*UserAddressOutput, int64) {
	var data []*models.BuyerAddr
	buyerAddr := models.BuyerAddr{}
	count, _ := getList(buyerAddr, page, size,
		func(db *gorm.DB) *gorm.DB {
			db = db.Order("is_default desc")
			return db
		}, func(db *gorm.DB) interface{} {
			db = db.Scan(&data)
			return data
		}, false)
	var result []*UserAddressOutput
	if data != nil && len(data) > 0 {
		result = make([]*UserAddressOutput, len(data))
		for i := 0; i < len(data); i++ {
			result[i] = CopyUserAddressOutputByBuyer(data[i])
			result[i].UserId = data[i].BuyerId
		}
	}
	return result, count
}

func SellerAddrList(page int, size int) ([]*UserAddressOutput, int64) {
	var data []*models.SellerAddr
	sellerAddr := models.SellerAddr{}
	count, _ := getList(sellerAddr, page, size,
		func(db *gorm.DB) *gorm.DB {
			db = db.Order("is_default desc")
			return db
		}, func(db *gorm.DB) interface{} {
			db = db.Scan(&data)
			return data
		}, false)
	var result []*UserAddressOutput
	if data != nil && len(data) > 0 {
		result = make([]*UserAddressOutput, len(data))
		for i := 0; i < len(data); i++ {
			result[i] = CopyUserAddressOutputBySeller(data[i])
			result[i].UserId = data[i].SellerId
		}
	}
	return result, count
}

func AgentAddrList(page int, size int) ([]*UserAddressOutput, int64) {
	var data []*models.AgentAddr
	agentAddr := models.AgentAddr{}
	count, _ := getList(agentAddr, page, size,
		func(db *gorm.DB) *gorm.DB {
			db = db.Order("is_default desc")
			return db
		}, func(db *gorm.DB) interface{} {
			db = db.Scan(&data)
			return data
		}, false)
	var result []*UserAddressOutput
	if data != nil && len(data) > 0 {
		result = make([]*UserAddressOutput, len(data))
		for i := 0; i < len(data); i++ {
			result[i] = CopyUserAddressOutputByAgent(data[i])
			result[i].UserId = data[i].AgentId
		}
	}
	return result, count
}

func ManufacturerAddrList(page int, size int) ([]*UserAddressOutput, int64) {
	var data []*models.ManufacturerAddr
	manufacturerAddr := models.ManufacturerAddr{}
	count, _ := getList(manufacturerAddr, page, size,
		func(db *gorm.DB) *gorm.DB {
			db = db.Order("is_default desc")
			return db
		}, func(db *gorm.DB) interface{} {
			db = db.Scan(&data)
			return data
		}, false)
	var result []*UserAddressOutput
	if data != nil && len(data) > 0 {
		result = make([]*UserAddressOutput, len(data))
		for i := 0; i < len(data); i++ {
			result[i] = CopyUserAddressOutputByManufacturer(data[i])
			result[i].UserId = data[i].ManufacturerId
		}
	}
	return result, count
}

func GetAddressList(page int, size int, userType UserType) ([]*UserAddressOutput, int64) {
	var data []*UserAddressOutput
	var count int64
	switch userType {
	case BuyerType:
		data, count = BuyerAddrList(page, size)
	case SellerType:
		data, count = SellerAddrList(page, size)
	case AgentType:
		data, count = AgentAddrList(page, size)
	case ManufacturerType:
		data, count = ManufacturerAddrList(page, size)
	}
	return data, count
}

func AddUserAddress(userAddress UserAddressOutput, userType UserType) (bool, error) {
	db := Db
	tx := db.Begin()
	switch userType {
	case BuyerType:
		if userAddress.IsDefault {
			tx = tx.Model(&models.BuyerAddr{}).Update("is_default", 0)
			if tx.Error != nil {
				return false, tx.Error
			}
		}
		address := CopyBuyerAddr(&userAddress)
		tx = tx.Model(&models.BuyerAddr{}).Create(address)
		if tx.Error != nil {
			tx.Rollback()
			return false, tx.Error
		}
		tx = tx.Commit()
		if tx.Error != nil {
			return false, tx.Error
		}
	case SellerType:
		if userAddress.IsDefault {
			tx = tx.Model(&models.SellerAddr{}).Update("is_default", 0)
			if tx.Error != nil {
				return false, tx.Error
			}
		}
		address := CopySellerAddr(&userAddress)
		tx = tx.Model(&models.SellerAddr{}).Create(address)
		if tx.Error != nil {
			tx.Rollback()
			return false, tx.Error
		}
		tx = tx.Commit()
		if tx.Error != nil {
			return false, tx.Error
		}
	case AgentType:
		if userAddress.IsDefault {
			tx = tx.Model(&models.AgentAddr{}).Update("is_default", 0)
			if tx.Error != nil {
				return false, tx.Error
			}
		}
		address := CopyAgentAddr(&userAddress)
		tx = tx.Model(&models.AgentAddr{}).Create(address)
		if tx.Error != nil {
			tx.Rollback()
			return false, tx.Error
		}
		tx = tx.Commit()
		if tx.Error != nil {
			return false, tx.Error
		}
	case ManufacturerType:
		if userAddress.IsDefault {
			tx = tx.Model(&models.ManufacturerAddr{}).Update("is_default", 0)
			if tx.Error != nil {
				return false, tx.Error
			}
		}
		address := CopyManufacturerAddr(&userAddress)
		tx = tx.Model(&models.ManufacturerAddr{}).Create(address)
		if tx.Error != nil {
			tx.Rollback()
			return false, tx.Error
		}
		tx = tx.Commit()
		if tx.Error != nil {
			return false, tx.Error
		}
	}
	return false, nil
}

func SetDefaultUserAddress(model models.BaseModel, id int64) (bool, error) {
	sql := "update " + model.TableName() + " set is_default=0 where id !=?"
	sql = sql + "update " + model.TableName() + " set is_default=1 where id =?"
	db := Db
	tx := db.Begin()
	tx = tx.Exec(sql, id, id)
	if tx.Error != nil {
		tx.Rollback()
		return false, tx.Error
	}
	tx = tx.Commit()
	if tx.Error != nil {
		return false, tx.Error
	}
	return true, nil
}

func AddUserFriend() {

}

func AgreeUserFriend() {

}

func UserFriendList() {

}

func UserReport() {

}

func UnUserReport() {

}

func UserReportList() {

}

func UserReportLog(reportId int64) {

}

func UserReportLogList() {

}
