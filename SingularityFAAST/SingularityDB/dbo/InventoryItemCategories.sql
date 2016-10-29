CREATE TABLE [dbo].[InventoryItemCategories]
(
	[InventoryCategoryId] INT NOT NULL IDENTITY(1,1), 
    [CategoryName] VARCHAR(50) NOT NULL,
	--[SubCategory] VARCHAR(50) NULL, 

    CONSTRAINT [PK_InventoryCategories] PRIMARY KEY ([InventoryCategoryId]),
	--CONSTRAINT	[FK_InventoryItemSubCategory]	FOREIGN KEY	([SubCategory])
	--	REFERENCES	dbo.InventoryItemSubCategories (InvSubCategoryName)
)
