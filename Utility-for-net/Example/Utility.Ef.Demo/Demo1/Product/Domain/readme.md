提示  --project 最好 用绝对 路劲
System.Reflection.AmbiguousMatchException: Ambiguous match found.

练习 

dotnet ef migrations add a1 -o Migrations --project E:\work\csharp\src\Shop\Shop.Product\Product.Domain
dotnet ef database update --project E:\work\csharp\src\Shop\Shop.Product\Product.Domain
基于 ddd 实现 还是 基于 event 实现(需要大变动 MediatR 搭配 ef 自动实现(框架已封装好 参考 eShopOnContainers) 处理) 
MediatR 原理 参考 https://www.jianshu.com/p/583bcba352ec


