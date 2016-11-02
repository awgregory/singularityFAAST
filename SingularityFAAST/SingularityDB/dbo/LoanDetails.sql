﻿CREATE TABLE [dbo].[LoanDetails]
(
	[LoanDetailId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [LoanMasterId] INT NOT NULL,

	[LoanDate] DATE NOT NULL, 
    [LoanDuration] INT NOT NULL DEFAULT 30, 
	[Purpose] VARCHAR(50) NOT NULL,
    [PurposeType] VARCHAR(25) NOT NULL, 
	[ClientOutcome] VARCHAR(30) NOT NULL,
    [Notes] VARCHAR(MAX) NULL, 
    CONSTRAINT [FK_LoanDetails_LoanMaster] FOREIGN KEY ([LoanMasterId]) REFERENCES dbo.LoanMaster([LoanMasterId]),
)
