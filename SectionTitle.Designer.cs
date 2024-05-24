namespace StressCheck
{
    partial class SectionTitle
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
            BtnNextScreen = new Button();
            TxtSectionTitle = new Label();
            TxtNumOfQuestion = new Label();
            BtnBack = new Button();
            BtnToTitle = new Button();
            SuspendLayout();
            // 
            // BtnNextScreen
            // 
            BtnNextScreen.Location = new Point(110, 324);
            BtnNextScreen.Name = "BtnNextScreen";
            BtnNextScreen.Size = new Size(112, 34);
            BtnNextScreen.TabIndex = 0;
            BtnNextScreen.Text = "次へ";
            BtnNextScreen.UseVisualStyleBackColor = true;
            BtnNextScreen.Click += BtnNextScreen_Click;
            // 
            // TxtSectionTitle
            // 
            TxtSectionTitle.AutoSize = true;
            TxtSectionTitle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtSectionTitle.Location = new Point(110, 112);
            TxtSectionTitle.Name = "TxtSectionTitle";
            TxtSectionTitle.Size = new Size(205, 48);
            TxtSectionTitle.TabIndex = 1;
            TxtSectionTitle.Text = "SectionTitle";
            TxtSectionTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TxtNumOfQuestion
            // 
            TxtNumOfQuestion.AutoSize = true;
            TxtNumOfQuestion.Location = new Point(110, 213);
            TxtNumOfQuestion.Name = "TxtNumOfQuestion";
            TxtNumOfQuestion.Size = new Size(184, 25);
            TxtNumOfQuestion.TabIndex = 3;
            TxtNumOfQuestion.Text = "Number of Questions";
            TxtNumOfQuestion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BtnBack
            // 
            BtnBack.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnBack.Location = new Point(166, 421);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(157, 26);
            BtnBack.TabIndex = 9;
            BtnBack.Text = "一つ前の画面に戻る";
            BtnBack.UseVisualStyleBackColor = true;
            BtnBack.Click += BtnBack_Click;
            // 
            // BtnToTitle
            // 
            BtnToTitle.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnToTitle.Location = new Point(3, 421);
            BtnToTitle.Name = "BtnToTitle";
            BtnToTitle.Size = new Size(157, 26);
            BtnToTitle.TabIndex = 8;
            BtnToTitle.Text = "タイトル画面に戻る";
            BtnToTitle.UseVisualStyleBackColor = true;
            BtnToTitle.Click += BtnToTitle_Click;
            // 
            // SectionTitle
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(BtnBack);
            Controls.Add(BtnToTitle);
            Controls.Add(TxtNumOfQuestion);
            Controls.Add(TxtSectionTitle);
            Controls.Add(BtnNextScreen);
            Name = "SectionTitle";
            Size = new Size(800, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnNextScreen;
        private Label TxtSectionTitle;
        private Label TxtNumOfQuestion;
        private Button BtnBack;
        private Button BtnToTitle;
    }
}
