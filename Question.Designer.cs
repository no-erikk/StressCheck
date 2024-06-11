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
            BtnToTitle = new Button();
            BtnBack = new Button();
            BtnSkip_DEBUG = new Button();
            TxtSectionName = new Label();
            TxtSectionCategory = new Label();
            PrgQuestions = new ProgressBar();
            SuspendLayout();
            // 
            // TxtQuestion
            // 
            TxtQuestion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TxtQuestion.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtQuestion.Location = new Point(0, 113);
            TxtQuestion.Name = "TxtQuestion";
            TxtQuestion.Padding = new Padding(10, 0, 10, 0);
            TxtQuestion.Size = new Size(900, 120);
            TxtQuestion.TabIndex = 0;
            TxtQuestion.Text = "Question";
            TxtQuestion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TxtQuestionSubtitle
            // 
            TxtQuestionSubtitle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TxtQuestionSubtitle.Location = new Point(3, 233);
            TxtQuestionSubtitle.Name = "TxtQuestionSubtitle";
            TxtQuestionSubtitle.Padding = new Padding(10, 0, 10, 0);
            TxtQuestionSubtitle.Size = new Size(897, 95);
            TxtQuestionSubtitle.TabIndex = 1;
            TxtQuestionSubtitle.Text = "Question Subtitle";
            TxtQuestionSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BtnAns1
            // 
            BtnAns1.Location = new Point(98, 341);
            BtnAns1.Name = "BtnAns1";
            BtnAns1.Size = new Size(112, 67);
            BtnAns1.TabIndex = 2;
            BtnAns1.Text = "Answer 1";
            BtnAns1.UseVisualStyleBackColor = true;
            BtnAns1.Click += SubmitAnswer;
            // 
            // BtnAns2
            // 
            BtnAns2.Location = new Point(295, 341);
            BtnAns2.Name = "BtnAns2";
            BtnAns2.Size = new Size(112, 67);
            BtnAns2.TabIndex = 3;
            BtnAns2.Text = "Answer 2";
            BtnAns2.UseVisualStyleBackColor = true;
            BtnAns2.Click += SubmitAnswer;
            // 
            // BtnAns3
            // 
            BtnAns3.Location = new Point(492, 341);
            BtnAns3.Name = "BtnAns3";
            BtnAns3.Size = new Size(112, 67);
            BtnAns3.TabIndex = 4;
            BtnAns3.Text = "Answer 3";
            BtnAns3.UseVisualStyleBackColor = true;
            BtnAns3.Click += SubmitAnswer;
            // 
            // BtnAns4
            // 
            BtnAns4.Location = new Point(689, 341);
            BtnAns4.Name = "BtnAns4";
            BtnAns4.Size = new Size(112, 67);
            BtnAns4.TabIndex = 5;
            BtnAns4.Text = "Answer 4";
            BtnAns4.UseVisualStyleBackColor = true;
            BtnAns4.Click += SubmitAnswer;
            // 
            // BtnToTitle
            // 
            BtnToTitle.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnToTitle.Location = new Point(4, 450);
            BtnToTitle.Name = "BtnToTitle";
            BtnToTitle.Size = new Size(157, 26);
            BtnToTitle.TabIndex = 6;
            BtnToTitle.Text = "タイトル画面に戻る";
            BtnToTitle.UseVisualStyleBackColor = true;
            BtnToTitle.Click += BtnToTitle_Click;
            // 
            // BtnBack
            // 
            BtnBack.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnBack.Location = new Point(167, 450);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(157, 26);
            BtnBack.TabIndex = 7;
            BtnBack.Text = "一つ前の画面に戻る";
            BtnBack.UseVisualStyleBackColor = true;
            BtnBack.Click += PrevQuestion_Click;
            // 
            // BtnSkip_DEBUG
            // 
            BtnSkip_DEBUG.Location = new Point(778, 445);
            BtnSkip_DEBUG.Name = "BtnSkip_DEBUG";
            BtnSkip_DEBUG.Size = new Size(119, 34);
            BtnSkip_DEBUG.TabIndex = 8;
            BtnSkip_DEBUG.Text = "Skip DEBUG";
            BtnSkip_DEBUG.UseVisualStyleBackColor = true;
            BtnSkip_DEBUG.Click += BtnSkip_DEBUG_Click;
            // 
            // TxtSectionName
            // 
            TxtSectionName.AutoSize = true;
            TxtSectionName.Location = new Point(108, 0);
            TxtSectionName.Name = "TxtSectionName";
            TxtSectionName.Size = new Size(122, 25);
            TxtSectionName.TabIndex = 9;
            TxtSectionName.Text = "Section Name";
            // 
            // TxtSectionCategory
            // 
            TxtSectionCategory.AutoSize = true;
            TxtSectionCategory.Location = new Point(0, 0);
            TxtSectionCategory.Name = "TxtSectionCategory";
            TxtSectionCategory.Size = new Size(112, 25);
            TxtSectionCategory.TabIndex = 10;
            TxtSectionCategory.Text = "セクションA：";
            // 
            // PrgQuestions
            // 
            PrgQuestions.Location = new Point(696, 3);
            PrgQuestions.Name = "PrgQuestions";
            PrgQuestions.Size = new Size(201, 22);
            PrgQuestions.Step = 1;
            PrgQuestions.Style = ProgressBarStyle.Continuous;
            PrgQuestions.TabIndex = 11;
            // 
            // Question
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(PrgQuestions);
            Controls.Add(TxtSectionCategory);
            Controls.Add(TxtSectionName);
            Controls.Add(BtnSkip_DEBUG);
            Controls.Add(BtnBack);
            Controls.Add(BtnToTitle);
            Controls.Add(BtnAns4);
            Controls.Add(BtnAns3);
            Controls.Add(BtnAns2);
            Controls.Add(BtnAns1);
            Controls.Add(TxtQuestionSubtitle);
            Controls.Add(TxtQuestion);
            Name = "Question";
            Size = new Size(900, 480);
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
        private Button BtnToTitle;
        private Button BtnBack;
        private Button BtnSkip_DEBUG;
        private Label TxtSectionName;
        private Label TxtSectionCategory;
        private ProgressBar PrgQuestions;
    }
}
