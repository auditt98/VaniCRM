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
		NoEmail bit default 0,
		LeadOwner int,
		NoCall bit default 0,
		Phone varchar(15),
		Website nvarchar(100),
		Avatar nvarchar(500),
		Skype nvarchar(32),
		CreatedAt datetime,
		CreatedBy int,
		ModifiedAt datetime,
		ModifiedBy int,
		PRIORITY_ID int
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

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CAMPAIGN_STATUS' and xtype='U')
BEGIN
    CREATE TABLE [CAMPAIGN_STATUS] (
        ID int primary key identity(1,1),
		Name nvarchar(20) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CAMPAIGN_TARGET' and xtype='U')
BEGIN
    CREATE TABLE [CAMPAIGN_TARGET] (
        ID int primary key identity(1,1),
		CAMPAIGN_ID int,
		CONTACT_ID int,
		LEAD_ID int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TAG' and xtype='U')
BEGIN
    CREATE TABLE [TAG] (
        ID int primary key identity(1,1),
		Name nvarchar(20) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LOST_REASON' and xtype='U')
BEGIN
    CREATE TABLE [LOST_REASON] (
        ID int primary key identity(1,1),
		Reason nvarchar(100) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='GROUP' and xtype='U')
BEGIN
    CREATE TABLE [GROUP] (
        ID int primary key identity(1,1),
		Name nvarchar(50) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='GROUP_PERMISSION' and xtype='U')
BEGIN
    CREATE TABLE [GROUP_PERMISSION] (
        GROUP_ID int,
		PERMISSION_ID int,
		PRIMARY KEY(GROUP_ID, PERMISSION_ID)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PERMISSION' and xtype='U')
BEGIN
    CREATE TABLE [PERMISSION] (
        ID int primary key identity(1,1),
		Name nvarchar(50) not null,
		Description nvarchar(100),
		PERMISSION_ORDER_ID int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PERMISSION_ORDER' and xtype='U')
BEGIN
    CREATE TABLE [PERMISSION_ORDER] (
        ID int primary key identity(1,1),
		Name nvarchar(50) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CAMPAIGN' and xtype='U')
BEGIN
    CREATE TABLE [CAMPAIGN] (
        ID int primary key identity(1,1),
		Name nvarchar(100) not null,
		Description nvarchar(2000),
		CampaignOwner int,
		StartDate datetime,
		EndDate datetime,
		ActualCost bigint,
		BudgetedCost bigint,
		ExpectedResponse int,
		ExpectedRevenue bigint,
		NumberSent int,
		CreatedAt datetime,
		CreatedBy int,
		ModifiedBy int,
		ModifiedAt datetime,
		CAMPAIGN_TYPE_ID int,
		CAMPAIGN_STATUS_ID int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='COMPETITOR' and xtype='U')
BEGIN
    CREATE TABLE [COMPETITOR] (
        ID int primary key identity(1,1),
		Name nvarchar(100) not null,
		Website nvarchar(100),
		Strengths nvarchar(2000),
		Weaknesses nvarchar(2000)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='STAGE' and xtype='U')
BEGIN
    CREATE TABLE [STAGE] (
        ID int primary key identity(1,1),
		Name nvarchar(30) not null,
		Probability int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DEAL' and xtype='U')
BEGIN
    CREATE TABLE [DEAL] (
        ID int primary key identity(1,1),
		Name nvarchar(100) not null,
		ClosingDate datetime,
		DealOwner int,
		Amount bigint,
		ExpectedRevenue bigint,
		Contact_ID int,
		ACCOUNT_ID int,
		CAMPAIGN_ID int,
		isLost bit default 0,
		LOST_REASON_ID int,
		Description nvarchar(2000),
		ModifiedBy int,
		ModifiedAt datetime,
		CreatedBy int,
		CreatedAt datetime,
		PRIORITY_ID int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='STAGE_HISTORY' and xtype='U')
BEGIN
    CREATE TABLE [STAGE_HISTORY] (
        STAGE_ID int,
		DEAL_ID int,
		ModifiedBy int,
		ModifiedAt datetime,
		PRIMARY KEY(STAGE_ID, DEAL_ID)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DEAL_COMPETITOR' and xtype='U')
BEGIN
    CREATE TABLE [DEAL_COMPETITOR] (
        COMPETITOR_ID int,
		DEAL_ID int,
		Suggestions nvarchar(2000),
		ThreatLevel nvarchar(20),
		PRIMARY KEY(COMPETITOR_ID, DEAL_ID)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TAG_ITEM' and xtype='U')
BEGIN
    CREATE TABLE [TAG_ITEM] (
        ID int primary key identity(1,1),
		TAG_ID int not null,
		DEAL_ID int,
		CALL_ID int,
		TASK_ID int, 
		MEETING_ID int,
		CAMPAIGN_ID int,
		LEAD_ID int,
		ACCOUNT_ID int,
		CONTACT_ID int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='NOTE' and xtype='U')
BEGIN
    CREATE TABLE [NOTE] (
        ID int primary key identity(1,1),
		CreatedBy int not null,
		CreatedAt datetime,
		NoteBody nvarchar(2000),
		ACCOUNT_ID int,
		CONTACT_ID int,
		LEAD_ID int,
		DEAL_ID int,
		TASK_TEMPLATE_ID int,
		CAMPAIGN_ID int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FILE' and xtype='U')
BEGIN
    CREATE TABLE [FILE] (
        ID int primary key identity(1,1),
		FileName nvarchar(200),
		FileSize int,
		NOTE_ID int,
		Data varbinary(max),
		FileStorePath nvarchar(500)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ACCOUNT' and xtype='U')
BEGIN
    CREATE TABLE [ACCOUNT] (
        ID int primary key identity(1,1),
		AccountOwner int,
		AccountCollaborator int,
		Name nvarchar(100),
		Phone varchar(15),
		Fax varchar(15),
		Website nvarchar(100),
		ACCOUNT_TYPE_ID int,
		INDUSTRY_ID int,
		TaxCode nvarchar(30),
		NoEmployees int,
		AnnualRevenue bigint,
		BankName varchar(100),
		BankAccountName nvarchar(100),
		BankAccount varchar(100),
		Avatar nvarchar(500),
		Email nvarchar(320),	
		CreatedAt datetime,
		CreatedBy int,
		ConvertFrom int,
		ModifiedBy int,
		ModifiedAt datetime
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CONTACT' and xtype='U')
BEGIN
    CREATE TABLE [CONTACT] (
        ID int primary key identity(1,1),
		ContactOwner int,
		ContactCollaborator int,
		Name nvarchar(100),
		Email nvarchar(320),
		Phone varchar(15),
		Mobile varchar(15),
		DepartmentName nvarchar(100),
		Birthday date,
		AssistantName nvarchar(100),
		AssitantPhone varchar(15),
		NoEmail bit default 0,
		NoCall bit default 0,		
		CreatedAt datetime,
		CreatedBy int,
		ConvertFrom int,
		ModifiedBy int,
		ModifiedAt datetime,
		PRIORITY_ID int,
		ACCOUNT_ID int
    )
END
GO


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TASK' and xtype='U')
BEGIN
    CREATE TABLE [TASK] (
        ID int primary key identity(1,1),
		TASK_TEMPLATE_ID int,
		TaskOwner int,
		CONTACT_ID int,
		LEAD_ID int,
		RELATED_CAMPAIGN int,
		RELATED_DEAL int,
		RELATED_ACCOUNT int,
		EndOn datetime
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TASK_TEMPLATE' and xtype='U')
BEGIN
    CREATE TABLE [TASK_TEMPLATE] (
        ID int primary key identity(1,1),
		Title nvarchar(100),
		IsRepeat bit default 0,
		RRule nvarchar(100),
		TASK_STATUS_ID int,
		Description nvarchar(2000),
		CreatedBy int, 
		CreatedAt datetime,
		ModifiedBy int,
		ModifiedAt datetime,
		PRIORITY_ID int,
		IsCompleted bit default 0,
		DueDate datetime
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CALL' and xtype='U')
BEGIN
    CREATE TABLE [CALL] (
        ID int primary key identity(1,1),
		TASK_TEMPLATE_ID int,
		CALLREASON_ID int,
		CALLRESULT_ID int,
		CALLTYPE_ID int,
		Length int,
		CONTACT_ID int,
		LEAD_ID int,
		RELATED_DEAL int,
		RELATED_ACCOUNT int,
		RELATED_CAMPAIGN int,
		StartTime datetime,
		CallOwner int
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MEETING' and xtype='U')
BEGIN
    CREATE TABLE [MEETING] (
        ID int primary key identity(1,1),
		TASK_TEMPLATE_ID int,
		Host int,
		Location nvarchar(200),
		FromDate datetime,
		ToDate datetime,
		IsAllDay bit default 0,
		IsRemindParticipant bit default 0
    )
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MEETING_PARTICIPANT' and xtype='U')
BEGIN
    CREATE TABLE [MEETING_PARTICIPANT] (
        ID int primary key identity(1,1),
		MEETING_ID int,
		LEAD_ID int,
		USER_ID int,
		CONTACT_ID int
    )
END
GO

--- USER ---
ALTER TABLE [USER]
ADD FOREIGN KEY (CreatedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [USER]
ADD FOREIGN KEY (GROUP_ID) REFERENCES [GROUP](ID);
GO
--- LEAD ----
ALTER TABLE [LEAD]
ADD FOREIGN KEY (LeadSource) REFERENCES [LEAD_SOURCE](ID);
GO

ALTER TABLE [LEAD]
ADD FOREIGN KEY (INDUSTRY_ID) REFERENCES [INDUSTRY](ID);
GO

ALTER TABLE [LEAD]
ADD FOREIGN KEY (LeadOwner) REFERENCES [USER](ID);
GO

ALTER TABLE [LEAD]
ADD FOREIGN KEY (LeadOwner) REFERENCES [USER](ID);
GO

ALTER TABLE [LEAD]
ADD FOREIGN KEY (PRIORITY_ID) REFERENCES [PRIORITY](ID);
GO
---CAMPAIGN_TARGET---
ALTER TABLE [CAMPAIGN_TARGET]
ADD FOREIGN KEY (CAMPAIGN_ID) REFERENCES [CAMPAIGN](ID) ON DELETE CASCADE;
GO

ALTER TABLE [CAMPAIGN_TARGET]
ADD FOREIGN KEY (CONTACT_ID) REFERENCES [CONTACT](ID) ON DELETE CASCADE;
GO

ALTER TABLE [CAMPAIGN_TARGET]
ADD FOREIGN KEY (LEAD_ID) REFERENCES [LEAD](ID) ON DELETE CASCADE;
GO

---GROUP_PERMISSION---
ALTER TABLE [GROUP_PERMISSION]
ADD FOREIGN KEY (GROUP_ID) REFERENCES [GROUP](ID) ON DELETE CASCADE;
GO

ALTER TABLE [GROUP_PERMISSION]
ADD FOREIGN KEY (PERMISSION_ID) REFERENCES [PERMISSION](ID) ON DELETE CASCADE;
GO

---PERMISSION---
ALTER TABLE [PERMISSION]
ADD FOREIGN KEY (PERMISSION_ORDER_ID) REFERENCES [PERMISSION_ORDER](ID);
GO

---CAMPAIGN---
ALTER TABLE [CAMPAIGN]
ADD FOREIGN KEY (CampaignOwner) REFERENCES [USER](ID);
GO

ALTER TABLE [CAMPAIGN]
ADD FOREIGN KEY (CreatedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [CAMPAIGN]
ADD FOREIGN KEY (ModifiedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [CAMPAIGN]
ADD FOREIGN KEY (CAMPAIGN_TYPE_ID) REFERENCES [CAMPAIGN_TYPE](ID);
GO

ALTER TABLE [CAMPAIGN]
ADD FOREIGN KEY (CAMPAIGN_STATUS_ID) REFERENCES [CAMPAIGN_STATUS](ID);
GO

---DEAL---
ALTER TABLE [DEAL]
ADD FOREIGN KEY (DealOwner) REFERENCES [USER](ID);
GO

ALTER TABLE [DEAL]
ADD FOREIGN KEY (CONTACT_ID) REFERENCES [CONTACT](ID);
GO

ALTER TABLE [DEAL]
ADD FOREIGN KEY (ACCOUNT_ID) REFERENCES [ACCOUNT](ID);
GO

ALTER TABLE [DEAL]
ADD FOREIGN KEY (CAMPAIGN_ID) REFERENCES [CAMPAIGN](ID);
GO

ALTER TABLE [DEAL]
ADD FOREIGN KEY (LOST_REASON_ID) REFERENCES [LOST_REASON](ID);
GO

ALTER TABLE [DEAL]
ADD FOREIGN KEY (ModifiedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [DEAL]
ADD FOREIGN KEY (CreatedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [DEAL]
ADD FOREIGN KEY (PRIORITY_ID) REFERENCES [PRIORITY](ID);
GO

---STAGE_HISTORY---
ALTER TABLE [STAGE_HISTORY]
ADD FOREIGN KEY (STAGE_ID) REFERENCES [STAGE](ID) ON DELETE CASCADE;
GO

ALTER TABLE [STAGE_HISTORY]
ADD FOREIGN KEY (DEAL_ID) REFERENCES [DEAL](ID) ON DELETE CASCADE;
GO

ALTER TABLE [STAGE_HISTORY]
ADD FOREIGN KEY (ModifiedBy) REFERENCES [USER](ID) ON DELETE CASCADE;
GO

--DEAL_COMPETITOR---
ALTER TABLE [DEAL_COMPETITOR]
ADD FOREIGN KEY (COMPETITOR_ID) REFERENCES [COMPETITOR](ID) ON DELETE CASCADE;
GO

ALTER TABLE [DEAL_COMPETITOR]
ADD FOREIGN KEY (DEAL_ID) REFERENCES [DEAL](ID) ON DELETE CASCADE;
GO

---TAG_ITEM---
ALTER TABLE [TAG_ITEM]
ADD FOREIGN KEY (TAG_ID) REFERENCES [TAG](ID) ON DELETE CASCADE;
GO

ALTER TABLE [TAG_ITEM]
ADD FOREIGN KEY (DEAL_ID) REFERENCES [DEAL](ID) ON DELETE CASCADE;
GO

ALTER TABLE [TAG_ITEM]
ADD FOREIGN KEY (CALL_ID) REFERENCES [CALL](ID) ON DELETE CASCADE;
GO

ALTER TABLE [TAG_ITEM]
ADD FOREIGN KEY (TASK_ID) REFERENCES [TASK](ID) ON DELETE CASCADE;
GO

ALTER TABLE [TAG_ITEM]
ADD FOREIGN KEY (MEETING_ID) REFERENCES [MEETING](ID) ON DELETE CASCADE;
GO

ALTER TABLE [TAG_ITEM]
ADD FOREIGN KEY (CAMPAIGN_ID) REFERENCES [CAMPAIGN](ID) ON DELETE CASCADE;
GO

ALTER TABLE [TAG_ITEM]
ADD FOREIGN KEY (LEAD_ID) REFERENCES [LEAD](ID) ON DELETE CASCADE;
GO

ALTER TABLE [TAG_ITEM]
ADD FOREIGN KEY (ACCOUNT_ID) REFERENCES [ACCOUNT](ID) ON DELETE CASCADE;
GO

ALTER TABLE [TAG_ITEM]
ADD FOREIGN KEY (CONTACT_ID) REFERENCES [CONTACT](ID) ON DELETE CASCADE;
GO

---NOTE---

ALTER TABLE [NOTE]
ADD FOREIGN KEY (CreatedBy) REFERENCES [USER](ID) ON DELETE CASCADE;
GO

ALTER TABLE [NOTE]
ADD FOREIGN KEY (ACCOUNT_ID) REFERENCES [ACCOUNT](ID) ON DELETE CASCADE;
GO

ALTER TABLE [NOTE]
ADD FOREIGN KEY (CONTACT_ID) REFERENCES [CONTACT](ID) ON DELETE CASCADE;
GO

ALTER TABLE [NOTE]
ADD FOREIGN KEY (LEAD_ID) REFERENCES [LEAD](ID) ON DELETE CASCADE;
GO

ALTER TABLE [NOTE]
ADD FOREIGN KEY (DEAL_ID) REFERENCES [DEAL](ID) ON DELETE CASCADE;
GO

ALTER TABLE [NOTE]
ADD FOREIGN KEY (TASK_TEMPLATE_ID) REFERENCES [TASK_TEMPLATE](ID) ON DELETE CASCADE;
GO

ALTER TABLE [NOTE]
ADD FOREIGN KEY (CAMPAIGN_ID) REFERENCES [CAMPAIGN](ID) ON DELETE CASCADE;
GO

---FILE---
ALTER TABLE [FILE]
ADD FOREIGN KEY (NOTE_ID) REFERENCES [NOTE](ID) ON DELETE CASCADE;
GO

---ACCOUNT---
ALTER TABLE [ACCOUNT]
ADD FOREIGN KEY (AccountOwner) REFERENCES [USER](ID);
GO

ALTER TABLE [ACCOUNT]
ADD FOREIGN KEY (AccountCollaborator) REFERENCES [USER](ID);
GO

ALTER TABLE [ACCOUNT]
ADD FOREIGN KEY (ACCOUNT_TYPE_ID) REFERENCES [ACCOUNT_TYPE](ID);
GO

ALTER TABLE [ACCOUNT]
ADD FOREIGN KEY (INDUSTRY_ID) REFERENCES [INDUSTRY](ID);
GO

ALTER TABLE [ACCOUNT]
ADD FOREIGN KEY (CreatedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [ACCOUNT]
ADD FOREIGN KEY (ConvertFrom) REFERENCES [LEAD](ID);
GO

ALTER TABLE [ACCOUNT]
ADD FOREIGN KEY (ModifiedBy) REFERENCES [USER](ID);
GO

---CONTACT---
ALTER TABLE [CONTACT]
ADD FOREIGN KEY (ContactOwner) REFERENCES [USER](ID);
GO

ALTER TABLE [CONTACT]
ADD FOREIGN KEY (ContactCollaborator) REFERENCES [USER](ID);
GO

ALTER TABLE [CONTACT]
ADD FOREIGN KEY (ACCOUNT_ID) REFERENCES [ACCOUNT](ID);
GO

ALTER TABLE [CONTACT]
ADD FOREIGN KEY (ModifiedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [CONTACT]
ADD FOREIGN KEY (CreatedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [CONTACT]
ADD FOREIGN KEY (PRIORITY_ID) REFERENCES [PRIORITY](ID);
GO


---TASK---
ALTER TABLE [TASK]
ADD FOREIGN KEY (TASK_TEMPLATE_ID) REFERENCES [TASK_TEMPLATE](ID);
GO

ALTER TABLE [TASK]
ADD FOREIGN KEY (TaskOwner) REFERENCES [USER](ID);
GO

ALTER TABLE [TASK]
ADD FOREIGN KEY (CONTACT_ID) REFERENCES [CONTACT](ID);
GO

ALTER TABLE [TASK]
ADD FOREIGN KEY (LEAD_ID) REFERENCES [LEAD](ID);
GO

ALTER TABLE [TASK]
ADD FOREIGN KEY (RELATED_CAMPAIGN) REFERENCES [CAMPAIGN](ID);
GO

ALTER TABLE [TASK]
ADD FOREIGN KEY (RELATED_DEAL) REFERENCES [DEAL](ID);
GO

ALTER TABLE [TASK]
ADD FOREIGN KEY (RELATED_ACCOUNT) REFERENCES [ACCOUNT](ID);
GO

---TASK_TEMPLATE---
ALTER TABLE [TASK_TEMPLATE]
ADD FOREIGN KEY (TASK_STATUS_ID) REFERENCES [TASK_STATUS](ID);
GO

ALTER TABLE [TASK_TEMPLATE]
ADD FOREIGN KEY (CreatedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [TASK_TEMPLATE]
ADD FOREIGN KEY (ModifiedBy) REFERENCES [USER](ID);
GO

ALTER TABLE [TASK_TEMPLATE]
ADD FOREIGN KEY (PRIORITY_ID) REFERENCES [PRIORITY](ID);
GO

---CALL---
ALTER TABLE [CALL]
ADD FOREIGN KEY (TASK_TEMPLATE_ID) REFERENCES [TASK_TEMPLATE](ID);
GO

ALTER TABLE [CALL]
ADD FOREIGN KEY (CALLREASON_ID) REFERENCES [CALL_REASON](ID);
GO

ALTER TABLE [CALL]
ADD FOREIGN KEY (CALLRESULT_ID) REFERENCES [CALL_RESULT](ID);
GO

ALTER TABLE [CALL]
ADD FOREIGN KEY (CALLTYPE_ID) REFERENCES [CALL_TYPE](ID);
GO

ALTER TABLE [CALL]
ADD FOREIGN KEY (CONTACT_ID) REFERENCES [CONTACT](ID);
GO

ALTER TABLE [CALL]
ADD FOREIGN KEY (LEAD_ID) REFERENCES [LEAD](ID);
GO

ALTER TABLE [CALL]
ADD FOREIGN KEY (RELATED_DEAL) REFERENCES [DEAL](ID);
GO

ALTER TABLE [CALL]
ADD FOREIGN KEY (RELATED_ACCOUNT) REFERENCES [ACCOUNT](ID);
GO

ALTER TABLE [CALL]
ADD FOREIGN KEY (RELATED_CAMPAIGN) REFERENCES [CAMPAIGN](ID);
GO

ALTER TABLE [CALL]
ADD FOREIGN KEY (CallOwner) REFERENCES [USER](ID);
GO
---MEETING---
ALTER TABLE [MEETING]
ADD FOREIGN KEY (TASK_TEMPLATE_ID) REFERENCES [TASK_TEMPLATE](ID);
GO

ALTER TABLE [MEETING]
ADD FOREIGN KEY (Host) REFERENCES [USER](ID);
GO
---MEETING_PARTICIPANT---
ALTER TABLE [MEETING_PARTICIPANT]
ADD FOREIGN KEY (MEETING_ID) REFERENCES [MEETING](ID) ON DELETE CASCADE;
GO

ALTER TABLE [MEETING_PARTICIPANT]
ADD FOREIGN KEY (LEAD_ID) REFERENCES [LEAD](ID) ON DELETE CASCADE;
GO

ALTER TABLE [MEETING_PARTICIPANT]
ADD FOREIGN KEY (USER_ID) REFERENCES [USER](ID) ON DELETE CASCADE;
GO

ALTER TABLE [MEETING_PARTICIPANT]
ADD FOREIGN KEY (CONTACT_ID) REFERENCES [CONTACT](ID) ON DELETE CASCADE;
GO
---INDEX---

CREATE INDEX IX_USER ON [USER] (FirstName, LastName, Username, Email, CreatedAt, CreatedBy, GROUP_ID)
GO

CREATE INDEX IX_LEAD ON [LEAD] (Name, Email, LeadSource, INDUSTRY_ID, LeadOwner, CreatedBy, ModifiedBy)
CREATE INDEX IX_CAMPAIGN_TARGET ON [CAMPAIGN_TARGET] (CAMPAIGN_ID, CONTACT_ID, LEAD_ID)
CREATE INDEX IX_TAG ON [TAG] (Name)
CREATE INDEX IX_GROUP_PERMISSION ON [GROUP_PERMISSION] (GROUP_ID, PERMISSION_ID)
CREATE INDEX IX_PERMISSION ON [PERMISSION] (PERMISSION_ORDER_ID)
CREATE INDEX IX_CAMPAIGN ON [CAMPAIGN] (CampaignOwner, CreatedBy, ModifiedBy, CAMPAIGN_TYPE_ID, CAMPAIGN_STATUS_ID)
CREATE INDEX IX_DEAL ON [DEAL] (Name, DealOwner, CONTACT_ID, ACCOUNT_ID, CAMPAIGN_ID, LOST_REASON_ID, ModifiedBy, CreatedBy, PRIORITY_ID)
CREATE INDEX IX_STAGE_HISTORY ON [STAGE_HISTORY] (STAGE_ID, DEAL_ID, ModifiedBy)
CREATE INDEX IX_DEAL_COMPETITOR ON [DEAL_COMPETITOR] (COMPETITOR_ID, DEAL_ID)
CREATE INDEX IX_TAG_ITEM ON [TAG_ITEM] (TAG_ID, DEAL_ID, CALL_ID, TASK_ID, MEETING_ID, CAMPAIGN_ID, LEAD_ID, ACCOUNT_ID, CONTACT_ID)
CREATE INDEX IX_NOTE ON [NOTE] (CreatedBy, ACCOUNT_ID, CONTACT_ID, LEAD_ID, DEAL_ID, TASK_TEMPLATE_ID, CAMPAIGN_ID)
CREATE INDEX IX_FILE ON [FILE] (FileName, NOTE_ID)
CREATE INDEX IX_ACCOUNT ON [ACCOUNT] (AccountOwner, AccountCollaborator, Name, ACCOUNT_TYPE_ID, INDUSTRY_ID, CreatedBy, ConvertFrom, ModifiedBy)
CREATE INDEX IX_CONTACT ON [CONTACT] (ContactOwner, ContactCollaborator, Name, ACCOUNT_ID, ModifiedBy, CreatedBy, PRIORITY_ID)
CREATE INDEX IX_TASK ON [TASK] (TASK_TEMPLATE_ID, TaskOwner, CONTACT_ID, LEAD_ID, RELATED_CAMPAIGN, RELATED_DEAL, RELATED_ACCOUNT)
CREATE INDEX IX_TASK_TEMPLATE ON [TASK_TEMPLATE] (TASK_STATUS_ID, CreatedBy, ModifiedBy, PRIORITY_ID)
CREATE INDEX IX_CALL ON [CALL] (TASK_TEMPLATE_ID, CALLREASON_ID, CALLRESULT_ID, CALLTYPE_ID, CONTACT_ID, LEAD_ID, RELATED_DEAL, RELATED_ACCOUNT, RELATED_CAMPAIGN, CallOwner)
CREATE INDEX IX_MEETING ON [MEETING] (TASK_TEMPLATE_ID, Host) 
CREATE INDEX IX_MEETING_PARTICIPANT ON [MEETING_PARTICIPANT] (MEETING_ID, LEAD_ID, USER_ID, CONTACT_ID)
go
---STORED PROCEDURE---
CREATE PROCEDURE PROC_INSERT_CALL_REASON @Name nvarchar(20) as begin INSERT INTO [CALL_REASON](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_CALL_REASON @ID int, @Name nvarchar(20) as begin UPDATE [CALL_REASON] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_CALL_REASON @ID int as begin delete from [CALL_REASON] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_CALL_REASON @ID int as begin select * from [CALL_REASON] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_CALL_REASON as begin select * from [CALL_REASON] end;
go
CREATE PROCEDURE PROC_INSERT_CALL_RESULT @Name nvarchar(40) as begin INSERT INTO [CALL_RESULT](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_CALL_RESULT @ID int, @Name nvarchar(40) as begin UPDATE [CALL_RESULT] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_CALL_RESULT @ID int as begin delete from [CALL_RESULT] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_CALL_RESULT @ID int as begin select * from [CALL_RESULT] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_CALL_RESULT as begin select * from [CALL_RESULT] end;
go
CREATE PROCEDURE PROC_INSERT_CALL_TYPE @Name nvarchar(20) as begin INSERT INTO [CALL_TYPE](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_CALL_TYPE @ID int, @Name nvarchar(20) as begin UPDATE [CALL_TYPE] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_CALL_TYPE @ID int as begin delete from [CALL_TYPE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_CALL_TYPE @ID int as begin select * from [CALL_TYPE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_CALL_TYPE as begin select * from [CALL_TYPE] end;
go
CREATE PROCEDURE PROC_INSERT_ACCOUNT_TYPE @Name nvarchar(20) as begin INSERT INTO [ACCOUNT_TYPE](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_ACCOUNT_TYPE @ID int, @Name nvarchar(20) as begin UPDATE [ACCOUNT_TYPE] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_ACCOUNT_TYPE @ID int as begin delete from [ACCOUNT_TYPE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_ACCOUNT_TYPE @ID int as begin select * from [ACCOUNT_TYPE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_ACCOUNT_TYPE as begin select * from [ACCOUNT_TYPE] end;
go
CREATE PROCEDURE PROC_INSERT_CAMPAIGN_STATUS @Name nvarchar(20) as begin INSERT INTO [CAMPAIGN_STATUS](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_CAMPAIGN_STATUS @ID int, @Name nvarchar(20) as begin UPDATE [CAMPAIGN_STATUS] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_CAMPAIGN_STATUS @ID int as begin delete from [CAMPAIGN_STATUS] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_CAMPAIGN_STATUS @ID int as begin select * from [CAMPAIGN_STATUS] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_CAMPAIGN_STATUS as begin select * from [CAMPAIGN_STATUS] end;
go
CREATE PROCEDURE PROC_INSERT_CAMPAIGN_TYPE @Name nvarchar(30) as begin INSERT INTO [CAMPAIGN_TYPE](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_CAMPAIGN_TYPE @ID int, @Name nvarchar(30) as begin UPDATE [CAMPAIGN_TYPE] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_CAMPAIGN_TYPE @ID int as begin delete from [CAMPAIGN_TYPE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_CAMPAIGN_TYPE @ID int as begin select * from [CAMPAIGN_TYPE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_CAMPAIGN_TYPE as begin select * from [CAMPAIGN_TYPE] end;
go
CREATE PROCEDURE PROC_INSERT_COMPETITOR @Name nvarchar(100), @Strengths nvarchar(2000), @Weaknesses nvarchar(2000), @Website nvarchar(100) as begin INSERT INTO [COMPETITOR](Name, Strengths, Weaknesses, Website) values (@Name, @Strengths, @Weaknesses, @Website) end;
go
CREATE PROCEDURE PROC_UPDATE_COMPETITOR @ID int, @Name nvarchar(100), @Strengths nvarchar(2000), @Weaknesses nvarchar(2000), @Website nvarchar(100) as begin UPDATE [COMPETITOR] set Name = @Name, Strengths = @Strengths, Weaknesses = @Weaknesses, Website = @Website where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_COMPETITOR @ID int as begin delete from [COMPETITOR] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_COMPETITOR @ID int as begin select * from [COMPETITOR] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_COMPETITOR as begin select * from [COMPETITOR] end;
go
CREATE PROCEDURE PROC_INSERT_GROUP @Name nvarchar(50) as begin INSERT INTO [GROUP](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_GROUP @ID int, @Name nvarchar(50) as begin UPDATE [GROUP] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_GROUP @ID int as begin delete from [GROUP] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_GROUP @ID int as begin select * from [GROUP] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_GROUP as begin select * from [GROUP] end;
go
CREATE PROCEDURE PROC_INSERT_LEAD_SOURCE @Name nvarchar(100) as begin INSERT INTO [LEAD_SOURCE](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_LEAD_SOURCE @ID int, @Name nvarchar(100) as begin UPDATE [LEAD_SOURCE] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_LEAD_SOURCE @ID int as begin delete from [LEAD_SOURCE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_LEAD_SOURCE @ID int as begin select * from [LEAD_SOURCE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_LEAD_SOURCE as begin select * from [LEAD_SOURCE] end;
go
CREATE PROCEDURE PROC_INSERT_LEAD_STATUS @Name nvarchar(50) as begin INSERT INTO [LEAD_STATUS](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_LEAD_STATUS @ID int, @Name nvarchar(50) as begin UPDATE [LEAD_STATUS] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_LEAD_STATUS @ID int as begin delete from [LEAD_STATUS] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_LEAD_STATUS @ID int as begin select * from [LEAD_STATUS] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_LEAD_STATUS as begin select * from [LEAD_STATUS] end;
go
CREATE PROCEDURE PROC_INSERT_LOST_REASON @Reason nvarchar(100) as begin INSERT INTO [LOST_REASON](Reason) values (@Reason) end;
go
CREATE PROCEDURE PROC_UPDATE_LOST_REASON @ID int, @Reason nvarchar(100) as begin UPDATE [LOST_REASON] set Reason = @Reason where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_LOST_REASON @ID int as begin delete from [LOST_REASON] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_LOST_REASON @ID int as begin select * from [LOST_REASON] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_LOST_REASON as begin select * from [LOST_REASON] end;
go
CREATE PROCEDURE PROC_INSERT_INDUSTRY @Name nvarchar(100) as begin INSERT INTO [INDUSTRY](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_INDUSTRY @ID int, @Name nvarchar(100) as begin UPDATE [INDUSTRY] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_INDUSTRY @ID int as begin delete from [INDUSTRY] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_INDUSTRY @ID int as begin select * from [INDUSTRY] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_INDUSTRY as begin select * from [INDUSTRY] end;
go
CREATE PROCEDURE PROC_INSERT_PERMISSION_ORDER @Name nvarchar(50) as begin INSERT INTO [PERMISSION_ORDER](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_PERMISSION_ORDER @ID int, @Name nvarchar(50) as begin UPDATE [PERMISSION_ORDER] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_PERMISSION_ORDER @ID int as begin delete from [PERMISSION_ORDER] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_PERMISSION_ORDER @ID int as begin select * from [PERMISSION_ORDER] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_PERMISSION_ORDER as begin select * from [PERMISSION_ORDER] end;
go
CREATE PROCEDURE PROC_INSERT_PRIORITY @Name nvarchar(10) as begin INSERT INTO [PRIORITY](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_PRIORITY @ID int, @Name nvarchar(10) as begin UPDATE [PRIORITY] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_PRIORITY @ID int as begin delete from [PRIORITY] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_PRIORITY @ID int as begin select * from [PRIORITY] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_PRIORITY as begin select * from [PRIORITY] end;
go
CREATE PROCEDURE PROC_INSERT_STAGE @Name nvarchar(30), @Probability int as begin INSERT INTO [STAGE](Name, Probability) values (@Name, @Probability) end;
go
CREATE PROCEDURE PROC_UPDATE_STAGE @ID int, @Name nvarchar(30), @Probability int as begin UPDATE [STAGE] set Name = @Name, Probability = @Probability where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_STAGE @ID int as begin delete from [STAGE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_STAGE @ID int as begin select * from [STAGE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_STAGE as begin select * from [STAGE] end;
go
CREATE PROCEDURE PROC_INSERT_TAG @Name nvarchar(20) as begin INSERT INTO [TAG](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_TAG @ID int, @Name nvarchar(20) as begin UPDATE [TAG] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_TAG @ID int as begin delete from [TAG] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_TAG @ID int as begin select * from [TAG] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_TAG as begin select * from [TAG] end;
go
CREATE PROCEDURE PROC_INSERT_TASK_STATUS @Name nvarchar(20) as begin INSERT INTO [TASK_STATUS](Name) values (@Name) end;
go
CREATE PROCEDURE PROC_UPDATE_TASK_STATUS @ID int, @Name nvarchar(20) as begin UPDATE [TASK_STATUS] set Name = @Name where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_TASK_STATUS @ID int as begin delete from [TASK_STATUS] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_TASK_STATUS @ID int as begin select * from [TASK_STATUS] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_TASK_STATUS as begin select * from [TASK_STATUS] end;
go
CREATE PROCEDURE PROC_INSERT_FILE @Data varbinary, @FileName nvarchar(200), @FileSize int, @FileStorePath nvarchar(500), @NOTE_ID int as begin INSERT INTO [FILE](Data, FileName, FileSize, FileStorePath, NOTE_ID) values (@Data, @FileName, @FileSize, @FileStorePath, @NOTE_ID) end;
go
CREATE PROCEDURE PROC_UPDATE_FILE @Data varbinary, @FileName nvarchar(200), @FileSize int, @FileStorePath nvarchar(500), @ID int, @NOTE_ID int as begin UPDATE [FILE] set Data = @Data, FileName = @FileName, FileSize = @FileSize, FileStorePath = @FileStorePath, NOTE_ID = @NOTE_ID where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_FILE @ID int as begin delete from [FILE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_FILE @ID int as begin select * from [FILE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_FILE as begin select * from [FILE] end;
go
CREATE PROCEDURE PROC_INSERT_PERMISSION @Description nvarchar(100), @Name nvarchar(50), @PERMISSION_ORDER_ID int as begin INSERT INTO [PERMISSION](Description, Name, PERMISSION_ORDER_ID) values (@Description, @Name, @PERMISSION_ORDER_ID) end;
go
CREATE PROCEDURE PROC_UPDATE_PERMISSION @Description nvarchar(100), @ID int, @Name nvarchar(50), @PERMISSION_ORDER_ID int as begin UPDATE [PERMISSION] set Description = @Description, Name = @Name, PERMISSION_ORDER_ID = @PERMISSION_ORDER_ID where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_PERMISSION @ID int as begin delete from [PERMISSION] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_PERMISSION @ID int as begin select * from [PERMISSION] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_PERMISSION as begin select * from [PERMISSION] end;
go
CREATE PROCEDURE PROC_INSERT_MEETING @FromDate datetime, @Host int, @IsAllDay bit, @IsRemindParticipant bit, @Location nvarchar(200), @TASK_TEMPLATE_ID int, @ToDate datetime as begin INSERT INTO [MEETING](FromDate, Host, IsAllDay, IsRemindParticipant, Location, TASK_TEMPLATE_ID, ToDate) values (@FromDate, @Host, @IsAllDay, @IsRemindParticipant, @Location, @TASK_TEMPLATE_ID, @ToDate) end;
go
CREATE PROCEDURE PROC_UPDATE_MEETING @FromDate datetime, @Host int, @ID int, @IsAllDay bit, @IsRemindParticipant bit, @Location nvarchar(200), @TASK_TEMPLATE_ID int, @ToDate datetime as begin UPDATE [MEETING] set FromDate = @FromDate, Host = @Host, IsAllDay = @IsAllDay, IsRemindParticipant = @IsRemindParticipant, Location = @Location, TASK_TEMPLATE_ID = @TASK_TEMPLATE_ID, ToDate = @ToDate where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_MEETING @ID int as begin delete from [MEETING] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_MEETING @ID int as begin select * from [MEETING] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_MEETING as begin select * from [MEETING] end;
go
CREATE PROCEDURE PROC_INSERT_GROUP_PERMISSION @GROUP_ID int, @PERMISSION_ID int as begin INSERT INTO [GROUP_PERMISSION](GROUP_ID, PERMISSION_ID) values (@GROUP_ID, @PERMISSION_ID) end;
go
CREATE PROCEDURE PROC_GETALL_GROUP_PERMISSION as begin select * from [GROUP_PERMISSION] end;
go
CREATE PROCEDURE PROC_INSERT_DEAL_COMPETITOR @COMPETITOR_ID int, @DEAL_ID int, @Suggestions nvarchar(2000), @ThreatLevel nvarchar(20) as begin INSERT INTO [DEAL_COMPETITOR](COMPETITOR_ID, DEAL_ID, Suggestions, ThreatLevel) values (@COMPETITOR_ID, @DEAL_ID, @Suggestions, @ThreatLevel) end;
go
CREATE PROCEDURE PROC_GETALL_DEAL_COMPETITOR as begin select * from [DEAL_COMPETITOR] end;
go
CREATE PROCEDURE PROC_INSERT_USER @Avatar nvarchar(500), @CreatedAt datetime, @CreatedBy int, @Email nvarchar(320), @FirstName nvarchar(50), @GROUP_ID int, @Hash varchar(200), @LastName nvarchar(50), @Phone varchar(15), @RememberMeToken varchar(200), @Skype nvarchar(32), @Username varchar(30) as begin INSERT INTO [USER](Avatar, CreatedAt, CreatedBy, Email, FirstName, GROUP_ID, Hash, LastName, Phone, RememberMeToken, Skype, Username) values (@Avatar, @CreatedAt, @CreatedBy, @Email, @FirstName, @GROUP_ID, @Hash, @LastName, @Phone, @RememberMeToken, @Skype, @Username) end;
go
CREATE PROCEDURE PROC_UPDATE_USER @Avatar nvarchar(500), @CreatedAt datetime, @CreatedBy int, @Email nvarchar(320), @FirstName nvarchar(50), @GROUP_ID int, @Hash varchar(200), @ID int, @LastName nvarchar(50), @Phone varchar(15), @RememberMeToken varchar(200), @Skype nvarchar(32), @Username varchar(30) as begin UPDATE [USER] set Avatar = @Avatar, CreatedAt = @CreatedAt, CreatedBy = @CreatedBy, Email = @Email, FirstName = @FirstName, GROUP_ID = @GROUP_ID, Hash = @Hash, LastName = @LastName, Phone = @Phone, RememberMeToken = @RememberMeToken, Skype = @Skype, Username = @Username where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_USER @ID int as begin delete from [USER] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_USER @ID int as begin select * from [USER] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_USER as begin select * from [USER] end;
go
CREATE PROCEDURE PROC_INSERT_CAMPAIGN_TARGET @CAMPAIGN_ID int, @CONTACT_ID int, @LEAD_ID int as begin INSERT INTO [CAMPAIGN_TARGET](CAMPAIGN_ID, CONTACT_ID, LEAD_ID) values (@CAMPAIGN_ID, @CONTACT_ID, @LEAD_ID) end;
go
CREATE PROCEDURE PROC_UPDATE_CAMPAIGN_TARGET @CAMPAIGN_ID int, @CONTACT_ID int, @ID int, @LEAD_ID int as begin UPDATE [CAMPAIGN_TARGET] set CAMPAIGN_ID = @CAMPAIGN_ID, CONTACT_ID = @CONTACT_ID, LEAD_ID = @LEAD_ID where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_CAMPAIGN_TARGET @ID int as begin delete from [CAMPAIGN_TARGET] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_CAMPAIGN_TARGET @ID int as begin select * from [CAMPAIGN_TARGET] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_CAMPAIGN_TARGET as begin select * from [CAMPAIGN_TARGET] end;
go
CREATE PROCEDURE PROC_INSERT_STAGE_HISTORY @DEAL_ID int, @ModifiedAt datetime, @ModifiedBy int, @STAGE_ID int as begin INSERT INTO [STAGE_HISTORY](DEAL_ID, ModifiedAt, ModifiedBy, STAGE_ID) values (@DEAL_ID, @ModifiedAt, @ModifiedBy, @STAGE_ID) end;
go
CREATE PROCEDURE PROC_GETALL_STAGE_HISTORY as begin select * from [STAGE_HISTORY] end;
go
CREATE PROCEDURE PROC_INSERT_TASK_TEMPLATE @CreatedAt datetime, @CreatedBy int, @Description nvarchar(2000), @DueDate datetime, @IsCompleted bit, @IsRepeat bit, @ModifiedAt datetime, @ModifiedBy int, @PRIORITY_ID int, @RRule nvarchar(100), @TASK_STATUS_ID int, @Title nvarchar(100) as begin INSERT INTO [TASK_TEMPLATE](CreatedAt, CreatedBy, Description, DueDate, IsCompleted, IsRepeat, ModifiedAt, ModifiedBy, PRIORITY_ID, RRule, TASK_STATUS_ID, Title) values (@CreatedAt, @CreatedBy, @Description, @DueDate, @IsCompleted, @IsRepeat, @ModifiedAt, @ModifiedBy, @PRIORITY_ID, @RRule, @TASK_STATUS_ID, @Title) end;
go
CREATE PROCEDURE PROC_UPDATE_TASK_TEMPLATE @CreatedAt datetime, @CreatedBy int, @Description nvarchar(2000), @DueDate datetime, @ID int, @IsCompleted bit, @IsRepeat bit, @ModifiedAt datetime, @ModifiedBy int, @PRIORITY_ID int, @RRule nvarchar(100), @TASK_STATUS_ID int, @Title nvarchar(100) as begin UPDATE [TASK_TEMPLATE] set CreatedAt = @CreatedAt, CreatedBy = @CreatedBy, Description = @Description, DueDate = @DueDate, IsCompleted = @IsCompleted, IsRepeat = @IsRepeat, ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, PRIORITY_ID = @PRIORITY_ID, RRule = @RRule, TASK_STATUS_ID = @TASK_STATUS_ID, Title = @Title where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_TASK_TEMPLATE @ID int as begin delete from [TASK_TEMPLATE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_TASK_TEMPLATE @ID int as begin select * from [TASK_TEMPLATE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_TASK_TEMPLATE as begin select * from [TASK_TEMPLATE] end;
go
CREATE PROCEDURE PROC_INSERT_MEETING_PARTICIPANT @CONTACT_ID int, @LEAD_ID int, @MEETING_ID int, @USER_ID int as begin INSERT INTO [MEETING_PARTICIPANT](CONTACT_ID, LEAD_ID, MEETING_ID, USER_ID) values (@CONTACT_ID, @LEAD_ID, @MEETING_ID, @USER_ID) end;
go
CREATE PROCEDURE PROC_UPDATE_MEETING_PARTICIPANT @CONTACT_ID int, @ID int, @LEAD_ID int, @MEETING_ID int, @USER_ID int as begin UPDATE [MEETING_PARTICIPANT] set CONTACT_ID = @CONTACT_ID, LEAD_ID = @LEAD_ID, MEETING_ID = @MEETING_ID, USER_ID = @USER_ID where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_MEETING_PARTICIPANT @ID int as begin delete from [MEETING_PARTICIPANT] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_MEETING_PARTICIPANT @ID int as begin select * from [MEETING_PARTICIPANT] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_MEETING_PARTICIPANT as begin select * from [MEETING_PARTICIPANT] end;
go
CREATE PROCEDURE PROC_INSERT_LEAD @AnnualRevenue bigint, @Avatar nvarchar(500), @CompanyName nvarchar(150), @CreatedAt datetime, @CreatedBy int, @Description nvarchar(2000), @Email nvarchar(320), @Fax varchar(15), @INDUSTRY_ID int, @LeadOwner int, @LeadSource int, @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @NoCall bit, @NoEmail bit, @Phone varchar(15), @PRIORITY_ID int, @Skype nvarchar(32), @Website nvarchar(100) as begin INSERT INTO [LEAD](AnnualRevenue, Avatar, CompanyName, CreatedAt, CreatedBy, Description, Email, Fax, INDUSTRY_ID, LeadOwner, LeadSource, ModifiedAt, ModifiedBy, Name, NoCall, NoEmail, Phone, PRIORITY_ID, Skype, Website) values (@AnnualRevenue, @Avatar, @CompanyName, @CreatedAt, @CreatedBy, @Description, @Email, @Fax, @INDUSTRY_ID, @LeadOwner, @LeadSource, @ModifiedAt, @ModifiedBy, @Name, @NoCall, @NoEmail, @Phone, @PRIORITY_ID, @Skype, @Website) end;
go
CREATE PROCEDURE PROC_UPDATE_LEAD @AnnualRevenue bigint, @Avatar nvarchar(500), @CompanyName nvarchar(150), @CreatedAt datetime, @CreatedBy int, @Description nvarchar(2000), @Email nvarchar(320), @Fax varchar(15), @ID int, @INDUSTRY_ID int, @LeadOwner int, @LeadSource int, @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @NoCall bit, @NoEmail bit, @Phone varchar(15), @PRIORITY_ID int, @Skype nvarchar(32), @Website nvarchar(100) as begin UPDATE [LEAD] set AnnualRevenue = @AnnualRevenue, Avatar = @Avatar, CompanyName = @CompanyName, CreatedAt = @CreatedAt, CreatedBy = @CreatedBy, Description = @Description, Email = @Email, Fax = @Fax, INDUSTRY_ID = @INDUSTRY_ID, LeadOwner = @LeadOwner, LeadSource = @LeadSource, ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, Name = @Name, NoCall = @NoCall, NoEmail = @NoEmail, Phone = @Phone, PRIORITY_ID = @PRIORITY_ID, Skype = @Skype, Website = @Website where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_LEAD @ID int as begin delete from [LEAD] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_LEAD @ID int as begin select * from [LEAD] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_LEAD as begin select * from [LEAD] end;
go
CREATE PROCEDURE PROC_INSERT_CAMPAIGN @ActualCost bigint, @BudgetedCost bigint, @CAMPAIGN_STATUS_ID int, @CAMPAIGN_TYPE_ID int, @CampaignOwner int, @CreatedAt datetime, @CreatedBy int, @Description nvarchar(2000), @EndDate datetime, @ExpectedResponse int, @ExpectedRevenue bigint, @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @NumberSent int, @StartDate datetime as begin INSERT INTO [CAMPAIGN](ActualCost, BudgetedCost, CAMPAIGN_STATUS_ID, CAMPAIGN_TYPE_ID, CampaignOwner, CreatedAt, CreatedBy, Description, EndDate, ExpectedResponse, ExpectedRevenue, ModifiedAt, ModifiedBy, Name, NumberSent, StartDate) values (@ActualCost, @BudgetedCost, @CAMPAIGN_STATUS_ID, @CAMPAIGN_TYPE_ID, @CampaignOwner, @CreatedAt, @CreatedBy, @Description, @EndDate, @ExpectedResponse, @ExpectedRevenue, @ModifiedAt, @ModifiedBy, @Name, @NumberSent, @StartDate) end;
go
CREATE PROCEDURE PROC_UPDATE_CAMPAIGN @ActualCost bigint, @BudgetedCost bigint, @CAMPAIGN_STATUS_ID int, @CAMPAIGN_TYPE_ID int, @CampaignOwner int, @CreatedAt datetime, @CreatedBy int, @Description nvarchar(2000), @EndDate datetime, @ExpectedResponse int, @ExpectedRevenue bigint, @ID int, @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @NumberSent int, @StartDate datetime as begin UPDATE [CAMPAIGN] set ActualCost = @ActualCost, BudgetedCost = @BudgetedCost, CAMPAIGN_STATUS_ID = @CAMPAIGN_STATUS_ID, CAMPAIGN_TYPE_ID = @CAMPAIGN_TYPE_ID, CampaignOwner = @CampaignOwner, CreatedAt = @CreatedAt, CreatedBy = @CreatedBy, Description = @Description, EndDate = @EndDate, ExpectedResponse = @ExpectedResponse, ExpectedRevenue = @ExpectedRevenue, ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, Name = @Name, NumberSent = @NumberSent, StartDate = @StartDate where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_CAMPAIGN @ID int as begin delete from [CAMPAIGN] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_CAMPAIGN @ID int as begin select * from [CAMPAIGN] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_CAMPAIGN as begin select * from [CAMPAIGN] end;
go
CREATE PROCEDURE PROC_INSERT_CONTACT @ACCOUNT_ID int, @AssistantName nvarchar(100), @AssitantPhone varchar(15), @Birthday date, @ContactCollaborator int, @ContactOwner int, @ConvertFrom int, @CreatedAt datetime, @CreatedBy int, @DepartmentName nvarchar(100), @Email nvarchar(320), @Mobile varchar(15), @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @NoCall bit, @NoEmail bit, @Phone varchar(15), @PRIORITY_ID int as begin INSERT INTO [CONTACT](ACCOUNT_ID, AssistantName, AssitantPhone, Birthday, ContactCollaborator, ContactOwner, ConvertFrom, CreatedAt, CreatedBy, DepartmentName, Email, Mobile, ModifiedAt, ModifiedBy, Name, NoCall, NoEmail, Phone, PRIORITY_ID) values (@ACCOUNT_ID, @AssistantName, @AssitantPhone, @Birthday, @ContactCollaborator, @ContactOwner, @ConvertFrom, @CreatedAt, @CreatedBy, @DepartmentName, @Email, @Mobile, @ModifiedAt, @ModifiedBy, @Name, @NoCall, @NoEmail, @Phone, @PRIORITY_ID) end;
go
CREATE PROCEDURE PROC_UPDATE_CONTACT @ACCOUNT_ID int, @AssistantName nvarchar(100), @AssitantPhone varchar(15), @Birthday date, @ContactCollaborator int, @ContactOwner int, @ConvertFrom int, @CreatedAt datetime, @CreatedBy int, @DepartmentName nvarchar(100), @Email nvarchar(320), @ID int, @Mobile varchar(15), @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @NoCall bit, @NoEmail bit, @Phone varchar(15), @PRIORITY_ID int as begin UPDATE [CONTACT] set ACCOUNT_ID = @ACCOUNT_ID, AssistantName = @AssistantName, AssitantPhone = @AssitantPhone, Birthday = @Birthday, ContactCollaborator = @ContactCollaborator, ContactOwner = @ContactOwner, ConvertFrom = @ConvertFrom, CreatedAt = @CreatedAt, CreatedBy = @CreatedBy, DepartmentName = @DepartmentName, Email = @Email, Mobile = @Mobile, ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, Name = @Name, NoCall = @NoCall, NoEmail = @NoEmail, Phone = @Phone, PRIORITY_ID = @PRIORITY_ID where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_CONTACT @ID int as begin delete from [CONTACT] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_CONTACT @ID int as begin select * from [CONTACT] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_CONTACT as begin select * from [CONTACT] end;
go
CREATE PROCEDURE PROC_INSERT_ACCOUNT @ACCOUNT_TYPE_ID int, @AccountCollaborator int, @AccountOwner int, @AnnualRevenue bigint, @Avatar nvarchar(500), @BankAccount varchar(100), @BankAccountName nvarchar(100), @BankName varchar(100), @ConvertFrom int, @CreatedAt datetime, @CreatedBy int, @Email nvarchar(320), @Fax varchar(15), @INDUSTRY_ID int, @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @NoEmployees int, @Phone varchar(15), @TaxCode nvarchar(30), @Website nvarchar(100) as begin INSERT INTO [ACCOUNT](ACCOUNT_TYPE_ID, AccountCollaborator, AccountOwner, AnnualRevenue, Avatar, BankAccount, BankAccountName, BankName, ConvertFrom, CreatedAt, CreatedBy, Email, Fax, INDUSTRY_ID, ModifiedAt, ModifiedBy, Name, NoEmployees, Phone, TaxCode, Website) values (@ACCOUNT_TYPE_ID, @AccountCollaborator, @AccountOwner, @AnnualRevenue, @Avatar, @BankAccount, @BankAccountName, @BankName, @ConvertFrom, @CreatedAt, @CreatedBy, @Email, @Fax, @INDUSTRY_ID, @ModifiedAt, @ModifiedBy, @Name, @NoEmployees, @Phone, @TaxCode, @Website) end;
go
CREATE PROCEDURE PROC_UPDATE_ACCOUNT @ACCOUNT_TYPE_ID int, @AccountCollaborator int, @AccountOwner int, @AnnualRevenue bigint, @Avatar nvarchar(500), @BankAccount varchar(100), @BankAccountName nvarchar(100), @BankName varchar(100), @ConvertFrom int, @CreatedAt datetime, @CreatedBy int, @Email nvarchar(320), @Fax varchar(15), @ID int, @INDUSTRY_ID int, @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @NoEmployees int, @Phone varchar(15), @TaxCode nvarchar(30), @Website nvarchar(100) as begin UPDATE [ACCOUNT] set ACCOUNT_TYPE_ID = @ACCOUNT_TYPE_ID, AccountCollaborator = @AccountCollaborator, AccountOwner = @AccountOwner, AnnualRevenue = @AnnualRevenue, Avatar = @Avatar, BankAccount = @BankAccount, BankAccountName = @BankAccountName, BankName = @BankName, ConvertFrom = @ConvertFrom, CreatedAt = @CreatedAt, CreatedBy = @CreatedBy, Email = @Email, Fax = @Fax, INDUSTRY_ID = @INDUSTRY_ID, ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, Name = @Name, NoEmployees = @NoEmployees, Phone = @Phone, TaxCode = @TaxCode, Website = @Website where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_ACCOUNT @ID int as begin delete from [ACCOUNT] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_ACCOUNT @ID int as begin select * from [ACCOUNT] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_ACCOUNT as begin select * from [ACCOUNT] end;
go
CREATE PROCEDURE PROC_INSERT_NOTE @ACCOUNT_ID int, @CAMPAIGN_ID int, @CONTACT_ID int, @CreatedAt datetime, @CreatedBy int, @DEAL_ID int, @LEAD_ID int, @NoteBody nvarchar(2000), @TASK_TEMPLATE_ID int as begin INSERT INTO [NOTE](ACCOUNT_ID, CAMPAIGN_ID, CONTACT_ID, CreatedAt, CreatedBy, DEAL_ID, LEAD_ID, NoteBody, TASK_TEMPLATE_ID) values (@ACCOUNT_ID, @CAMPAIGN_ID, @CONTACT_ID, @CreatedAt, @CreatedBy, @DEAL_ID, @LEAD_ID, @NoteBody, @TASK_TEMPLATE_ID) end;
go
CREATE PROCEDURE PROC_UPDATE_NOTE @ACCOUNT_ID int, @CAMPAIGN_ID int, @CONTACT_ID int, @CreatedAt datetime, @CreatedBy int, @DEAL_ID int, @ID int, @LEAD_ID int, @NoteBody nvarchar(2000), @TASK_TEMPLATE_ID int as begin UPDATE [NOTE] set ACCOUNT_ID = @ACCOUNT_ID, CAMPAIGN_ID = @CAMPAIGN_ID, CONTACT_ID = @CONTACT_ID, CreatedAt = @CreatedAt, CreatedBy = @CreatedBy, DEAL_ID = @DEAL_ID, LEAD_ID = @LEAD_ID, NoteBody = @NoteBody, TASK_TEMPLATE_ID = @TASK_TEMPLATE_ID where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_NOTE @ID int as begin delete from [NOTE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_NOTE @ID int as begin select * from [NOTE] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_NOTE as begin select * from [NOTE] end;
go
CREATE PROCEDURE PROC_INSERT_TASK @CONTACT_ID int, @EndOn datetime, @LEAD_ID int, @RELATED_ACCOUNT int, @RELATED_CAMPAIGN int, @RELATED_DEAL int, @TASK_TEMPLATE_ID int, @TaskOwner int as begin INSERT INTO [TASK](CONTACT_ID, EndOn, LEAD_ID, RELATED_ACCOUNT, RELATED_CAMPAIGN, RELATED_DEAL, TASK_TEMPLATE_ID, TaskOwner) values (@CONTACT_ID, @EndOn, @LEAD_ID, @RELATED_ACCOUNT, @RELATED_CAMPAIGN, @RELATED_DEAL, @TASK_TEMPLATE_ID, @TaskOwner) end;
go
CREATE PROCEDURE PROC_UPDATE_TASK @CONTACT_ID int, @EndOn datetime, @ID int, @LEAD_ID int, @RELATED_ACCOUNT int, @RELATED_CAMPAIGN int, @RELATED_DEAL int, @TASK_TEMPLATE_ID int, @TaskOwner int as begin UPDATE [TASK] set CONTACT_ID = @CONTACT_ID, EndOn = @EndOn, LEAD_ID = @LEAD_ID, RELATED_ACCOUNT = @RELATED_ACCOUNT, RELATED_CAMPAIGN = @RELATED_CAMPAIGN, RELATED_DEAL = @RELATED_DEAL, TASK_TEMPLATE_ID = @TASK_TEMPLATE_ID, TaskOwner = @TaskOwner where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_TASK @ID int as begin delete from [TASK] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_TASK @ID int as begin select * from [TASK] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_TASK as begin select * from [TASK] end;
go
CREATE PROCEDURE PROC_INSERT_DEAL @ACCOUNT_ID int, @Amount bigint, @CAMPAIGN_ID int, @ClosingDate datetime, @Contact_ID int, @CreatedAt datetime, @CreatedBy int, @DealOwner int, @Description nvarchar(2000), @ExpectedRevenue bigint, @isLost bit, @LOST_REASON_ID int, @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @PRIORITY_ID int as begin INSERT INTO [DEAL](ACCOUNT_ID, Amount, CAMPAIGN_ID, ClosingDate, Contact_ID, CreatedAt, CreatedBy, DealOwner, Description, ExpectedRevenue, isLost, LOST_REASON_ID, ModifiedAt, ModifiedBy, Name, PRIORITY_ID) values (@ACCOUNT_ID, @Amount, @CAMPAIGN_ID, @ClosingDate, @Contact_ID, @CreatedAt, @CreatedBy, @DealOwner, @Description, @ExpectedRevenue, @isLost, @LOST_REASON_ID, @ModifiedAt, @ModifiedBy, @Name, @PRIORITY_ID) end;
go
CREATE PROCEDURE PROC_UPDATE_DEAL @ACCOUNT_ID int, @Amount bigint, @CAMPAIGN_ID int, @ClosingDate datetime, @Contact_ID int, @CreatedAt datetime, @CreatedBy int, @DealOwner int, @Description nvarchar(2000), @ExpectedRevenue bigint, @ID int, @isLost bit, @LOST_REASON_ID int, @ModifiedAt datetime, @ModifiedBy int, @Name nvarchar(100), @PRIORITY_ID int as begin UPDATE [DEAL] set ACCOUNT_ID = @ACCOUNT_ID, Amount = @Amount, CAMPAIGN_ID = @CAMPAIGN_ID, ClosingDate = @ClosingDate, Contact_ID = @Contact_ID, CreatedAt = @CreatedAt, CreatedBy = @CreatedBy, DealOwner = @DealOwner, Description = @Description, ExpectedRevenue = @ExpectedRevenue, isLost = @isLost, LOST_REASON_ID = @LOST_REASON_ID, ModifiedAt = @ModifiedAt, ModifiedBy = @ModifiedBy, Name = @Name, PRIORITY_ID = @PRIORITY_ID where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_DEAL @ID int as begin delete from [DEAL] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_DEAL @ID int as begin select * from [DEAL] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_DEAL as begin select * from [DEAL] end;
go
CREATE PROCEDURE PROC_INSERT_TAG_ITEM @ACCOUNT_ID int, @CALL_ID int, @CAMPAIGN_ID int, @CONTACT_ID int, @DEAL_ID int, @LEAD_ID int, @MEETING_ID int, @TAG_ID int, @TASK_ID int as begin INSERT INTO [TAG_ITEM](ACCOUNT_ID, CALL_ID, CAMPAIGN_ID, CONTACT_ID, DEAL_ID, LEAD_ID, MEETING_ID, TAG_ID, TASK_ID) values (@ACCOUNT_ID, @CALL_ID, @CAMPAIGN_ID, @CONTACT_ID, @DEAL_ID, @LEAD_ID, @MEETING_ID, @TAG_ID, @TASK_ID) end;
go
CREATE PROCEDURE PROC_UPDATE_TAG_ITEM @ACCOUNT_ID int, @CALL_ID int, @CAMPAIGN_ID int, @CONTACT_ID int, @DEAL_ID int, @ID int, @LEAD_ID int, @MEETING_ID int, @TAG_ID int, @TASK_ID int as begin UPDATE [TAG_ITEM] set ACCOUNT_ID = @ACCOUNT_ID, CALL_ID = @CALL_ID, CAMPAIGN_ID = @CAMPAIGN_ID, CONTACT_ID = @CONTACT_ID, DEAL_ID = @DEAL_ID, LEAD_ID = @LEAD_ID, MEETING_ID = @MEETING_ID, TAG_ID = @TAG_ID, TASK_ID = @TASK_ID where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_TAG_ITEM @ID int as begin delete from [TAG_ITEM] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_TAG_ITEM @ID int as begin select * from [TAG_ITEM] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_TAG_ITEM as begin select * from [TAG_ITEM] end;
go
CREATE PROCEDURE PROC_INSERT_CALL @CallOwner int, @CALLREASON_ID int, @CALLRESULT_ID int, @CALLTYPE_ID int, @CONTACT_ID int, @LEAD_ID int, @Length int, @RELATED_ACCOUNT int, @RELATED_CAMPAIGN int, @RELATED_DEAL int, @StartTime datetime, @TASK_TEMPLATE_ID int as begin INSERT INTO [CALL](CallOwner, CALLREASON_ID, CALLRESULT_ID, CALLTYPE_ID, CONTACT_ID, LEAD_ID, Length, RELATED_ACCOUNT, RELATED_CAMPAIGN, RELATED_DEAL, StartTime, TASK_TEMPLATE_ID) values (@CallOwner, @CALLREASON_ID, @CALLRESULT_ID, @CALLTYPE_ID, @CONTACT_ID, @LEAD_ID, @Length, @RELATED_ACCOUNT, @RELATED_CAMPAIGN, @RELATED_DEAL, @StartTime, @TASK_TEMPLATE_ID) end;
go
CREATE PROCEDURE PROC_UPDATE_CALL @CallOwner int, @CALLREASON_ID int, @CALLRESULT_ID int, @CALLTYPE_ID int, @CONTACT_ID int, @ID int, @LEAD_ID int, @Length int, @RELATED_ACCOUNT int, @RELATED_CAMPAIGN int, @RELATED_DEAL int, @StartTime datetime, @TASK_TEMPLATE_ID int as begin UPDATE [CALL] set CallOwner = @CallOwner, CALLREASON_ID = @CALLREASON_ID, CALLRESULT_ID = @CALLRESULT_ID, CALLTYPE_ID = @CALLTYPE_ID, CONTACT_ID = @CONTACT_ID, LEAD_ID = @LEAD_ID, Length = @Length, RELATED_ACCOUNT = @RELATED_ACCOUNT, RELATED_CAMPAIGN = @RELATED_CAMPAIGN, RELATED_DEAL = @RELATED_DEAL, StartTime = @StartTime, TASK_TEMPLATE_ID = @TASK_TEMPLATE_ID where ID = @ID end;
go
CREATE PROCEDURE PROC_DELETE_CALL @ID int as begin delete from [CALL] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETBYID_CALL @ID int as begin select * from [CALL] where ID = @ID end;
go
CREATE PROCEDURE PROC_GETALL_CALL as begin select * from [CALL] end;
go
