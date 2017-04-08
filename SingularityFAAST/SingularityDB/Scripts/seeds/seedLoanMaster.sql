
IF OBJECT_ID('tempdb..#tempLoanMasters') IS NOT NULL DROP TABLE #tempLoanMasters

CREATE TABLE #tempLoanMasters
(
	[LoanMasterId] INT NOT NULL IDENTITY(1,1),
    [DateCreated] DATETIME NOT NULL, 
	[ClientID] INT NOT NULL, 
    [IsActive] BIT DEFAULT (1) NOT NULL, 
	[LoanNumber] VARCHAR(20) NOT NULL,
	[IsDeleted] BIT DEFAULT (0) NOT NULL,
	[ClientOutcome] VARCHAR(50) NULL,
	[LoanNotes] VARCHAR(MAX) NULL
)

--LoanMasterId is auto
INSERT INTO #tempLoanMasters([DateCreated],[ClientId],[IsActive],[LoanNumber],[IsDeleted],[ClientOutcome],[LoanNotes])
     VALUES						(N'2016-01-21 00:00:00',		1,			1,			00001, 0,'AT will meet needs','two charger cords & one charger'),
								(N'2016-03-01 00:00:00',		1,			1,			00002, 0,'AT will meet needs','Ms. Sowada checked out item for client whose name is John Kinsler. They are from Brooks Rehab.'),
								(N'2016-06-12 00:00:00',		1,			0,			00003, 0,'AT will meet needs','AT left message with client at phone number and emailed her at jaydensamir@gmail.com to return iPAD on 8/31/16'),
								(N'2017-01-22 00:00:00',		2,			1,			00004, 0,'AT will meet needs',''),
								(N'2017-02-23 00:00:00',		3,			1,			00005, 0,'AT will meet needs',''),
								(N'2017-02-17 00:00:00',		4,			0,			00006, 0,'AT will meet needs','client also has Bug Vibrator'),
								(N'2017-02-23 00:00:00',		2,			1,			00007, 0,'AT will meet needs','client wears a metal suit'),
								(N'2017-03-12 00:00:00',		3,			1,			00008, 1,'AT will meet needs','Item will be borrowed again in 2 months'),
								(N'2017-03-17 00:00:00',		4,			1,			00009, 1,'AT will meet needs','');


MERGE dbo.LoanMasters AS target
USING #tempLoanMasters AS source
	ON source.LoanMasterId = target.LoanMasterId


WHEN NOT MATCHED THEN 
	INSERT (DateCreated, ClientID, IsActive, LoanNumber, IsDeleted, ClientOutcome, LoanNotes)
	VALUES (source.DateCreated, source.ClientID, source.IsActive, source.LoanNumber, source.IsDeleted, source.ClientOutcome, source.LoanNotes)


WHEN MATCHED THEN UPDATE 
SET 
	
	target.DateCreated = source.DateCreated,
	target.ClientID = source.ClientID,
	target.IsActive = source.IsActive,
	target.LoanNumber = source.LoanNumber,
	target.IsDeleted = source.IsDeleted,
	target.ClientOutcome = source.ClientOutcome,
	target.LoanNotes = source.LoanNotes;


	
	DROP TABLE #tempLoanMasters