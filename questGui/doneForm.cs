using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SheetDownloader;
using Spellmaker;

namespace questGui
{
    public partial class DoneForm : Form
    {
        private readonly string filePathToShow;
        private Spellmaker.SettingsData settingsData;
        private string name;
        private string filename;
        private string path;
        private Progress<int> progress;
        private string uplaodRet;

        public DoneForm(string path, Spellmaker.SettingsData settingsData, string name, Image image =null)
        {
            this.path = path;
            InitializeComponent();
            this.filePathToShow = path;
            this.settingsData = settingsData;
            this.name = name;
            if (name.Length < 1)
                this.name = Path.GetFileNameWithoutExtension(path);
            if (image != null)
                pictureBox1.Image = image;
            else
                pictureBox1.ImageLocation = path;
            this.Text = name;
            Progress<int> p = new Progress<int>(percent =>
            {
                progressBar1.Value = percent;
            });
            this.progress = p;
        }

        private void doneForm_Load(object sender, EventArgs e)
        {

        }
        [STAThread]
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = " Image|*.png",
                Title = "Save an Image File"
            };
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
     (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }

        private string SteamSavedObjectsPath()
        {
            var doc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var pat = System.IO.Path.Combine(doc, @"My Games\Tabletop Simulator\Saves\Saved Objects\KOD\relsease\"); 
            return pat;
        }
        [STAThread]
        private void SaveJsonAndPictrueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Done.Visible = false;
            var saveFileDialog1 = savefiledialog();
            if (saveFileDialog1.Value == DialogResult.Cancel)
                return;
            if (saveFileDialog1.Key.FileName != "")
                SaveDeck.SaveJsonAndImage(saveFileDialog1.Key.FileName, this.settingsData, this.pictureBox1.ImageLocation);
            this.Done.Visible = true;
        }

        private KeyValuePair<SaveFileDialog, DialogResult> savefiledialog()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                FileName = name + ".json",
                Filter = " Json|*.json",
                Title = "Save TTS deck + .png"
            };
            ;
            saveFileDialog1.InitialDirectory = SteamSavedObjectsPath();
            var Out = saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                var name = saveFileDialog1.FileName;
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:


                        filename = saveFileDialog1.FileName;
                        break;
                }

            }

            return new KeyValuePair<SaveFileDialog, DialogResult>( saveFileDialog1, Out) ;
        }

        private void jsonWithURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Done.Visible = false;
            SteamUrlForm testDialog = new SteamUrlForm(this.pictureBox1.ImageLocation);
            if (this.uplaodRet != null)
                testDialog.textBox1.Text = uplaodRet;
            string text = "";
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                text = testDialog.textBox1.Text;
            }
            else
            {
               text = "Cancelled";
            }
            if (text == "Cancelled")
            {
                MessageBox.Show("Cancelled");
                return;
            }
            //if (this.filename == null)
            {
                var res = savefiledialog();
                if (res.Value == DialogResult.Cancel)
                    return;
            }
            SaveDeck.JsonWithURlCLicked(settingsData, filename, text, this.pictureBox1.ImageLocation);
                
            testDialog.Dispose();
            this.Done.Visible = true;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }//var ret = Drive.Upload(path, p);

        private void doneForm_Shown(object sender, EventArgs e)
        {
            var ret = Drive.Upload(path, progress, pictureBox1.Image);
            ret.Value.ContinueWith(t =>
            {
                this.Invoke(new Action(() =>
                {
                    progressBar1.Value = 100;
                    this.UploadDone.Visible = true;
                    this.uplaodRet = t.Result;
                    filename = Path.GetDirectoryName(this.path) + Path.DirectorySeparatorChar +
                    Path.GetFileNameWithoutExtension(this.path) + ".json";
                    Spellmaker.Deck deck;
                    //Spellmaker.Deck deck = settingsData.decks.FirstOrDefault(A => Path.GetFileName(A.pathImage) == Path.GetFileName(path));
                    var s = settingsData.decks.Select((A, ind) => new { val = Path.GetFileName(A.name), ind }).ToList();
                    //string mPath = Path.GetFileName(name);
                    deck = settingsData.decks[s.First(A => A.val == name).ind];
                    if (deck == null)
                        throw new NotImplementedException();
                    SaveDeck.JsonWithURlCLicked(settingsData, filename, t.Result , path, deck:deck);
                }));
            });
        }

        private void DoneForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Image.Dispose();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if  (this.UploadDone.Visible == true)
                jsonWithURLToolStripMenuItem_Click( sender,  e);
        }
    }
}
