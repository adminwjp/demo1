flutter clean
flutter doctor 出现问题 
(重启 (2次))之后 解决 了 
mysql 出现 问题 应该 哪里 配置造成的(没动 mysql )
https://blog.csdn.net/terry_water/article/details/17911057
https://www.jb51.net/article/82593.htm
mysqld --console
解决一个错误 又 出现 另一个 错误
删除日志 文件 找不到 根据文件 时间删除 重新启动就可以了
这里 暂时 操作 查询 所有  实际 操作 应该 需要 哪些 查询 哪些
图 方便 
npm run start
sqlite locked 解决 trans begin
await async task 阻塞 根本执行不了 什么玩意 jit 什么编译的 
//await new Task<string>(()=>"test async await")//error 阻塞
var t = await Task.FromResult("test async await");//pass 有些版本 不支持 坑嗲 啊低版本 就不用了
//net 10 -net48 -> net45- net48 
net40 Task.FromResult 不存在这玩意(但 async await 也不支持)

 Task.FromResult
 return Task.CompletedTask;




dotnet build 编译 不过 
则 vs 编译 否则 dotnet 编译

dotnet run --urls="http://*:5000"
ef dapper nhibernate tran 内部封装死了 需要修改对应配置需要查看源码
不知道 有没改动地方配置 重新 实现 再 配置 这种方式比较麻烦
需要继承(无法继承话 复制 代码 每次更新话 都要 对应版本) 改动东西比较多 这种方式放弃 只能手动控制每个框架的事务了
这里 没有提供接口直接变动 方式 可能没找到吧

提示  --project 最好 用绝对 路劲
System.Reflection.AmbiguousMatchException: Ambiguous match found.

dotnet ef migrations add a1 -o Migrations --project E:\work\utility\Utility-for-net\Example\Example.Web
dotnet ef database update --project E:\work\csharp\src\Shop\Shop.Product\Product.Infrastructure

dotnet ef database update  a1
基于 ddd 实现 还是 基于 event 实现(需要大变动 MediatR 搭配 ef 自动实现(框架已封装好 参考 eProduct.InfrastructureOnContainers) 处理) 
MediatR 原理 参考 https://www.jianshu.com/p/583bcba352ec
id guid 分库 或 分表 时 用到 否则 long 就 可以了

netstandard2.0 不支持(dotnet ef 迁移 失败);
netcore 支持; System.Configuration.ConfigurationManager  不支持(什么玩意 有的支持 有的不支持 )
Method not found: 'System.Collections.Generic.Dictionary`2<System.String,System.Object> Microsoft.Extensions.Configuration.IConfigurationBuilder.get_Properties()'.


dotnet ef migrations add  ConfigDbContext -c ConfigDbContext -o Migrations --project E:\work\utility\Utility-for-net\Example\Example.Web
dotnet ef migrations add  ProductDbContext -c ProductDbContext -o Migrations --project E:\work\utility\Utility-for-net\Example\Example.Web

An error occurred while accessing the Microsoft.Extensions.Hosting services. Continuing without the application service provider. Error: 
ConfigureServices returning an System.IServiceProvider isn't supported.

SdkToolsPath

 dotnet ef migrations add a1  -n Config.Ef -c Config.Ef.ConfigDbContext -o Migrations/Config --project E:\work\utility\Utility-for-net\Example\Example.Web

utility.demo netstandard2.0  netstandard2.0
example.web net5.0  netstandard2.1
 版本 不一致坑
  netstandard2.0; net5.0  netstandard2.0;netstandard2.1
 net5.0  netstandard2.1

 The Entity Framework tools version '5.0.3' is older than that of the runtime '5.
0.7'. Update the tools for the latest features and bug fixes.
An error occurred while accessing the Microsoft.Extensions.Hosting services. Con
tinuing without the application service provider. Error: ConfigureServices retur
ning an System.IServiceProvider isn't supported.
No DbContext named 'ConfigDbContext' was found.

https://docs.microsoft.com/zh-tw/ef/core/cli/dotnet

 dotnet ef migrations add a1 -c Config.Ef.ConfigDbContext -o Migrations/Config
 

 最好 放在 同一个 程序集 中 不然 dotnet ef 各种问题 
 注意 放入 aspnet core 中 会有 一些问题 
 # pass 
 dotnet ef migrations add a1  -n Config.Ef -c ConfigDbContext -o Migrations/Config
 dotnet ef database update  a1 -c ConfigDbContext

 dotnet ef migrations add a2   -c ProductDbContext -o Migrations/Product
 dotnet ef database update  a2  -c ProductDbContext
  
 dotnet ef migrations remove a3   -c CompanyDbContext -o Migrations/Company
  dotnet ef migrations add a3   -c CompanyDbContext -o Migrations/Company
 dotnet ef database update  a3  -c CompanyDbContext


   dotnet ef migrations add a4   -c ProductDbContext -o Migrations/Product
 dotnet ef database update  a4  -c ProductDbContext
   
 dotnet ef migrations add a5   -c DemoDbContext -o Migrations/Demo
 dotnet ef database update  a5  -c DemoDbContext

  dotnet ef migrations add a6   -c CarouselDbContext -o Cap/Migrations/Carousel
 dotnet ef database update  a6  -c CarouselDbContext

   dotnet ef migrations add a7   -c CommentDbContent -o Migrations/Comment
 dotnet ef database update  a7  -c CommentDbContent


 Your target project 'Utility.Demo' doesn't match your migrations assembly 'Utili
