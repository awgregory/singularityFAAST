use [singularitydb]

insert into inventoryitemcategories values 	('hearing'),
											('computersAndTechnology'),
											('speech'),
											('dailyLiving');

declare @categoryid int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'hearing');
declare @categoryid2 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'computersAndTechnology');
declare @categoryid3 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'speech');
declare @categoryid4 int = (select cat.inventorycategoryid from inventoryitemcategories as cat where cat.categoryname like 'dailyLiving');


insert into	inventoryitems			values	('20161106',@categoryid,'invisible hearing aid',200.00,200.00,'superhearing 1000','north east demonstration center',1,'00123456','this hearing aid is awesome!',null,'none'),
											('20170211',@categoryid3,'sound board',40.00,40.00,'communication board','north east demonstration center',1,'98846528','Sound board used to aid communication.',null,'none'),
											('20170124',@categoryid4,'steady spoon',350.00,425.00,'steadyeater1000','north east demonstration center',0,'22225865','Stabilized eating utensil',null,'none'),
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
