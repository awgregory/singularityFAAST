CREATE TABLE [dbo].[UserLogIns]
(
	[UserId]	INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY, 
    [Username]	NVARCHAR(32)	NOT NULL, 
    [Password]	NVARCHAR(32)	NOT NULL
)
