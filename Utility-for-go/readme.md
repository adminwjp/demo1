cannot find package 
其他项目 可以 一拆开 就出现问题 
导入 包 格式 也不同
什么 语法 乱七八糟的
项目名 改变了 导入 格式 也跟着 改变
报错 位置 不对 乱 报错
cmd 执行 的 跟 ide 配置 修改 应该 没 关系
红线 不用管

go mod init news
go mod tidy
go mod vendor
go run main.go
go build main.go
go test  -count=1 -v  tests/news_test.go

mong db col 手动 创建
 指针 作用域 
不同 包  指针 为null

http://c.biancheng.net/view/109.html
http://www.topgoer.com/%E5%B8%B8%E7%94%A8%E6%A0%87%E5%87%86%E5%BA%93/%E5%8F%8D%E5%B0%84.html

go zipkin

https://blog.csdn.net/feizaoSYUACM/article/details/108099502

https://help.aliyun.com/document_detail/93482.html?spm=a2c4g.11186623.6.567.7fbeacceAHUGit

go consul
https://www.cnblogs.com/chaselogs/p/11462954.html

go eureka
https://blog.csdn.net/weixin_39128119/article/details/110875550

CGO_ENABLED=1  GOOS=windows GOARCH=amd64 CC=x86_64-w64-mingw32-gcc go run main.go
CGO_ENABLED=1  GOOS=windows GOARCH=amd64 CC=x86_64-w64-mingw32-gcc go test -count=1 -v shop/tests

go swagger
https://www.jianshu.com/p/3af6665a716b

gorm
https://blog.csdn.net/ListFish/article/details/109157039




shop
参考  php 项目
迁移 go php c# java python 微服务

html 页面太多 可以组合成一个页面
seo 则服务器渲染 登录后 则客户端渲染

http://c.biancheng.net/view/5574.html
https://www.jb51.net/article/162331.htm
php 不好迁移 php smarty 都有 代码

服务器 模板 渲染 不灵活 要么服务器端 生成 html 渲染模板
要么 客户端渲染

https://blog.csdn.net/u013210620/article/details/78525369

go 服务器端 渲染 if 普通 判断 支持
if null 异常
要么 必须存在 要么 声明 变量 比较
多 循环 异常
.a pass
$a error

rang $i := .a pass

rang i := .a error



