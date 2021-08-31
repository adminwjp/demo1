提示  --project 最好 用绝对 路劲
System.Reflection.AmbiguousMatchException: Ambiguous match found.

dotnet ef migrations add a1 -o Migrations --project E:\work\csharp\src\Shop\Shop.Product\Product.Infrastructure
dotnet ef database update --project E:\work\csharp\src\Shop\Shop.Product\Product.Infrastructure
基于 ddd 实现 还是 基于 event 实现(需要大变动 MediatR 搭配 ef 自动实现(框架已封装好 参考 eProduct.InfrastructureOnContainers) 处理) 
MediatR 原理 参考 https://www.jianshu.com/p/583bcba352ec
id guid 分库 或 分表 时 用到 否则 long 就 可以了

netstandard2.0 不支持(dotnet ef 迁移 失败);
netcore 支持; System.Configuration.ConfigurationManager  不支持(什么玩意 有的支持 有的不支持 )
Method not found: 'System.Collections.Generic.Dictionary`2<System.String,System.Object> Microsoft.Extensions.Configuration.IConfigurationBuilder.get_Properties()'.


E:\work\utility\Utility-for-net\Example\Example.Web