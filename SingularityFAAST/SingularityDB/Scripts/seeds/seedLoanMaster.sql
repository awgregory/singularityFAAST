
IF OBJECT_ID('tempdb..#tempLoanMasters') IS NOT NULL DROP TABLE #tempLoanMasters

CREATE TABLE #tempLoanMasters
(
	[LoanMasterId] INT NOT NULL IDENTITY(1,1),
    [DateCreated] DATETIME NOT NULL, 
	[ClientID] INT NOT NULL, 
    [IsActive] BIT DEFAULT (1) NOT NULL, 
	[LoanNumber] VARCHAR(20) NOT NULL,
	[IsDeleted] BIT DEFAULT (0) NOT NULL
)

--LoanMasterId is auto
INSERT INTO #tempLoanMasters([DateCreated],[ClientId],[IsActive],[LoanNumber],[IsDeleted])
     VALUES						(N'2016-01-21 00:00:00',		1,			1,			00001, 0),
								(N'2016-03-01 00:00:00',		1,			1,			00002, 0),
								(N'2016-06-12 00:00:00',		1,			0,			00003, 0),
								(N'2017-01-22 00:00:00',		2,			1,			00004, 0),
								(N'2017-02-23 00:00:00',		3,			1,			00005, 0),
								(N'2017-02-17 00:00:00',		4,			0,			00006, 0),
								(N'2017-02-23 00:00:00',		2,			1,			00007, 0),
								(N'2017-03-12 00:00:00',		3,			1,			00008, 1),
								(N'2017-03-17 00:00:00',		4,			1,			00009, 1)


MERGE dbo.LoanMasters AS target
USING #tempLoanMasters AS source
	ON source.LoanMasterId = target.LoanMasterId


WHEN NOT MATCHED THEN 
	INSERT (DateCreated, ClientID, IsActive, LoanNumber, IsDeleted)
	VALUES (source.DateCreated, source.ClientID, source.IsActive, source.LoanNumber, source.IsDeleted)


WHEN MATCHED THEN UPDATE 
SET 
	
	target.DateCreated = source.DateCreated,
	target.ClientID = source.ClientID,
	target.IsActive = source.IsActive,
	target.LoanNumber = source.LoanNumber,
	target.IsDeleted = source.IsDeleted;


	
	DROP TABLE #tempLoanMasters