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
using System.IO;

namespace questGui
{
    public partial class DriveForm : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        StringWriterExt writer = new StringWriterExt(true);
        private string _display;
        public string display
        {
            get { return _display; }
            set { _display = value; OnPropertyChanged(new PropertyChangedEventArgs("display")); }
        }
        public DriveForm()
        {
            writer.Flushed += new StringWriterExt.FlushedEventHandler(writer_Flushed);
            InitializeComponent();
            display = "Hi";
            richTextBox1.DataBindings.Add("Text", this, "display", false, DataSourceUpdateMode.OnPropertyChanged);

        }
        public void writer_Flushed(object sender, EventArgs args)
        {
            this.display = ((StringWriterExt)sender).ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Drive.Show(writer);
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
