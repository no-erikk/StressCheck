USE STRESS_CHECK;

DELETE FROM EMPLOYEE;
DELETE FROM QUESTION;
DELETE FROM QUESTION_TITLE;
DELETE FROM QUESTION_SUBTITLE;
DELETE FROM ANSWER;
DELETE FROM RESULT_CAT;
DELETE FROM RESULT_TYPE;
DELETE FROM RESULT_FACTOR;
DELETE FROM TYPE;
DELETE FROM FACTOR;
DELETE FROM LOGIN;
DELETE FROM YEAR;
GO

BULK INSERT EMPLOYEE
FROM 'C:\Users\eriky\source\repos\StressCheck\db\employee.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

BULK INSERT QUESTION
FROM 'C:\Users\eriky\source\repos\StressCheck\db\question.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

BULK INSERT QUESTION_TITLE
FROM 'C:\Users\eriky\source\repos\StressCheck\db\question_title.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

BULK INSERT QUESTION_SUBTITLE
FROM 'C:\Users\eriky\source\repos\StressCheck\db\question_subtitle.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

BULK INSERT ANSWER
FROM 'C:\Users\eriky\source\repos\StressCheck\db\answer.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

BULK INSERT RESULT_CAT
FROM 'C:\Users\eriky\source\repos\StressCheck\db\result_cat.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

BULK INSERT RESULT_TYPE
FROM 'C:\Users\eriky\source\repos\StressCheck\db\result_type.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

BULK INSERT RESULT_FACTOR
FROM 'C:\Users\eriky\source\repos\StressCheck\db\result_factor.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

BULK INSERT TYPE
FROM 'C:\Users\eriky\source\repos\StressCheck\db\type.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

BULK INSERT FACTOR
FROM 'C:\Users\eriky\source\repos\StressCheck\db\factor.csv'
WITH
(
   FIELDTERMINATOR = ',',
   ROWTERMINATOR = '\n',
   FIRSTROW = 2
);

INSERT INTO LOGIN SELECT EMP_ID, '' FROM EMPLOYEE;

INSERT INTO YEAR VALUES ('2020',0);
INSERT INTO YEAR VALUES ('2021',0);
INSERT INTO YEAR VALUES ('2022',0);
INSERT INTO YEAR VALUES ('2023',0);
INSERT INTO YEAR VALUES ('2024',1);
