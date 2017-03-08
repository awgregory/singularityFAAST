--DBCC CHECKIDENT('dbo.InventoryItems', RESEED, 0)
-- Note on Reseed command above. This resets Identity columns  
-- Only use this command if you are reseeding a table which you previously had records in and then remove
-- For example if you previously made your own clients table with records and now are re-populating it with these records
-- Don't use the command above on a table that never had data

IF OBJECT_ID('tempdb..#tempLoanMasters') IS NOT NULL DROP TABLE #tempLoanMasters 

SET IDENTITY_INSERT [dbo].[LoanMasters] ON
INSERT INTO [dbo].[LoanMasters] ([LoanMasterId], [DateCreated], [ClientId], [IsActive], [LoanNumber]) VALUES (1, N'2017-01-01 00:00:00', 1, 1, N'2')
INSERT INTO [dbo].[LoanMasters] ([LoanMasterId], [DateCreated], [ClientId], [IsActive], [LoanNumber]) VALUES (2, N'2016-11-05 00:00:00', 2, 1, N'3')
SET IDENTITY_INSERT [dbo].[LoanMasters] OFF

MERGE dbo.LoanMasters AS target
USING #tempLoanMasters AS source

	ON source.LoanMasterId = target.LoanMasterId

WHEN NOT MATCHED THEN 
	INSERT (Type)
	VALUES (SOURCE.TYPE)

WHEN MATCHED THEN UPDATE 
SET 
	
	target.Type = source.Type;

DROP TABLE #tempLoanMasters
