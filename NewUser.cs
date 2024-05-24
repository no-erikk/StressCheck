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
            Viewport = viewport;
            ActiveControl = FrmEmpID;
        }

        // insert new user into database
        // 新しいユーザーをデータベースに挿入する
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            using var sql = RDB.Connection.CreateCommand(); // NEED HANDLING OF DUPES IN DB, PREFERRABLY UPDATE IF A VALUE EXISTS OR INSERT IF NOT
            sql.CommandText = @"INSERT INTO EMPLOYEE 
                                    VALUES (@EMP_ID, @EMP_NAME, @GENDER);
                                    
                                    INSERT INTO LOGIN 
                                    VALUES (@EMP_ID, @EMP_PASSWD)";
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
                // execute query
                // クエリを実行
                if (sql.ExecuteNonQuery() > 0)
                {
                    // success
                    // 成功
                    Viewport.currentUser = FrmEmpID.Text;
                    NextScreen?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show("データを更新しました。", "更新成功");
                    // run login procedure with new user information
                    // 新しいユーザーの情報を使ってログインする
                    //LoginAfterRegistration();

                    // ensure connection is closed
                    // 接続を閉じる
                    //sql.Connection.Close();
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

        // cancel new user registration and return to login page
        // 新しいユーザーの登録をキャンセルし、ログインページに戻る
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            PrevScreen?.Invoke(this, EventArgs.Empty);
        }

        // run login procedure with new user information
        // 新しいユーザーの情報を使ってログインする
        private void LoginAfterRegistration()
        {
            using var sql = RDB.Connection.CreateCommand(); // this can probably be changed to not call the server
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

                // move to section title
                // セクション名に移動
                NextScreen?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("従業員IDとパスワードを確認して下さい。", "エラー");
            }
        }
    }
}
