CREATE TABLE [dbo].[LoanMasters]
(
	[LoanMasterId] INT NOT NULL IDENTITY(1,1),
    [DateCreated] DATETIME NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [ClientId] INT NOT NULL, 
    [LoanCategoryId] INT NOT NULL, 
    [InventoryItemId] INT NOT NULL,
	--[InventoryItemId2] INT NOT NULL,
	--[InventoryItemId3] INT NOT NULL,
	--[InventoryItemId4] INT NOT NULL,
	--[InventoryItemId5] INT NOT NULL,
	--[InventoryItemId6] INT NOT NULL,
	[LoanNumber] VARCHAR(20) NOT NULL,

	CONSTRAINT [PK_LoanMasters] PRIMARY KEY ([LoanMasterId]),	 
    CONSTRAINT [FK_LoanMasters_Clients] FOREIGN KEY ([ClientId]) REFERENCES dbo.Clients ([ClientId]),
	CONSTRAINT [FK_LoanMasters_InventoryItems1] FOREIGN KEY ([InventoryItemId]) REFERENCES dbo.InventoryItems ([InventoryItemId]),
	--CONSTRAINT [FK_LoanMasters_InventoryItems2] FOREIGN KEY ([InventoryItemId2]) REFERENCES dbo.InventoryItems ([InventoryItemId]),
	--CONSTRAINT [FK_LoanMasters_InventoryItems3] FOREIGN KEY ([InventoryItemId3]) REFERENCES dbo.InventoryItems ([InventoryItemId]),
	--CONSTRAINT [FK_LoanMasters_InventoryItems4] FOREIGN KEY ([InventoryItemId4]) REFERENCES dbo.InventoryItems ([InventoryItemId]),
	--CONSTRAINT [FK_LoanMasters_InventoryItems5] FOREIGN KEY ([InventoryItemId5]) REFERENCES dbo.InventoryItems ([InventoryItemId]),
	--CONSTRAINT [FK_LoanMasters_InventoryItems6] FOREIGN KEY ([InventoryItemId6]) REFERENCES dbo.InventoryItems ([InventoryItemId]),
	
	CONSTRAINT [FK_LoanMasters_LoanCategories] FOREIGN KEY ([LoanCategoryId]) REFERENCES dbo.LoanCategories ([LoanCategoryId])
	
)
