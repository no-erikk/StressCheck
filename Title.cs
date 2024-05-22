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
    public partial class Title : UserControl
    {

        public event EventHandler NextScreen;
        public event EventHandler NewUser;

        private Viewport Viewport;

        public Title(Viewport viewport)
        {
            InitializeComponent();
            Viewport = viewport;
        }

        private void CheckLogin()
        {
            try
            {
                // use entered ID and password to check if valid, login if valid
                using var sql = RDB.Connection.CreateCommand();
                sql.CommandText = "SELECT EMPLOYEE.EMP_NAME AS EMP_NAME, LOGIN.EMP_ID AS EMP_ID " +
                                  "FROM LOGIN " +
                                  "LEFT JOIN EMPLOYEE ON LOGIN.EMP_ID = EMPLOYEE.EMP_ID " +
                                  "WHERE LOGIN.EMP_ID = @EMP_ID AND LOGIN.PASSWD = @PASSWD";
                sql.Parameters.AddWithValue("@EMP_ID", FrmEmpID.Text);
                sql.Parameters.AddWithValue("@PASSWD", FrmEmpPass.Text);
                
                using var reader = sql.ExecuteReader();
                if(reader.Read())
                {
                    // set current user ID in viewport
                    Viewport.currentUser = (string)reader["EMP_ID"];
                    var userName = (string)reader["EMP_NAME"];
                    MessageBox.Show(userName + "としてログインしました。", "ログイン成功");
                }
                else
                {
                    MessageBox.Show("従業員IDとパスワードを確認して下さい。", "エラー");
                }
            }
            catch
            {

            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if(FrmEmpID.Text != "" && FrmEmpPass.Text != "")
            {
                CheckLogin();
                //NextScreen?.Invoke(sender, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("従業員IDとパスワードを入力して下さい。", "エラー");
            }
        }

        private void BtnNewUser_Click(object sender, EventArgs e)
        {
            NewUser?.Invoke(sender, EventArgs.Empty);
        }
    }
}