ty'. Either change your target project or change your migrations assembly.


怎么使用 的 拆开 可以 
ef 坑多  迁移问题 
The Entity Framework tools version '5.0.3' is older than that of the runtime '5.0.7'. Update the tools for the latest features and bug fixes.
An error occurred while accessing the Microsoft.Extensions.Hosting services. Continuing without the application service provider. Error: Unable to cast object of type 'Microsoft.Extensions.DependencyInjection.ServiceCollection' to type 'Autofac.ContainerBuilder'.
No DbContext named 'ConfigDbContext' was found.

dotnet tool update --global dotnet-ef

An error occurred while accessing the Microsoft.Extensions.Hosting services. Continuing without the application service provider. Error: Unable to cast object of type 'Microsoft.Extensions.DependencyInjection.ServiceCollection' to type 'Autofac.ContainerBuilder'.
No DbContext named 'ConfigDbContext' was found.

An error occurred while accessing the Microsoft.Extensions.Hosting services. Continuing without the application service provider. Error: ConfigureServices returning an System.IServiceProvider isn't supported.
No DbContext named 'ConfigDbContext' was found.

无解 只能拆开？ 单独执行 坑 多 算了 
最好不要跟 ap.net core 使用迁移 容易出现各种问题
netstandard 不能支持 dotnet ef

https://blog.csdn.net/laidanchao/article/details/103314131?utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromMachineLearnPai2%7Edefault-3.pc_relevant_baidujshouduan&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromMachineLearnPai2%7Edefault-3.pc_relevant_baidujshouduan

company
http://127.0.0.1:5001/company/admin/about.html

http://127.0.0.1:5001/company/index.html

改动 后放到 一起 好难  改啊  太 混乱了

使用 aop 必须 自己写 要么用第三方 (不好控制 坑)
这里 混合 在 一起在 必须 根据名字 获取 
ioc 有些无法知道 怎么创建(业务 变 复杂)
多写点  用第三方 自己写 各种 bug 不好控制

atuofac  怎么 运行 各种 错 改 尼玛 
An exception was thrown while activating

ioc 不能 手动 获取 了 不然 事务 有问题
其实 没关系 

根据 方法名 路由 地址 区分事务 逻辑
手动设置

应该使用时 获取事务 启动事务 否则 则 不启动

前端 最好 不要使用 long date 好点 不然 不好控制 需要写 大量 逻辑
后端转 date -> long(sql nosql date 可能有些问题 查询麻烦) 
时间 格式 不统一 用 long 稍微 好点
设计 不合理造成的



https://cloud.tencent.com/developer/article/1511929?from=article.detail.1151145

组合到 一起 要 使用 ioc name 获取 服务
不然 异常

Autofac.Annotation
注解 拦截器 有问题 

Unhandled exception. System.InvalidOperationException: 'Product.Infrastructure.R
epositories.CatagoryAttributeEfRepository' can not interceptor by both EnableAsp
ect and Interceptor:'Utility.Interceptors.IocTranstationAopInterceptor'

之前 不用注解 拦截器  可以通过 按理 应该支持 不清楚 是否 是其它原因造成


Method not found: 'System.__Canon AutoMapper.IMapper.Map

https://stackoverflow.com/questions/63519439/automapper-issue-with-identityserver4-missingmethodexception-method-not-found

abp 跟这 组合造成影响 
删除 abp 依赖 组件  代码 也删除
identityserver4 依赖  AutoMapper


Do not move backward, Do not downgrade, Upgrade

It seems that the AutoMapper 10.0.0 has a bug, the solution to that problem was to 
upgrade AutoMapper to 10.1.1 version


NumberChangeDomainEvent does not implement IRequest

Send Publish 注意下 写反了 按 规则来
注解 aop 无法使用 二选一 
这里 无法使用注解 
因为 使用 了 封装 aop 事务  要么 手动 可以 用注解

nable to cast object of type 'System.Int64' to type 'System.String'.",
    "Data": null,
    "InnerException": null,
    "HelpURL": null,
    "StackTraceString": "   at System.ComponentModel.DataAnnotations.StringLengthAttribute

    ef 注解 但 api 自动 验证 了 坑嗲啊 需要 改变规则
    不能 使用 ef 模型？