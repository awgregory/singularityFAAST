-- This is the Merge Script that seeds and updates 
-- The Post Deployment script is the master script which decides the order by which to run these Merge Scripts

IF OBJECT_ID('tempdb..#tempInventoryItemCategories') IS NOT NULL DROP TABLE #tempInventoryItemCategories 


CREATE TABLE #tempInventoryItemCategories (
	[InventoryCategoryId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Type] VARCHAR(50) NOT NULL
)

SET IDENTITY_INSERT [dbo].[InventoryItemCategories] ON
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (1, N'Computers and Related Technology')
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (2, N'Daily Living')
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (3, N'Environmental Adaptations')
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (4, N'Hearing')
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (5, N'Learning, Cognition, and Development')
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (6, N'Mobility, Seating, and Positioning')
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (7, N'Recreational, Sports, and Leisure')
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (8, N'Speech Communication')
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (9, N'Vehicle Mods and Transport')
INSERT INTO [dbo].[InventoryItemCategories] ([InventoryCategoryId], [CategoryName]) VALUES (10, N'Vision')
SET IDENTITY_INSERT [dbo].[InventoryItemCategories] OFF

MERGE dbo.InventoryItemCategories AS target 
USING #tempInventoryItemCategories AS source
	ON source.InventoryCategoryId = target.InventoryCategoryId 

	

WHEN NOT MATCHED THEN 
	INSERT (Type)	
	VALUES (source.Type) 



WHEN MATCHED THEN UPDATE 
SET 
	
	target.Type = source.Type;


DROP TABLE #tempInventoryItemCategories