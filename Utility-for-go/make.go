package main
//go run: cannot run non-main package
//package main

import (
	"log"
	"utility/util"
	"utility/model"
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
func Test ()  {
	model.Empty()
	util.Empty()
	log.Println(util.SeurityInstance)
}