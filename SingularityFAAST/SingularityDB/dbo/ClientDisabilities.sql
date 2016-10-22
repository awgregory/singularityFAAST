CREATE TABLE [dbo].[ClientDisabilities]
(
	[DisabilityCategoryId] INT NOT NULL, 
    [ClientId] INT NOT NULL,

	CONSTRAINT [PK_ClientDisabilities] PRIMARY KEY ([DisabilityCategoryId], [ClientId]),

	CONSTRAINT [FK_ClientDisabilities_Clients] FOREIGN KEY ([ClientId])
	REFERENCES dbo.Clients ([ClientId]),

	CONSTRAINT [FK_ClientDisabilities_DisabilityCategories] FOREIGN KEY ([DisabilityCategoryId])
	REFERENCES dbo.DisabilityCategories ([DisabilityCategoryId])
)
