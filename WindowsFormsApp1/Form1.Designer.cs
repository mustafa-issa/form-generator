namespace WindowsFormsApp1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbXsdPath = new System.Windows.Forms.TextBox();
            this.tbHtmlPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "XSD Path:";
            // 
            // tbXsdPath
            // 
            this.tbXsdPath.Location = new System.Drawing.Point(94, 6);
            this.tbXsdPath.Name = "tbXsdPath";
            this.tbXsdPath.Size = new System.Drawing.Size(458, 22);
            this.tbXsdPath.TabIndex = 1;
            // 
            // tbHtmlPath
            // 
            this.tbHtmlPath.Location = new System.Drawing.Point(841, 9);
            this.tbHtmlPath.Name = "tbHtmlPath";
            this.tbHtmlPath.Size = new System.Drawing.Size(458, 22);
            this.tbHtmlPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(759, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "HTML Path:";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(15, 34);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1284, 613);
            this.webBrowser1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1126, 653);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 41);
            this.button1.TabIndex = 5;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 706);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.tbHtmlPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbXsdPath);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbXsdPath;
        private System.Windows.Forms.TextBox tbHtmlPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button1;
    }
}

