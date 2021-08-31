package models

//"github.com/jinzhu/gorm"

type BaseDate struct {
	//Buyer -1 0 未激活 1 激活
	Active      int32 `json:"active"`
	CreatedDate int32 `json:"created_date"`
	UpdatedDate int32 `json:"updated_date"`
}
