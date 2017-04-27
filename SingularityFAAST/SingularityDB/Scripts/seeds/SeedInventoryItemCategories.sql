-- This is the Merge Script that seeds and updates 
-- The Post Deployment script is the master script which decides the order by which to run these Merge Scripts

IF OBJECT_ID('tempdb..#tempInventoryItemCategories') IS NOT NULL DROP TABLE #tempInventoryItemCategories 


CREATE TABLE #tempInventoryItemCategories (
	[InventoryCategoryId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CategoryName] VARCHAR(50) NOT NULL
)

--SET IDENTITY_INSERT [dbo].[InventoryItemCategories] ON


INSERT INTO #tempInventoryItemCategories (CategoryName) 
VALUES 
	(N'Computers and Related Technology'),
	(N'Daily Living'),
	(N'Environmental Adaptations'),
	(N'Hearing'),
	(N'Learning, Cognition, and Development'),
	(N'Mobility, Seating, and Positioning'),
	(N'Recreational, Sports, and Leisure'),
	(N'Speech Communication'),
	(N'Vehicle Mods and Transport'),
	(N'Vision')


--SET IDENTITY_INSERT [dbo].[InventoryItemCategories] OFF

MERGE dbo.InventoryItemCategories AS target 
USING #tempInventoryItemCategories AS source
	ON source.InventoryCategoryId = target.InventoryCategoryId 

	

WHEN NOT MATCHED THEN 
	INSERT (CategoryName)	
	VALUES (source.CategoryName) 



WHEN MATCHED THEN UPDATE 
SET 
	
	target.CategoryName = source.CategoryName;


DROP TABLE #tempInventoryItemCategories