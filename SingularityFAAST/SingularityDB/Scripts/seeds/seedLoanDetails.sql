

IF OBJECT_ID('tempdb..#tempLoanDetails') IS NOT NULL DROP TABLE #tempLoanDetails 

CREATE TABLE #tempLoanDetails
(
	[LoanDetailId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [LoanMasterId] INT NOT NULL,
	[LoanDate] DATETIME NOT NULL DEFAULT (getutcdate()), 
    [LoanDuration] INT NOT NULL DEFAULT (28), 
    [InventoryItemId] INT NOT NULL,
	[Purpose] VARCHAR(80) NOT NULL,
    [PurposeType] VARCHAR(25) NOT NULL 
)

--LoanDetailId is auto
INSERT INTO #tempLoanDetails([LoanMasterId],[LoanDate],[LoanDuration],[InventoryItemId],[Purpose],[PurposeType])
     VALUES
		   (1,N'2017-01-21 00:00:00',28,1,'Assist in decision making (device trial or evaluation)','Education'),
		   (2,N'2017-03-01 00:00:00',28,2,'Assist in decision making (device trial or evaluation)','Education'),
		   (3,N'2016-06-12 00:00:00',28,3,'Assist in decision making (device trial or evaluation)','Employment'),
		   (3,N'2016-06-12 00:00:00',28,9,'Assist in decision making (device trial or evaluation)','Education'),
		   (4,N'2017-03-22 00:00:00',28,4,'Assist in decision making (device trial or evaluation)','Education'),
		   (5,N'2017-02-23 00:00:00',28,5,'Assist in decision making (device trial or evaluation)','Community Living'),
		   (6,N'2016-08-17 00:00:00',28,6,'Assist in decision making (device trial or evaluation)','Community Living'),
		   (6,N'2016-08-17 00:00:00',28,7,'Assist in decision making (device trial or evaluation)','Community Living'),
		   (6,N'2016-08-17 00:00:00',28,8,'Assist in decision making (device trial or evaluation)','Community Living'),
		   (7,N'2017-02-23 00:00:00',28,10,'Assist in decision making (device trial or evaluation)','Employment'),
		   (8,N'2017-03-24 00:00:00',28,11,'Assist in decision making (device trial or evaluation)','Community Living'),
		   (9,N'2017-03-17 00:00:00',28,13,'Assist in decision making (device trial or evaluation)','Education');


MERGE dbo.LoanDetails AS target
USING #tempLoanDetails AS source
	ON source.LoanDetailId = target.LoanDetailId

WHEN NOT MATCHED THEN 
	INSERT ([LoanMasterId],[LoanDate],[LoanDuration],[InventoryItemId],[Purpose],[PurposeType])
	VALUES (source.LoanMasterId, source.LoanDate, source.LoanDuration, source.InventoryItemId, source.Purpose, source.PurposeType)


WHEN MATCHED THEN UPDATE 
SET 
	
	target.LoanMasterId = source.LoanMasterId,
	target.LoanDate = source.LoanDate,
	target.LoanDuration = source.LoanDuration,
	target.InventoryItemId = source.InventoryItemId,
	target.Purpose = source.Purpose,
	target.PurposeType = source.PurposeType;
	
	DROP TABLE #tempLoanDetails

