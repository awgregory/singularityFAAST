
IF OBJECT_ID('tempdb..#tempDisabilityCategories') IS NOT NULL DROP TABLE #tempDisabilityCategories


CREATE TABLE #tempDisabilityCategories (
	[DisabilityCategoryId] INT NOT NULL IDENTITY(1,1), 
    [DisabilityType] VARCHAR(50) NOT NULL,
)


INSERT INTO #tempDisabilityCategories (DisabilityType) 
VALUES 
	('Cognitive Impairment'),
	('Hearing Impairment'), 
	('Visual Impairment'), 
	('Deaf-Blind'), 
	('Learning Disability'), 
	('No Impairment'), 
	('Dexterity Impairment'), 
	('Mobility Impairment'), 
	('Spinal Cord Injury'), 
	('Elderly'), 
	('Speech/Language Impairment'), 
	('Traumatic Brain Injury'), 
	('Other')


MERGE dbo.DisabilityCategories AS target
USING #tempDisabilityCategories AS source

	ON source.DisabilityCategoryId = target.DisabilityCategoryId

WHEN NOT MATCHED THEN 
	INSERT (DisabilityType)	
	VALUES (source.DisabilityType)

WHEN MATCHED THEN UPDATE 
SET 
	
	target.DisabilityType = source.DisabilityType;

DROP TABLE #tempDisabilityCategories

