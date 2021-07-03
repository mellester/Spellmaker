using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spellmaker;

namespace questGui
{
    public partial class SteamUrlForm : Form
    {
        private string path;
        public SteamUrlForm(string path)
        {
            this.path = path;
            InitializeComponent();
            Okbut.DialogResult = DialogResult.OK;
            this.textBox2.Text = path;
        }



        private void SteamUrlForm_Load(object sender, EventArgs e)
        {
            
        }

        private void SteamUrlForm_Load_1(object sender, EventArgs e)
        {

        }

        private void Uploadbut_Click(object sender, EventArgs e)
        {
            Progress<int> p = new Progress<int>(percent =>
            {
                progressBar1.Value = percent;
            });
            var ret = Drive.Upload(path, p);
            
            ret.Value.ContinueWith(t =>
            {
                this.Invoke(new Action(() =>
                {
                    progressBar1.Value = 100;
                    textBox1.Text = t.Result;
                }                ));
            });
        }
    }
}
