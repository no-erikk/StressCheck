using System.Data;

namespace StressCheck
{
    public partial class Viewport : Form
    {
        private Stack<UserControl> screenHistory = new Stack<UserControl>();
        private UserControl currentScreen;

        public string currentUser { get; set; }
        public string currentQuestionCategory { get; set; }
        public string previousQuestionCategory { get; set; }
        public string currentYear { get; set; }
        public int currentQuestion { get; set; } = 0;
        public int lastQuestionNo { get; set; } // WIP

        public DataTable questions = new DataTable();

        public Viewport()
        {
            InitializeComponent();
            currentQuestionCategory = "A";
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
                    sectionTitle.NextScreen += SectionTitle_NextScreen;
                    sectionTitle.PrevScreen += SectionTitle_PrevScreen;

                    sectionTitle.ReturnToTitle += SectionTitle_ReturnToTitle;
                    break;
                case Question question:
                    question.NextScreen += Question_NextScreen;
                    question.PrevScreen += Question_PrevScreen;
                    question.ReturnToTitle += Question_ReturnToTitle;
                    question.Complete += Question_Complete;
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
        private void SectionTitle_NextScreen(object? sender, EventArgs e)
        {
            screenHistory.Push(currentScreen);
            ShowScreen(new Question(this));
        }
        // SectionTitle -> Previous Screen
        private void SectionTitle_PrevScreen(object? sender, EventArgs e)
        {
            if (screenHistory.Count > 0)
            {
                UserControl prevScreen = screenHistory.Pop();
                ShowScreen(prevScreen);
            }
        }
        // SectionTitle -> Title
        private void SectionTitle_ReturnToTitle1(object? sender, EventArgs e)
        {
            screenHistory.Clear();
            ShowScreen(new Title(this));
        }
        // sectionTitle -> Title
        private void SectionTitle_ReturnToTitle(object? sender, EventArgs e)
        {
            screenHistory.Clear();
            ShowScreen(new Title(this));
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
                UserControl prevScreen = screenHistory.Pop();
                ShowScreen(prevScreen);
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
    }
}
