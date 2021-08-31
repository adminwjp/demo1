CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] CHAR(10) NOT NULL
)
EXEC  sp_attach_db  @dbname  =  'Database1',     

@filename1  =  'E:\work\csharp\src\Examle.CsDemo\Database1.mdf',    

@filename2  =  'E:\work\csharp\src\Examle.CsDemo\Database1_log.ldf'