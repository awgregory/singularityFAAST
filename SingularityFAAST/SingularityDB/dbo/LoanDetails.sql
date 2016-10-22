CREATE TABLE [dbo].[LoanDetails]
(
	[LoanDetailId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [LoanMasterId] INT NOT NULL,

	CONSTRAINT [FK_LoanDetails_LoanMaster] FOREIGN KEY ([LoanMasterId]) REFERENCES dbo.LoanMaster([LoanMasterId]),
)
