package controllers

import (
	"regexp"

	//"strings"
	"news/shop/dto"
	"news/shop/models"
	"news/shop/services"

	"github.com/gin-gonic/gin"
)

func emptyValidate(c *gin.Context, name string, val string) bool {
	if val == "" {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    name + " is not null",
		})
		return true
	}
	return false
}
func register(c *gin.Context, userType services.UserType, register bool) {
	var user services.CreateUserInput
	c.BindJSON(&user)
	if emptyValidate(c, "account", user.Account) {
		return
	}
	if emptyValidate(c, "pwd", user.Pwd) {
		return
	}
	if user.Type == 1 {

	} else if user.Type == 2 {
		match, _ := regexp.Match("[13|15|17|18]\\d{9,9}", []byte(user.Account))
		if !match {
			c.JSON(200, gin.H{
				"status": false,
				"code":   303,
				"msg":    "phone error",
			})
			return
		}

	} else {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "type error",
		})
		return
	}

	var model models.BaseModel
	switch userType {
	case services.BuyerType:
		model = models.Buyer{}
	case services.SellerType:
		model = models.Seller{}
	case services.AgentType:
		model = models.Agent{}
	case services.ManufacturerType:
		model = models.Manufacturer{}
	}
	if !register {
		//login
		c.JSON(200, gin.H{
			"status": false,
			"code":   300,
			"msg":    "not implement",
		})
	}
	res := services.RegUser(model, user)
	switch res {
	case services.AccountExists:
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "exists",
		})
		return
	case services.RegisterSuccess:
		c.JSON(200, gin.H{
			"status": true,
			"code":   200,
			"msg":    "success",
		})
		return
	}
	c.JSON(200, gin.H{
		"status": false,
		"code":   300,
		"msg":    "fail",
	})
}

func RegisterBuyer(c *gin.Context) {
	register(c, services.BuyerType, true)
}

func RegisterSeller(c *gin.Context) {
	register(c, services.SellerType, true)
}

func RegisterAgent(c *gin.Context) {
	register(c, services.AgentType, true)
}

func RegisterManufacturer(c *gin.Context) {
	register(c, services.ManufacturerType, true)
}

func existAccount(c *gin.Context, userType services.UserType) {
	account := c.GetString("account")
	if emptyValidate(c, "account", account) {
		return
	}
	flag := c.GetInt("type")
	if flag == 1 {

	} else if flag == 2 {
		match, _ := regexp.Match("[13|15|17|18]\\d{9,9}", []byte(account))
		if !match {
			c.JSON(200, gin.H{
				"status": false,
				"code":   303,
				"msg":    "phone error",
			})
			return
		}

	} else {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "type error",
		})
		return
	}

	var model models.BaseModel
	switch userType {
	case services.BuyerType:
		model = models.Buyer{}
	case services.SellerType:
		model = models.Seller{}
	case services.AgentType:
		model = models.Agent{}
	case services.ManufacturerType:
		model = models.Manufacturer{}
	}
	user := services.CreateUserInput{Account: account, Type: flag}
	res := services.ExistsAccount(model, user)
	switch res {
	case services.AccountExists:
		c.JSON(200, gin.H{
			"status": true,
			"code":   200,
			"msg":    "exists",
		})
		return
	case services.AccountNotExists:
		c.JSON(200, gin.H{
			"status": false,
			"code":   301,
			"msg":    "not exists",
		})
		return
	}
	c.JSON(200, gin.H{
		"status": false,
		"code":   300,
		"msg":    "fail",
	})
}

func ExistAccountByBuyer(c *gin.Context) {
	existAccount(c, services.BuyerType)
}

func ExistAccountBySeller(c *gin.Context) {
	existAccount(c, services.SellerType)
}

func ExistAccountByAgent(c *gin.Context) {
	existAccount(c, services.AgentType)
}

func ExistAccountByManufacturer(c *gin.Context) {
	existAccount(c, services.ManufacturerType)
}

func LoginBuyer(c *gin.Context) {
	register(c, services.BuyerType, false)
}

func LoginSeller(c *gin.Context) {
	register(c, services.SellerType, false)
}

func LoginAgent(c *gin.Context) {
	register(c, services.AgentType, false)
}

func LoginManufacturer(c *gin.Context) {
	register(c, services.ManufacturerType, false)
}

