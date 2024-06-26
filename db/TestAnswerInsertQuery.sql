﻿DECLARE @YEAR INT = 2025;
DECLARE @EMP_ID CHAR(5) = '10007';
DECLARE @Q_CATEGORY CHAR(1) = 'A';
DECLARE @Q_NO INT = 1;
DECLARE @ANSWER INT = 2;
DECLARE @MOD_ANSWER INT = 3;
DECLARE @MOD_ANSWER_2 INT = 4;

MERGE INTO ANSWER AS TARGET
USING (VALUES (@YEAR, @EMP_ID, @Q_CATEGORY, @Q_NO, @ANSWER, @MOD_ANSWER, @MOD_ANSWER_2)) AS SOURCE (YEAR, EMP_ID, Q_CATEGORY, Q_NO, ANSWER, MOD_ANSWER, MOD_ANSWER_2)
ON TARGET.YEAR = SOURCE.YEAR AND TARGET.EMP_ID = SOURCE.EMP_ID AND TARGET.Q_CATEGORY = SOURCE.Q_CATEGORY AND TARGET.Q_NO = SOURCE.Q_NO AND TARGET.ANSWER = SOURCE.ANSWER
WHEN MATCHED THEN
UPDATE SET MOD_ANSWER = TARGET.ANSWER
WHEN NOT MATCHED THEN
INSERT (YEAR, EMP_ID, Q_CATEGORY, Q_NO, ANSWER)
VALUES (YEAR, EMP_ID, Q_CATEGORY, Q_NO, ANSWER);