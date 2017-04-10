CREATE PROCEDURE [dbo].[GetLoanNumberByInventoryItemId]
	@inventoryItemId VARCHAR(20)
AS
	SELECT
		LM.LoanNumber
	FROM dbo.LoanMasters as LM
	INNER JOIN dbo.LoanDetails AS LD
		ON LM.LoanMasterId = LD.LoanMasterId
	WHERE LD.InventoryItemId = @inventoryItemId

RETURN 0