CREATE PROCEDURE [dbo].[GetInventoryItemsByLoanNumber]
	@loanNumber VARCHAR(20)
AS
	SELECT
		LD.InventoryItemId
	FROM dbo.LoanMasters as LM
	INNER JOIN dbo.LoanDetails AS LD
		ON LM.LoanMasterId = LD.LoanMasterId
	WHERE LM.LoanNumber = @loanNumber

RETURN 0
