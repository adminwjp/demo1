# Config.Example

目录结构 可以再次拆分类库。  拆分类库太多 爆炸 所以这里取消拆分

#ef
dotnet ef migrations add a1 -o Ef.DAL/Migrations --project E:\work\csharp\src\Utility\Example\Config.Ef.DAL
dotnet ef database update --project E:\work\csharp\src\Utility\Example\Config.Ef.DAL
netstandard2.0 不支持(dotnet ef 迁移 失败);
netcore 支持;
netframework 不支持(utility 包依赖使用的是 ef, netframework也支持 efcore)
表存在 会异常 

error tip:
Startup project 'Config.Example.csproj' targets framework '.NETStandard'. There is no runtime associated with this framework, and projects targeting it cannot be executed directly. To use the Entity Framework Core .NET Command-line Tools with this
 project, add an executable project targeting .NET Core or .NET Framework that references this project, and set it as the startup project using --startup-project; or, update this project to cross-target .NET Core or .NET Framework. For more inform
ation on using the EF Core Tools with .NET Standard projects, see https://go.microsoft.com/fwlink/?linkid=2034781

wcf remote (复制 另外类库) 必须使用 netframework 环境  (只能使用dapper nhibernate  ef core ,ef EnterpriseLibrary 有些有问题 配置问题(驱动需要安装或其它问题))

async asp.net core 5.0 pending, not support

