using System.Data;

namespace StressCheck
{
    public partial class Viewport : Form
    {
        private readonly Stack<UserControl> screenHistory = new();
        private UserControl currentScreen;

        public string CurrentUserID { get; set; }
        public string CurrentUserName { get; set; }
        public string CurrentUserGender { get; set; }

        public string CurrentQuestionCategory { get; set; }
        public string PreviousQuestionCategory { get; set; }
        public string CurrentCategoryTitle { get; set; }
        public string PreviousCategoryTitle { get; set; }

        public string CurrentYear { get; set; }
        public int CurrentQuestion { get; set; } = 0;

        public DataTable questions = new();

        public Viewport()
        {
            InitializeComponent();
            CurrentQuestionCategory = "A";
            ShowScreen(new Title(this));
        }

        /// -------------------- Event Handlers -------------------- ///

        // event bus for screen navigation
        // 画面の移動を通知するイベントバス
        private void ShowScreen(UserControl screen)
        {
            switch (screen)
            {
                case Title title:
                    title.NextScreen += Title_NextScreen;
                    title.NewUser += Title_NewUser;
                    break;
                case NewUser newUser:
                    newUser.NextScreen += NewUser_NextScreen;
                    newUser.PrevScreen += NewUser_Title;
                    break;
                case SectionTitle sectionTitle:
                    sectionTitle.NextQuestion += SectionTitle_NextQuestion;
                    sectionTitle.PrevQuestion += SectionTitle_PrevQuestion;
                    sectionTitle.ReturnToTitle += SectionTitle_ReturnToTitle;
                    sectionTitle.DEBUGGoToResults += SectionTitle_DEBUGGoToResults;
                    break;
                case Question question:
                    question.NextScreen += Question_NextScreen;
                    question.PrevScreen += Question_PrevScreen;
                    question.ReturnToTitle += Question_ReturnToTitle;
                    question.Complete += Question_Complete;
                    break;
                case Complete complete:
                    complete.NextScreen += Complete_NextScreen;
                    break;
            }

            // clear panel and show new screen
            // パネルを消去し、新しい画面を表示する
            MainContent.Controls.Clear();
            screen.Dock = DockStyle.Fill;
            MainContent.Controls.Add(screen);

            // set current screen
            // 現在の画面を設定する
            currentScreen = screen;
        }

        // ---------- Title Page ---------- //
        // Title -> SectionTitle
        private void Title_NextScreen(object? sender, EventArgs e)
        {
            screenHistory.Push(currentScreen);
            ShowScreen(new SectionTitle(this));
        }
        // Title -> NewUser
        private void Title_NewUser(object? sender, EventArgs e)
        {
            screenHistory.Push(currentScreen);
            ShowScreen(new NewUser(this));

        }

        // ---------- New User Page ---------- //
        // NewUser -> SectionTitle
        private void NewUser_NextScreen(object? sender, EventArgs e)
        {
            screenHistory.Push(currentScreen);
            ShowScreen(new SectionTitle(this));
        }
        // NewUser -> Title
        private void NewUser_Title(object? sender, EventArgs e)
        {
            if (screenHistory.Count > 0)
            {
                UserControl prevScreen = screenHistory.Pop();
                ShowScreen(prevScreen);
            }
        }

        // ---------- Section Title Page ---------- //
        // SectionTitle -> Questions
        private void SectionTitle_NextQuestion(object? sender, EventArgs e)
        {
            screenHistory.Push(currentScreen);
            ShowScreen(new Question(this));
        }
        // SectionTitle -> Previous Screen
        private void SectionTitle_PrevQuestion(object? sender, EventArgs e)
        {
            if (screenHistory.Count > 0)
            {
                if (screenHistory.Peek() is Question)
                {
                    UserControl prevScreen = screenHistory.Pop();
                    ShowScreen(prevScreen);
                }
            }
        }
        // sectionTitle -> Title
        private void SectionTitle_ReturnToTitle(object? sender, EventArgs e)
        {
            screenHistory.Clear();
            ShowScreen(new Title(this));
        }
        // DEBUG -- SKIP TO RESULT PAGE
        private void SectionTitle_DEBUGGoToResults(object? sender, EventArgs e)
        {
            ShowScreen(new Result(this));
        }

        // ---------- Question Page ---------- //
        // Question -> SectionTitle
        private void Question_NextScreen(object? sender, EventArgs e)
        {
            screenHistory.Push(currentScreen);
            ShowScreen(new SectionTitle(this));
        }
        // Question -> Previous Screen
        private void Question_PrevScreen(object? sender, EventArgs e)
        {
            if (screenHistory.Count > 0)
            {
                if(screenHistory.Peek() is SectionTitle)
                {
                    UserControl prevScreen = screenHistory.Pop();
                    ShowScreen(prevScreen);
                }
            }
        }
        // Question -> Title
        private void Question_ReturnToTitle(object? sender, EventArgs e)
        {
            screenHistory.Clear();
            ShowScreen(new Title(this));
        }
        // Question -> Complete
        private void Question_Complete(object? sender, EventArgs e)
        {
            screenHistory.Push(currentScreen);
            ShowScreen(new Complete());
        }

        // ---------- Completion Page ---------- //
        private void Complete_NextScreen(object? sender, EventArgs e)
        {
            screenHistory.Push(currentScreen);
            ShowScreen(new Result(this));
        }
    } 
}
