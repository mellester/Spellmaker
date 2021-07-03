namespace questGui
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ImageList imageList1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SheetLink = new System.Windows.Forms.ToolTip(this.components);
            this.tab1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sheetNamesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.localDatacsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.item1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Next = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DriveButton = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.Label();
            this.NameLink = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.tab2 = new System.Windows.Forms.Panel();
            imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tab1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetNamesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localDatacsBindingSource)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.item1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            imageList1.Images.SetKeyName(0, "AbilityBane.png");
            imageList1.Images.SetKeyName(1, "AllorNothing.png");
            imageList1.Images.SetKeyName(2, "AlphaStyle.png");
            // 
            // SheetLink
            // 
            this.SheetLink.ToolTipTitle = "URL";
            this.SheetLink.Popup += new System.Windows.Forms.PopupEventHandler(this.SheetLink_Popup);
            // 
            // tab1
            // 
            this.tab1.AutoSize = true;
            this.tab1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tab1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab1.Controls.Add(this.dataGridView1);
            this.tab1.Controls.Add(this.flowLayoutPanel1);
            this.tab1.Controls.Add(this.panel3);
            this.tab1.Controls.Add(this.panel2);
            this.tab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab1.Location = new System.Drawing.Point(0, 0);
            this.tab1.Margin = new System.Windows.Forms.Padding(20);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(20);
            this.tab1.Size = new System.Drawing.Size(721, 475);
            this.tab1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.Selected});
            this.dataGridView1.DataSource = this.sheetNamesBindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(20, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(479, 351);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.RowErrorTextChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_RowErrorTextChanged);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // Selected
            // 
            this.Selected.DataPropertyName = "Selected";
            this.Selected.FalseValue = false;
            this.Selected.HeaderText = "Selected";
            this.Selected.Name = "Selected";
            this.Selected.TrueValue = true;
            // 
            // sheetNamesBindingSource1
            // 
            this.sheetNamesBindingSource1.DataMember = "SheetNames";
            this.sheetNamesBindingSource1.DataSource = this.localDatacsBindingSource;
            // 
            // localDatacsBindingSource
            // 
            this.localDatacsBindingSource.DataSource = typeof(Spellmaker.LocalDatacs);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.item1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(499, 49);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 351);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // item1
            // 
            this.item1.AutoSize = true;
            this.item1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.item1.Controls.Add(this.textBox1);
            this.item1.Controls.Add(this.label1);
            this.item1.Dock = System.Windows.Forms.DockStyle.Top;
            this.item1.Location = new System.Drawing.Point(3, 3);
            this.item1.Name = "item1";
            this.item1.Size = new System.Drawing.Size(192, 43);
            this.item1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(181, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "No data";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Next);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(20, 400);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(679, 53);
            this.panel3.TabIndex = 6;
            // 
            // Next
            // 
            this.Next.Dock = System.Windows.Forms.DockStyle.Right;
            this.Next.Location = new System.Drawing.Point(599, 5);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(75, 43);
            this.Next.TabIndex = 0;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.DriveButton);
            this.panel2.Controls.Add(this.ID);
            this.panel2.Controls.Add(this.NameLink);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(20, 20);
            this.panel2.Margin = new System.Windows.Forms.Padding(10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(679, 29);
            this.panel2.TabIndex = 4;
            // 
            // DriveButton
            // 
            this.DriveButton.Location = new System.Drawing.Point(238, 0);
            this.DriveButton.Name = "DriveButton";
            this.DriveButton.Size = new System.Drawing.Size(75, 23);
            this.DriveButton.TabIndex = 6;
            this.DriveButton.Text = "Drive";
            this.DriveButton.UseVisualStyleBackColor = true;
            this.DriveButton.Click += new System.EventHandler(this.DriveButton_Click);
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.localDatacsBindingSource, "ID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ID.Dock = System.Windows.Forms.DockStyle.Right;
            this.ID.Location = new System.Drawing.Point(643, 3);
            this.ID.MinimumSize = new System.Drawing.Size(0, 23);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(33, 23);
            this.ID.TabIndex = 5;
            this.ID.Text = "None";
            this.ID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NameLink
            // 
            this.NameLink.AutoSize = true;
            this.NameLink.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.NameLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameLink.Location = new System.Drawing.Point(78, 3);
            this.NameLink.MinimumSize = new System.Drawing.Size(30, 23);
            this.NameLink.Name = "NameLink";
            this.NameLink.Size = new System.Drawing.Size(63, 23);
            this.NameLink.TabIndex = 4;
            this.NameLink.TabStop = true;
            this.NameLink.Text = "Sheetname";
            this.NameLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Sheet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // tab2
            // 
            this.tab2.AutoSize = true;
            this.tab2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tab2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tab2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab2.Location = new System.Drawing.Point(0, 0);
            this.tab2.Name = "tab2";
            this.tab2.Size = new System.Drawing.Size(721, 475);
            this.tab2.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 475);
            this.Controls.Add(this.tab1);
            this.Controls.Add(this.tab2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetNamesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localDatacsBindingSource)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.item1.ResumeLayout(false);
            this.item1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip SheetLink;
        private System.Windows.Forms.Panel tab1;
        protected internal System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label ID;
        private System.Windows.Forms.LinkLabel NameLink;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel tab2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel item1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DriveButton;
        private System.Windows.Forms.BindingSource localDatacsBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource sheetNamesBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
    }
}

