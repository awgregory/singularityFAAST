

IF OBJECT_ID('tempdb..#tempInventoryItems') IS NOT NULL DROP TABLE #tempInventoryItems 



CREATE TABLE #tempInventoryItems
(
	[InventoryItemId]		 INT				NOT NULL	IDENTITY(1,1), 
	[DatePurchased]			 DATETIME			NOT NULL, 
    [InventoryCategoryId]	 INT				NOT NULL,
    [ItemName]				 VARCHAR(80)		NOT NULL,
	[Manufacturer]			 VARCHAR(80)		NULL,
    [PricePaid]				 DECIMAL(18, 2)		NOT NULL,
	[RetailCost]			 DECIMAL(18, 2)		NOT NULL,
    [ModelName]				 VARCHAR(50)		NULL, 
    [Location]				 VARCHAR(50)		NULL, 
    [Availability]			 BIT				NOT NULL,
	[Active]				 BIT				NULL,
	[SerialNumber]			 VARCHAR(35)		NULL , 
    [Description]			 VARCHAR(MAX)		NULL, 
    [Accessories]			 VARCHAR(150)		NULL,
	[Damages]				 VARCHAR(200)		NULL, 

)


INSERT INTO #tempInventoryItems ([DatePurchased], [InventoryCategoryId], [ItemName], [Manufacturer], [PricePaid], [RetailCost], [ModelName], [Location], [Availability], [Active], [SerialNumber], [Description], [Accessories], [Damages]) 
VALUES 

