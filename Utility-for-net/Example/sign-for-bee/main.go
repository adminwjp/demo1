package main

import (
	beego "github.com/beego/beego/v2/server/web"
	"github.com/beego/beego/v2/server/web/filter/cors"
	"github.com/google/uuid"
	"log"
	_ "sign/routers"
	"sign/util"
	"strings"

	//"github.com/beego/beego/v2/client/orm"

	//_ "github.com/go-sql-driver/mysql"
	//_ "github.com/mattn/go-sqlite3"
)

//sqlite CGo - 交叉编译
// panic: Binary was compiled with 'CGO_ENABLED=0', go-sqlite3 requires cgo to work. This is a stub [recovered]
// https://blog.csdn.net/halo_hsuh/article/details/106450423
// var ormObject orm.Ormer

// 编码 没 提示  看 源码 不然 不知道写啥
// liteide 源码 那么多 文件 那么多 不好找
//go get beedemo
//如果 出现问题  GOBIN 删除掉
//https://beego.me/docs/mvc/model/orm.md
func init() {
	//model.Init()
	/*
		orm.DRMySQL
		orm.DRSqlite
		orm.DRPostgres
		orm.DRTiDB

		// < 1.6
		orm.DR_MySQL
		orm.DR_Sqlite`
		orm.DR_Postgres
	*/
	//orm.RegisterDriver("mysql", orm.DRMySQL)
	//orm.RegisterDataBase("default", "mysql", "root:wjp930514.W@(192.168.1.4:3306)/bee?charset=utf8")

	//orm.DR_Sqlite undefined
	//orm.RegisterDriver("sqlite3", orm.DRSqlite)
	//orm.RegisterDataBase("default", "sqlite3", "E:/work/db/sqlite/bee.sqlite3")

	//orm.RunSyncdb("default", false, true)
	//orm.DefaultTimeLoc = time.UTC

}

/*
func GetOrmObject() orm.Ormer {
	 return ormObject
}
*/

func  registerConsul()  {
	port:=beego.AppConfig.DefaultInt("Port",8701)

	ip:=beego.AppConfig.DefaultString("Ip","192.168.1.4")

	consulIp:=beego.AppConfig.DefaultString("ConsulIp","192.168.1.4")

	serviceName:=beego.AppConfig.DefaultString("ServiceName","sign.api")

	consulPort:=beego.AppConfig.DefaultInt("ConsulPort",8500)

	consulTag:=beego.AppConfig.DefaultString("ConsulTag","sign.api,go,gin")

	consul,err:=util.NewConsulServiceRegistry(consulIp,consulPort,"")
	if err!=nil{
		log.Fatal("consul get instace fail,error:"+err.Error())
	}
	reg:=consul.Register(util.ServiceInfo{Id: uuid.New().String(),Host: ip,Ip:ip,Port:port,ServiceName:serviceName ,Tags: strings.Split(consulTag,",")})
	if !reg {
		log.Fatal("consul register fail")
	}
}
func main() {
	registerConsul()
	//InsertFilter是提供一个过滤函数
	beego.InsertFilter("*", beego.BeforeRouter, cors.Allow(&cors.Options{
		//允许访问所有源
		AllowAllOrigins: true,
		//可选参数"GET", "POST", "PUT", "DELETE", "OPTIONS" (*为所有)
		//其中Options跨域复杂请求预检
		AllowMethods:   []string{"*"},
		//指的是允许的Header的种类
		AllowHeaders: 	[]string{"*"},
		//公开的HTTP标头列表
		ExposeHeaders:	[]string{"Content-Length"},
		//如果设置，则允许共享身份验证凭据，例如cookie
		AllowCredentials: true,
	}))
	//routers.RegisterRouter()
	//beego.Run(":8080")
	beego.Run()
}
