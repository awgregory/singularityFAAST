--DBCC CHECKIDENT('dbo.InventoryItems', RESEED, 0)
-- Note on Reseed command above. This resets Identity columns  
-- Only use this command if you are reseeding a table which you previously had records in and then remove
-- For example if you previously made your own clients table with records and now are re-populating it with these records
-- Don't use the command above on a table that never had data

IF OBJECT_ID('tempdb..#tempInventoryItems') IS NOT NULL DROP TABLE #tempInventoryItems 

SET IDENTITY_INSERT [dbo].[InventoryItems] ON
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (1, N'2017-01-21 00:00:00', 4, N'Amplified Business Phone', N'Harris Communications', CAST(50.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), N'busPhone1', N'Northeast Regional Center', 1, 1, N'82710946', N'A desk phone with a loud, high pitched ringtone.', NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (2, N'2017-02-07 00:00:00', 10, N'3 Alert Timer Flashing Red light
', N'Harris Communications', CAST(60.00 AS Decimal(18, 2)), CAST(75.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 1, 1, N'97462091', N'', NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (3, N'2016-11-19 00:00:00', 2, N'Echo Smart Pen', N'ECHO', CAST(159.00 AS Decimal(18, 2)), CAST(159.00 AS Decimal(18, 2)), N'Smart Pen', N'Northeast Regional Center', 0, 1, N'88888888', NULL, NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (4, N'2016-06-12 00:00:00', 2, N'Super Shell Blue iPad 2 Case', N'Info Grip', CAST(34.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)), N'SUPERSB', N'Northeast Regional Center', 1, 1, N'21390932', NULL, NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (6, N'2016-02-02 00:00:00', 5, N'Solutions for Students with Autisum - Test Me SolarSystem CD
', N'Enable Mart', CAST(30.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 0, 0, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (7, N'2017-03-01 00:00:00', 8, N'PocketTalker Ultra
', N'Maxi Aids', CAST(129.95 AS Decimal(18, 2)), CAST(129.85 AS Decimal(18, 2)), N'Williams S189', N'Northeast Regional Center', 1, 1, N'KB3262012
', N'A speech communication device', NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (8, N'2016-06-27 00:00:00', 1, N'iPad 16G WIFI', N'Apple', CAST(500.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 0, 1, N'KB01213001', N'An Apple Tablet', NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (9, N'2016-06-27 00:00:00', 1, N'iPad 2 16G WIFI', N'Apple', CAST(600.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 0, 0, N'KB98391817', N'An Apple Tablet', NULL, N'Broken')
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (12, N'2017-02-23 00:00:00', 3, N'Amazon Echo', N'Amazon', CAST(177.99 AS Decimal(18, 2)), CAST(177.99 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 1, 1, N'B0F0071560940029
', NULL, NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (14, N'2016-12-23 00:00:00', 6, N'Adjustable Foot Rest', N'Info gri[', CAST(80.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 1, 1, N'83779893', NULL, NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (15, N'2016-07-25 00:00:00', 7, N'Green Weighted Vest', N'Ablenet', CAST(17.00 AS Decimal(18, 2)), CAST(17.00 AS Decimal(18, 2)), N'vest-O-Matic 1000', N'Northeast Regional Center', 0, 1, N'12344', N'A weight vest', NULL, NULL)
INSERT INTO [dbo].[InventoryItems] ([InventoryItemId], [DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) VALUES (16, N'2016-09-29 00:00:00', 9, N'Garmin 50 GPS w/ Dash Holder, charger', N'Garmin', CAST(127.99 AS Decimal(18, 2)), CAST(127.99 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 1, 1, N'KB04232012', N'A Garmin Navigation System', N'Charging Cable and Suction Cup Mount', N'Small Chip on the GPS body, device still functional')
SET IDENTITY_INSERT [dbo].[InventoryItems] OFF


MERGE dbo.InventoryItems AS target
USING #tempInventoryItems AS source

	ON source.InventoryItemId = target.InventoryItemId

WHEN NOT MATCHED THEN 
	INSERT (DatePurchased, InventoryCategoryId, ItemName, Manufacturer, PricePaid, RetailCost, ModelName, Location, Availability, Active, SerialNumber,
	Description, Accessories, Damages)
	VALUES (source.DatePurchased, source.InventoryCategoryId, source.ItemName, source.Manufacturer, source.PricePaid, source.RetailCost,
	source.ModelName, source.Location, source.Availability, source.Active, source.SerialNumber, source.Description, source.Accessories, 
	source.Damages)

WHEN MATCHED THEN UPDATE 
SET 
	
	target.DatePurchased = source.DatePurchased,
	target.InventoryCategoryId = source.InventoryCategoryId,
	target.ItemName = source.ItemName,
	target.Manufacturer = source.Manufacturer,
	target.PricePaid = source.PricePaid,
	target.RetailCost = source.RetailCost,
	target.ModelName = source.ModelName,
	target.Location = source.Location,
	target.Availability = source.Availability,
	target.Active = source.Active,
	target.SerialNumber = source.SerialNumber,
	target.Description = source.Description,
	target.Accessories = source.Accessories,
	target.Damages = source.Damages;

DROP TABLE #tempInventoryItems
