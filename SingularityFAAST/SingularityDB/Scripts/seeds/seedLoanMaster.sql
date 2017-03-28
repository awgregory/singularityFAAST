
IF OBJECT_ID('tempdb..#tempLoanMasters') IS NOT NULL DROP TABLE #tempLoanMasters

CREATE TABLE #tempLoanMasters
(
	[LoanMasterId] INT NOT NULL IDENTITY(1,1),
    [DateCreated] DATETIME NOT NULL, 
	[ClientID] INT NOT NULL, 
    [IsActive] BIT NOT NULL, 
	[LoanNumber] VARCHAR(20) NOT NULL
)

--LoanMasterId is auto
INSERT INTO #tempLoanMasters([DateCreated],[ClientId],[IsActive],[LoanNumber])
     VALUES						(GETDATE(),		1,			1,			00001),
								(GETDATE(),		1,			1,			00002),
								(GETDATE(),		1,			0,			00003),
								(GETDATE(),		2,			1,			00004),
								(GETDATE(),		3,			1,			00005),
								(GETDATE(),		4,			0,			00006),
								(GETDATE(),		2,			1,			00007),
								(GETDATE(),		3,			1,			00008),
								(GETDATE(),		4,			1,			00009)


MERGE dbo.LoanMasters AS target
USING #tempLoanMasters AS source
	ON source.LoanMasterId = target.LoanMasterId


WHEN NOT MATCHED THEN 
	INSERT (DateCreated, ClientID, IsActive, LoanNumber)
	VALUES (source.DateCreated, source.ClientID, source.IsActive, source.LoanNumber)


WHEN MATCHED THEN UPDATE 
SET 
	
	target.DateCreated = source.DateCreated,
	target.ClientID = source.ClientID,
	target.IsActive = source.IsActive,
	target.LoanNumber = source.LoanNumber;


	
	DROP TABLE #tempLoanMasters