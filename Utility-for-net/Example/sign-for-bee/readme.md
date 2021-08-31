https://beego.me/docs/intro/contributing.md

CGO_ENABLED=1  GOOS=windows GOARCH=amd64 CC=x86_64-w64-mingw32-gcc go run main.go
CGO_ENABLED=1  GOOS=windows GOARCH=amd64 CC=x86_64-w64-mingw32-gcc go test -count=1 -v beedemo/tests
多余 参数 不能 随便 写 个数  影响 结果 看提示 样例 设置 参数 

golang 都是 红线 怎么 搞的  都不知道 哪个错了
报错 位置 不对