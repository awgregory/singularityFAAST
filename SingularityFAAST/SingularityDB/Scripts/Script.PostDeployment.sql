/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
--:r .\seeds\seedDummyData.sql
:r .\seeds\SeedAdminLogInCredentials.sql

:r .\seeds\SeedClientCategories.sql
:r .\seeds\SeedDisabilityCategories.sql
:r .\seeds\SeedClients.sql
:r .\seeds\SeedClientDisabilities.sql

:r .\seeds\SeedInventoryItemCategories.sql
:r .\seeds\SeedInventoryItems.sql

--:r .\seeds\loan\seedLoanDetails.sql