(N'2017-01-21 00:00:00', 4, N'Amplified Business Phone', N'Harris Communications', CAST(50.00 AS Decimal(18, 2)), CAST(50.00 AS Decimal(18, 2)), N'busPhone1', N'Northeast Regional Center', 0, 1, N'82710946', N'A desk phone with a loud, high pitched ringtone.', NULL, NULL),
(N'2017-02-07 00:00:00', 10, N'3 Alert Timer Flashing Red light', N'Harris Communications', CAST(60.00 AS Decimal(18, 2)), CAST(75.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 0, 1, N'97462091', N'', NULL, NULL),
(N'2016-11-19 00:00:00', 2, N'Echo Smart Pen', N'ECHO', CAST(159.00 AS Decimal(18, 2)), CAST(159.00 AS Decimal(18, 2)), N'Smart Pen', N'Northeast Regional Center', 0, 1, N'88888888', NULL, NULL, NULL),
(N'2016-06-12 00:00:00', 2, N'Super Shell Blue iPad 2 Case', N'Info Grip', CAST(34.00 AS Decimal(18, 2)), CAST(34.00 AS Decimal(18, 2)), N'SUPERSB', N'Northeast Regional Center', 0, 1, N'21390932', NULL, NULL, NULL),
(N'2016-02-02 00:00:00', 5, N'Solutions for Students with Autisum - Test Me SolarSystem CD', N'Enable Mart', CAST(30.00 AS Decimal(18, 2)), CAST(45.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 0, 0, NULL, NULL, NULL, NULL),
(N'2017-03-01 00:00:00', 8, N'PocketTalker Ultra', N'Maxi Aids', CAST(129.95 AS Decimal(18, 2)), CAST(129.85 AS Decimal(18, 2)), N'Williams S189', N'Northeast Regional Center', 0, 1, N'KB3262012', N'A speech communication device', NULL, NULL),
(N'2016-06-27 00:00:00', 1, N'iPad 16G WIFI', N'Apple', CAST(500.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 0, 1, N'KB01213001', N'An Apple Tablet', NULL, NULL),
(N'2016-06-27 00:00:00', 1, N'iPad 2 16G WIFI', N'Apple', CAST(600.00 AS Decimal(18, 2)), CAST(600.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 0, 0, N'KB98391817', N'An Apple Tablet', NULL, N'Broken'),
(N'2017-02-23 00:00:00', 3, N'Amazon Echo', N'Amazon', CAST(177.99 AS Decimal(18, 2)), CAST(177.99 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 0, 1, N'B0F0071560940029', NULL, NULL, NULL),
(N'2016-12-23 00:00:00', 6, N'Adjustable Foot Rest', N'Info grip', CAST(80.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 0, 1, N'83779893', NULL, NULL, NULL),
(N'2016-07-25 00:00:00', 7, N'Green Weighted Vest', N'Ablenet', CAST(17.00 AS Decimal(18, 2)), CAST(17.00 AS Decimal(18, 2)), N'vest-O-Matic 1000', N'Northeast Regional Center', 0, 1, N'12344', N'A weight vest', NULL, NULL),
(N'2016-09-29 00:00:00', 9, N'Garmin 50 GPS w/ Dash Holder, charger', N'Garmin', CAST(127.99 AS Decimal(18, 2)), CAST(127.99 AS Decimal(18, 2)), NULL, N'Northeast Regional Center', 1, 1, N'KB04232012', N'A Garmin Navigation System', N'Charging Cable and Suction Cup Mount', N'Small Chip on the GPS body, device still functional'),
(N'2016-09-29 00:00:00', 2, N'Modified Keyboard', N'SnailsNStuff', CAST(80.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), N'One Handed Keyboard Deluxe', N'North East Demonstration Center', 0, 1, N'99032256', N'This is a keyboard', N'1 extension cable', N'none'),
(N'2016-09-29 00:00:00', 2, N'ViVo Mouse (3 parts)', N'School Health Corp', CAST(559.98 AS Decimal(18, 2)), CAST(559.98 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, N'23490939', NULL, NULL, N'none'),
(N'2016-09-29 00:00:00', 4, N'Thomas The Tank Track', N'School Health Corp', CAST(189.95 AS Decimal(18, 2)), CAST(189.95 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL),
(N'2016-09-29 00:00:00', 2, N'Blue 2 Bluetooth Switches', N'School Health Corp', CAST(148.00 AS Decimal(18, 2)), CAST(148.00 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL),
(N'2016-09-29 00:00:00', 2, N'4ft  Charger Sync Cable  30-pin', N'Insignia', CAST(14.99 AS Decimal(18, 2)), CAST(14.99 AS Decimal(18, 2)), N'SKU: 5019255', N'Hope Haven', 1, 1, N'16B26A 00603164494', NULL, NULL, NULL),
(N'2016-09-29 00:00:00', 8, N'Medium Yellow Jelly Switch', N'Enabling Devices', CAST(54.95 AS Decimal(18, 2)), CAST(54.95 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL),
(N'2016-09-29 00:00:00', 2, N'Amazon Kindle 3G', N'Amazon', CAST(139.00 AS Decimal(18, 2)), CAST(139.00 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL),
(N'2016-09-29 00:00:00', 2, N'Learn To Respond Appropriately', N'Autism Resources', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL),
(N'2016-06-27 00:00:00', 5, N'Sleep Tight Weighted Blankets -S', N'Therapro', CAST(180.00 AS Decimal(18, 2)), CAST(180.00 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL),
(N'2016-06-27 00:00:00', 5, N'Sound Sensitive Crab', N'Enabling Devices', CAST(59.99 AS Decimal(18, 2)), CAST(59.99 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL),
(N'2016-06-27 00:00:00', 6, N'Reading Guide Strips- Variety Pack', N'School Health Corp', CAST(7.49 AS Decimal(18, 2)), CAST(7.49 AS Decimal(18, 2)), NULL, N'Hope Haven', 1, 1, NULL, NULL, NULL, NULL)


MERGE dbo.InventoryItems AS target
USING #tempInventoryItems AS source

	ON source.InventoryItemId = target.InventoryItemId

WHEN NOT MATCHED THEN 
	INSERT (DatePurchased, InventoryCategoryId, ItemName, Manufacturer, PricePaid, RetailCost, ModelName, Location, Availability, Active, SerialNumber, Description, Accessories, Damages)
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
