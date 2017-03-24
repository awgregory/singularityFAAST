--DBCC CHECKIDENT('InventoryItems', reseed, 0)

Select IDENT_CURRENT ('InventoryItems')