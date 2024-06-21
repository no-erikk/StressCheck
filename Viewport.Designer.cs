namespace StressCheck
{
    partial class Viewport
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainContent = new Panel();
            SuspendLayout();
            // 
            // MainContent
            // 
            MainContent.Dock = DockStyle.Fill;
            MainContent.Location = new Point(0, 0);
            MainContent.Name = "MainContent";
            MainContent.Size = new Size(900, 480);
            MainContent.TabIndex = 8;
            // 
            // Viewport
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 480);
            Controls.Add(MainContent);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Viewport";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ストレスチェック";
            ResumeLayout(false);
        }

        #endregion

        private Panel MainContent;
    }
}