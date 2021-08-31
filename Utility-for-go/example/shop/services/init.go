package services

import (
	"fmt"
	"log"
	"news/shop/models"
	"utlity/util"

	"github.com/jinzhu/gorm"

	// package utility/model is not in GOROOT
	//GO111MODULE="off"

	//_ "github.com/jinzhu/gorm/dialects/mysql"
	"github.com/go-basic/uuid"
	_ "github.com/jinzhu/gorm/dialects/sqlite"
)

//unsupported destination model new()
// no such table
func getList(model interface{}, page int, size int, where func(db *gorm.DB) *gorm.DB,
	datas func(db *gorm.DB) interface{}, all bool) (int64, error) {
	db := Db
	db = db.Model(model)
	db = where(db)
	countDb := db
	if !all {
		//no such table errror  db.Model(model).Offset((page-1)*size)
		db = db.Offset((page - 1) * size)
		db = db.Limit(size)
	}
	datas(db)
	if db.Error != nil {
		return -1, db.Error
	}
	var count int64
	countDb = countDb.Count(&count)
	return count, countDb.Error
}
func add(model interface{}) (bool, error) {
	db := Db
	db = db.Model(model).Create(model)
	if db.Error != nil {
		return false, db.Error
	}
	return true, nil
}

func update(model interface{}) (bool, error) {
	db := Db
	//var temp models.Agent
	//db.First(&temp,agent.Id)
	db = db.Save(&model)
	if db.Error != nil {
		return false, db.Error
	}
	return true, nil
}

func selectList(model interface{}, page int, size int, data interface{}) (interface{}, int64, error) {
	count, err := getList(model, page, size,
		func(db *gorm.DB) *gorm.DB {
			return db
		}, func(db *gorm.DB) interface{} {
			db = db.Scan(&data)
			return data
		}, false)
	return data, count, err
}

func init() {
	Id = IdService{}
}

var Id IIdService

var Db *gorm.DB

func InitDb() {
	//var cfg *goconfig.ConfigFile
	//cfg = util.ConfigInstance.LoadFile()
	models.InitCfg()
	//db, err := gorm.Open("sqlite3", "E:/work/db/sqlite/company2.db")
	db, err := gorm.Open(models.Config.Dialet, models.Config.Addrs)
	if err != nil {
		fmt.Print("connection database fail")
		panic(err)
	}
	db = db.Debug()
	//db=db.Logger
	Db = db
	//ServerPort = models.Config.Port
	db.AutoMigrate(new(models.Agent), new(models.AgentAduitLog),
		new(models.AgentCommission), new(models.AgentRank), new(models.Buyer),
		new(models.BuyerAddr), new(models.BuyerRecharge), new(models.Seller),
		new(models.SellerAddr), models.SocilWay{}, models.SellerFriend{}, models.BuyerFriend{},
		new(models.Manufacturer), new(models.ManufacturerFriend),
	)
	//registerConsul()
}

func registerConsul() {
	consul, err := util.NewConsulServiceRegistry(models.Config.ConsulIp, models.Config.ConsulPort, "")
	if err != nil {
		log.Fatal("consul get instace fail,error:" + err.Error())
	}
	uuid := uuid.New()
	reg := consul.Register(util.ServiceInfo{Id: uuid, //util.SeurityInstance.AesEncrypt(models.Config.ServiceName),
		Host: models.Config.Ip, Ip: models.Config.Ip, Port: models.Config.Port, ServiceName: models.Config.ServiceName, Tags: models.Config.ConsulTag})
	if !reg {
		log.Fatal("consul register fail")
	}
}
