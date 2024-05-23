﻿namespace StressCheck
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
            ActiveControl = FrmEmpID;
        }

        private void CheckLogin()
        {
            try
            {
                // use entered ID and password to check if valid, login if valid
                // 入力されたIDとパスワードを使って有効かどうかをチェックし、有効ならログインする
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
                    // 現在のユーザーIDをビューポートに設定
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

        // run login check and move to next screen
        // ログインチェックを実行し、次の画面に移動
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (FrmEmpID.Text != "" && FrmEmpPass.Text != "")
            {
                CheckLogin();
                // check for active user, if found move to next screen
                // 現在のユーザーを検索し、見つかったら次の画面に移動
                if(Viewport.currentUser != null)
                {
                    NextScreen?.Invoke(sender, EventArgs.Empty);
                }
            }
            else
            {
                MessageBox.Show("従業員IDとパスワードを入力して下さい。", "エラー");
            }
        }

        // go to NewUser page
        // NewUserページに移動
        private void BtnNewUser_Click(object sender, EventArgs e)
        {
            NewUser?.Invoke(sender, EventArgs.Empty);
        }
    }
}
