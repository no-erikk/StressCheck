using Microsoft.Data.SqlClient;
using System.Data;

namespace StressCheck
{
    public partial class Question : UserControl
    {
        public event EventHandler NextScreen;
        public event EventHandler PrevScreen;
        public event EventHandler ReturnToTitle;
        public event EventHandler Complete;

        private int selectedAnswer;
        private int modAnswer;

        private readonly Viewport Viewport;

        public Question(Viewport viewport)
        {
            InitializeComponent();
            Viewport = viewport;
            this.Load += Question_Load;
        }

        private void Question_Load(object sender, EventArgs e)
        {
            GetQuestion(Viewport.CurrentQuestion);
            PrgQuestions.Maximum = Viewport.questions.Rows.Count;
        }
        
        // get next question
        // 次の質問を取得
        private void GetQuestion(int questionIndex)
        {
            if (Viewport.questions != null && questionIndex >= 0 && questionIndex < Viewport.questions.Rows.Count)
            {
                DataRow dataRow = Viewport.questions.Rows[questionIndex];

                TxtQuestion.Text = ((string)dataRow["Q_TEXT"]).Replace("\\r\\n", Environment.NewLine);
                TxtQuestionSubtitle.Text = dataRow["SUBTITLE"].ToString();
                TxtSectionCategory.Text = $"セクション{Viewport.CurrentQuestionCategory}：";
                TxtSectionName.Text = Viewport.CurrentCategoryTitle;
                BtnAns1.Text = ((string)dataRow["ANSWER_1"]).Replace("\\r\\n", Environment.NewLine);
                BtnAns2.Text = ((string)dataRow["ANSWER_2"]).Replace("\\r\\n", Environment.NewLine);
                BtnAns3.Text = ((string)dataRow["ANSWER_3"]).Replace("\\r\\n", Environment.NewLine);
                BtnAns4.Text = ((string)dataRow["ANSWER_4"]).Replace("\\r\\n", Environment.NewLine);

                // fix text alignment from dynamic content
                // テキストの配置を修正
                TxtQuestion.TextAlign = ContentAlignment.MiddleCenter;
                TxtQuestionSubtitle.TextAlign = ContentAlignment.MiddleCenter;

                // update progress bar
                // 進捗バーを更新
                PrgQuestions.Value = questionIndex + 1;
                PrgQuestions.Refresh();
            }
            else if (questionIndex >= Viewport.questions.Rows.Count)
            {
                // end of questions, determine next section and go
                // 質問終了後、次のセクションを決めて進む
                switch (Viewport.CurrentQuestionCategory)
                {
                    case "A":
                        Viewport.PreviousQuestionCategory = "A";
                        Viewport.CurrentQuestionCategory = "B";
                        // go to SectionTitle
                        // SectionTitleに移動
                        NextScreen?.Invoke(this, EventArgs.Empty);
                        break;
                    case "B":
                        Viewport.PreviousQuestionCategory = "B";
                        Viewport.CurrentQuestionCategory = "C";
                        // go to SectionTitle
                        // SectionTitleに移動
                        NextScreen?.Invoke(this, EventArgs.Empty);
                        break;
                    case "C":
                        Viewport.PreviousQuestionCategory = "C";
                        Viewport.CurrentQuestionCategory = "D";
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
            // cast sender to button to get the name property
            // senderをボタンにキャストし、nameプロパティを取得
            Button btn = (Button)sender;

            try
            {
                // determine which button was selected and assign answer
                // 選択されたボタンを取得し、解答を割り当てる
                if (Viewport.questions.Rows[0]["REV"].Equals(true))
                {
                    switch (btn.Name)
                    {
                        case "BtnAns1":
                            selectedAnswer = 1;
                            modAnswer = 4;
                            break;
                        case "BtnAns2":
                            selectedAnswer = 2;
                            modAnswer = 3;
                            break;
                        case "BtnAns3":
                            selectedAnswer = 3;
                            modAnswer = 2;
                            break;
                        case "BtnAns4":
                            selectedAnswer = 4;
                            modAnswer = 1;
                            break;
                    }
                }
                else
                {
                    switch (btn.Name)
                    {
                        case "BtnAns1":
                            selectedAnswer = 1;
                            modAnswer = 1;
                            break;
                        case "BtnAns2":
                            selectedAnswer = 2;
                            modAnswer = 2;
                            break;
                        case "BtnAns3":
                            selectedAnswer = 3;
                            modAnswer = 3;
                            break;
                        case "BtnAns4":
                            selectedAnswer = 4;
                            modAnswer = 4;
                            break;
                    }
                }   

                // submit answer to database
                // 回答をデータベースに送信する
                using var sql = RDB.Connection.CreateCommand();
                sql.CommandText = @"MERGE INTO ANSWER AS TARGET
                                    USING (VALUES (@YEAR, @EMP_ID, @Q_CATEGORY, @Q_NO, @ANSWER, @MOD_ANSWER, @MOD_ANSWER_2)) AS SOURCE (YEAR, EMP_ID, Q_CATEGORY, Q_NO, ANSWER, MOD_ANSWER, MOD_ANSWER_2)
                                    ON TARGET.YEAR = SOURCE.YEAR AND TARGET.EMP_ID = SOURCE.EMP_ID AND TARGET.Q_CATEGORY = SOURCE.Q_CATEGORY AND TARGET.Q_NO = SOURCE.Q_NO
                                    WHEN MATCHED THEN
                                        UPDATE SET TARGET.ANSWER = SOURCE.ANSWER, TARGET.MOD_ANSWER = SOURCE.MOD_ANSWER, TARGET.MOD_ANSWER_2 = SOURCE.MOD_ANSWER_2
                                    WHEN NOT MATCHED THEN
                                        INSERT (YEAR, EMP_ID, Q_CATEGORY, Q_NO, ANSWER, MOD_ANSWER, MOD_ANSWER_2)
                                        VALUES (YEAR, EMP_ID, Q_CATEGORY, Q_NO, ANSWER, MOD_ANSWER, MOD_ANSWER_2);";
                sql.Parameters.AddWithValue("@YEAR", Viewport.CurrentYear); // ADD YEAR FROM SQL SERVER?
                sql.Parameters.AddWithValue("@EMP_ID", Viewport.CurrentUser);
                sql.Parameters.AddWithValue("@Q_CATEGORY", Viewport.CurrentQuestionCategory);
                sql.Parameters.AddWithValue("@Q_NO", Viewport.CurrentQuestion +1);
                sql.Parameters.AddWithValue("@ANSWER", selectedAnswer);
                sql.Parameters.AddWithValue(@"MOD_ANSWER", modAnswer);
                sql.Parameters.AddWithValue(@"MOD_ANSWER_2", 0); // PLACEHOLDER ---- ADD LATER

                // if answer submitted successfully, move to next question
                // 問題なく提出された場合、次の質問に移動
                if (sql.ExecuteNonQuery() > 0)
                {
                    Viewport.CurrentQuestion++;
                    GetQuestion(Viewport.CurrentQuestion);

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

        // return to the previous question
        // 前の質問に戻る
        private void PrevQuestion_Click(object sender, EventArgs e)
        {
            if (Viewport.CurrentQuestion > 0)
            {
                Viewport.CurrentQuestion--;
                GetQuestion(Viewport.CurrentQuestion);

                // update progress bar backwards
                // 進捗ボタンを逆に更新
                PrgQuestions.Value = Viewport.CurrentQuestion;
                PrgQuestions.Refresh();
            }
            // if it's the first question, return to previous screen
            // 最初の質問なら、前の画面に戻る
            else if (Viewport.CurrentQuestion == 0)
            {
                switch (Viewport.PreviousQuestionCategory)
                {
                    case "A":
                        Viewport.CurrentQuestionCategory = "A";
                        PrevScreen?.Invoke(sender, EventArgs.Empty);
                        break;
                    case "B":
                        Viewport.CurrentQuestionCategory = "B";
                        PrevScreen?.Invoke(sender, EventArgs.Empty);
                        Viewport.PreviousQuestionCategory = "A";
                        break;
                    case "C":
                        Viewport.CurrentQuestionCategory = "C";
                        PrevScreen?.Invoke(sender, EventArgs.Empty);
                        Viewport.PreviousQuestionCategory = "B";
                        break;
                    case "D":
                        Viewport.CurrentQuestionCategory = "D";
                        PrevScreen?.Invoke(sender, EventArgs.Empty);
                        Viewport.PreviousQuestionCategory = "C";
                        break;
                    default:
                        PrevScreen?.Invoke(sender, EventArgs.Empty);
                        Viewport.CurrentQuestionCategory = null;
                        break;
                }
            }
        }

        // return to title screen
        // タイトル画面に戻る
        private void BtnToTitle_Click(object sender, EventArgs e)
        {
            ReturnToTitle?.Invoke(this, new EventArgs());
        }

        // DEBUG - skip question
        private void BtnSkip_DEBUG_Click(object sender, EventArgs e)
        {
            Viewport.CurrentQuestion++;
            GetQuestion(Viewport.CurrentQuestion);
        }
    }
}