func updateEmail(c *gin.Context, userType services.UserType) {
	var user services.UpdateUserEmailInput
	c.BindJSON(&user)
	if emptyValidate(c, "old_email", user.Email) {
		return
	}
	if emptyValidate(c, "new_email", user.NewEmail) {
		return
	}
	var model models.BaseModel
	switch userType {
	case services.BuyerType:
		model = models.Buyer{}
	case services.SellerType:
		model = models.Seller{}
	case services.AgentType:
		model = models.Agent{}
	case services.ManufacturerType:
		model = models.Manufacturer{}
	}
	res := services.UpdateEmail(model, user)
	if res {
		c.JSON(200, gin.H{
			"status": true,
			"code":   200,
			"msg":    "success",
		})
		return
	}
	c.JSON(200, gin.H{
		"status": false,
		"code":   300,
		"msg":    "fail",
	})
}

func UpdateEmailByBuyer(c *gin.Context) {
	updateEmail(c, services.BuyerType)
}

func UpdateEmailBySeller(c *gin.Context) {
	updateEmail(c, services.SellerType)
}

func UpdateEmailByAgent(c *gin.Context) {
	updateEmail(c, services.AgentType)
}

func UpdateEmailByManufacturer(c *gin.Context) {
	updateEmail(c, services.ManufacturerType)
}

func updatePhone(c *gin.Context, userType services.UserType) {
	var user services.UpdateUserPhoneInput
	c.BindJSON(&user)
	if emptyValidate(c, "phone", user.Phone) {
		return
	}
	if emptyValidate(c, "new_phone", user.NewPhone) {
		return
	}
	match, _ := regexp.Match("[13|15|17|18]\\d{9,9}", []byte(user.Phone))
	if !match {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "phone error",
		})
		return
	}
	match, _ = regexp.Match("[13|15|17|18]\\d{9,9}", []byte(user.NewPhone))
	if !match {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "new_phone error",
		})
		return
	}

	var model models.BaseModel
	switch userType {
	case services.BuyerType:
		model = models.Buyer{}
	case services.SellerType:
		model = models.Seller{}
	case services.AgentType:
		model = models.Agent{}
	case services.ManufacturerType:
		model = models.Manufacturer{}
	}
	res := services.UpdatePhone(model, user)
	if res {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "exists",
		})
		return
	}
	c.JSON(200, gin.H{
		"status": false,
		"code":   300,
		"msg":    "fail",
	})
}

func UpdatePhoneByBuyer(c *gin.Context) {
	updatePhone(c, services.BuyerType)
}

func UpdatePhoneBySeller(c *gin.Context) {
	updatePhone(c, services.SellerType)
}

func UpdatePhoneByAgent(c *gin.Context) {
	updatePhone(c, services.AgentType)
}

func UpdatePhoneByManufacturer(c *gin.Context) {
	updatePhone(c, services.ManufacturerType)
}
func updatePwdByEmail(c *gin.Context, userType services.UserType) {
	var user services.UpdateUserPwdByEmailInput
	c.BindJSON(&user)
	if emptyValidate(c, "email", user.Email) {
		return
	}
	if emptyValidate(c, "pwd", user.Pwd) {
		return
	}
	var model models.BaseModel
	switch userType {
	case services.BuyerType:
		model = models.Buyer{}
	case services.SellerType:
		model = models.Seller{}
	case services.AgentType:
		model = models.Agent{}
	case services.ManufacturerType:
		model = models.Manufacturer{}
	}
	res := services.UpdatePwdByEmail(model, user)
	if res {
		c.JSON(200, gin.H{
			"status": true,
			"code":   200,
			"msg":    "success",
		})
		return
	}
	c.JSON(200, gin.H{
		"status": false,
		"code":   300,
		"msg":    "fail",
	})
}
func UpdatePwdByEmailByBuyer(c *gin.Context) {
	updatePwdByEmail(c, services.BuyerType)
}

func UpdatePwdByEmailBySeller(c *gin.Context) {
	updatePwdByEmail(c, services.SellerType)
}

func UpdatePwdByEmailByAgent(c *gin.Context) {
	updatePwdByEmail(c, services.AgentType)
}

func UpdatePwdByEmailByManufacturer(c *gin.Context) {
	updatePwdByEmail(c, services.ManufacturerType)
}
func updatePwdByPhone(c *gin.Context, userType services.UserType) {
	var user services.UpdateUserPwdByPhoneInput
	c.BindJSON(&user)
	if emptyValidate(c, "phone", user.Phone) {
		return
	}
	if emptyValidate(c, "pwd", user.Pwd) {
		return
	}
	match, _ := regexp.Match("[13|15|17|18]\\d{9,9}", []byte(user.Phone))
	if !match {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "phone error",
		})
		return
	}

	var model models.BaseModel
	switch userType {
	case services.BuyerType:
		model = models.Buyer{}
	case services.SellerType:
		model = models.Seller{}
	case services.AgentType:
		model = models.Agent{}
	case services.ManufacturerType:
		model = models.Manufacturer{}
	}
	res := services.UpdatePwdByPhone(model, user)
	if res {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "exists",
		})
		return
	}
	c.JSON(200, gin.H{
		"status": false,
		"code":   300,
		"msg":    "fail",
	})
}

