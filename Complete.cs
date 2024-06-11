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
    public partial class Complete : UserControl
    {
        public event EventHandler NextScreen;

        public Complete()
        {
            InitializeComponent();
        }

        private void BtnViewResult_Click(object sender, EventArgs e)
        {
            NextScreen?.Invoke(this, EventArgs.Empty);
        }
    }
}
