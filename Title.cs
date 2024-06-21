namespace StressCheck
{
    public partial class Title : UserControl
    {

        public event EventHandler NextScreen;
        public event EventHandler NewUser;

        private readonly Viewport Viewport;

        public Title(Viewport viewport)
        {
            InitializeComponent();
            Viewport = viewport;
            ActiveControl = FrmEmpID;

            // set current year in viewport
            // 現在の年をビューポートに設定
            Viewport.CurrentYear = DateTime.Now.Year.ToString();
        }

        private void CheckLogin() // TO DO ---- add to query: check ID, Year against ANSWER table to see what the last question answered was
        {
            try
            {
                // use entered ID and password to check if valid, login if valid
                // 入力されたIDとパスワードを使って有効かどうかをチェックし、有効ならログインする
                using var sql = RDB.Connection.CreateCommand();
                sql.CommandText = @"SELECT EMPLOYEE.EMP_NAME AS EMP_NAME, LOGIN.EMP_ID AS EMP_ID
                                    FROM LOGIN 
                                    LEFT JOIN EMPLOYEE ON LOGIN.EMP_ID = EMPLOYEE.EMP_ID
                                    WHERE LOGIN.EMP_ID = @EMP_ID AND LOGIN.PASSWD = @PASSWD;";
                sql.Parameters.AddWithValue("@EMP_ID", FrmEmpID.Text);
                sql.Parameters.AddWithValue("@PASSWD", FrmEmpPass.Text);

                using var reader = sql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // set current user info in viewport
                        // 現在のユーザー情報をビューポートに設定
                        Viewport.CurrentUserID = (string)reader["EMP_ID"];
                        Viewport.CurrentUserName = (string)reader["EMP_NAME"];
                    }

                    MessageBox.Show(Viewport.CurrentUserName + "としてログインしました。", "ログイン成功");
                }
                else
                {
                    MessageBox.Show("従業員IDとパスワードを確認して下さい。", "エラー");
                }
            }
            catch
            {
                MessageBox.Show("従業員IDとパスワードを確認して下さい。", "エラー");
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
                if(Viewport.CurrentUserID != null)
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
