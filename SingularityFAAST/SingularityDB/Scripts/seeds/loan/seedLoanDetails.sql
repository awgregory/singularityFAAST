IF OBJECT_ID('tempdb..#tempLoanDetails') IS NOT NULL DROP TABLE #tempLoanDetails 

CREATE TABLE #tempLoanDetails
(
	[LoanDetailId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [LoanMasterId] INT NOT NULL,

	[LoanDate] DATE NOT NULL, 
    [LoanDuration] INT NOT NULL DEFAULT 28, 
    [InventoryItemId] INT NOT NULL,
	[Purpose] VARCHAR(60) NOT NULL,
    [PurposeType] VARCHAR(25) NOT NULL, 
	[ClientOutcome] VARCHAR(30) NOT NULL,
    [Notes] VARCHAR(MAX) NULL, 
    CONSTRAINT [FK_LoanDetails_LoanMasters] FOREIGN KEY ([LoanMasterId]) REFERENCES dbo.LoanMasters([LoanMasterId]),
	CONSTRAINT [FK_LoanDetails_InventoryItems] FOREIGN KEY ([InventoryItemId]) REFERENCES dbo.InventoryItems ([InventoryItemId]),
)

INSERT INTO #tempLoanDetails([DateCreated],[ClientId],[IsActive],[LoanNumber])
     VALUES						(GETDATE(),		3,			1,			15499),
								(GETDATE(),		6,			1,			24700),
								(GETDATE(),2, 1, 3454),
								(GETDATE(),9, 1, 457),
								(GETDATE(),12, 1, 2222),
								(GETDATE(),7, 1, 46859),
								(GETDATE(),11, 1, 23547),
								(GETDATE(),13, 1, 35477),
								(GETDATE(),4, 1, 54686)

MERGE dbo.LoanDetails AS target
USING #tempLoanDetails AS source
	ON source.LoanNumber = target.LoanNumber

WHEN NOT MATCHED THEN 
	INSERT (DateCreated, ClientID, IsActive, LoanNumber)
	VALUES (source.DateCreated, source.ClientID, source.IsActive, source.LoanNumber)

WHEN MATCHED THEN UPDATE 
SET 
	
	target.DateCreated = source.DateCreated,
	target.ClientID = source.ClientID,
	target.IsActive = source.IsActive,
	target.LoanNumber = source.LoanNumber;
	
	DROP TABLE #tempLoanDetails

