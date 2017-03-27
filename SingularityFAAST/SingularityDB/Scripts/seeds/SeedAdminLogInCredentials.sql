-- This is the Merge Script that seeds and updates 
-- The Post Deployment script is the master script which decides the order by which to run these Merge Scripts

IF OBJECT_ID('tempdb..#tempUserLogIns') IS NOT NULL DROP TABLE #tempUserLogIns


CREATE TABLE #tempUserLogIns (
	[UserId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Username] NVARCHAR(32) NOT NULL,
	[Password] NVARCHAR(32) NOT NULL
)



--SET IDENTITY_INSERT [dbo].[UserLogIns] ON

INSERT INTO #tempUserLogIns ([Username], [Password]) VALUES (N'Admin', N'passwordpassword')

--SET IDENTITY_INSERT [dbo].[UserLogIns] OFF


MERGE dbo.UserLogIns AS target 
USING #tempUserLogIns AS source
	ON source.UserId = target.UserId 


WHEN NOT MATCHED THEN 
	INSERT (Username, Password)	
	VALUES (source.Username, source.Password) 



WHEN MATCHED THEN UPDATE 
SET 
	
	target.Username = source.Username,
	target.Password = source.Password;


DROP TABLE #tempUserLogIns