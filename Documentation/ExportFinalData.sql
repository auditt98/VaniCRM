USE [CRM]
GO
SET IDENTITY_INSERT [dbo].[ACCOUNT_TYPE] ON 

INSERT [dbo].[ACCOUNT_TYPE] ([ID], [Name]) VALUES (1, N'Customer')
INSERT [dbo].[ACCOUNT_TYPE] ([ID], [Name]) VALUES (2, N'Distributor')
INSERT [dbo].[ACCOUNT_TYPE] ([ID], [Name]) VALUES (3, N'Investor')
INSERT [dbo].[ACCOUNT_TYPE] ([ID], [Name]) VALUES (4, N'Vendor')
INSERT [dbo].[ACCOUNT_TYPE] ([ID], [Name]) VALUES (5, N'Reseller')
INSERT [dbo].[ACCOUNT_TYPE] ([ID], [Name]) VALUES (6, N'Competitor')
INSERT [dbo].[ACCOUNT_TYPE] ([ID], [Name]) VALUES (7, N'Supplier')
INSERT [dbo].[ACCOUNT_TYPE] ([ID], [Name]) VALUES (8, N'Other')
SET IDENTITY_INSERT [dbo].[ACCOUNT_TYPE] OFF
GO
SET IDENTITY_INSERT [dbo].[INDUSTRY] ON 

INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (1, N'Education')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (2, N'Financial Services/Banking')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (3, N'Government')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (4, N'Military')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (5, N'Healthcare')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (6, N'Insurance')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (7, N'Manufacturing')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (8, N'Automotive')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (9, N'Pharmaceutical/Chemical')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (10, N'Professional Services')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (11, N'Retail')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (12, N'Sports')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (13, N'Entertainment')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (14, N'Technology')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (15, N'Telecom')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (16, N'Transportation')
INSERT [dbo].[INDUSTRY] ([ID], [Name]) VALUES (17, N'Travel')
SET IDENTITY_INSERT [dbo].[INDUSTRY] OFF
GO
SET IDENTITY_INSERT [dbo].[LEAD_SOURCE] ON 

INSERT [dbo].[LEAD_SOURCE] ([ID], [Name]) VALUES (1, N'Advertisement')
INSERT [dbo].[LEAD_SOURCE] ([ID], [Name]) VALUES (2, N'Call')
INSERT [dbo].[LEAD_SOURCE] ([ID], [Name]) VALUES (3, N'Referred by Employee')
INSERT [dbo].[LEAD_SOURCE] ([ID], [Name]) VALUES (4, N'Referred by Others')
INSERT [dbo].[LEAD_SOURCE] ([ID], [Name]) VALUES (5, N'Facebook')
INSERT [dbo].[LEAD_SOURCE] ([ID], [Name]) VALUES (6, N'Website view')
INSERT [dbo].[LEAD_SOURCE] ([ID], [Name]) VALUES (7, N'Chat')
SET IDENTITY_INSERT [dbo].[LEAD_SOURCE] OFF
GO
SET IDENTITY_INSERT [dbo].[LEAD_STATUS] ON 

INSERT [dbo].[LEAD_STATUS] ([ID], [Name]) VALUES (1, N'Contacted')
INSERT [dbo].[LEAD_STATUS] ([ID], [Name]) VALUES (2, N'Will contact')
INSERT [dbo].[LEAD_STATUS] ([ID], [Name]) VALUES (3, N'Not contacted')
INSERT [dbo].[LEAD_STATUS] ([ID], [Name]) VALUES (4, N'Can not contact')
INSERT [dbo].[LEAD_STATUS] ([ID], [Name]) VALUES (5, N'False lead')
INSERT [dbo].[LEAD_STATUS] ([ID], [Name]) VALUES (6, N'Marketing qualified')
INSERT [dbo].[LEAD_STATUS] ([ID], [Name]) VALUES (7, N'Not qualified')
INSERT [dbo].[LEAD_STATUS] ([ID], [Name]) VALUES (8, N'Converted')
SET IDENTITY_INSERT [dbo].[LEAD_STATUS] OFF
GO
SET IDENTITY_INSERT [dbo].[PRIORITY] ON 

