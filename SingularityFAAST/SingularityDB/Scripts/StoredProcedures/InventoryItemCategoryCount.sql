CREATE PROCEDURE [dbo].[InventoryItemCategoryCount]
	@startDate DateTime,
	@endDate DateTime
AS
	;with loanItems as (
	select
		ii.InventoryItemId,
		ii.InventoryCategoryId
	from dbo.LoanMasters as lm
	inner join dbo.LoanDetails as ld
		on lm.LoanMasterId = ld.LoanMasterId
	inner join dbo.InventoryItems as ii
		on ld.InventoryItemId = ii.InventoryItemId
	where lm.DateCreated between @startDate and @endDate
)

select 
	--left side category, regardless if exists in the CTE
	iic.CategoryName
	--count the number of distinct items that share the same
	--item category id as the table it's being joined to
	,count(distinct li.InventoryItemId) as [Count]
from dbo.InventoryItemCategories as iic

--get all values for item categories
left join loanItems as li
	on li.InventoryCategoryId = iic.InventoryCategoryId
	
--group by to get the count	
group by iic.CategoryName

