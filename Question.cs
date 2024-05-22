using Microsoft.Data.SqlClient;
using System.Data;

namespace StressCheck
{
    public partial class Question : UserControl
    {
        public event EventHandler NextScreen;
        //public event EventHandler PrevScreen;
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

        private void GetQuestion(int questionIndex)
        {
            if (Viewport.questions != null && questionIndex >= 0 && questionIndex < Viewport.questions.Rows.Count)
            {
                DataRow dataRow = Viewport.questions.Rows[questionIndex];

                TxtQuestion.Text = dataRow["Q_TEXT"].ToString();
                // NEED SUBTITLE DATA FROM SECTIONTITLE.CS
                BtnAns1.Text = dataRow["ANSWER_1"].ToString();
                BtnAns2.Text = dataRow["ANSWER_2"].ToString();
                BtnAns3.Text = dataRow["ANSWER_3"].ToString();
                BtnAns4.Text = dataRow["ANSWER_4"].ToString();
            }
            else if (questionIndex == Viewport.questions.Rows.Count)
            {
                // end of questions, go to next screen
                switch (Viewport.currentQuestionCategory)
                {
                    case "A":
                        Viewport.currentQuestionCategory = "B";
                        break;
                    case "B":
                        Viewport.currentQuestionCategory = "C";
                        break;
                    case "C":
                        Viewport.currentQuestionCategory = "D";
                        break;
                    case "D":
                        break;
                }
                currentQuestion = 0;
                // event handler to go to SectionTitle
                NextScreen?.Invoke(this, EventArgs.Empty);
            }
        }

        // check which button was selected and return the matching answer
        private void SubmitAnswer(object sender, EventArgs e)
        {
            try
            {
                // cast sender to button to get the name property
                Button btn = (Button)sender;

                // determine which button was selected and assign answer
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
                if (sql.ExecuteNonQuery() > 0)
                {
                    currentQuestion++;
                    GetQuestion(currentQuestion);

                    // clear answer
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
