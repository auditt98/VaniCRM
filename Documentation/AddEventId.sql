use CRM
go
alter table [TASK_TEMPLATE]
add EventId nvarchar(2000)
go

alter table [DEAL]
add ExpectedClosingDate datetime
go

alter table [CAMPAIGN]
alter column Description nvarchar(max)
go