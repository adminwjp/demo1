package services

//包 不能嵌套 好像 main 引用 不能嵌套 其他没关系 什么玩意其它包嵌套 没错

import "news/shop/models"

type CreateUserInput struct {
	Account string `json:"account"`
	Type    int    `json:"type" ` //1 phone //2 email
	Pwd     string `json:"pwd"`
	Code    string `json:"code"`
}

type UpdateUserEmailInput struct {
	Email    string `json:"email"`
	NewEmail string `json:"new_email" `
	Token    string `json:"token"`
}

type UpdateUserPhoneInput struct {
	Phone    string `json:"phone"`
	NewPhone string `json:"new_phone" `
	Token    string `json:"token"`
}

type UpdateUserPwdByEmailInput struct {
	Email string `json:"email"`
	Pwd   string `json:"pwd"`
	Token string `json:"token"`
	Key   string `json:"Key"`
}

type UpdateUserPwdByPhoneInput struct {
	Phone string `json:"phone"`
	Pwd   string `json:"pwd"`
	Token string `json:"token"`
	Key   string `json:"Key"`
}

type UserAddressOutput struct {
	models.BaseUserAddr
	UserId int64 `json:"user_id"`
}
