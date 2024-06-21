using System.Windows.Forms.DataVisualization.Charting;

namespace StressCheck
{
    public partial class Result : UserControl
    {
        private Label[] labels;

        private readonly Viewport Viewport;

        public Result(Viewport viewport)
        {
            InitializeComponent();
            Viewport = viewport;
            this.Load += Result_Load;
        }

        private void Result_Load(object sender, EventArgs e)
        {
            Viewport.Size = new Size(this.Width + 22, this.Height + 56);

            // initialize list of labels to use for results display
            // 結果表示用のラベルのリストを初期化
            labels = [LblFactor11, LblFactor12, LblFactor13, LblFactor14, LblFactor15, LblFactor16, LblFactor17, LblFactor18, LblFactor19,
                      LblFactor21, LblFactor22, LblFactor23, LblFactor24, LblFactor25, LblFactor26,
                      LblFactor31, LblFactor32, LblFactor33, LblFactor34];

            // display user information
            // ユーザー情報を表示
            LblEmpID.Text = Viewport.CurrentUserID;
            LblEmpName.Text = Viewport.CurrentUserName;

            // get results from db and display
            // dbから結果を取得し、表示
            GetResult();
        }

        private void GetResult()
        {
            // path to query file
            // クエリファイルのパス
            string queryFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GetResults.sql");
            string queryText = File.ReadAllText(queryFilePath);

            // get stress level from database
            // データベースからストレスレベルを取得
            using var sql = RDB.Connection.CreateCommand();
            sql.CommandText = queryText;

            sql.Parameters.AddWithValue("@YEAR", Viewport.CurrentYear);
            sql.Parameters.AddWithValue("@EMP_ID", Viewport.CurrentUserID);
            using var reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string stressLevel = null;

                    // translate to Japanese outside of db due to incorrect collation
                    // 照合順序が正しくないため、dbの外で日本語に翻訳される
                    if (reader["stress_level"].ToString() == "High")
                    {
                        stressLevel = "高い";
                    }
                    else if (reader["stress_level"].ToString() == "Normal")
                    {
                        stressLevel = "普通";
                    }

                    TxtStressLevel.Text = $"あなたのストレスレベルは{stressLevel}です。";
                }
            }

            if (reader.NextResult())
            {
                // iterate through results and assign to matching labels
                // 結果を順に処理し、対応するラベルに代入
                int labelIndex = 0;
                while (reader.Read() && labelIndex < labels.Length)
                {
                    Chart chartName;
                    string labelText = labels[labelIndex].Text;

                    switch ((string)reader["Q_CATEGORY"])
                    {
                        case "A":
                            chartName = ChartA;
                            break;
                        case "B":
                            chartName = ChartB;
                            break;
                        default:
                            chartName = ChartC;
                            break;
                    }
                    // distribute data between charts based on category
                    // カテログリごとにデータをグラフに配分
                    int factor = Convert.ToInt32(reader["FACTOR"]);
                    int value = Convert.ToInt32(reader["factor_summary"]);
                    chartName.Series["Series1"].Points.AddXY(factor, value);
                    chartName.Series["Series2"].Points.AddXY(factor, 1); // 参照点

                    // set ★ labels to indicate when axis is inverted (5 = high stress)
                    // 5 = 高ストレスの時に★を設定
                    int dataIndex = chartName.Series["Series1"].Points.Count - 1;
                    chartName.Series["Series1"].Points[dataIndex].AxisLabel = (string)reader["FACTOR_TEXT"];

                    labels[labelIndex].Text = labelText + "：" + value;
                    labelIndex++;

                }
            }
        }
    }
}
