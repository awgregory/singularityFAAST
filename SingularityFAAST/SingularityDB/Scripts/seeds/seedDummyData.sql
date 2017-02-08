use [singularitydb]

insert into inventoryitemcategories values 	('hearing aids'),
											('technology');

declare @categoryid int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'hearing aids');
declare @categoryid2 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'technology');

insert into	inventoryitems			values	('20161106',@categoryid,'invisible hearing aid',200.00,200.00,'superhearing 1000','north east demonstration center',1,'00123456','this hearing aid is awesome!',null,'none'),
											('20161106',@categoryid2,'modified keyboard',80.00,90.00,'one handed keyboard deluxe','north east demonstration center',0,'99032256','this is a keyboard','1 extension cable','none');







SET IDENTITY_INSERT [dbo].[ClientCategories] ON 
INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (1, N'Individual with Disability')
INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (2, N'Family Guardian or Authorized Rep')
INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (3, N'Reps of Education')
INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (4, N'Reps of Employment')
INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (5, N'Reps of Community Living')
INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (6, N'Reps of Technology')
INSERT [dbo].[ClientCategories] ([ClientCategoryId], [Type]) VALUES (7, N'Health Allied Health and Rehab')
SET IDENTITY_INSERT [dbo].[ClientCategories] OFF



SET IDENTITY_INSERT [dbo].[DisabilityCategories] ON 
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (1, N'Cognitive Impairment')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (2, N'Hearing Impairment')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (3, N'Visual Impairment')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (4, N'Deaf-Blind')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (5, N'Learning Disability')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (6, N'No Impairment')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (7, N'Dexterity Impairment')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (8, N'Mobility Impairment')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (9, N'Spinal Cord Injury')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (10, N'Elderly')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (11, N' Speech/Language Impairment')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (12, N'Traumatic Brain Injury')
INSERT [dbo].[DisabilityCategories] ([DisabilityCategoryId], [DisabilityType]) VALUES (13, N'Other')
SET IDENTITY_INSERT [dbo].[DisabilityCategories] OFF


SET IDENTITY_INSERT [dbo].[Clients] ON 
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (2, 1, CAST(N'2016-11-02T00:00:00.0000000' AS DateTime2), N'David', NULL, N'Teske', N'PO Box 909', NULL, N'North Carolina', N'NC', N'28711', N'Davie', N'37059     ', N'Advance', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2)
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (3, 1, CAST(N'2016-11-03T00:00:00.0000000' AS DateTime2), N'Jeanette', NULL, N'Otero', N'5734 W 57th Way', NULL, N'Florida', N'FL', N'33409', N'Palm Beach', N'12099     ', N'West Palm Beach', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2)
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (4, 1, CAST(N'2016-11-03T00:00:00.0000000' AS DateTime2), N'Bruce', NULL, N'Stayer', N'134 Deanna Dr', NULL, N'Florida', N'FL', N'33852', N'Highlands', N'12055     ', N'Lake Placid', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 1)
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (6, 1, CAST(N'2016-11-03T00:00:00.0000000' AS DateTime2), N'Bruce', NULL, N'Stayer', N'134 Deanna Dr', NULL, N'Florida', N'FL', N'33852', N'Highlands', N'12055     ', N'Lake Placid', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2)
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (7, 1, CAST(N'2016-11-03T00:00:00.0000000' AS DateTime2), N'Lorin', NULL, N'Anderson', N'15 B Cherry Ridge Dr', NULL, N'Florida', N'FL', N'32746', N'Seminole', N'12117     ', N'Lake Mary', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2)
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (9, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Tom', N'T', N'Stevens', N'1847 Reynolds Dr', NULL, N'Florida', N'FL', N'32256', N'Duval', NULL, N'Jacksonville', NULL, NULL, NULL, NULL, NULL, N'Mr', 0, N'Hearing Disabled', 2)
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (10, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Chad', NULL, N'Stevens', N'1847 Reynolds Dr', NULL, N'Florida', N'FL', N'32256', N'Duval', NULL, N'Jacksonville', NULL, NULL, NULL, NULL, NULL, N'Mr', 0, N'Hearing Disabled', 1)
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (11, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'John', NULL, N'Mack', N'45761 Youmen dr', NULL, N'Florida', N'FL', N'32247', NULL, NULL, N'Jacksonville', NULL, NULL, NULL, NULL, NULL, NULL, 0, N'Blah', 1)
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (12, 0, CAST(N'2017-01-14T15:56:33.8972760' AS DateTime2), N'Tom', N'Y', N'Meyers', N'1234 Main St', NULL, N'Florida', N'FL', N'32210', N'Duval', NULL, N'Jacksonville', N'tm@yahoo.com', NULL, N'9049999999', NULL, N'Beeline', N'Mr', 0, N'new client', 2)
INSERT [dbo].[Clients] ([ClientId], [Active], [DateCreated], [FirstName], [MiddleInitial], [LastName], [Address1], [Address2], [StateName], [StateCode], [Zip], [County], [CountyFIPS], [City], [Email], [HomePhone], [CellPhone], [WorkPhone], [Company], [Title], [LoanEligibility], [Notes], [ClientCategoryId]) VALUES (13, 0, CAST(N'2017-01-14T15:56:45.4448770' AS DateTime2), N'Tom', N'Y', N'Meyers', N'1234 Main St', NULL, N'Florida', N'FL', N'32210', N'Duval', NULL, N'Jacksonville', N'tm@yahoo.com', NULL, N'9049999999', NULL, N'Beeline', N'Mr', 0, N'new client', 1)
SET IDENTITY_INSERT [dbo].[Clients] OFF


INSERT [dbo].[ClientDisabilities] ([DisabilityCategoryId], [ClientId]) VALUES (1, 2)
INSERT [dbo].[ClientDisabilities] ([DisabilityCategoryId], [ClientId]) VALUES (2, 3)
INSERT [dbo].[ClientDisabilities] ([DisabilityCategoryId], [ClientId]) VALUES (3, 4)







SET IDENTITY_INSERT [dbo].[LoanMasters] ON  
GO
INSERT INTO [dbo].[LoanMasters] ([LoanMasterID],[DateCreated],[IsActive],[ClientId],[LoanCategoryId],[InventoryItemId],[LoanNumber])
     VALUES						(1, GETDATE(),		1,			3,			1,				1,				15499),
								(2, GETDATE(),		1,			6,			1,				2,				24700)
GO 

SET IDENTITY_INSERT [dbo].[LoanMasters] ON
GO

SET IDENTITY_INSERT [dbo].[LoanDetails] OFF   
GO

INSERT INTO [dbo].[LoanDetails]([LoanMasterId],[LoanDate],[LoanDuration],[Purpose],[PurposeType],[ClientOutcome],[Notes])
     VALUES
           (1,GETDATE(),28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger'),
		   (3,GETDATE(),28,'Assist in decision making (device trial or evaluation)','Work','AT will meet needs','batteries and cards included')
		   --(3,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger')
		   --(4,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger')
		   --(5,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger')
		   --(6,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger') 
GO