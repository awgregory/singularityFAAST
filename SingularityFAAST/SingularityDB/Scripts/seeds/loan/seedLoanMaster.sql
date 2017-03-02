IF OBJECT_ID('tempdb..#tempLoanMasters') IS NOT NULL DROP TABLE #tempLoanMasters

CREATE TABLE #tempLoanMasters
(
	[LoanMasterId] INT NOT NULL IDENTITY(1,1),
    [DateCreated] DATETIME NOT NULL, 
	[ClientID] INT NOT NULL, 
    [IsActive] BIT NOT NULL, 
	[LoanNumber] VARCHAR(20) NOT NULL,

	CONSTRAINT [PK_LoanMasters] PRIMARY KEY ([LoanMasterId]),	 
    CONSTRAINT [FK_LoanMasters_Clients] FOREIGN KEY ([ClientID]) REFERENCES dbo.Clients ([ClientID]),

)

INSERT INTO [dbo].[LoanDetails]([LoanMasterId],[LoanDate],[LoanDuration],[InventoryItemId],[Purpose],[PurposeType],[ClientOutcome],[Notes])
     VALUES
			--LoanMasterId will be assigned programmatically not manually
			(1,GETDATE(),28,1,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger'),
		   (3,GETDATE(),28,2,'Assist in decision making (device trial or evaluation)','Employment','AT will meet needs','AT left message with client at phone number and emailed her at jaydensamir@gmail.com to return iPAD on 8/31/16'),
		   (3,GETDATE(),28,19,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs',''),
		   (7,GETDATE(),28,23,'Assist in decision making (device trial or evaluation)','Community Living','AT will meet needs','client also has Bug Vibrator'),
		   (3,GETDATE(),28,26,'Assist in decision making (device trial or evaluation)','Employment','AT will meet needs','10 cards included'),
		   (8,GETDATE(),28,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','Ms. Sowada checked out item for client whose name is John Kinsler. They are from Brooks Rehab.') 


MERGE dbo.LoanMasters AS target
USING #tempLoanMasters AS source
	ON source.LoanMasterId = target.LoanMasterId

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
	
	DROP TABLE #tempLoanMasters