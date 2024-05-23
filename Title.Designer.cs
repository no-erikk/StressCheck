namespace StressCheck
{
    partial class Title
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
            TxtEmpPass = new Label();
            TxtEmpID = new Label();
            BtnNewUser = new Button();
            BtnLogin = new Button();
            FrmEmpPass = new TextBox();
            FrmEmpID = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // TxtEmpPass
            // 
            TxtEmpPass.AutoSize = true;
            TxtEmpPass.Location = new Point(487, 231);
            TxtEmpPass.Name = "TxtEmpPass";
            TxtEmpPass.Size = new Size(86, 25);
            TxtEmpPass.TabIndex = 39;
            TxtEmpPass.Text = "パスワード";
            // 
            // TxtEmpID
            // 
            TxtEmpID.AutoSize = true;
            TxtEmpID.Location = new Point(139, 231);
            TxtEmpID.Name = "TxtEmpID";
            TxtEmpID.Size = new Size(87, 25);
            TxtEmpID.TabIndex = 38;
            TxtEmpID.Text = "従業員ID";
            // 
            // BtnNewUser
            // 
            BtnNewUser.Location = new Point(513, 320);
            BtnNewUser.Name = "BtnNewUser";
            BtnNewUser.Size = new Size(149, 34);
            BtnNewUser.TabIndex = 4;
            BtnNewUser.Text = "新ユーザ登録";
            BtnNewUser.UseVisualStyleBackColor = true;
            BtnNewUser.Click += BtnNewUser_Click;
            // 
            // BtnLogin
            // 
            BtnLogin.Location = new Point(209, 320);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(149, 34);
            BtnLogin.TabIndex = 3;
            BtnLogin.Text = "ログイン";
            BtnLogin.UseVisualStyleBackColor = true;
            BtnLogin.Click += BtnLogin_Click;
            // 
            // FrmEmpPass
            // 
            FrmEmpPass.Location = new Point(487, 263);
            FrmEmpPass.Name = "FrmEmpPass";
            FrmEmpPass.Size = new Size(187, 31);
            FrmEmpPass.TabIndex = 2;
            // 
            // FrmEmpID
            // 
            FrmEmpID.Location = new Point(139, 263);
            FrmEmpID.Name = "FrmEmpID";
            FrmEmpID.Size = new Size(187, 31);
            FrmEmpID.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(162, 106);
            label2.Name = "label2";
            label2.Size = new Size(358, 25);
            label2.TabIndex = 33;
            label2.Text = "短時間で自分のストレスを確認してみましょう。";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(155, 61);
            label1.Name = "label1";
            label1.Size = new Size(119, 25);
            label1.TabIndex = 32;
            label1.Text = "ストレスチェック";
            // 
            // Title
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TxtEmpPass);
            Controls.Add(TxtEmpID);
            Controls.Add(BtnNewUser);
            Controls.Add(BtnLogin);
            Controls.Add(FrmEmpPass);
            Controls.Add(FrmEmpID);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Title";
            Size = new Size(800, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TxtEmpPass;
        private Label TxtEmpID;
        private Button BtnNewUser;
        private Button BtnLogin;
        private TextBox FrmEmpPass;
        private TextBox FrmEmpID;
        private Label label2;
        private Label label1;
    }
}
