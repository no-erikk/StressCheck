WITH CategorySums AS (
	SELECT
		SUM(CASE WHEN A.Q_CATEGORY = 'A' THEN A.MOD_ANSWER_2 ELSE 0 END) AS sum_A,
		SUM(CASE WHEN A.Q_CATEGORY = 'B' THEN A.MOD_ANSWER_2 ELSE 0 END) AS sum_B,
		SUM(CASE WHEN A.Q_CATEGORY = 'C' OR A.Q_CATEGORY = 'D' THEN A.MOD_ANSWER_2 ELSE 0 END) AS sum_C
	FROM ANSWER A
	LEFT JOIN QUESTION Q ON A.Q_CATEGORY = Q.Q_CATEGORY AND A.Q_NO = Q.Q_NO
	WHERE YEAR = @YEAR AND EMP_ID = @EMP_ID
)
SELECT
	sum_A,
	sum_B,
	sum_C,
	CASE
		WHEN (sum_A + sum_C > 76 AND sum_B > 63) OR sum_B > 77 THEN 'High'
		ELSE 'Normal'
	END AS stress_level
FROM CategorySums;

WITH FactorSums AS (
	SELECT
		E.GENDER,
		A.Q_CATEGORY,
		F.FACTOR,
		SUM(CASE WHEN Q.REV_2 = 1 THEN A.MOD_ANSWER_2 ELSE A.MOD_ANSWER END) AS factor_total,
		F.FACTOR_TEXT
	FROM ANSWER A
	LEFT JOIN QUESTION Q ON A.Q_CATEGORY = Q.Q_CATEGORY AND A.Q_NO = Q.Q_NO
	LEFT JOIN FACTOR F ON Q.FACTOR = F.FACTOR
	LEFT JOIN EMPLOYEE E ON A.EMP_ID = E.EMP_ID
	WHERE YEAR = @YEAR AND A.EMP_ID = @EMP_ID
	GROUP BY E.GENDER, A.Q_CATEGORY, F.FACTOR, F.FACTOR_TEXT
)
SELECT
	Q_CATEGORY,
	FACTOR_TEXT,
	FACTOR,
	CASE
		WHEN GENDER = 'M' THEN　--　男性
			CASE
				WHEN FACTOR IN (11, 12) THEN
					CASE 
						WHEN factor_total = 12 THEN 1
						WHEN factor_total BETWEEN 10 AND 11 THEN 2
						WHEN factor_total BETWEEN 8 AND 9 THEN 3
						WHEN factor_total BETWEEN 6 AND 7 THEN 4
						WHEN factor_total BETWEEN 3 AND 5 THEN 5
						END
				WHEN FACTOR IN (13, 15) THEN factor_total -- 自覚的な身体的負担 / 職場環境によるストレス
				WHEN FACTOR IN (14) THEN -- 職場の対人関係でのストレス
					CASE 
						WHEN factor_total BETWEEN 10 AND 12 THEN 1
						WHEN factor_total BETWEEN 8 AND 9 THEN 2
						WHEN factor_total BETWEEN 6 AND 7 THEN 3
						WHEN factor_total BETWEEN 4 AND 5 THEN 4
						WHEN factor_total = 3 THEN 5
						END
				WHEN FACTOR IN (16, 31) THEN -- 仕事のコントロール度★ / 上司からのサポート★
					CASE
						WHEN factor_total BETWEEN 3 AND 4 THEN 1
						WHEN factor_total BETWEEN 5 AND 6 THEN 2
						WHEN factor_total BETWEEN 7 AND 8 THEN 3
						WHEN factor_total BETWEEN 9 AND 10 THEN 4
						WHEN factor_total BETWEEN 11 AND 12 THEN 5
						END
				WHEN FACTOR IN (17) THEN factor_total -- あなたの技能の活用度★
				WHEN FACTOR IN (18, 19) THEN -- あなたが感じている仕事の適性度★ / 働きがい★
					CASE
						WHEN factor_total BETWEEN 1 AND 3 THEN factor_total
						WHEN factor_total = 4 THEN 5
						END
				WHEN FACTOR IN (21) THEN -- 活気★
					CASE
						WHEN factor_total = 3 THEN 1
						WHEN factor_total BETWEEN 4 AND 5 THEN 2
						WHEN factor_total BETWEEN 6 AND 7 THEN 3
						WHEN factor_total BETWEEN 8 AND 9 THEN 4
						WHEN factor_total BETWEEN 10 AND 12 THEN 5
						END
				WHEN FACTOR IN (22) THEN -- イライラ感
					CASE
						WHEN factor_total BETWEEN 10 AND 12 THEN 1
						WHEN factor_total BETWEEN 8 AND 9 THEN 2
						WHEN factor_total BETWEEN 6 AND 7 THEN 3
						WHEN factor_total BETWEEN 4 AND 5 THEN 4
						WHEN factor_total = 3 THEN 5
						END
				WHEN FACTOR IN (23) THEN -- 疲労感
					CASE
						WHEN factor_total BETWEEN 11 AND 12 THEN 1
						WHEN factor_total BETWEEN 8 AND 10 THEN 2
						WHEN factor_total BETWEEN 5 AND 7 THEN 3
						WHEN factor_total = 4 THEN 4
						WHEN factor_total = 3 THEN 5
						END
				WHEN FACTOR IN (24) THEN -- 不安感
					CASE
						WHEN factor_total BETWEEN 10 AND 12 THEN 1
						WHEN factor_total BETWEEN 8 AND 9 THEN 2
						WHEN factor_total BETWEEN 5 AND 7 THEN 3
						WHEN factor_total = 4 THEN 4
						WHEN factor_total = 3 THEN 5
						END
				WHEN FACTOR IN (25) THEN -- 抑うつ度
					CASE
						WHEN factor_total BETWEEN 17 AND 24 THEN 1
						WHEN factor_total BETWEEN 13 AND 16 THEN 2
						WHEN factor_total BETWEEN 9 AND 12 THEN 3
						WHEN factor_total BETWEEN 7 AND 8 THEN 4
						WHEN factor_total = 6 THEN 5
						END
				WHEN FACTOR IN (26) THEN -- 身体的愁訴
					CASE
						WHEN factor_total BETWEEN 27 AND 44 THEN 1
						WHEN factor_total BETWEEN 22 AND 26 THEN 2
						WHEN factor_total BETWEEN 16 AND 21 THEN 3
						WHEN factor_total BETWEEN 12 AND 15 THEN 4
						WHEN factor_total = 11 THEN 5
						END
				WHEN FACTOR IN (32) THEN -- 同僚からのサポート★
					CASE
						WHEN factor_total BETWEEN 3 AND 5 THEN 1
						WHEN factor_total BETWEEN 6 AND 7 THEN 2
						WHEN factor_total BETWEEN 8 AND 9 THEN 3
						WHEN factor_total BETWEEN 10 AND 11 THEN 4
						WHEN factor_total = 12 THEN 5
						END
				WHEN FACTOR IN (33) THEN -- 家族や友人からのサポート★
					CASE
						WHEN factor_total BETWEEN 3 AND 6 THEN 1
						WHEN factor_total BETWEEN 7 AND 8 THEN 2
						WHEN factor_total = 9 THEN 3
						WHEN factor_total BETWEEN 10 AND 11 THEN 4
						WHEN factor_total = 12 THEN 5
						END
				WHEN FACTOR IN (34) THEN -- 仕事や生活の満足度★
					CASE
						WHEN factor_total BETWEEN 2 AND 3 THEN 1
						WHEN factor_total = 4 THEN 2
						WHEN factor_total BETWEEN 5 AND 6 THEN 3
						WHEN factor_total = 7 THEN 4
						WHEN factor_total = 8 THEN 5
						END
				END
		WHEN GENDER = 'F' THEN -- 女性
			CASE
				WHEN FACTOR IN (11, 12) THEN
					CASE 
						WHEN factor_total = 12 THEN 1
						WHEN factor_total BETWEEN 10 AND 11 THEN 2
						WHEN factor_total BETWEEN 7 AND 9 THEN 3
						WHEN factor_total BETWEEN 5 AND 6 THEN 4
						WHEN factor_total BETWEEN 3 AND 4 THEN 5
						END
				WHEN FACTOR IN (13, 15) THEN factor_total -- 自覚的な身体的負担 / 職場環境によるストレス
				WHEN FACTOR IN (14) THEN -- 職場の対人関係でのストレス
					CASE 
						WHEN factor_total BETWEEN 10 AND 12 THEN 1
						WHEN factor_total BETWEEN 8 AND 9 THEN 2
						WHEN factor_total BETWEEN 6 AND 7 THEN 3
						WHEN factor_total BETWEEN 4 AND 5 THEN 4
						WHEN factor_total = 3 THEN 5
						END
				WHEN FACTOR IN (16, 31) THEN -- 仕事のコントロール度★ / 上司からのサポート★
					CASE
						WHEN factor_total = 3 THEN 1
						WHEN factor_total BETWEEN 4 AND 5 THEN 2
						WHEN factor_total BETWEEN 6 AND 8 THEN 3
						WHEN factor_total BETWEEN 9 AND 10 THEN 4
						WHEN factor_total BETWEEN 11 AND 12 THEN 5
						END
				WHEN FACTOR IN (17) THEN factor_total -- あなたの技能の活用度★
				WHEN FACTOR IN (18, 19) THEN -- あなたが感じている仕事の適性度★ / 働きがい★
					CASE
						WHEN factor_total BETWEEN 1 AND 3 THEN factor_total
						WHEN factor_total = 4 THEN 5
						END
				WHEN FACTOR IN (21) THEN -- 活気★
					CASE
						WHEN factor_total = 3 THEN 1
						WHEN factor_total BETWEEN 4 AND 5 THEN 2
						WHEN factor_total BETWEEN 6 AND 7 THEN 3
						WHEN factor_total BETWEEN 8 AND 9 THEN 4
						WHEN factor_total BETWEEN 10 AND 12 THEN 5
						END
				WHEN FACTOR IN (22) THEN -- イライラ感
					CASE
						WHEN factor_total BETWEEN 11 AND 12 THEN 1
						WHEN factor_total BETWEEN 9 AND 10 THEN 2
						WHEN factor_total BETWEEN 6 AND 8 THEN 3
						WHEN factor_total BETWEEN 4 AND 5 THEN 4
						WHEN factor_total = 3 THEN 5
						END
				WHEN FACTOR IN (23) THEN -- 疲労感
					CASE
						WHEN factor_total = 12 THEN 1
						WHEN factor_total BETWEEN 9 AND 11 THEN 2
						WHEN factor_total BETWEEN 6 AND 8 THEN 3
						WHEN factor_total BETWEEN 4 AND 5 THEN 4
						WHEN factor_total = 3 THEN 5
						END
				WHEN FACTOR IN (24) THEN -- 不安感
					CASE
						WHEN factor_total BETWEEN 11 AND 12 THEN 1
						WHEN factor_total BETWEEN 8 AND 10 THEN 2
						WHEN factor_total BETWEEN 5 AND 7 THEN 3
						WHEN factor_total = 4 THEN 4
						WHEN factor_total = 3 THEN 5
						END
				WHEN FACTOR IN (25) THEN -- 抑うつ度
					CASE
						WHEN factor_total BETWEEN 18 AND 24 THEN 1
						WHEN factor_total BETWEEN 13 AND 17 THEN 2
						WHEN factor_total BETWEEN 9 AND 12 THEN 3
						WHEN factor_total BETWEEN 7 AND 8 THEN 4
						WHEN factor_total = 6 THEN 5
						END
				WHEN FACTOR IN (26) THEN -- 身体的愁訴
					CASE
						WHEN factor_total BETWEEN 30 AND 44 THEN 1
						WHEN factor_total BETWEEN 24 AND 29 THEN 2
						WHEN factor_total BETWEEN 18 AND 23 THEN 3
						WHEN factor_total BETWEEN 14 AND 17 THEN 4
						WHEN factor_total BETWEEN 11 AND 13 THEN 5
						END
				WHEN FACTOR IN (32) THEN -- 同僚からのサポート★
					CASE
						WHEN factor_total BETWEEN 3 AND 5 THEN 1
						WHEN factor_total BETWEEN 6 AND 7 THEN 2
						WHEN factor_total BETWEEN 8 AND 9 THEN 3
						WHEN factor_total BETWEEN 10 AND 11 THEN 4
						WHEN factor_total = 12 THEN 5
						END
				WHEN FACTOR IN (33) THEN -- 家族や友人からのサポート★
					CASE
						WHEN factor_total BETWEEN 3 AND 6 THEN 1
						WHEN factor_total BETWEEN 7 AND 8 THEN 2
						WHEN factor_total = 9 THEN 3
						WHEN factor_total BETWEEN 10 AND 11 THEN 4
						WHEN factor_total = 12 THEN 5
						END
				WHEN FACTOR IN (34) THEN -- 仕事や生活の満足度★
					CASE
						WHEN factor_total BETWEEN 2 AND 3 THEN 1
						WHEN factor_total = 4 THEN 2
						WHEN factor_total BETWEEN 5 AND 6 THEN 3
						WHEN factor_total = 7 THEN 4
						WHEN factor_total = 8 THEN 5
						END
			END
	END AS factor_summary
FROM FactorSums
GROUP BY GENDER, Q_CATEGORY, FACTOR, FACTOR_TEXT, factor_total;