INSERT [dbo].[PRIORITY] ([ID], [Name]) VALUES (1, N'Low')
INSERT [dbo].[PRIORITY] ([ID], [Name]) VALUES (2, N'Medium')
INSERT [dbo].[PRIORITY] ([ID], [Name]) VALUES (3, N'High')
SET IDENTITY_INSERT [dbo].[PRIORITY] OFF
GO
SET IDENTITY_INSERT [dbo].[GROUP] ON 

INSERT [dbo].[GROUP] ([ID], [Name]) VALUES (1, N'Marketing Team')
INSERT [dbo].[GROUP] ([ID], [Name]) VALUES (2, N'Sales Team')
INSERT [dbo].[GROUP] ([ID], [Name]) VALUES (3, N'Admin Team')
SET IDENTITY_INSERT [dbo].[GROUP] OFF
GO
SET IDENTITY_INSERT [dbo].[USER] ON 

INSERT [dbo].[USER] ([ID], [FirstName], [Avatar], [LastName], [Username], [Hash], [RememberMeToken], [Email], [Phone], [Skype], [CreatedAt], [CreatedBy], [GROUP_ID], [CalendarId]) VALUES (1, N'Trịnh Việt', N'Catalina_Night-72b82483a13d4ff19bdc0b813ff7b2b2.jpg', N'Anh', N'crm.admin', N'$2a$11$Td/BmURqKXV1YC2pJF01ueMNYDUdV20lAmyyDUB/dg0T5XmpIO5jy', NULL, N'crm.quantri1@gmail.com', N'0348856601', N'', CAST(N'2021-06-01T20:18:00.000' AS DateTime), NULL, 3, N'h8je7moevikfkqevsdplgjc404@group.calendar.google.com')
INSERT [dbo].[USER] ([ID], [FirstName], [Avatar], [LastName], [Username], [Hash], [RememberMeToken], [Email], [Phone], [Skype], [CreatedAt], [CreatedBy], [GROUP_ID], [CalendarId]) VALUES (2, N'CRM', NULL, N'Hùng Sales', N'crm.sales10', N'$2a$11$3nkUeBDhnJblmqqyKc70t.oQIQT8bRIvLTg6WaBGHkf48ZUG7bJ.C', NULL, N'crm.sale10@gmail.com', N'0326373667', NULL, CAST(N'2021-06-01T20:37:54.167' AS DateTime), 1, 2, N'7k75inehe0drm577ks936nrdl0@group.calendar.google.com')
INSERT [dbo].[USER] ([ID], [FirstName], [Avatar], [LastName], [Username], [Hash], [RememberMeToken], [Email], [Phone], [Skype], [CreatedAt], [CreatedBy], [GROUP_ID], [CalendarId]) VALUES (3, N'CRM', NULL, N'Anh Tuấn Marketing', N'crm.marketing08', N'$2a$11$dYaVgk.OudDWwzwZpg7md.GIV3laC2BKLwidqR6FeOKPYVOjcUJXO', NULL, N'crm.marketing08@gmail.com', NULL, NULL, CAST(N'2021-06-01T20:38:53.193' AS DateTime), 1, 1, N'c7nl89t7ckaf0tps08so9otd5s@group.calendar.google.com')
SET IDENTITY_INSERT [dbo].[USER] OFF
GO
SET IDENTITY_INSERT [dbo].[LEAD] ON 

INSERT [dbo].[LEAD] ([ID], [Name], [Email], [LeadSource], [AnnualRevenue], [CompanyName], [Fax], [Description], [INDUSTRY_ID], [NoEmail], [LeadOwner], [NoCall], [Phone], [Website], [Avatar], [Skype], [CreatedAt], [CreatedBy], [ModifiedAt], [ModifiedBy], [PRIORITY_ID], [Country], [City], [AddressDetail], [LeadStatus]) VALUES (1, N'Nguyễn Lê Anh Thư', N'crm.lead10@gmail.com', 3, 124280000000, N'Prudential', NULL, NULL, 6, 0, 2, 0, N'0348856601', N'https://www.prudential.com.vn/vi/', NULL, NULL, CAST(N'2021-06-01T20:43:57.613' AS DateTime), 1, CAST(N'2021-06-01T20:43:57.613' AS DateTime), NULL, 3, N'Việt Nam', N'Hà Nội', N'24 Hai Bà Trưng', 8)
SET IDENTITY_INSERT [dbo].[LEAD] OFF
GO

SET IDENTITY_INSERT [dbo].[CAMPAIGN_STATUS] ON 

