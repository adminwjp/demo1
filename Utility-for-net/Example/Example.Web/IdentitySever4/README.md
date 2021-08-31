# IdentityServer

支持 mysql sqlite oracle postgre  sqlserver layui 后台 前端 功能 不完善没修改 可以用

支持 3.x 4.x  有些功能 3.x 未实现 需要 实现

去参考 模板  最好 按模板 规则 来 不然 会出现 其他问题 

docker build -t identityserver:v1 .
docker run --name identity-server -p 6000:6000  identityserver:v1

SQLite Error 1: 'no su ch table: AspNetUsers'.
需要 使用 绝对地址

错误 没有 表  用工具 查看 数据库 信息
ef 升级后 不应该啊  sqlite 

ef 5.0 
https://docs.microsoft.com/zh-cn/ef/core/modeling/keyless-entity-types?tabs=data-annotations

 ---> System.InvalidOperationException: The entity type 'IdentityUserLogin<strin
g>' requires a primary key to be defined. If you intended to use a keyless entit
y type, call 'HasNoKey' in 'OnModelCreating'. For more information on keyless en
tity types, see https://go.microsoft.com/fwlink/?linkid=2141943.
   