CREATE TABLE [dbo].[DisabilityCategories]
(
	[DisabilityCategoryId] INT NOT NULL IDENTITY(1,1), 
    [DisabilityType] VARCHAR(50) NOT NULL,

	CONSTRAINT [PK_DisabilityCategories] PRIMARY KEY ([DisabilityCategoryId])
)