INSERT [dbo].[CAMPAIGN_STATUS] ([ID], [Name]) VALUES (1, N'Ongoing')
INSERT [dbo].[CAMPAIGN_STATUS] ([ID], [Name]) VALUES (2, N'Planned')
INSERT [dbo].[CAMPAIGN_STATUS] ([ID], [Name]) VALUES (3, N'Cancelled')
SET IDENTITY_INSERT [dbo].[CAMPAIGN_STATUS] OFF
GO
SET IDENTITY_INSERT [dbo].[CAMPAIGN_TYPE] ON 

INSERT [dbo].[CAMPAIGN_TYPE] ([ID], [Name]) VALUES (1, N'Email')
INSERT [dbo].[CAMPAIGN_TYPE] ([ID], [Name]) VALUES (2, N'Partner Program')
INSERT [dbo].[CAMPAIGN_TYPE] ([ID], [Name]) VALUES (3, N'Referral')
INSERT [dbo].[CAMPAIGN_TYPE] ([ID], [Name]) VALUES (4, N'Advertisement')
INSERT [dbo].[CAMPAIGN_TYPE] ([ID], [Name]) VALUES (5, N'Workshop')
INSERT [dbo].[CAMPAIGN_TYPE] ([ID], [Name]) VALUES (6, N'Webinar')
INSERT [dbo].[CAMPAIGN_TYPE] ([ID], [Name]) VALUES (7, N'Conference')
INSERT [dbo].[CAMPAIGN_TYPE] ([ID], [Name]) VALUES (8, N'Product Demo')
SET IDENTITY_INSERT [dbo].[CAMPAIGN_TYPE] OFF
GO
SET IDENTITY_INSERT [dbo].[LOST_REASON] ON 

INSERT [dbo].[LOST_REASON] ([ID], [Reason]) VALUES (1, N'No budget')
INSERT [dbo].[LOST_REASON] ([ID], [Reason]) VALUES (2, N'Insufficient budget')
INSERT [dbo].[LOST_REASON] ([ID], [Reason]) VALUES (3, N'Product not needed')
INSERT [dbo].[LOST_REASON] ([ID], [Reason]) VALUES (4, N'Wrong timing')
INSERT [dbo].[LOST_REASON] ([ID], [Reason]) VALUES (5, N'Better products on the market')
INSERT [dbo].[LOST_REASON] ([ID], [Reason]) VALUES (6, N'Product doesn’t have required features')
INSERT [dbo].[LOST_REASON] ([ID], [Reason]) VALUES (7, N'Lack of Expertise')
INSERT [dbo].[LOST_REASON] ([ID], [Reason]) VALUES (8, N'Wrong Decision Maker')
INSERT [dbo].[LOST_REASON] ([ID], [Reason]) VALUES (9, N'Poor Follow-up')
SET IDENTITY_INSERT [dbo].[LOST_REASON] OFF
GO
SET IDENTITY_INSERT [dbo].[TASK_STATUS] ON 

INSERT [dbo].[TASK_STATUS] ([ID], [Name]) VALUES (1, N'Pending')
INSERT [dbo].[TASK_STATUS] ([ID], [Name]) VALUES (2, N'In Progress')
INSERT [dbo].[TASK_STATUS] ([ID], [Name]) VALUES (3, N'Completed')
INSERT [dbo].[TASK_STATUS] ([ID], [Name]) VALUES (4, N'Scrapped')
INSERT [dbo].[TASK_STATUS] ([ID], [Name]) VALUES (5, N'Open')
SET IDENTITY_INSERT [dbo].[TASK_STATUS] OFF
GO
SET IDENTITY_INSERT [dbo].[CALL_REASON] ON 

INSERT [dbo].[CALL_REASON] ([ID], [Name]) VALUES (1, N'Prospecting')
INSERT [dbo].[CALL_REASON] ([ID], [Name]) VALUES (2, N'Negotiation')
INSERT [dbo].[CALL_REASON] ([ID], [Name]) VALUES (3, N'Advertisement')
INSERT [dbo].[CALL_REASON] ([ID], [Name]) VALUES (4, N'Inquiry')
SET IDENTITY_INSERT [dbo].[CALL_REASON] OFF
GO
SET IDENTITY_INSERT [dbo].[CALL_RESULT] ON 

