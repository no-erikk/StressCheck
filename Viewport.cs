using System.Data;

namespace StressCheck
{
    public partial class Viewport : Form
    {
        private Stack<UserControl> screenHistory = new Stack<UserControl>();
        private UserControl currentScreen;

        public string currentUser { get; set; }
        public string currentQuestionCategory { get; set; }
        public string currentYear { get; set; }

        public DataTable questions = new DataTable();

        public Viewport()
        {
            InitializeComponent();
            currentQuestionCategory = "A";
            ShowScreen(new Title(this));
        }

        /// -------------------- Event Handlers -------------------- ///
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

        private void SectionTitle_ReturnToTitle1(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
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

        // Question -> Complete
        private void Question_Complete(object? sender, EventArgs e)
        {
            ShowScreen(new Complete());
        }
    }
}
