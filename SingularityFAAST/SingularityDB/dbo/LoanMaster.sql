CREATE TABLE [dbo].[LoanMaster]
(
	[LoanMasterId] INT NOT NULL IDENTITY(1,1),
    [DateCreated] DATETIME NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [ClientId] INT NOT NULL, 
    [LoanCategoryId] INT NOT NULL, 
    [InventoryItemId] INT NOT NULL,
	[LoanNumber] VARCHAR(20) NOT NULL,

	CONSTRAINT [PK_LoanMaster] PRIMARY KEY ([LoanMasterId]),	 
    CONSTRAINT [FK_LoanMaster_Clients] FOREIGN KEY ([ClientId]) REFERENCES dbo.Clients ([ClientId]),
	CONSTRAINT [FK_LoanMaster_InventoryItems] FOREIGN KEY ([InventoryItemId]) REFERENCES dbo.InventoryItems ([InventoryItemId]),
	CONSTRAINT [FK_LoanMaster_LoanCategories] FOREIGN KEY ([LoanCategoryId]) REFERENCES dbo.LoanCategories ([LoanCategoryId])
	
)
