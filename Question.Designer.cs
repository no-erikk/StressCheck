namespace StressCheck
{
    partial class Question
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
            TxtQuestion = new Label();
            TxtQuestionSubtitle = new Label();
            BtnAns1 = new Button();
            BtnAns2 = new Button();
            BtnAns3 = new Button();
            BtnAns4 = new Button();
            SuspendLayout();
            // 
            // TxtQuestion
            // 
            TxtQuestion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TxtQuestion.AutoSize = true;
            TxtQuestion.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtQuestion.Location = new Point(308, 171);
            TxtQuestion.Name = "TxtQuestion";
            TxtQuestion.Size = new Size(163, 48);
            TxtQuestion.TabIndex = 0;
            TxtQuestion.Text = "Question";
            TxtQuestion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TxtQuestionSubtitle
            // 
            TxtQuestionSubtitle.AutoSize = true;
            TxtQuestionSubtitle.Location = new Point(315, 237);
            TxtQuestionSubtitle.Name = "TxtQuestionSubtitle";
            TxtQuestionSubtitle.Size = new Size(149, 25);
            TxtQuestionSubtitle.TabIndex = 1;
            TxtQuestionSubtitle.Text = "Question Subtitle";
            TxtQuestionSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BtnAns1
            // 
            BtnAns1.Location = new Point(80, 360);
            BtnAns1.Name = "BtnAns1";
            BtnAns1.Size = new Size(112, 34);
            BtnAns1.TabIndex = 2;
            BtnAns1.Text = "Answer 1";
            BtnAns1.UseVisualStyleBackColor = true;
            BtnAns1.Click += SubmitAnswer;
            // 
            // BtnAns2
            // 
            BtnAns2.Location = new Point(252, 360);
            BtnAns2.Name = "BtnAns2";
            BtnAns2.Size = new Size(112, 34);
            BtnAns2.TabIndex = 3;
            BtnAns2.Text = "Answer 2";
            BtnAns2.UseVisualStyleBackColor = true;
            BtnAns2.Click += SubmitAnswer;
            // 
            // BtnAns3
            // 
            BtnAns3.Location = new Point(424, 360);
            BtnAns3.Name = "BtnAns3";
            BtnAns3.Size = new Size(112, 34);
            BtnAns3.TabIndex = 4;
            BtnAns3.Text = "Answer 3";
            BtnAns3.UseVisualStyleBackColor = true;
            BtnAns3.Click += SubmitAnswer;
            // 
            // BtnAns4
            // 
            BtnAns4.Location = new Point(596, 360);
            BtnAns4.Name = "BtnAns4";
            BtnAns4.Size = new Size(112, 34);
            BtnAns4.TabIndex = 5;
            BtnAns4.Text = "Answer 4";
            BtnAns4.UseVisualStyleBackColor = true;
            BtnAns4.Click += SubmitAnswer;
            // 
            // Question
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(BtnAns4);
            Controls.Add(BtnAns3);
            Controls.Add(BtnAns2);
            Controls.Add(BtnAns1);
            Controls.Add(TxtQuestionSubtitle);
            Controls.Add(TxtQuestion);
            Name = "Question";
            Size = new Size(800, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TxtQuestion;
        private Label TxtQuestionSubtitle;
        private Button BtnAns1;
        private Button BtnAns2;
        private Button BtnAns3;
        private Button BtnAns4;
    }
}
