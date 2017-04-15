CREATE PROCEDURE [dbo].[CreateClientList]
	@param1 int = 0,
	@param2 int
AS
	SELECT Clients.FirstName, Clients.LastName, Clients.Email
RETURN 0
