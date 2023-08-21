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
            this.txt_q = new System.Windows.Forms.RichTextBox();
            this.btn_tijiao = new System.Windows.Forms.Button();
            this.txt_a = new System.Windows.Forms.RichTextBox();
            this.splitContainer_main = new System.Windows.Forms.SplitContainer();
            this.splitContainer_bottom = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).BeginInit();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_bottom)).BeginInit();
            this.splitContainer_bottom.Panel1.SuspendLayout();
            this.splitContainer_bottom.Panel2.SuspendLayout();
            this.splitContainer_bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_q
            // 
            this.txt_q.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_q.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_q.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txt_q.Location = new System.Drawing.Point(0, 0);
            this.txt_q.Name = "txt_q";
            this.txt_q.Size = new System.Drawing.Size(25, 46);
            this.txt_q.TabIndex = 0;
            this.txt_q.Text = "";
            // 
            // btn_tijiao
            // 
            this.btn_tijiao.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_tijiao.Location = new System.Drawing.Point(5, 87);
            this.btn_tijiao.Name = "btn_tijiao";
            this.btn_tijiao.Size = new System.Drawing.Size(119, 52);
            this.btn_tijiao.TabIndex = 1;
            this.btn_tijiao.Text = "提交问题";
            this.btn_tijiao.UseVisualStyleBackColor = true;
            this.btn_tijiao.Click += new System.EventHandler(this.btn_tijiao_Click);
            // 
            // txt_a
            // 
            this.txt_a.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(248)))));
            this.txt_a.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_a.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_a.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_a.Location = new System.Drawing.Point(0, 0);
            this.txt_a.Name = "txt_a";
            this.txt_a.ReadOnly = true;
            this.txt_a.Size = new System.Drawing.Size(1436, 707);
            this.txt_a.TabIndex = 0;
            this.txt_a.Text = "";
            // 
            // splitContainer_main
            // 
            this.splitContainer_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer_main.IsSplitterFixed = true;
            this.splitContainer_main.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_main.Name = "splitContainer_main";
            this.splitContainer_main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_main.Panel1
            // 
            this.splitContainer_main.Panel1.Controls.Add(this.txt_a);
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.splitContainer_bottom);
            this.splitContainer_main.Panel2Collapsed = true;
            this.splitContainer_main.Size = new System.Drawing.Size(1436, 707);
            this.splitContainer_main.SplitterDistance = 549;
            this.splitContainer_main.SplitterWidth = 1;
            this.splitContainer_main.TabIndex = 2;
            // 
            // splitContainer_bottom
            // 
            this.splitContainer_bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_bottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer_bottom.IsSplitterFixed = true;
            this.splitContainer_bottom.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_bottom.Name = "splitContainer_bottom";
            // 
            // splitContainer_bottom.Panel1
            // 
            this.splitContainer_bottom.Panel1.Controls.Add(this.txt_q);
            // 
            // splitContainer_bottom.Panel2
            // 
            this.splitContainer_bottom.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer_bottom.Panel2.Controls.Add(this.btn_tijiao);
            this.splitContainer_bottom.Size = new System.Drawing.Size(150, 46);
            this.splitContainer_bottom.SplitterDistance = 25;
            this.splitContainer_bottom.SplitterWidth = 1;
            this.splitContainer_bottom.TabIndex = 0;
            // 
            // FormRefactoring
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 707);
            this.Controls.Add(this.splitContainer_main);
            this.Name = "FormRefactoring";
            this.Text = "代码重构对话";
            this.Load += new System.EventHandler(this.FormRefactoring_Load);
            this.splitContainer_main.Panel1.ResumeLayout(false);
            this.splitContainer_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).EndInit();
            this.splitContainer_main.ResumeLayout(false);
            this.splitContainer_bottom.Panel1.ResumeLayout(false);
            this.splitContainer_bottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_bottom)).EndInit();
            this.splitContainer_bottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_tijiao;
        private System.Windows.Forms.RichTextBox txt_q;
        private System.Windows.Forms.RichTextBox txt_a;
        private System.Windows.Forms.SplitContainer splitContainer_main;
        private System.Windows.Forms.SplitContainer splitContainer_bottom;
    }
}