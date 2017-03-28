CREATE PROCEDURE [dbo].[InventoryItemCategoryCount]
	@startDate DateTime,
	@endDate DateTime
AS
	SELECT IIC.CategoryName, COUNT(1) AS [Count] FROM LoanMasters AS lm
INNER JOIN LoanDetails AS ld
	ON lm.LoanMasterId = ld.LoanMasterId
INNER JOIN InventoryItems AS II
	on ld.InventoryItemId = II.InventoryItemId
INNER JOIN InventoryItemCategories as IIC 
	on II.InventoryCategoryId = IIC.InventoryCategoryId
WHERE lm.DateCreated BETWEEN @startDate AND @endDate
GROUP BY CategoryName
RETURN 0
