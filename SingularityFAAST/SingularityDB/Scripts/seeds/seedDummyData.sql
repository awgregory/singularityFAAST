use [singularitydb]

insert into inventoryitemcategories values 	('hearing aids'),
											('technology');

declare @categoryid int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'hearing aids');
declare @categoryid2 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'technology');

insert into	inventoryitems			values	('20161106',@categoryid,'invisible hearing aid',200.00,200.00,'superhearing 1000','north east demonstration center',1,'00123456','this hearing aid is awesome!',null,'none'),
											('20161106',@categoryid2,'modified keyboard',80.00,90.00,'one handed keyboard deluxe','north east demonstration center',0,'99032256','this is a keyboard','1 extension cable','none');


go
set identity_insert [dbo].[clientcategories] on 

go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (1, N'individual with disability')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (2, N'family guardian or authorized rep')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (3, N'reps of education')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (4, N'reps of employment')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (5, N'reps of community living')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (6, N'reps of technology')
go
insert [dbo].[clientcategories] ([clientcategoryid], [type]) values (7, N'health allied health and rehab')
go
set identity_insert [dbo].[clientcategories] off
go
set identity_insert [dbo].[clients] on 

go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (2, 1, cast(n'2016-11-02t00:00:00.000' as datetime), N'david', null, n'teske', n'po box 909', null, n'north carolina', n'nc', n'28711', n'davie', n'37059     ', n'advance', null, null, null, null, null, null, 1, null, 2)
go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (3, 1, cast(n'2016-11-03t00:00:00.000' as datetime), n'jeanette', null, n'otero', n'5734 w 57th way', null, n'florida', n'fl', n'33409', n'palm beach', n'12099     ', n'west palm beach', null, null, null, null, null, null, 1, null, 2)
go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (4, 1, cast(n'2016-11-03t00:00:00.000' as datetime), n'bruce', null, n'stayer', n'134 deanna dr', null, n'florida', n'fl', n'33852', n'highlands', n'12055     ', n'lake placid', null, null, null, null, null, null, 1, null, 1)
go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (6, 1, cast(n'2016-11-03t00:00:00.000' as datetime), n'bruce', null, n'stayer', n'134 deanna dr', null, n'florida', n'fl', n'33852', n'highlands', n'12055     ', n'lake placid', null, null, null, null, null, null, 1, null, 2)
go
insert [dbo].[clients] ([clientid], [active], [datecreated], [firstname], [middleinitial], [lastname], [address1], [address2], [statename], [statecode], [zip], [county], [countyfips], [city], [email], [homephone], [cellphone], [workphone], [company], [title], [loaneligibility], [notes], [clientcategoryid]) values (7, 1, cast(n'2016-11-03t00:00:00.000' as datetime), n'lorin', null, n'anderson', n'15 b cherry ridge dr', null, n'florida', n'fl', n'32746', n'seminole', n'12117     ', n'lake mary', null, null, null, null, null, null, 1, null, 2)
go
set identity_insert [dbo].[clients] off
go


INSERT INTO [dbo].[LoanCategories] ([LoanCategoryId],[CategoryName])   --What is a Loan Category?  Not found in existing data.  We have Loan Purpose and Purpose Type in LoanDetails table.
     VALUES (1,'General')
GO

SET IDENTITY_INSERT [dbo].[LoanMasters] ON  
GO
INSERT INTO [dbo].[LoanMasters] ([LoanMasterID],[DateCreated],[IsActive],[ClientId],[LoanCategoryId],[InventoryItemId],[LoanNumber])
     VALUES						(1, GETDATE(),		1,			3,			1,				1,				15499),
								(2, GETDATE(),		1,			6,			1,				2,				24700)
GO 

SET IDENTITY_INSERT [dbo].[LoanMasters] ON
GO

SET IDENTITY_INSERT [dbo].[LoanDetails] OFF   
GO

--Why is DateCreated in LoanMaster and LoanDate(presumably the date loan created) here?
INSERT INTO [dbo].[LoanDetails]([LoanMasterId],[LoanDate],[LoanDuration],[Purpose],[PurposeType],[ClientOutcome],[Notes])
     VALUES
           (1,GETDATE(),28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger'),
		   (3,GETDATE(),28,'Assist in decision making (device trial or evaluation)','Work','AT will meet needs','batteries and cards included')
		   --(3,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger')
		   --(4,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger')
		   --(5,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger')
		   --(6,null,28,'Assist in decision making (device trial or evaluation)','Education','AT will meet needs','two charger cords & one charger') 
GO