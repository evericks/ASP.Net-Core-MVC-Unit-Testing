create database Supper
Go
use Supper
Go
create table [Player] (
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[Image] [nvarchar](256) NULL,
	[Description] [nvarchar](MAX) NULL,
	[Gender] [nvarchar](10) NULL,
	[Phone] [char](10) NULL,
	[Email] [varchar](50) UNIQUE NOT NULL,
	[Price] [float] NULL,
	[Star] [float] NULL,
)
Go
create table [User] (
	[Username] [nvarchar](256) PRIMARY KEY NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) UNIQUE NOT NULL,
	[Role] [varchar](10) DEFAULT 'user' NOT NULL,
	[Status] [bit] DEFAULT 1 NOT NULL, 
)
Go
insert into [User] values('votantai4899', 'tantai4899', 'votantai4899@gmail.com', 'admin', 1)
Go
create table [Comment] (
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](200) NOT NULL,
	[Date] [datetime] DEFAULT GETDATE() NOT NULL,
	[PlayerId] [int] NOT NULL,
	[Email] [nvarchar](256) UNIQUE NOT NULL,
)
Go
create proc CheckLogin
@username nvarchar(256),
@password nvarchar(256)
as
begin
	Select [Name], [Email], [Role], [Status] From [User] Where Username = @username and Password = @password
end
Go
create proc CreateUser
@username nvarchar(256),
@password nvarchar(256),
@name nvarchar(256),
@email nvarchar(256),
@result int out
as
begin
	Insert Into [User] ([Username], [Password], [Name], [Email]) Values (@username, @password, @name, @email)
	Set @result = (Select Count(1) From [User] Where Username = @username)
end
Go
create proc CheckDuplicate
@username nvarchar(256),
@email nvarchar(256),
@result int out
as
begin
	Set @result = (Select Count(1) From [User] Where [Username] = @username Or [Email] = @email)
end
Go