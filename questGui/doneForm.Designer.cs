namespace questGui
{
    partial class DoneForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveJsonAndPictrueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jsonWithURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Done = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.UploadDone = new System.Windows.Forms.Label();
            this.Next = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadToolStripMenuItem,
            this.saveJsonAndPictrueToolStripMenuItem,
            this.jsonWithURLToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.uploadToolStripMenuItem.Text = "Save picture";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveJsonAndPictrueToolStripMenuItem
            // 
            this.saveJsonAndPictrueToolStripMenuItem.Name = "saveJsonAndPictrueToolStripMenuItem";
            this.saveJsonAndPictrueToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.saveJsonAndPictrueToolStripMenuItem.Text = "Save Deck + picture";
            this.saveJsonAndPictrueToolStripMenuItem.Click += new System.EventHandler(this.SaveJsonAndPictrueToolStripMenuItem_Click);
            // 
            // jsonWithURLToolStripMenuItem
            // 
            this.jsonWithURLToolStripMenuItem.Name = "jsonWithURLToolStripMenuItem";
            this.jsonWithURLToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.jsonWithURLToolStripMenuItem.Text = "Json with URL";
            this.jsonWithURLToolStripMenuItem.Click += new System.EventHandler(this.jsonWithURLToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(30, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 426);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // Done
            // 
            this.Done.AutoSize = true;
            this.Done.BackColor = System.Drawing.SystemColors.Info;
            this.Done.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Done.ForeColor = System.Drawing.Color.DarkGreen;
            this.Done.Location = new System.Drawing.Point(315, 160);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(173, 31);
            this.Done.TabIndex = 1;
            this.Done.Text = "Done saving.";
            this.Done.Visible = false;
            this.Done.Click += new System.EventHandler(this.label1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(517, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(271, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // UploadDone
            // 
            this.UploadDone.AutoSize = true;
            this.UploadDone.BackColor = System.Drawing.Color.White;
            this.UploadDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UploadDone.ForeColor = System.Drawing.Color.DarkGreen;
            this.UploadDone.Location = new System.Drawing.Point(515, -7);
            this.UploadDone.Name = "UploadDone";
            this.UploadDone.Size = new System.Drawing.Size(285, 31);
            this.UploadDone.TabIndex = 3;
            this.UploadDone.Text = "Upload to google done";
            this.UploadDone.Visible = false;
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(713, 415);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(75, 23);
            this.Next.TabIndex = 4;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // DoneForm
            // 
            this.AcceptButton = this.Next;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.UploadDone);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DoneForm";
            this.Text = "Form3";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DoneForm_FormClosed);
            this.Load += new System.EventHandler(this.doneForm_Load);
            this.Shown += new System.EventHandler(this.doneForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem saveJsonAndPictrueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jsonWithURLToolStripMenuItem;
        private System.Windows.Forms.Label Done;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label UploadDone;
        private System.Windows.Forms.Button Next;
    }
}