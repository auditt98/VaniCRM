use CRM
go

alter table [TASK_TEMPLATE]
add StartDate datetime;
go

alter table [USER]
add CalendarId nvarchar(2000)
go