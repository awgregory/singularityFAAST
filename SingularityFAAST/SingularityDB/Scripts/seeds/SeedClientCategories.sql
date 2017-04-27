
-- This is the Merge Script that seeds and updates 
-- The Post Deployment script is the master script which decides the order by which to run these Merge Scripts

IF OBJECT_ID('tempdb..#tempClientCategories') IS NOT NULL DROP TABLE #tempClientCategories 


CREATE TABLE #tempClientCategories (
	[ClientCategoryId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Type] VARCHAR(50) NOT NULL
)


INSERT INTO #tempClientCategories (Type)  
VALUES 
	('Individual with Disability'),
	('Family Guardian or Authorized Rep'), 
	('Reps of Education'), 
	('Reps of Employment'),
	('Reps of Community Living'),
	('Reps of Technology'),
	('Allied Health and Rehabilitation')



MERGE dbo.ClientCategories AS target 
USING #tempClientCategories AS source
	ON source.ClientCategoryId = target.ClientCategoryId 

	

WHEN NOT MATCHED THEN 
	INSERT (Type)	
	VALUES (source.Type) 



WHEN MATCHED THEN UPDATE 
SET 
	
	target.Type = source.Type;


DROP TABLE #tempClientCategories