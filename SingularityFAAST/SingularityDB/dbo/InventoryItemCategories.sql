CREATE TABLE [dbo].[InventoryItemCategories]
(
	[InventoryCategoryId] INT NOT NULL IDENTITY(1,1), 
    [InvCategoryName] VARCHAR(50) NOT NULL,

	CONSTRAINT [PK_InventoryCategories] PRIMARY KEY ([InventoryCategoryId])
)
