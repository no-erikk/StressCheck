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
            TxtSectionTitle.Size = new Size(115, 48);
            TxtSectionTitle.TabIndex = 1;
            TxtSectionTitle.Text = "label1";
            // 
            // TxtNumOfQuestion
            // 
            TxtNumOfQuestion.AutoSize = true;
            TxtNumOfQuestion.Location = new Point(110, 213);
            TxtNumOfQuestion.Name = "TxtNumOfQuestion";
            TxtNumOfQuestion.Size = new Size(59, 25);
            TxtNumOfQuestion.TabIndex = 3;
            TxtNumOfQuestion.Text = "label1";
            // 
            // SectionTitle
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
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
    }
}
