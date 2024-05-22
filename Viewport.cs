using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StressCheck
{
    public partial class Viewport : Form
    {
        private Stack<UserControl> screenHistory = new Stack<UserControl>();
        private UserControl currentScreen;
        public string currentUser { get; set; }

        

        public Viewport()
        {
            InitializeComponent();
            ShowScreen(new Title(this));
        }

        /// -------------------- Event Handlers -------------------- ///

        private void ShowScreen(UserControl screen)
        {
           switch(screen)
            {
                case Title title:
                    title.NextScreen += Title_NextScreen;
                    title.NewUser += Title_NewUser;
                    break;
                case NewUser newUser:
                    newUser.NextScreen += NewUser_NextScreen;
                    newUser.PrevScreen += NewUser_Title;
                    break;
            }

            // clear panel and show new screen
            MainContent.Controls.Clear();
            screen.Dock = DockStyle.Fill;
            MainContent.Controls.Add(screen);

            // set current screen
            currentScreen = screen;
        }

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
            if(screenHistory.Count > 0)
            {
                UserControl prevScreen = screenHistory.Pop();
                ShowScreen(prevScreen);
            }
        }

        // Title -> NewUser
        private void Title_NewUser(object? sender, EventArgs e)
        {
            //throw new NotImplementedException();
            screenHistory.Push(currentScreen);
            ShowScreen(new NewUser(this));

        }

        // Title -> SectionTitle
        private void Title_NextScreen(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
            screenHistory.Push((UserControl)sender);
        }
    }
}
