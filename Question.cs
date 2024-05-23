using Microsoft.Data.SqlClient;
using System.Data;

namespace StressCheck
{
    public partial class Question : UserControl
    {
        public event EventHandler NextScreen;
        public event EventHandler ReturnToTitle;
        public event EventHandler Complete;

        private int currentQuestion = 0;
        private int selectedAnswer;

        private Viewport Viewport;

        public Question(Viewport viewport)
        {
            InitializeComponent();
            Viewport = viewport;
            this.Load += Question_Load;
        }

        private void Question_Load(object sender, EventArgs e)
        {
            GetQuestion(currentQuestion);
        }

        // get next question
        // 次の質問を取得
        private void GetQuestion(int questionIndex)
        {
            if (Viewport.questions != null && questionIndex >= 0 && questionIndex < Viewport.questions.Rows.Count)
            {
                DataRow dataRow = Viewport.questions.Rows[questionIndex];

                TxtQuestion.Text = dataRow["Q_TEXT"].ToString();
                TxtQuestionSubtitle.Text = dataRow["SUBTITLE"].ToString();
                BtnAns1.Text = dataRow["ANSWER_1"].ToString();
                BtnAns2.Text = dataRow["ANSWER_2"].ToString();
                BtnAns3.Text = dataRow["ANSWER_3"].ToString();
                BtnAns4.Text = dataRow["ANSWER_4"].ToString();
            }
            else if (questionIndex == Viewport.questions.Rows.Count)
            {
                // end of questions, determine next section and go
                // 質問終了後、次のセクションを決めて進む
                switch (Viewport.currentQuestionCategory)
                {
                    case "A":
                        Viewport.currentQuestionCategory = "B";
                        currentQuestion = 0;
                        // go to SectionTitle
                        // SectionTitleに移動
                        NextScreen?.Invoke(this, EventArgs.Empty);
                        break;
                    case "B":
                        Viewport.currentQuestionCategory = "C";
                        currentQuestion = 0;
                        // go to SectionTitle
                        // SectionTitleに移動
                        NextScreen?.Invoke(this, EventArgs.Empty);
                        break;
                    case "C":
                        Viewport.currentQuestionCategory = "D";
                        currentQuestion = 0;
                        // go to SectionTitle
                        // SectionTitleに移動
                        NextScreen?.Invoke(this, EventArgs.Empty);
                        break;
                    case "D":
                        // last section, move to Complete
                        // 最後のセクションなので、Complete画面に移動
                        Complete?.Invoke(this, EventArgs.Empty);
                        break;
                }
            }
        }

        // check which button was selected and return the matching answer
        // 選択されたボタンをチェックし、対応する解答を返す
        private void SubmitAnswer(object sender, EventArgs e)
        {
            try
            {
                // cast sender to button to get the name property
                // senderをボタンにキャストし、nameプロパティを取得
                Button btn = (Button)sender;

                // determine which button was selected and assign answer
                // 選択されたボタンを取得し、解答を割り当てる
                switch (btn.Name)
                {
                    case "BtnAns1":
                        selectedAnswer = 1;
                        break;
                    case "BtnAns2":
                        selectedAnswer = 2;
                        break;
                    case "BtnAns3":
                        selectedAnswer = 3;
                        break;
                    case "BtnAns4":
                        selectedAnswer = 4;
                        break;
                }

                // submit answer to database
                // 回答をデータベースに送信する
                using var sql = RDB.Connection.CreateCommand();
                sql.CommandText = @"INSERT INTO ANSWER 
                                    VALUES (@YEAR, @EMP_ID, @Q_CATEGORY, @Q_NO, @ANSWER, @MOD_ANSWER, @MON_ANSWER2)";

                sql.Parameters.AddWithValue("@YEAR", "2025"); // PLACEHOLDER ---- NEED TO ADD YEAR LATER
                sql.Parameters.AddWithValue("@EMP_ID", Viewport.currentUser);
                sql.Parameters.AddWithValue("@Q_CATEGORY", Viewport.currentQuestionCategory);
                sql.Parameters.AddWithValue("@Q_NO", currentQuestion);
                sql.Parameters.AddWithValue("@ANSWER", selectedAnswer);
                sql.Parameters.AddWithValue(@"MOD_ANSWER", 1); // PLACEHOLDER ---- ADD LATER
                sql.Parameters.AddWithValue(@"MON_ANSWER2", 1); // PLACEHOLDER ---- ADD LATER

                // if answer submitted successfully, move to next question
                // 問題なく提出された場合、次の質問に移動
                if (sql.ExecuteNonQuery() > 0)
                {
                    currentQuestion++;
                    GetQuestion(currentQuestion);

                    // reset answer
                    // 回答をリセット
                    selectedAnswer = 0;
                }
            }
            catch (SqlException ex)
            {
                RDB.ErrorMessage(ex);
            }
        }
    }
}
