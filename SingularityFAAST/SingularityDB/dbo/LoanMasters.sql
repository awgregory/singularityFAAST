CREATE TABLE [dbo].[LoanMasters]
(
	[LoanMasterId] INT NOT NULL IDENTITY(1,1),
    [DateCreated] DATETIME NOT NULL , 
	[ClientId] INT NOT NULL, 
    [IsActive] BIT DEFAULT (1) NOT NULL, 
	[LoanNumber] VARCHAR(20) NOT NULL,
	[IsDeleted]	BIT DEFAULT (0) NOT NULL,
	[ClientOutcome] VARCHAR(50) NULL,
	[LoanNotes] VARCHAR(500) NULL,

	CONSTRAINT [PK_LoanMasters] PRIMARY KEY ([LoanMasterId]),	 
    CONSTRAINT [FK_LoanMasters_Clients] FOREIGN KEY ([ClientId]) REFERENCES dbo.Clients ([ClientId]),

)
