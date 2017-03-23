

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

--LoanDetailId is auto
INSERT INTO #tempLoanDetails([LoanMasterId],[LoanDate],[LoanDuration],[InventoryItemId],[Purpose],[PurposeType],[ClientOutcome],[Notes])
     VALUES
		   (1,GETDATE(),28,1,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger'),
		   (2,GETDATE(),28,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','Ms. Sowada checked out item for client whose name is John Kinsler. They are from Brooks Rehab.'),
		   (3,GETDATE(),28,2,'Assist in decision making (device trial or evaluation)','Employment','AT will meet needs','AT left message with client at phone number and emailed her at jaydensamir@gmail.com to return iPAD on 8/31/16'),
		   (3,GETDATE(),28,19,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs',''),
		   (4,GETDATE(),28,23,'Assist in decision making (device trial or evaluation)','Community Living','AT will meet needs','client also has Bug Vibrator')
		   


MERGE dbo.LoanDetails AS target
USING #tempLoanDetails AS source
	ON source.LoanNumber = target.LoanNumber

WHEN NOT MATCHED THEN 
	INSERT ([LoanMasterId],[LoanDate],[LoanDuration],[InventoryItemId],[Purpose],[PurposeType],[ClientOutcome],[Notes])
	VALUES (source.LoanMasterId, source.LoanDate, source.LoanDuration, source.InventoryItemId, source.Purpose, source.PurposeType, source.ClientOutcome, source.Notes)


WHEN MATCHED THEN UPDATE 
SET 
	
	target.LoanMasterId = source.LoanMasterId,
	target.LoanDate = source.LoanDate,
	target.LoanDuration = source.LoanDuration,
	target.InventoryItemId = source.InventoryItemId,
	target.Purpose = source.Purpose,
	target.PurposeType = source.PurposeType,
	target.ClientOutcome = source.ClientOutcome,
	target.Notes = source.Notes;

	
	DROP TABLE #tempLoanDetails

