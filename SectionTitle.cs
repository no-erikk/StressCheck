namespace StressCheck
{
    public partial class SectionTitle : UserControl
    {
        public event EventHandler NextQuestion;
        public event EventHandler PrevQuestion;
        public event EventHandler ReturnToTitle;

        public event EventHandler DEBUGGoToResults;

        private readonly Viewport Viewport;


        public SectionTitle(Viewport viewport)
        {
            InitializeComponent();
            Viewport = viewport;
            this.Load += SectionTitle_Load;
        }

        private void SectionTitle_Load(object sender, EventArgs e)
        {
            Viewport.CurrentQuestion = 0;
            GetSectionTitle();
        }

        // query database to get information for the current section and set the text
        // 現在のセクションの情報を取得し、テキストをセット
        private void GetSectionTitle()
        {
            using var sql = RDB.Connection.CreateCommand();
            sql.CommandText = @"SELECT T.TITLE, T.Q_CATEGORY, Q.Q_NO, Q.Q_TEXT, T.ANSWER_1, T.ANSWER_2, T.ANSWER_3, T.ANSWER_4, Q.REV, Q.REV_2, S.SUBTITLE
                                FROM QUESTION_TITLE AS T
                                LEFT JOIN QUESTION AS Q ON T.Q_CATEGORY = Q.Q_CATEGORY
                                LEFT JOIN QUESTION_SUBTITLE AS S ON Q.Q_CATEGORY = S.Q_CATEGORY AND Q.Q_NO = S.Q_NO
                                WHERE T.Q_CATEGORY = @Q_CATEGORY;";
            sql.Parameters.AddWithValue("@Q_CATEGORY", Viewport.CurrentQuestionCategory);
            using var reader = sql.ExecuteReader();
            if (reader.HasRows)
            {
                // clear old values from datatable and replace with new ones
                // 古い値を削除し、新しい値をセット
                Viewport.questions.Clear();
                Viewport.questions.Load(reader);

                // set current category title
                // 現在のセクション名を設定
                Viewport.CurrentCategoryTitle = ((string)Viewport.questions.Rows[0]["TITLE"]).Replace("\\r\\n", Environment.NewLine);
                TxtSectionTitle.Text = Viewport.CurrentCategoryTitle;
                TxtSectionCategory.Text = $"セクション{Viewport.CurrentQuestionCategory}";

                // count number of questions and display
                // 問題の数をカウントし、表示
                int questionCount = Viewport.questions.Rows.Count;
                TxtNumOfQuestion.Text = $"全{questionCount}問";
            }
        }

        // move to questions screen
        // 質問の画面に移動
        private void BtnNextScreen_Click(object sender, EventArgs e)
        {
            NextQuestion?.Invoke(sender, EventArgs.Empty);
        }

        // return to title screen
        // タイトル画面に戻る
        private void BtnToTitle_Click(object sender, EventArgs e)
        {
            ReturnToTitle?.Invoke(sender, EventArgs.Empty);
        }

        // go back to previous screen (question or section title)
        // 前の画面に戻る（質問またはセクションタイトル）
        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (Viewport.CurrentQuestionCategory != null)
            {
                if (Viewport.CurrentQuestion == 0)
                {
                    GetSectionTitle();
                    Viewport.CurrentQuestion = Viewport.questions.Rows.Count;

                    PrevQuestion?.Invoke(sender, EventArgs.Empty);
                }
            }
            else
            {
                ReturnToTitle?.Invoke(sender, EventArgs.Empty);
            }
        }

        // debug -- skip to result page
        // 
        private void button1_Click(object sender, EventArgs e)
        {
            DEBUGGoToResults?.Invoke(sender, EventArgs.Empty);
        }
    }
}
