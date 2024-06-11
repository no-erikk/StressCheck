using System.Data;

namespace StressCheck
{
    public partial class Result : UserControl
    {

        private readonly Viewport Viewport;

        public Result(Viewport viewport)
        {
            InitializeComponent();
            Viewport = viewport;
            this.Load += Result_Load;
        }

        private void Result_Load(object sender, EventArgs e)
        {
            GetResult();
        }

        private void GetResult()
        {
            // get stress level from database
            // データベースからストレスレベルを取得
            using var sql = RDB.Connection.CreateCommand();
            sql.CommandText = @"WITH CategorySums AS (
	                                SELECT
		                                SUM(CASE WHEN A.Q_CATEGORY = 'A' THEN A.MOD_ANSWER ELSE 0 END) AS sum_A,
		                                SUM(CASE WHEN A.Q_CATEGORY = 'B' THEN A.MOD_ANSWER ELSE 0 END) AS sum_B,
		                                SUM(CASE WHEN A.Q_CATEGORY = 'C' THEN A.MOD_ANSWER ELSE 0 END) AS sum_C,
		                                SUM(CASE WHEN A.Q_CATEGORY = 'D' THEN A.MOD_ANSWER ELSE 0 END) AS sum_D
	                                FROM ANSWER A
	                                LEFT JOIN QUESTION Q ON A.Q_CATEGORY = Q.Q_CATEGORY AND A.Q_NO = Q.Q_NO
	                                WHERE YEAR = @YEAR AND EMP_ID = @EMP_ID
                                )
                                SELECT
	                                CASE
		                                WHEN (sum_A + sum_C > 76 AND sum_B > 63) OR sum_B > 77 THEN 'High'
		                                ELSE 'Normal'
	                                END AS stress_level
                                FROM CategorySums;";
            sql.Parameters.AddWithValue("@YEAR", Viewport.CurrentYear);
            sql.Parameters.AddWithValue("@EMP_ID", Viewport.CurrentUser);
            using var reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // translate to Japanese outside of db due to incorrect collation
                    // 照合順序が正しくないため、dbの外で日本語に翻訳される
                    if (reader["stress_level"].ToString() == "High")
                    {
                        string stressLevel = "高ストレス";
                        TxtStressLevel.Text = stressLevel;
                    }
                    else if(reader["stress_level"].ToString() == "Normal")
                    {
                        string stressLevel = "低ストレス";
                        TxtStressLevel.Text = stressLevel;
                    }

                    
                }
            }
        }
    }
}
