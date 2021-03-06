﻿CREATE TABLE [dbo].[Clients]
(
	[ClientId]			INT				NOT NULL	IDENTITY(1,1), 
    [Active]			BIT				NOT NULL DEFAULT 1, 
    [DateCreated]		DATETIME2		NOT NULL DEFAULT sysdatetime() , 
    [FirstName]			VARCHAR(50)		NOT NULL,
	[MiddleInitial]		CHAR			NULL,
    [LastName]			VARCHAR(50)		NOT NULL, 
    [Address1]			VARCHAR(200)	NOT NULL, 
	[Address2]			VARCHAR(200)	NULL,
	[StateName]			VARCHAR(30)		NULL, 
    [StateCode]			CHAR(2)			NULL , 
    [Zip]				VARCHAR(10)		NOT NULL, 
    [County]			VARCHAR(50)		NULL, 
    [CountyFIPS]		CHAR(10)		NOT NULL, 
    [City]				VARCHAR(50)		NOT NULL,
    [Email]				VARCHAR(100)	NULL, 
    [HomePhone]			VARCHAR(20)		NULL,
	[CellPhone]			VARCHAR(20)		NOT NULL,
	[WorkPhone]			VARCHAR(30)		NULL,
    [Company]			VARCHAR(50)		NULL, 
    [Title]				VARCHAR(20)		NULL, 
    [LoanEligibility]	BIT				NOT NULL DEFAULT 1 , 
    [Notes]				VARCHAR(MAX)	NULL, 
    [ClientCategoryId] INT NOT NULL,
	[IsDeleted]			BIT				NOT NULL DEFAULT 0,
	
	CONSTRAINT [PK_Clients] PRIMARY KEY ([ClientId]),
	
	CONSTRAINT [FK_Clients_ClientCategories] FOREIGN KEY ([ClientCategoryId])
	REFERENCES dbo.ClientCategories(ClientCategoryId)
)
