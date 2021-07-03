using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace questGui
{
    public partial class Form3 : Form
    {
        public Form3(Image img)
        {
            InitializeComponent();
            this.pictureBox1.Image = img;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
