CREATE TABLE [dbo].[InventoryItems]
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
--serial numbers have a max size of 14 characters and can contain numbers letters and special characters
	[SerialNumber]			 VARCHAR(35)		NULL , 
    [Description]			 VARCHAR(MAX)		NULL, 
    [Accessories]			 VARCHAR(150)		NULL,
	[Damages]				 VARCHAR(200)		NULL, 

    CONSTRAINT	[PK_InventoryItemId]		PRIMARY KEY	([InventoryItemId]),
	CONSTRAINT	[FK_InventoryItems_InventoryItemCategories]	FOREIGN KEY	([InventoryCategoryId])
		REFERENCES	dbo.InventoryItemCategories ([InventoryCategoryId])
)