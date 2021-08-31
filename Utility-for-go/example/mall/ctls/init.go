package ctls

import (
	"fmt"
	"news/models"
	"github.com/jinzhu/gorm"
	// package utility/model is not in GOROOT
	//GO111MODULE="off"
	"news/model"

	//_ "github.com/jinzhu/gorm/dialects/mysql"
	_ "github.com/jinzhu/gorm/dialects/sqlite"
)
var ServerPort int
var Db *gorm.DB
func init(){
	model.InitCfg()
	//db, err := gorm.Open("sqlite3", "E:/work/db/sqlite/company2.db")
	db, err := gorm.Open(model.Config.Dialet, model.Config.Addrs)
	if err!= nil{
		fmt.Print("connection database fail")
		panic(err)
	}
	db=db.Debug()
	//db=db.Logger
	Db=db
	ServerPort =model.Config.Port
	db.AutoMigrate(new (models.Addressinfor),new (models.Cartinfor),
		new (models.Cataloginfor))

}
