CREATE TABLE [dbo].[LoanDetails]
(
	[LoanDetailId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [LoanMasterId] INT NOT NULL,

	[LoanDate] DATETIME NOT NULL, 
    [LoanDuration] INT NOT NULL DEFAULT 28, 
    [InventoryItemId] INT NOT NULL,
	[Purpose] VARCHAR(80) NOT NULL,
    [PurposeType] VARCHAR(25) NOT NULL, 
	[ClientOutcome] VARCHAR(30) NULL,
    [Notes] VARCHAR(MAX) NULL, 
    CONSTRAINT [FK_LoanDetails_LoanMasters] FOREIGN KEY ([LoanMasterId]) REFERENCES dbo.LoanMasters([LoanMasterId]),
	CONSTRAINT [FK_LoanDetails_InventoryItems] FOREIGN KEY ([InventoryItemId]) REFERENCES dbo.InventoryItems ([InventoryItemId]),
)
