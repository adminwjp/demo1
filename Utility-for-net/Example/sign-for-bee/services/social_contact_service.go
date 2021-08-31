package services

import (
	"github.com/beego/beego/v2/client/orm"
	"log"
)

func Add(model interface{})bool {
	o := orm.NewOrm()
	rows,err:=o.Insert(model)
	if err!=nil{
		log.Println("insert fail,error:"+err.Error())
		return  false
	}
	return  rows>0
}


