----check if temp table exists
--if OBJECT_ID('tempdb..#tempLoanMasterTable') is not null drop table #tempLoanMasterTable;
----drop if exists


----create temp table
--create table #tempLoanMasterTable
--(
--	--copy designer

--);


----insert values 
--insert into #tempLoanMasterTable (table field here)
--values
--('blah'),
--('blah'),
--('blah');

----merge
--merge dbo.LoanMaster as target
--using #tempLoanMasterTable as source
-- on source.Identity column here = target.Identity column here;
--when not matched then
-- insert (table field(s))
-- values( source.table field(s)
 
-- when matched then update 
-- set 
-- target.table field  = source. table field;


----drop temp table
--drop table #tempLoanMasterTable;