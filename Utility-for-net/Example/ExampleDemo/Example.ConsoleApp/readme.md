dotnet ef migrations remove   --project E:\work\csharp\src\ExampleDemo\Example.ConsoleApp
dotnet ef migrations add a1 -o Migrations --project E:\work\csharp\src\ExampleDemo\Example.ConsoleApp
dotnet ef database update --project E:\work\csharp\src\ExampleDemo\Example.ConsoleApp

sqlserver
--select * from dbo.sysobjects ;
--if exists (select * from dbo.sysobjects where id = object_id('insert_user_proc') and OBJECTPROPERTY(id, 'IsProcedure') = 1)
drop PROCEDURE insert_user_proc;
CREATE PROCEDURE insert_user_proc 
@Id bigint,
@Account varchar(20),
@Pwd varchar(20),
@RegDate bigint,
@ModifyDate bigint,
@LoginDate bigint,
@LoginIp varchar(20),
@TimeSpan bigint,
@Result int OUTPUT
as
BEGIN
	SELECT @Result=count(Account) from Users u where  Account=@Account;
		
	if @Result>0
		insert into Users(Id,Account) VALUES(@Id,@Account);
end