func UpdatePwdByPhoneByBuyer(c *gin.Context) {
	updatePwdByPhone(c, services.BuyerType)
}

func UpdatePwdByPhoneBySeller(c *gin.Context) {
	updatePwdByPhone(c, services.SellerType)
}

func UpdatePwdByPhoneByAgent(c *gin.Context) {
	updatePwdByPhone(c, services.AgentType)
}

func UpdatePwdByPhoneByManufacturer(c *gin.Context) {
	updatePwdByPhone(c, services.ManufacturerType)
}

func addressList(c *gin.Context, userType services.UserType) {
	page, size := getPageAndSize(c)
	data, count := services.GetAddressList(page, size, userType)
	result := dto.ResultDto{Data: data}
	result.SetPage(page, size, int(count))
	c.JSON(200, gin.H{
		"data":    result,
		"success": true,
		"code":    200,
		"msg":     "success",
	})
}

func BuyerAddressList(c *gin.Context) {
	addressList(c, services.BuyerType)
}

func SellerAddressList(c *gin.Context) {
	addressList(c, services.SellerType)
}

func AgentAddressList(c *gin.Context) {
	addressList(c, services.AgentType)
}

func ManufacturerAddressList(c *gin.Context) {
	addressList(c, services.ManufacturerType)
}

func addAddress(c *gin.Context, userType services.UserType) {
	var user services.UserAddressOutput
	c.BindJSON(&user)
	if emptyValidate(c, "phone", user.Phone) {
		return
	}
	if emptyValidate(c, "addr", user.Addr) {
		return
	}
	if emptyValidate(c, "city", user.City) {
		return
	}
	if emptyValidate(c, "contact_name", user.ContactName) {
		return
	}
	if emptyValidate(c, "country", user.Country) {
		return
	}
	if emptyValidate(c, "post_code", user.PostCode) {
		return
	}
	if emptyValidate(c, "province", user.Province) {
		return
	}
	if emptyValidate(c, "memo", user.Memo) {
		return
	}
	if user.AreaId < 1 {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "area_id error",
		})
		return
	}
	match, _ := regexp.Match("[13|15|17|18]\\d{9,9}", []byte(user.Phone))
	if !match {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "phone error",
		})
		return
	}
	//set user id

	res, _ := services.AddUserAddress(user, userType)
	if res {
		c.JSON(200, gin.H{
			"status": true,
			"code":   200,
			"msg":    "success",
		})
		return
	}
	c.JSON(200, gin.H{
		"status": false,
		"code":   300,
		"msg":    "fail",
	})
}

func AddAddressByBuyer(c *gin.Context) {
	addAddress(c, services.BuyerType)
}

func AddAddressBySeller(c *gin.Context) {
	addAddress(c, services.SellerType)
}

func AddAddressByAgent(c *gin.Context) {
	addAddress(c, services.AgentType)
}

func AddAddressByManufacturer(c *gin.Context) {
	addAddress(c, services.ManufacturerType)
}

func setDefaultUserAddress(c *gin.Context, userType services.UserType) {
	id := c.GetInt64("id")
	if id < 1 {
		c.JSON(200, gin.H{
			"status": false,
			"code":   303,
			"msg":    "id error",
		})
		return
	}
	var model models.BaseModel
	switch userType {
	case services.BuyerType:
		model = models.Buyer{}
	case services.SellerType:
		model = models.Seller{}
	case services.AgentType:
		model = models.Agent{}
	case services.ManufacturerType:
		model = models.Manufacturer{}
	}
	res, _ := services.SetDefaultUserAddress(model, id)
	if res {
		c.JSON(200, gin.H{
			"status": true,
			"code":   200,
			"msg":    "success",
		})
		return
	}
	c.JSON(200, gin.H{
		"status": false,
		"code":   300,
		"msg":    "fail",
	})
}
func SetDefaultAddressByBuyer(c *gin.Context) {
	setDefaultUserAddress(c, services.BuyerType)
}

func SetDefaultAddressBySeller(c *gin.Context) {
	setDefaultUserAddress(c, services.SellerType)
}

func SetDefaultAddressByAgent(c *gin.Context) {
	setDefaultUserAddress(c, services.AgentType)
}

func SetDefaultAddressByManufacturer(c *gin.Context) {
	setDefaultUserAddress(c, services.ManufacturerType)
}
