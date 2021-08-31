//package utility
//go run: cannot run non-main package
package main

import (
	//"io/fs"
	"io/ioutil"
	"log"
	"os"
	"utility/example/news"
	"utility/util"
	//"regexp"
)

/*
connection database failpanic: Binary was compiled with 'CGO_ENABLED=0',
go-sqli te3 requires cgo to work. This is a stub

win10
https://blog.csdn.net/sanqima/article/details/108930210

https://blog.csdn.net/sanqima/article/details/108930210

https://blog.csdn.net/halo_hsuh/article/details/106450423
go get github.com/mattn/go-sqlite3
go mod rendor

CGO_ENABLED=1 GOOS=windows GOARCH=amd64 CC=x86_64-w64-mingw32-gcc go build -v

CGO_ENABLED=1 GOOS=windows GOARCH=amd64  go build -v

//pass
CGO_ENABLED=1  GOOS=windows GOARCH=amd64 CC=x86_64-w64-mingw32-gcc go run main.go

//error
CGO_ENABLED=1 go build -v

//error
CGO_ENABLED=1  GOOS=windows go build -v

CGO_ENABLED=0 GOOS=windows GOARCH=amd64 CC=x86_64-w64-mingw32-gcc go build -v

https://blog.csdn.net/henly1217/article/details/102551251/?utm_medium=distribute.pc_relevant.none-task-blog-baidujs_title-0&spm=1001.2101.3001.4242


//这 安装花费 了 9 g  空间 感觉 好多 没用 qt 的
pacman -Syu
pacman -S mingw-w64-x86_64-qt5-static

  warning: database file for 'ucrt64' does not exist (use '-Sy' to download)
error: failed to prepare transaction (could not find database)

pacman -Syu
pacman -S mingw-w64-x86_64-qt5-static

Error opening file "C:/msys64/mingw64/qt5-static/bin/qmake.exe". Error 13.

pacman -S mingw-w64-x86_64-qt5-static

pacman -S mingw-w64-x86_64-gcc
pacman -S mingw-w64-x86_64-make
pacman -S mingw-w64-x86_64-clang-tools-extra
pacman -S mingw-w64-x86_64-clang
*/

//sqlite
//CGO_ENABLED=1  GOOS=windows GOARCH=amd64 CC=x86_64-w64-mingw32-gcc go run main.go
//CGO_ENABLED=1  GOOS=windows GOARCH=amd64 CC=x86_64-w64-mingw32-gcc go test  -count=1 -v  utility/test utility/test_mysql
//其他方式运行
// go run main.go
//-v  Log 输出 日志
//go test -count=1 -v utility/test utility/test_mysql
//golang 怎么 调试 运行 设置 参数的
//调试 还需要使用第三方 框架 调试
//主程序 入口
//注意 gorm(不用 自带 模型) 查询 数据 值 绑定 失败 为 默认值

// golang sqlite 怎么调试 的 参数化命令 不对
// @title 这里写标题`
// @version 1.0`
// @description 这里写描述信息`
// @termsOfService [http://swagger.io/terms/](http://swagger.io/terms/)`
// @contact.name 这里写联系人信息`
// @contact.url [http://www.swagger.io/support](http://www.swagger.io/support)`
// @contact.email support@swagger.io`
// @license.name Apache 2.0`
// @license.url [http://www.apache.org/licenses/LICENSE-2.0.html](http://www.apache.org/licenses/LICENSE-2.0.html)`
// @host 这里写接口服务的host`
// @BasePath 这里写base path`
const (
	Stock                     = 1
	HttpServer                = 2
	ExampleCompany            = 3
	Grpc                      = 4
	HttpServerAndGrpc         = 5
	HttpServerAndGrpcAndStock = 6
)

func main() {
	//example.Init()
	//news.NewDataByUniApp()
	news.NewDataByWy()
	//demo.Init()
	log.Println(util.SeurityInstance)
	s := HttpServerAndGrpcAndStock
	if s == Stock || s == HttpServerAndGrpcAndStock {
		go util.TestServerStart()
	}
	if s == HttpServer || s == HttpServerAndGrpcAndStock {

		ports := make([]string, 3)
		ports[0] = ":5003"
		ports[1] = ":5001"
		ports[2] = ":5002"
		go util.Start(func() {}, ports, []string{"manager"}) //404
	}
	if s == ExampleCompany {
		//go example.Start()
	}
	select {}
}

func shopData() {
	pwd, err := os.Getwd()
	if err != nil {
		println("path get fail")
		os.Exit(1)
	}
	p := pwd + "/config/product/catagory.json"
	buffer, err := ioutil.ReadFile(p)
	if err != nil {
		println("read file fail")
		os.Exit(1)
	}
	str := string(buffer)
	println(str)
	//按规则 替换 这是死 的啊 怎么 灵活 替换啊  难道 遍历 再 替换？
	//\"a\":\“1\” -> \"a\":1 怎么弄
	//re,_:=regexp.Complie("")
	//str1:=re.ReplaceAllString(str,"b")
	//ioutil.WriteFile(p,[]byte(str1),0666)
}
