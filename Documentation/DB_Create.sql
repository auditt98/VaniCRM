IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DataBase')
  BEGIN
    CREATE DATABASE CRM
    END
    GO
       USE CRM
    GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='USER' and xtype='U')
BEGIN
    CREATE TABLE [USER] (
        ID int primary key identity(1,1)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='USER' and xtype='U')
BEGIN
    CREATE TABLE [USER] (
        ID int primary key identity(1,1),
		FirstName nvarchar(50) not null,
		Avatar nvarchar(500),
		LastName nvarchar(50) not null,
		Username varchar(30) not null,
		Hash varchar(200),
		RememberMeToken varchar(200),
		Email nvarchar(320) not null,
		Phone varchar(15),
		Skype nvarchar(32),
		CreatedAt datetime,
		CreatedBy int,
		GROUP_ID int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LEAD' and xtype='U')
BEGIN
    CREATE TABLE [LEAD] (
        ID int primary key identity(1,1),
		Name nvarchar(100) not null,
		Email nvarchar(320) not null,
		LeadSource int,
		AnnualRevenue bigint,
		CompanyName nvarchar(150),
		Fax varchar(15),
		Description nvarchar(2000),
		INDUSTRY_ID int,
		NoEmail bit default 1,
		LeadOwner int,
		NoCall bit,
		Phone varchar(15),
		Website nvarchar(100),
		Avatar nvarchar(500),
		Skype nvarchar(32),
		CreatedAt datetime,
		CreatedBy int,
		ModifiedAt datetime,
		ModifiedBy int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LEAD_SOURCE' and xtype='U')
BEGIN
    CREATE TABLE [LEAD_SOURCE] (
        ID int primary key identity(1,1),
		Name nvarchar(100) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='INDUSTRY' and xtype='U')
BEGIN
    CREATE TABLE [INDUSTRY] (
        ID int primary key identity(1,1),
		Name nvarchar(100) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LEAD_STATUS' and xtype='U')
BEGIN
    CREATE TABLE [LEAD_STATUS] (
        ID int primary key identity(1,1),
		Name nvarchar(50) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ACCOUNT_TYPE' and xtype='U')
BEGIN
    CREATE TABLE [ACCOUNT_TYPE] (
        ID int primary key identity(1,1),
		Name nvarchar(20) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PRIORITY' and xtype='U')
BEGIN
    CREATE TABLE [PRIORITY] (
        ID int primary key identity(1,1),
		Name nvarchar(10) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TASK_STATUS' and xtype='U')
BEGIN
    CREATE TABLE [TASK_STATUS] (
        ID int primary key identity(1,1),
		Name nvarchar(20) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CALL_RESULT' and xtype='U')
BEGIN
    CREATE TABLE [CALL_RESULT] (
        ID int primary key identity(1,1),
		Name nvarchar(40) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CALL_REASON' and xtype='U')
BEGIN
    CREATE TABLE [CALL_REASON] (
        ID int primary key identity(1,1),
		Name nvarchar(20) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CALL_TYPE' and xtype='U')
BEGIN
    CREATE TABLE [CALL_TYPE] (
        ID int primary key identity(1,1),
		Name nvarchar(20) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CAMPAIGN_TYPE' and xtype='U')
BEGIN
    CREATE TABLE [CAMPAIGN_TYPE] (
        ID int primary key identity(1,1),
		Name nvarchar(30) not null
    )
END
GO