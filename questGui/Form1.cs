using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using SheetDownloader;
using Spellmaker;

namespace questGui
{
    public partial class Form1 : Form
    {
        Spellmaker.SettingsData settingsData;
        LocalDatacs localData;
        public TaskCompletionSource<bool> promise = new TaskCompletionSource<bool>();
        SheetDownloader.DownloaderAndParser Downloader;
        Lazy<DriveForm> drive = new Lazy<DriveForm>();
        private IList<string> names;

        public IList<string> Names { get => names; set
            {
                names = value;
                CheckistBox(names);
            }
        }

        public Form1(Spellmaker.SettingsData data,
            SheetDownloader.DownloaderAndParser SD = null)
        {
            if (data == null)
                MessageBox.Show("error empty settings");
            if (SD == null)
            {
                Downloader = DownloaderAndParser.GetInstance(data.sheetID);
            } else
                Downloader = SD;
            settingsData = data;
            InitializeComponent();
            localData = new LocalDatacs();
            localDatacsBindingSource.DataSource = localData;
            sheetNamesBindingSource1.DataSource = localData.SheetNames;
            this.dataGridView1.CellFormatting += dataGridView_CellFormatting;
            this.dataGridView1.RowValidating += DataGridView_RowValidating;
            this.dataGridView1.DataError += DataGridView1_DataError;
            
            //((ListBox)checkedListBox1).DisplayMember = "SheetNames";
        }




        public void itsUpdate()
        {
            new LocalDatacs().ID = settingsData.sheetID;
            NameLink.Text = Downloader.SpreadSheetname();
            SheetLink.ToolTipTitle = "";
            SheetLink.SetToolTip(this.NameLink,
                "https://docs.google.com/spreadsheets/d/" + settingsData.sheetID
                + "/edit?usp=sharing");
            // checkedListBox1.Items.Clear();
            var copy = localData.SheetNames.ToArray();
            localData.SheetNames.Clear();
            foreach (string s in Downloader.spreadSheetItems())
                if (!copy.Select(x => x.Name).Contains(s))
                    localData.SheetNames.Add(new LocalDatacs.SheetObject { Name = s });
                else
                    localData.SheetNames.Add(new LocalDatacs.SheetObject { Name = s , Selected = true});
        }   



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            var form = new SheetLink(this);
            form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            itsUpdate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NameLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(SheetLink.GetToolTip(NameLink));
        }

        private void SheetLink_Popup(object sender, PopupEventArgs e)
        {

        }

        public void CheckistBox(IList<string> names = null)
        {
            if (names != null)
            {
                List<string> wrong = new List<string>();
                foreach (var name in names)
                {
                    var Tlist = Downloader.spreadSheetItems();
                    if (!Tlist.Contains(name))
                    {
                        wrong.Add(name);
                    }
                }
                foreach (var name in wrong)
                {
                    names.Remove(name);
                }

                Downloader.SelectedSheet = new List<string>(names);
                //Next_Click(this, null);
                return;
            }
            var list = Downloader.SelectedSheet;

            Downloader.SelectedSheet = list;
            //var temp = flowLayoutPanel1.Controls[0].
//            flowLayoutPanel1.Controls.Add();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (names == null  )
                CheckistBox();
            promise.TrySetResult(true);
            tab1.Hide();
            UserControl box = new pictureBox(settingsData)
            {
                Dock = DockStyle.Fill
            };
            this.tab2.Controls.Clear();
            this.tab2.Controls.Add(box);
            //box.Visible = true;
            tab2.Show();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3) // column of the enum
            {
                try
                {
                    e.Value = (e.Value.ToString());
                }
                catch (Exception ex)
                {
                    e.Value = ex.Message;
                }
            }
        }

        private void DataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs data)
        {
       //     DataGridViewRow row =
       //dataGridView1.Rows[data.RowIndex];
       //     if (!row.Cells["Selected"].Value.Equals(true))
       //     { row.ErrorText = null; return; }
       //     row.ErrorText =
       //     "Error in parsing this sheet";
       //     data.Cancel = true;
        }

        private void DriveButton_Click(object sender, EventArgs e)
        {
            drive.Value.ShowDialog();
            
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {

            MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
        }

        private void dataGridView1_RowErrorTextChanged(object sender, DataGridViewRowEventArgs e)
        {
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} in {1}", "Parsing error", e.Row.Cells[1].Value);
            messageBoxCS.AppendLine();
            MessageBox.Show(messageBoxCS.ToString(), "Error");
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            var form = new SheetLink(this);
            form.ShowDialog();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (promise.Task.Status == TaskStatus.WaitingForActivation || promise.Task.Status == TaskStatus.Running)
                promise.SetCanceled();
        }
    }
}
