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
                    break;
                case Question question:
                    question.NextScreen += Question_NextScreen;
                    break;
            }

            // clear panel and show new screen
            MainContent.Controls.Clear();
            screen.Dock = DockStyle.Fill;
            MainContent.Controls.Add(screen);

            // set current screen
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
            throw new NotImplementedException();
            screenHistory.Push(currentScreen);
            //ShowScreen(new SectionTitle());
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
        // SectionTitle -> Title
        private void SectionTitle_PrevScreen(object? sender, EventArgs e)
        {
            if (screenHistory.Count > 0)
            {
                UserControl prevScreen = screenHistory.Pop();
                ShowScreen(prevScreen);
            }
        }

        // ---------- Question Page ---------- //
        // Question -> SectionTitle
        private void Question_NextScreen(object? sender, EventArgs e)
        {
            screenHistory.Push(currentScreen);
            ShowScreen(new SectionTitle(this));
        }

    }
}
