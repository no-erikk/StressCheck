using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace StressCheck
{
    public partial class NewUser : UserControl
    {
        public event EventHandler NextScreen;
        public event EventHandler PrevScreen;

        private readonly Viewport Viewport;

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
            // ensure no fields are empty
            // 入力されていないフィールドがあるか確認する
            if (!(FrmEmpID.Text.IsNullOrEmpty() && FrmEmpPass.Text.IsNullOrEmpty() && FrmEmpName.Text.IsNullOrEmpty()))
            {
                // ensure gender is selected, if selected, insert user into database
                // 性別が選択されている場合、データベースにユーザーを挿入する
                if (!(RbtnGenderM.Text.IsNullOrEmpty() || RbtnGenderF.Text.IsNullOrEmpty()))
                {
                    // insert new user into database or update if already exists
                    // 新しいユーザーをデータベースに挿入する。またはユーザーが既に存在する場合は更新する
                    using var sql = RDB.Connection.CreateCommand();
                    sql.CommandText = @"MERGE INTO EMPLOYEE AS TARGET
                                        USING (VALUES (@EMP_ID, @EMP_NAME, @GENDER)) AS SOURCE (EMP_ID, EMP_NAME, GENDER)
                                        ON TARGET.EMP_ID = SOURCE.EMP_ID
                                        WHEN MATCHED THEN
	                                        UPDATE SET TARGET.EMP_NAME = @EMP_NAME, TARGET.GENDER = @GENDER
                                        WHEN NOT MATCHED THEN
	                                        INSERT (EMP_ID, EMP_NAME, GENDER)
	                                        VALUES (SOURCE.EMP_ID, SOURCE.EMP_NAME, SOURCE.GENDER);
                                    
                                        MERGE INTO LOGIN AS TARGET
                                        USING (VALUES (@EMP_ID, @EMP_PASSWD)) AS SOURCE (EMP_ID, PASSWD)
                                        ON TARGET.EMP_ID = SOURCE.EMP_ID
                                        WHEN MATCHED THEN
                                            UPDATE SET TARGET.PASSWD = @EMP_PASSWD
                                        WHEN NOT MATCHED THEN
                                            INSERT (EMP_ID, PASSWD)
                                            VALUES (SOURCE.EMP_ID, SOURCE.PASSWD);";
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
                            Viewport.CurrentUserID = FrmEmpID.Text;
                            NextScreen?.Invoke(this, EventArgs.Empty);
                            MessageBox.Show($"従業員 ID:{FrmEmpID.Text}は登録しました。", "更新成功");
                            // run login procedure with new user information
                            // 新しいユーザーの情報を使ってログインする
                            LoginAfterRegistration();
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
            }
            else
            {
                MessageBox.Show("すべての項目を入力して下さい。", "エラー");
                return;
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
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    // set current user info in viewport
                    // 現在のユーザー情報をビューポートに設定
                    Viewport.CurrentUserID = (string)reader["EMP_ID"];
                    Viewport.CurrentUserName = (string)reader["EMP_NAME"];
                    MessageBox.Show(Viewport.CurrentUserName + "としてログインしました。", "ログイン成功");
                }
            }
            else
            {
                MessageBox.Show("従業員IDとパスワードを確認して下さい。", "エラー");
            }

            // ensure connection is closed
            // 接続を閉じる
            reader.Close();
            sql.Dispose();

            // run login check and move to next screen
            // ログインチェックを実行し、次の画面に移動
            if (Viewport.CurrentUserID != null)
            {
                // move to section title
                // セクション名に移動
                NextScreen?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
