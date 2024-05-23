namespace StressCheck
{
    public partial class SectionTitle : UserControl
    {
        public event EventHandler NextScreen;
        public event EventHandler PrevScreen;
        public event EventHandler ReturnToTitle;

        private Viewport Viewport;


        public SectionTitle(Viewport viewport)
        {
            InitializeComponent();
            Viewport = viewport;
            this.Load += SectionTitle_Load;
        }

        private void SectionTitle_Load(object sender, EventArgs e)
        {
            GetSectionTitle();
        }

        // query database to get information for the current section and set the text
        // 現在のセクションの情報を取得し、テキストをセット
        private void GetSectionTitle()
        {
            using var sql = RDB.Connection.CreateCommand();
            sql.CommandText = @"SELECT T.TITLE, T.Q_CATEGORY, Q.Q_NO, Q.Q_TEXT, T.ANSWER_1, T.ANSWER_2, T.ANSWER_3, T.ANSWER_4, S.SUBTITLE
                                FROM QUESTION_TITLE AS T
                                LEFT JOIN QUESTION AS Q ON T.Q_CATEGORY = Q.Q_CATEGORY
                                LEFT JOIN QUESTION_SUBTITLE AS S ON Q.Q_CATEGORY = S.Q_CATEGORY AND Q.Q_NO = S.Q_NO
                                WHERE T.Q_CATEGORY = 'C';"; // PLACEHOLDER ----- NEED TO JOIN QUESTION_SUBTITLE AS WELL
            sql.Parameters.AddWithValue("@Q_CATEGORY", Viewport.currentQuestionCategory);
            using var reader = sql.ExecuteReader();
            if (reader.Read())
            {
                TxtSectionTitle.Text = ((string)reader["TITLE"]).Replace("\r\n", Environment.NewLine); // NOT CREATING NEW LINE

                // clear old values from datatable and replace with new ones
                // 古い値を削除し、新しい値をセット
                Viewport.questions.Clear();
                Viewport.questions.Load(reader); // WHY DOES ROW 1 GET CUT OFF?

                // count number of questions and display
                // 問題の数をカウントし、表示
                int questionCount = Viewport.questions.Rows.Count;
                TxtNumOfQuestion.Text = questionCount.ToString();
            }
        }

        // move to questions screen
        // 質問の画面に移動
        private void BtnNextScreen_Click(object sender, EventArgs e)
        {
            NextScreen?.Invoke(sender, EventArgs.Empty);
        }
    }
}
