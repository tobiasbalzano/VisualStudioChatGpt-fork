namespace VisualStudioChatGpt.Commands
{
    partial class FormRefactoring
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
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_q = new System.Windows.Forms.RichTextBox();
            this.panel_main = new System.Windows.Forms.Panel();
            this.txt_a = new System.Windows.Forms.RichTextBox();
            this.panel_bottom.SuspendLayout();
            this.panel_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.button1);
            this.panel_bottom.Controls.Add(this.txt_q);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 582);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(1436, 125);
            this.panel_bottom.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(1266, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 47);
            this.button1.TabIndex = 1;
            this.button1.Text = "提交问题";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_q
            // 
            this.txt_q.Dock = System.Windows.Forms.DockStyle.Left;
            this.txt_q.Location = new System.Drawing.Point(0, 0);
            this.txt_q.Name = "txt_q";
            this.txt_q.Size = new System.Drawing.Size(1246, 125);
            this.txt_q.TabIndex = 0;
            this.txt_q.Text = "";
            // 
            // panel_main
            // 
            this.panel_main.Controls.Add(this.txt_a);
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_main.Location = new System.Drawing.Point(0, 0);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1436, 582);
            this.panel_main.TabIndex = 1;
            // 
            // txt_a
            // 
            this.txt_a.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_a.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_a.Location = new System.Drawing.Point(0, 0);
            this.txt_a.Name = "txt_a";
            this.txt_a.Size = new System.Drawing.Size(1436, 582);
            this.txt_a.TabIndex = 0;
            this.txt_a.Text = "";
            // 
            // FormRefactoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 707);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_bottom);
            this.Name = "FormRefactoring";
            this.Text = "FormRefactoring";
            this.Load += new System.EventHandler(this.FormRefactoring_Load);
            this.panel_bottom.ResumeLayout(false);
            this.panel_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txt_q;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.RichTextBox txt_a;
    }
}