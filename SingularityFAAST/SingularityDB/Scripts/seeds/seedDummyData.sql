--USE [SingularityDB]

--INSERT INTO InventoryItemCategories VALUES 	('Hearing Aids'),
--											('Technology');

--DECLARE @categoryId INT = (SELECT cat.InventoryCategoryId FROM InventoryItemCategories AS cat WHERE cat.CategoryName LIKE 'Hearing Aids');
--DECLARE @categoryId2 INT = (SELECT cat.InventoryCategoryId FROM InventoryItemCategories AS cat WHERE cat.CategoryName LIKE 'Technology');

--INSERT INTO	InventoryItems			VALUES	('20161106',@categoryId,'Invisible Hearing Aid',200.00,200.00,'SuperHearing 1000','North East Demonstration Center',1,'00123456','This hearing aid is awesome!',NULL,'none'),
--											('20161106',@categoryId2,'Modified Keyboard',80.00,90.00,'One Handed Keyboard Deluxe','North East Demonstration Center',0,'99032256','This is a keyboard','1 extension cable','none');


--GO
--SET IDENTITY_INSERT [dbo].[ClientCategories] ON 

--GO
--INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (1, N'Individual with Disability')
--GO
--INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (2, N'Family Guardian or Authorized Rep')
--GO
--INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (3, N'Reps of Education')
--GO
--INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (4, N'Reps of Employment')
--GO
--INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (5, N'Reps of Community Living')
--GO
--INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (6, N'Reps of Technology')
--GO
--INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (7, N'Health Allied Health and Rehab')
--GO
--SET IDENTITY_INSERT [dbo].[ClientCategories] OFF
--GO
--SET IDENTITY_INSERT [dbo].[Clients] ON 

--GO
--INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (2, 1, CAST(N'2016-11-02T00:00:00.000' AS DateTime), N'David', NULL, N'Teske', N'PO Box 909', NULL, N'North Carolina', N'NC', N'28711', N'Davie', N'37059     ', N'Advance', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2)
--GO
--INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (3, 1, CAST(N'2016-11-03T00:00:00.000' AS DateTime), N'Jeanette', N' ', N'Otero', N'5734 W 57th Way', NULL, N'Florida', N'FL', N'33409', N'Palm Beach', N'12099     ', N'West Palm Beach', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2)
--GO
--INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (4, 1, CAST(N'2016-11-03T00:00:00.000' AS DateTime), N'Bruce', NULL, N'Stayer', N'134 Deanna Dr', NULL, N'Florida', N'FL', N'33852', N'Highlands', N'12055     ', N'Lake Placid', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 1)
--GO
--INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (6, 1, CAST(N'2016-11-03T00:00:00.000' AS DateTime), N'Bruce', NULL, N'Stayer', N'134 Deanna Dr', NULL, N'Florida', N'FL', N'33852', N'Highlands', N'12055     ', N'Lake Placid', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2)
--GO
--INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (7, 1, CAST(N'2016-11-03T00:00:00.000' AS DateTime), N'Lorin', N' ', N'Anderson', N'15 B Cherry Ridge Dr', NULL, N'Florida', N'FL', N'32746', N'Seminole', N'12117     ', N'Lake Mary', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2)
--GO
--SET IDENTITY_INSERT [dbo].[Clients] OFF
--GO
