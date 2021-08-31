package company

import (
	"fmt"
	"github.com/jinzhu/gorm"
	"utility/model"
	"utility/util"

	//_ "github.com/jinzhu/gorm/dialects/mysql"
	_ "github.com/jinzhu/gorm/dialects/sqlite"
)
var GormUtil util.GormUtil
//test 改个 每次 防止 init
func init()  {
	//model.InitCfg()
	//initDb()
}

func  initDb()  {
	//db, err := gorm.Open("mysql", "root:root@(127.0.0.1:3306)/db1?charset=utf8mb4&parseTime=True&loc=Local")
	//db, err := gorm.Open("sqlite3", "E:/work/db/sqlite/company2.db")
	db, err := gorm.Open(model.Config.Dialet, model.Config.Addrs)
	if err!= nil{
		fmt.Print("connection database fail")
		panic(err)
	}
	// 全局禁用表名复数
	//db.SingularTable(true) // 如果设置为true,Company 的默认表名为 company,使用TableName设置的表名不受影响
	GormUtil.Db=db
	//defer db.Close()

	// 自动迁移
	db.AutoMigrate(&AdminEntity{},&AboutEntity{},&CategoryEntity{},&CompanyEntity{},&ImageEntity{},&MainEntity{},
	&RelationEntity{},&TeamEntity{})
}

