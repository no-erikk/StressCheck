using Microsoft.Data.SqlClient;

namespace StressCheck
{
    public partial class NewUser : UserControl
    {
        public event EventHandler NextScreen;
        public event EventHandler PrevScreen;

        private Viewport Viewport;

        public NewUser(Viewport viewport)
        {
            InitializeComponent();
            this.Load += NewUser_Load;
            Viewport = viewport;
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            ActiveControl = FrmEmpID;
        }
        
        private void BtnUpdate_Click(object sender, EventArgs e) // ADJUST TO INSERT INTO LOGIN AND EMPLOYEE VIA TRANSACTION?
        {
            try
            {
                using var sql = RDB.Connection.CreateCommand();
                sql.CommandText = @"INSERT INTO EMPLOYEE
                                VALUES(@EMP_ID, @EMP_PASSWD, @EMP_NAME, @GENDER);";
                sql.Parameters.AddWithValue("@EMP_ID", FrmEmpID.Text);
                sql.Parameters.AddWithValue("@EMP_PASSWD", FrmEmpPass.Text);
                sql.Parameters.AddWithValue("@EMP_NAME", FrmEmpName.Text);
                if (RbtnGenderM.Checked)
                {
                    sql.Parameters.AddWithValue("@GENDER", "M");
                }
                else
                {
                    sql.Parameters.AddWithValue("@GENDER", "F");
                }

                try
                {
                    if (sql.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("データを更新しました。", "更新成功");
                        LoginAfterRegistration();
                        NextScreen?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show($"従業員 ID:{FrmEmpID.Text}は登録されていません", "更新エラー");

                    }
                }
                catch (SqlException ex)
                {
                    RDB.ErrorMessage(ex);
                }
            }
            catch (Exception ex)
            {
                
            }
            
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            PrevScreen?.Invoke(this, EventArgs.Empty);
        }

        private void LoginAfterRegistration()
        {
            using var sql = RDB.Connection.CreateCommand();
            sql.CommandText = @"SELECT EMPLOYEE.EMP_NAME AS EMP_NAME, LOGIN.EMP_ID AS EMP_ID
                                  FROM LOGIN 
                                  LEFT JOIN EMPLOYEE ON LOGIN.EMP_ID = EMPLOYEE.EMP_ID
                                  WHERE LOGIN.EMP_ID = @EMP_ID AND LOGIN.PASSWD = @PASSWD";
            sql.Parameters.AddWithValue("@EMP_ID", FrmEmpID.Text);
            sql.Parameters.AddWithValue("@PASSWD", FrmEmpPass.Text);

            using var reader = sql.ExecuteReader();
            if (reader.Read())
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
    }
}
