namespace StressCheck
{
    partial class NewUser
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BtnCancel = new Button();
            BtnUpdate = new Button();
            emp_name = new Label();
            emp_id = new Label();
            groupBox1 = new GroupBox();
            RbtnGenderF = new RadioButton();
            RbtnGenderM = new RadioButton();
            FrmEmpName = new TextBox();
            FrmEmpID = new TextBox();
            TxtEmpPass = new Label();
            FrmEmpPass = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(478, 343);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(112, 34);
            BtnCancel.TabIndex = 8;
            BtnCancel.Text = "キャンセル";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // BtnUpdate
            // 
            BtnUpdate.Location = new Point(280, 343);
            BtnUpdate.Name = "BtnUpdate";
            BtnUpdate.Size = new Size(112, 34);
            BtnUpdate.TabIndex = 7;
            BtnUpdate.Text = "更新";
            BtnUpdate.UseVisualStyleBackColor = true;
            BtnUpdate.Click += BtnUpdate_Click;
            // 
            // emp_name
            // 
            emp_name.AutoSize = true;
            emp_name.Location = new Point(84, 191);
            emp_name.Name = "emp_name";
            emp_name.Size = new Size(50, 25);
            emp_name.TabIndex = 13;
            emp_name.Text = "氏名";
            // 
            // emp_id
            // 
            emp_id.AutoSize = true;
            emp_id.Location = new Point(47, 96);
            emp_id.Name = "emp_id";
            emp_id.Size = new Size(87, 25);
            emp_id.TabIndex = 12;
            emp_id.Text = "従業員ID";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(RbtnGenderF);
            groupBox1.Controls.Add(RbtnGenderM);
            groupBox1.Location = new Point(81, 225);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(246, 94);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "性別";
            // 
            // RbtnGenderF
            // 
            RbtnGenderF.AutoSize = true;
            RbtnGenderF.Location = new Point(141, 40);
            RbtnGenderF.Name = "RbtnGenderF";
            RbtnGenderF.Size = new Size(75, 29);
            RbtnGenderF.TabIndex = 6;
            RbtnGenderF.TabStop = true;
            RbtnGenderF.Text = "女性";
            RbtnGenderF.UseVisualStyleBackColor = true;
            // 
            // RbtnGenderM
            // 
            RbtnGenderM.AutoSize = true;
            RbtnGenderM.Location = new Point(36, 40);
            RbtnGenderM.Name = "RbtnGenderM";
            RbtnGenderM.Size = new Size(75, 29);
            RbtnGenderM.TabIndex = 5;
            RbtnGenderM.TabStop = true;
            RbtnGenderM.Text = "男性";
            RbtnGenderM.UseVisualStyleBackColor = true;
            // 
            // FrmEmpName
            // 
            FrmEmpName.Location = new Point(140, 188);
            FrmEmpName.Name = "FrmEmpName";
            FrmEmpName.Size = new Size(310, 31);
            FrmEmpName.TabIndex = 3;
            // 
            // FrmEmpID
            // 
            FrmEmpID.Location = new Point(140, 93);
            FrmEmpID.Name = "FrmEmpID";
            FrmEmpID.Size = new Size(150, 31);
            FrmEmpID.TabIndex = 1;
            // 
            // TxtEmpPass
            // 
            TxtEmpPass.AutoSize = true;
            TxtEmpPass.Location = new Point(47, 145);
            TxtEmpPass.Name = "TxtEmpPass";
            TxtEmpPass.Size = new Size(86, 25);
            TxtEmpPass.TabIndex = 17;
            TxtEmpPass.Text = "パスワード";
            // 
            // FrmEmpPass
            // 
            FrmEmpPass.Location = new Point(140, 142);
            FrmEmpPass.Name = "FrmEmpPass";
            FrmEmpPass.Size = new Size(150, 31);
            FrmEmpPass.TabIndex = 2;
            // 
            // NewUser
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TxtEmpPass);
            Controls.Add(FrmEmpPass);
            Controls.Add(BtnCancel);
            Controls.Add(BtnUpdate);
            Controls.Add(emp_name);
            Controls.Add(emp_id);
            Controls.Add(groupBox1);
            Controls.Add(FrmEmpName);
            Controls.Add(FrmEmpID);
            Name = "NewUser";
            Size = new Size(800, 450);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnCancel;
        private Button BtnUpdate;
        private Label emp_name;
        private Label emp_id;
        private GroupBox groupBox1;
        private RadioButton RbtnGenderF;
        private RadioButton RbtnGenderM;
        private TextBox FrmEmpName;
        private TextBox FrmEmpID;
        private Label TxtEmpPass;
        private TextBox FrmEmpPass;
    }
}
