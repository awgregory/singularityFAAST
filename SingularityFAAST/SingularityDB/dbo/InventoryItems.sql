CREATE TABLE [dbo].[InventoryItems]
(
	[InventoryItemId] INT NOT NULL PRIMARY KEY, 
    [DatePurchased] DATETIME NOT NULL, 
    [InventoryItemCategory] VARCHAR(50) NOT NULL
)
