CREATE TABLE [dbo].[InventoryItemSubCategories]
(
	[InvSubCategoryId] INT NOT NULL IDENTITY(1,1), 
    [InvSubCategoryName] VARCHAR(50) NOT NULL,

	CONSTRAINT [PK_InventorySubCategories] PRIMARY KEY	([InvSubCategoryId])
)
