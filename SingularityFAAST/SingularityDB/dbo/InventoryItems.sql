CREATE TABLE [dbo].[InventoryItems]
(
	[InventoryItemId]		INT				NOT NULL	IDENTITY(1,1), 
    [DatePurchased]			DATETIME		NOT NULL, 
    [InventoryItemCategory] VARCHAR(50)		NOT NULL, 
    [ItemName]				VARCHAR(80)		NOT NULL, 
    [PricePaid]				MONEY			NOT NULL,
	[RetailCost]			MONEY			NOT NULL, 
    [SubCategory]			VARCHAR(50)		NULL, 
    [ModelOfItem]			VARCHAR(50)		NULL, 
    [Location]				VARCHAR(50)		NULL, 
    [Availability]			BIT				NOT NULL, 
--serial numbers have a max size of 14 characters and can contain numbers letters and special characters
	[SerialNumber]			VARCHAR(14)		NOT NULL , 
    [Description]			VARCHAR(MAX)	NULL, 
    [Accessories]			VARCHAR(150)	NULL,

	CONSTRAINT	[PK_InventoryItemId]		PRIMARY KEY	([InventoryItemId]),
	CONSTRAINT	[FK_InventoryItemCategory]	FOREIGN KEY	([InventoryItemCategory])
		REFERENCES	dbo.InventoryItemCategories (InvCategoryName),
	CONSTRAINT	[FK_InventoryItemSubCategory]	FOREIGN KEY	([SubCategory])
		REFERENCES	dbo.InventoryItemSubCategories (InvSubCategoryName)
)