INSERT [dbo].[CALL_RESULT] ([ID], [Name]) VALUES (1, N'Interested in product')
INSERT [dbo].[CALL_RESULT] ([ID], [Name]) VALUES (2, N'Request to call back')
INSERT [dbo].[CALL_RESULT] ([ID], [Name]) VALUES (3, N'Invalid number')
INSERT [dbo].[CALL_RESULT] ([ID], [Name]) VALUES (4, N'Not interested')
INSERT [dbo].[CALL_RESULT] ([ID], [Name]) VALUES (5, N'Busy line')
SET IDENTITY_INSERT [dbo].[CALL_RESULT] OFF
GO
SET IDENTITY_INSERT [dbo].[CALL_TYPE] ON 

INSERT [dbo].[CALL_TYPE] ([ID], [Name]) VALUES (1, N'Outgoing')
INSERT [dbo].[CALL_TYPE] ([ID], [Name]) VALUES (2, N'Incoming')
INSERT [dbo].[CALL_TYPE] ([ID], [Name]) VALUES (3, N'Missed')
SET IDENTITY_INSERT [dbo].[CALL_TYPE] OFF
GO
SET IDENTITY_INSERT [dbo].[STAGE] ON 

INSERT [dbo].[STAGE] ([ID], [Name], [Probability]) VALUES (1, N'Qualified', 10)
INSERT [dbo].[STAGE] ([ID], [Name], [Probability]) VALUES (2, N'Value Proposition', 20)
INSERT [dbo].[STAGE] ([ID], [Name], [Probability]) VALUES (3, N'Find Key Contacts', 40)
INSERT [dbo].[STAGE] ([ID], [Name], [Probability]) VALUES (4, N'Send Proposal', 50)
INSERT [dbo].[STAGE] ([ID], [Name], [Probability]) VALUES (5, N'Review', 60)
INSERT [dbo].[STAGE] ([ID], [Name], [Probability]) VALUES (6, N'Negotiate', 80)
INSERT [dbo].[STAGE] ([ID], [Name], [Probability]) VALUES (7, N'Won', 100)
INSERT [dbo].[STAGE] ([ID], [Name], [Probability]) VALUES (8, N'Lost', 0)
SET IDENTITY_INSERT [dbo].[STAGE] OFF
GO
SET IDENTITY_INSERT [dbo].[PERMISSION_ORDER] ON 

INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (1, N'User')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (2, N'Task')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (3, N'Tag')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (4, N'Permission')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (5, N'Note')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (6, N'Lead')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (7, N'Deal')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (8, N'Account')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (9, N'Contact')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (10, N'Campaign')
INSERT [dbo].[PERMISSION_ORDER] ([ID], [Name]) VALUES (11, N'Report')
SET IDENTITY_INSERT [dbo].[PERMISSION_ORDER] OFF
GO
SET IDENTITY_INSERT [dbo].[PERMISSION] ON 

INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (1, N'USER_CREATE', N'Permission to create an user', 1)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (2, N'USER_MODIFY', N'Permission to modify an user', 1)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (3, N'USER_DELETE', N'Permission to delete an user', 1)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (4, N'USER_VIEW', N'Permission to view another user''s information', 1)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (5, N'USER_MODIFY_SELF', N'Permission to modify his/her own account information', 1)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (6, N'TASK_CREATE', N'Permission to create a task (Meetings, Calls, Tasks)', 2)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (7, N'TASK_MODIFY', N'Permission to modify a task (Meetings, Calls, Tasks)', 2)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (8, N'TASK_DELETE', N'Permission to delete self task (Meetings, Calls, Tasks)', 2)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (9, N'TASK_VIEW', N'Permission to view a task (Meetings, Calls, Tasks)', 2)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (10, N'TAG_CREATE', N'Permission to create a tag', 3)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (11, N'TAG_DELETE_ITEM', N'Permission to remove a tag from an item', 3)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (12, N'TAG_DELETE', N'Permission to delete a tag', 3)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (13, N'GROUP_CREATE', N'Permission to create a group', 4)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (14, N'GROUP_DELETE', N'Permission to delete a group', 4)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (15, N'GROUP_MODIFY', N'Permission to modify a group', 4)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (16, N'GROUP_ASSIGN_PERMISSION', N'Permission to assign permission to a group', 4)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (17, N'GROUP_VIEW', N'Permission to view a group', 4)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (18, N'NOTE_CREATE', N'Permission to create a note', 5)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (19, N'NOTE_DELETE', N'Permission to delete self note', 5)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (20, N'NOTE_DELETE_ANY', N'Permission to delete any note', 5)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (21, N'LEAD_CREATE', N'Permission to create a lead', 6)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (22, N'LEAD_DELETE', N'Permission to delete a lead', 6)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (23, N'LEAD_MODIFY', N'Permission to modify a lead', 6)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (24, N'LEAD_CONVERT', N'Permission to convert a lead into an account', 6)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (25, N'LEAD_VIEW', N'Permission to view a lead', 6)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (26, N'LEAD_VIEW_LIST', N'Permission to view all leads', 6)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (27, N'DEAL_CREATE', N'Permission to create a deal', 7)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (28, N'DEAL_MODIFY', N'Permission to modify a deal', 7)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (29, N'DEAL_DELETE', N'Permission to delete a deal', 7)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (30, N'DEAL_CHANGE_STAGE', N'Permission to change the stage of a deal', 7)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (31, N'DEAL_VIEW', N'Permission to view a deal', 7)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (32, N'DEAL_VIEW_LIST', N'Permission to view all deals', 7)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (33, N'ACCOUNT_CREATE', N'Permission to create an account', 8)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (34, N'ACCOUNT_MODIFY', N'Permission to modify an account', 8)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (35, N'ACCOUNT_DELETE', N'Permission to delete an account', 8)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (36, N'ACCOUNT_VIEW', N'Permission to view an account', 8)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (37, N'ACCOUNT_VIEW_LIST', N'Permission to view all accounts', 8)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (38, N'CONTACT_CREATE', N'Permission to create a contact', 9)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (39, N'CONTACT_MODIFY', N'Permission to modify a contact', 9)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (40, N'CONTACT_DELETE', N'Permission to delete a contact', 9)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (41, N'CONTACT_VIEW', N'Permission to view a contact', 9)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (42, N'CONTACT_VIEW_LIST', N'Permission to view all contacts', 9)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (43, N'CAMPAIGN_CREATE', N'Permission to create a campaign', 10)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (44, N'CAMPAIGN_MODIFY', N'Permission to modify a campaign', 10)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (45, N'CAMPAIGN_DELETE', N'Permission to delete a campaign', 10)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (46, N'CAMPAIGN_VIEW', N'Permission to view a campaign', 10)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (47, N'CAMPAIGN_VIEW_LIST', N'Permission to view all campaigns', 10)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (48, N'REPORT_VIEW_ALL', N'Permission to view all reports', 11)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (49, N'TASK_DELETE_ANY', N'Permission to delete any task', 2)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (50, N'TASK_MODIFY_ANY', N'Permission to modify any task', 2)
INSERT [dbo].[PERMISSION] ([ID], [Name], [Description], [PERMISSION_ORDER_ID]) VALUES (51, N'USER_VIEW_LIST', N'Permission to view a list of users', 1)
SET IDENTITY_INSERT [dbo].[PERMISSION] OFF
GO
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 5)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 6)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 7)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 8)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 9)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 10)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 11)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 18)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 19)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 21)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 23)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 24)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 25)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 26)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 27)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 28)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 30)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 31)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 32)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 33)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 34)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 36)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 37)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 38)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 39)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 41)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 42)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 46)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 47)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 48)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (1, 51)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 5)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 6)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 7)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 8)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 9)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 10)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 11)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 18)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 19)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 26)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 27)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 28)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 30)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 31)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 32)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 33)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 34)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 36)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 37)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 38)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 39)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 41)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 42)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 47)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 48)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (2, 51)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 1)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 2)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 3)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 4)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 5)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 6)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 7)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 8)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 9)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 10)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 11)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 12)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 13)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 14)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 15)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 16)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 17)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 18)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 19)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 20)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 21)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 22)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 23)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 24)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 25)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 26)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 27)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 28)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 29)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 30)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 31)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 32)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 33)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 34)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 35)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 36)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 37)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 38)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 39)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 40)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 41)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 42)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 43)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 44)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 45)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 46)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 47)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 48)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 49)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 50)
INSERT [dbo].[GROUP_PERMISSION] ([GROUP_ID], [PERMISSION_ID]) VALUES (3, 51)
GO
