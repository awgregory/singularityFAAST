use [singularitydb]

GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (1, N'Hearing')
GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (2, N'Computers and Related')
GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (3, N'Vision')
GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (4, N'Recreational Sports and Leisure')
GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (5, N'Daily Living')
GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (6, N'Learning, Cognition and Development')
GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (7, N'Environmental Adaptations')
GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (8, N'Speech Communication')
GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (15, N'Vehicle Mods and Transportation')
GO
INSERT [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (16, N'Mobility, Seating and Positioning')
GO

declare @categoryid int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Hearing');
declare @categoryid2 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Computers and Related');
declare @categoryid3 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Vision');
declare @categoryid4 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Recreational Sports and Leisure');
declare @categoryid5 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Daily Living');
declare @categoryid6 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Learning, Cognition and Development');
declare @categoryid7 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Environmental Adaptations');
declare @categoryid8 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Speech Communication');
declare @categoryid9 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Vehicle Mods and Transportation');
declare @categoryid10 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'Mobility, Seating and Positioning');

--insert into	inventoryitems			values	('20161106',@categoryid,'invisible hearing aid',200.00,200.00,'superhearing 1000','north east demonstration center',1,'00123456','this hearing aid is awesome!',null,'none'),
--											('20161106',@categoryid2,'modified keyboard',80.00,90.00,'one handed keyboard deluxe','north east demonstration center',0,'99032256','this is a keyboard','1 extension cable','none');

GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (1, N'12-IN-18441', CAST(N'2016-11-06T00:00:00.000' AS DateTime), @categoryid, N'Invisible Hearing Aid', N'Knapp', CAST(200.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), N'SuperHearing 1000', N'North East Demonstration Center', 1, 1, N'00123456', N'This hearing aid is awesome!', NULL, N'none')
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (2, N'12-IN-17993', CAST(N'2016-11-06T00:00:00.000' AS DateTime), @categoryid2, N'Modified Keyboard', N'SnailsNStuff', CAST(80.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), N'One Handed Keyboard Deluxe', N'North East Demonstration Center', 0, 1, N'99032256', N'This is a keyboard', N'1 extension cable', N'none')
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (15, N'12-IN-12340', CAST(N'2016-11-06T00:00:00.000' AS DateTime), @categoryid2, N'ViVo Mouse (3 parts)', N'School Health Corp', CAST(559.98 AS Decimal(18, 2)), CAST(559.98 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, N'23490939', NULL, NULL, N'none')
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (19, N'12-IN-12336', CAST(N'2016-11-06T00:00:00.000' AS DateTime), @categoryid4, N'Thomas The Tank Track', N'School Health Corp', CAST(189.95 AS Decimal(18, 2)), CAST(189.95 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (20, N'12-IN-12337', CAST(N'2016-11-06T00:00:00.000' AS DateTime), @categoryid2, N'Blue 2 Bluetooth Switches', N'School Health Corp', CAST(148.00 AS Decimal(18, 2)), CAST(148.00 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (22, N'12-IN-16382', CAST(N'2016-06-22T00:00:00.000' AS DateTime), @categoryid2, N'4ft  Charger Sync Cable  30-pin', N'Insignia', CAST(14.99 AS Decimal(18, 2)), CAST(14.99 AS Decimal(18, 2)), N'SKU: 5019255', N'Hope Haven', 1, 1, N'16B26A 00603164494', NULL, NULL, NULL)
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (23, N'12-IN-14339', CAST(N'2010-09-29T00:00:00.000' AS DateTime), 8, N'Medium Yellow Jelly Switch', N'Enabling Devices', CAST(54.95 AS Decimal(18, 2)), CAST(54.95 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (24, N'12-IN-12361', CAST(N'2013-04-01T00:00:00.000' AS DateTime), 2, N'Amazon Kindle 3G', N'Amazon', CAST(139.00 AS Decimal(18, 2)), CAST(139.00 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (26, N'12-IN-12406', CAST(N'2014-03-04T00:00:00.000' AS DateTime), 2, N'Learn To Respond Appropriately', N'Autism Resources', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (28, N'12-IN-12802', CAST(N'2014-09-05T00:00:00.000' AS DateTime), 5, N'Sleep Tight Weighted Blankets -S', N'Therapro', CAST(180.00 AS Decimal(18, 2)), CAST(180.00 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (30, N'12-IN-13079', CAST(N'2014-03-04T00:00:00.000' AS DateTime), 5, N'Sound Sensitive Crab', N'Enabling Devices', CAST(59.99 AS Decimal(18, 2)), CAST(59.99 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[InventoryItems] ([InventoryItemId], [AutoInventoryId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (31, N'12-IN-12783', CAST(N'2014-03-04T00:00:00.000' AS DateTime), 6, N'Reading Guide Strips- Variety Pack', NULL, CAST(7.49 AS Decimal(18, 2)), CAST(7.49 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL)
GO

go
set identity_insert [dbo].[clientcategories] on 

go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (1, N'individual with disability')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (2, N'family guardian or authorized rep')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (3, N'reps of education')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (4, N'reps of employment')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (5, N'reps of community living')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (6, N'reps of technology')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (7, N'health allied health and rehab')
go
set identity_insert [dbo].[clientcategories] off
go
set identity_insert [dbo].[clients] on 

go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (2, 1, cast(N'2016-11-02t00:00:00.000' as datetime), N'david', null, N'teske', N'po box 909', null, N'north carolina', N'nc', N'28711', N'davie', N'37059     ', N'advance', null, null, null, null, null, null, 1, null, 2)
go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (3, 1, cast(N'2016-11-03t00:00:00.000' as datetime), N'jeanette', null, N'otero', N'5734 w 57th way', null, N'florida', N'fl', N'33409', N'palm beach', N'12099     ', N'west palm beach', null, null, null, null, null, null, 1, null, 2)
go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (4, 1, cast(N'2016-11-03t00:00:00.000' as datetime), N'bruce', null, N'stayer', N'134 deanna dr', null, N'florida', N'fl', N'33852', N'highlands', N'12055     ', N'lake placid', null, null, null, null, null, null, 1, null, 1)
go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (6, 1, cast(N'2016-11-03t00:00:00.000' as datetime), N'bruce', null, N'stayer', N'134 deanna dr', null, N'florida', N'fl', N'33852', N'highlands', N'12055     ', N'lake placid', null, null, null, null, null, null, 1, null, 2)
go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (7, 1, cast(N'2016-11-03t00:00:00.000' as datetime), N'loriN', null, N'andersoN', N'15 b cherry ridge dr', null, N'florida', N'fl', N'32746', N'seminole', N'12117     ', N'lake mary', null, null, null, null, null, null, 1, null, 2)
go
set identity_insert [dbo].[clients] off
go

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
		   (3,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger')
		   (4,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger')
		   (5,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger')
		   (6,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger') 
GO