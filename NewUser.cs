using Microsoft.Data.SqlClient;
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

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            using var sql = RDB.Connection.CreateCommand();
            sql.CommandText = "UPDATE EMPLOYEE SET EMP_NAME = @EMP_NAME, GENDER = @GENDER WHERE EMP_ID = @EMP_ID";
            sql.Parameters.AddWithValue("@EMP_NAME", FrmEmpName.Text);
            if (RbtnGenderM.Checked)
            {
                sql.Parameters.AddWithValue("@GENDER", "M");
            }
            else
            {
                sql.Parameters.AddWithValue("@GENDER", "F");
            }
            sql.Parameters.AddWithValue("@EMP_ID", FrmEmpID.Text);

            try
            {
                if (sql.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("データを更新しました。", "更新成功");
                }
                else
                {
                    MessageBox.Show($"従業員 ID:{FrmEmpID}は登録されていません", "更新エラー");

                }
            }
            catch (SqlException ex)
            {
                RDB.ErrorMessage(ex);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            PrevScreen?.Invoke(this, EventArgs.Empty);
        }
    }
}
