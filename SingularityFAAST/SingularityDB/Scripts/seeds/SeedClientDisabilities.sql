

IF OBJECT_ID('tempdb..#tempClientDisabilities') IS NOT NULL DROP TABLE #tempClientDisabilities 


CREATE TABLE #tempClientDisabilities (
	[DisabilityCategoryId] INT NOT NULL, 
    [ClientId] INT NOT NULL,
)


INSERT INTO #tempClientDisabilities (DisabilityCategoryId, ClientId) 
VALUES 
	(1, 2),
	(2, 3),
	(2, 2),
	(2, 4)



MERGE dbo.ClientDisabilities AS target
USING #tempClientDisabilities AS source

	ON source.DisabilityCategoryId = target.DisabilityCategoryId AND source.ClientId = target.ClientId

WHEN NOT MATCHED THEN 
	INSERT (DisabilityCategoryId, ClientId)	
	VALUES (source.DisabilityCategoryId, source.ClientId)

WHEN MATCHED THEN UPDATE 
SET 
	
	target.DisabilityCategoryId = source.DisabilityCategoryId,
	target.ClientId = source.ClientId;

DROP TABLE #tempClientDisabilities 

