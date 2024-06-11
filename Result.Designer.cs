namespace StressCheck
{
    partial class Result
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
            TxtStressLevel = new Label();
            SuspendLayout();
            // 
            // TxtStressLevel
            // 
            TxtStressLevel.AutoSize = true;
            TxtStressLevel.Location = new Point(408, 195);
            TxtStressLevel.Name = "TxtStressLevel";
            TxtStressLevel.Size = new Size(102, 25);
            TxtStressLevel.TabIndex = 0;
            TxtStressLevel.Text = "Stress Level";
            // 
            // Result
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TxtStressLevel);
            Name = "Result";
            Size = new Size(900, 480);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TxtStressLevel;
    }
}
