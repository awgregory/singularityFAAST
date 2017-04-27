CREATE TABLE [dbo].[ClientCategories]
(
	[ClientCategoryId] INT NOT NULL IDENTITY(1,1), 
    [Type] VARCHAR(50) NOT NULL

	CONSTRAINT [PK_ClientCategories] PRIMARY KEY ([ClientCategoryId])
)
