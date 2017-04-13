--DBCC CHECKIDENT('dbo.Clients', RESEED, 0)

IF OBJECT_ID('tempdb..#tempClients') IS NOT NULL DROP TABLE #tempClients 


CREATE TABLE #tempClients (
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
    [LoanEligibility]	BIT				NOT NULL DEFAULT 1, 
    [Notes]				VARCHAR(MAX)	NULL, 
    [ClientCategoryId]	INT				NOT NULL,
	[IsDeleted]			BIT				NOT NULL DEFAULT 0
)


INSERT INTO #tempClients (Active, DateCreated, FirstName, MiddleInitial, LastName, Address1, Address2, StateName, StateCode,
Zip, County, CountyFIPS, City, Email, HomePhone, CellPhone, WorkPhone, Company, Title, LoanEligibility, Notes, ClientCategoryId, IsDeleted) 
VALUES 

(1, N'2016-11-03 00:00:00', N'Jeanette', NULL, N'Otero', N'5734 W 57th Way', NULL, N'Florida', N'FL', N'33409', N'Palm Beach', N'12099     ', N'West Palm Beach', NULL, NULL, N'904-771-4298', NULL, NULL, NULL, 1, NULL, 2, 0),
(1, N'2016-11-03 00:00:00', N'Bruce', NULL, N'Stayer', N'134 Deanna Dr', NULL, N'Florida', N'FL', N'33852', N'Highlands', N'12055     ', N'Lake Placid', NULL, NULL, N'904-419-1874', NULL, NULL, NULL, 1, NULL, 1, 0),
(1, N'2016-11-03 00:00:00', N'Lorin', NULL, N'Anderson', N'15 B Cherry Ridge Dr', NULL, N'Florida', N'FL', N'32746', N'Seminole', N'12117     ', N'Lake Mary', NULL, NULL, N'904-388-1996', NULL, NULL, NULL, 1, NULL, 2, 0),
(1, N'2017-02-01 20:08:26', N'Anthony', NULL, N'Stark', N'Avengers Mansion', NULL, N'California', N'Ca', N'78441', N'Big Sur', N'12117     ', N'Big Sur', N'IronTony@starke.com', NULL, N'714-145-7746', NULL, N'Stark Industries', N'Mr.', 0, N'Has chest reactor that requires special magnetic charger adaptor', 1, 0),
(1, N'2017-02-01 20:08:26', N'Tom', N'T', N'Stevens', N'1847 Reynolds Dr', NULL, N'Florida', N'FL', N'32256', N'Duval', N'12117     ', N'Jacksonville', NULL, NULL, N'904-396-2243', NULL, NULL, N'Mr', 0, N'Hearing Disabled', 2, 0),
(1, N'2017-02-01 20:08:26', N'Chad', NULL, N'Stevens', N'1847 Reynolds Dr', NULL, N'Florida', N'FL', N'32256', N'Duval', N'12117     ', N'Jacksonville', NULL, NULL, N'904-384-4755', NULL, NULL, N'Mr', 0, N'Hearing Disabled', 1, 0),
(1, N'2017-02-01 20:08:26', N'John', NULL, N'Mack', N'45761 Youmen dr', NULL, N'Florida', N'FL', N'32247', NULL, N'12117     ', N'Jacksonville', NULL, NULL, N'904-384-1874', NULL, NULL, NULL, 0, N'Blah', 1, 0),
(1, N'2017-02-01 20:08:26', N'Tom', N'Y', N'Meyers', N'1234 Main St', NULL, N'Florida', N'FL', N'32210', N'Duval', N'12117     ', N'Jacksonville', N'tm@yahoo.com', NULL, N'904-832-1884', NULL, N'Beeline', N'Mr', 0, N'new client', 2, 0),
(1, N'2016-11-03 00:00:00', N'David', NULL, N'Teske', N'PO Box 909', NULL, N'North Carolina', N'NC', N'28711', N'Davie', N'37059     ', N'Advance', NULL, NULL, N'904-384-1874', NULL, NULL, NULL, 1, NULL, 2, 0),
(1, N'2017-02-01 20:08:26', N'Bob', N'T', N'Welloers', N'1234 Main St', NULL, N'Florida', N'FL', N'32210', N'Duval', N'12117     ', N'Jacksonville', N'tm@yahoo.com', NULL, N'904-384-1874', NULL, N'Beeline', N'Mr', 0, N'new client', 2, 0),
(1, N'2017-02-01 20:08:26', N'Test', N'Y', N'Test', N'1234 Main St', NULL, N'Florida', N'FL', N'32210', N'Duval', N'12117     ', N'Jacksonville', N'tm@yahoo.com', NULL, N'904-774-1274', NULL, N'Beeline', N'Mr', 0, N'new client', 2, 0),
(1, N'2017-02-01 20:08:26', N'Test1', N'Y', N'Test', N'1234 Main St', NULL, N'Florida', N'FL', N'32210', N'Duval', N'12117     ', N'Jacksonville', N'tm@yahoo.com', NULL, N'904-919-7847', NULL, N'Beeline', N'Mr', 0, N'new client', 2, 0)


MERGE dbo.Clients AS target
USING #tempClients AS source

	ON source.ClientId = target.ClientId

WHEN NOT MATCHED THEN 
	INSERT (Active, DateCreated, FirstName, MiddleInitial, LastName, Address1, Address2, StateName, StateCode, Zip, County,
	CountyFIPS, City, Email, HomePhone, CellPhone, WorkPhone, Company, Title, LoanEligibility, Notes, ClientCategoryId, IsDeleted)
	VALUES (source.Active, source.DateCreated, source.FirstName, source.MiddleInitial, source.LastName, source.Address1,
	source.Address2, source.StateName, source.StateCode, source.Zip, source.County, source.CountyFIPS, source.City, 
	source.Email, source.HomePhone, source.CellPhone, source.WorkPhone, source.Company, source.Title, source.LoanEligibility,
	source.Notes, source.ClientCategoryId, source.IsDeleted)

WHEN MATCHED THEN UPDATE 
SET 
	
	target.Active = source.Active,
	target.DateCreated = source.DateCreated,
	target.FirstName = source.FirstName,
	target.MiddleInitial = source.MiddleInitial,
	target.LastName = source.LastName,
	target.Address1 = source.Address1,
	target.Address2 = source.Address2,
	target.StateName = source.StateName,
	target.StateCode = source.StateCode,
	target.Zip = source.Zip,
	target.County = source.County,
	target.CountyFIPS = source.CountyFIPS,
	target.City = source.City,
	target.Email = source.Email,
	target.HomePhone = source.HomePhone,
	target.CellPhone = source.CellPhone,
	target.WorkPhone = source.WorkPhone,
	target.Company = source.Company,
	target.Title = source.Title,
	target.LoanEligibility = source.LoanEligibility,
	target.Notes = source.Notes,
	target.ClientCategoryId = source.ClientCategoryId,
	target.IsDeleted = source.IsDeleted;

DROP TABLE #tempClients

