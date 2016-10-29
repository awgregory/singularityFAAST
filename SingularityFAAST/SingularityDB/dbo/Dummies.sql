CREATE TABLE [dbo].[Dummies]
(
	[DummyId] INT NOT NULL IDENTITY,
	[Name] VARCHAR(20) NOT NULL,
	[Age] INT NOT NULL,
	[Description] VARCHAR(20) NULL,
	--[DummyTypeId] INT NOT NULL

	CONSTRAINT [PK_Dummies] PRIMARY KEY (DummyId)
	--CONSTRAINT [FK_Dummies_DummyTypes] FOREIGN KEY ([DummyTypeId])
	
)
