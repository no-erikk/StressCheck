namespace StressCheck
{
    partial class Complete
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
            TxtCompleteMsg = new Label();
            TxtCompleteSubtitle = new Label();
            BtnViewResult = new Button();
            SuspendLayout();
            // 
            // TxtCompleteMsg
            // 
            TxtCompleteMsg.AutoSize = true;
            TxtCompleteMsg.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtCompleteMsg.Location = new Point(126, 159);
            TxtCompleteMsg.Name = "TxtCompleteMsg";
            TxtCompleteMsg.Size = new Size(432, 48);
            TxtCompleteMsg.TabIndex = 0;
            TxtCompleteMsg.Text = "ストレスチェックは終了です。";
            // 
            // TxtCompleteSubtitle
            // 
            TxtCompleteSubtitle.AutoSize = true;
            TxtCompleteSubtitle.Location = new Point(126, 223);
            TxtCompleteSubtitle.Name = "TxtCompleteSubtitle";
            TxtCompleteSubtitle.Size = new Size(370, 25);
            TxtCompleteSubtitle.TabIndex = 1;
            TxtCompleteSubtitle.Text = "ボタンを押すとストレスチェックの結果が見えます。";
            // 
            // BtnViewResult
            // 
            BtnViewResult.Location = new Point(126, 373);
            BtnViewResult.Name = "BtnViewResult";
            BtnViewResult.Size = new Size(112, 34);
            BtnViewResult.TabIndex = 2;
            BtnViewResult.Text = "結果へ";
            BtnViewResult.UseVisualStyleBackColor = true;
            BtnViewResult.Click += BtnViewResult_Click;
            // 
            // Complete
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(BtnViewResult);
            Controls.Add(TxtCompleteSubtitle);
            Controls.Add(TxtCompleteMsg);
            Name = "Complete";
            Size = new Size(900, 480);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TxtCompleteMsg;
        private Label TxtCompleteSubtitle;
        private Button BtnViewResult;
    }
}
