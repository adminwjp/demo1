# Config.Example

Ŀ¼�ṹ �����ٴβ����⡣  ������̫�� ��ը ��������ȡ�����

#ef
dotnet ef migrations add a1 -o Ef.DAL/Migrations --project E:\work\csharp\src\Utility\Example\Config.Ef.DAL
dotnet ef database update --project E:\work\csharp\src\Utility\Example\Config.Ef.DAL
netstandard2.0 ��֧��(dotnet ef Ǩ�� ʧ��);
netcore ֧��;
netframework ��֧��(utility ������ʹ�õ��� ef, netframeworkҲ֧�� efcore)
����� ���쳣 

error tip:
Startup project 'Config.Example.csproj' targets framework '.NETStandard'. There is no runtime associated with this framework, and projects targeting it cannot be executed directly. To use the Entity Framework Core .NET Command-line Tools with this
 project, add an executable project targeting .NET Core or .NET Framework that references this project, and set it as the startup project using --startup-project; or, update this project to cross-target .NET Core or .NET Framework. For more inform
ation on using the EF Core Tools with .NET Standard projects, see https://go.microsoft.com/fwlink/?linkid=2034781

wcf remote (���� �������) ����ʹ�� netframework ����  (ֻ��ʹ��dapper nhibernate  ef core ,ef EnterpriseLibrary ��Щ������ ��������(������Ҫ��װ����������))

async asp.net core 5.0 pending, not support

