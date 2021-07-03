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
using ImageMagick;

namespace questGui
{

    public partial class FileForm1 : Form
    {
        private Spellmaker.SettingsData settings;
        

        public FileForm1(Spellmaker.SettingsData set)
        {
            this.bindingSource1 = new BindingSource();
            this.settings = set;
            InitializeComponent();
            this.bindingNavigator1.BindingSource = this.bindingSource1;
        }


        private void FileForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = settings.MyObjects;
            this.bindingSource2.DataSource = new LocalDatacs().SheetNames;
            this.bindingSource2.ListChanged += MenuItembuildHandler;
            BuildMenuItems();
            var b = new Binding("Text", this.bindingSource1, "Item3", true,DataSourceUpdateMode.OnValidation);
            b.Format += new ConvertEventHandler(PointToString);
            b.Parse += new ConvertEventHandler(StringToPoint);
            this.textBoxLocation.DataBindings.Add(b);
               
            this.textBoxName.DataBindings.Add(
                new Binding("Text", this.bindingSource1, "Item1", true));
            this.textBoxPointSize.DataBindings.Add(
            new Binding("Text", this.bindingSource1, "Item2.FontPointsize", true));
            this.textBoxFont.DataBindings.Add(
                        new Binding("Text", this.bindingSource1, "Item2.Font", true));
            this.textBoxHeight.DataBindings.Add(
                        new Binding("Text", this.bindingSource1, "Item2.Height", true));
            this.textBoxWidth.DataBindings.Add(
                        new Binding("Text", this.bindingSource1, "Item2.Width", true));
        }
        private void BuildMenuItems()
        {
            var list = this.bindingSource2.List as BindingList<Spellmaker.LocalDatacs.SheetObject>;
            ToolStripMenuItem[] items = new ToolStripMenuItem[list.Count]; // You would obviously calculate this value at runtime
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new ToolStripMenuItem();
                items[i].Name = list[i].Name;
                items[i].Tag = list[i];
                items[i].Text = list[i].Name;
                items[i].Click += new EventHandler(MenuItemClickHandler);
            }
            toolStripDropDownButton1.DropDownItems.Clear();
            toolStripDropDownButton1.DropDownItems.AddRange(items);
        }
        private void MenuItembuildHandler(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => {BuildMenuItems(); }));
        }

        private void MenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            // Take some action based on the data in clickedItem
            var sheet = Sheet.BatchSheetGet(null, ranges: new List<string> {clickedItem.Name}).First();
            this.bindingSource3.DataSource = settings.sheets;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                SettingsData.DoRefreshImage(EventArgs.Empty);
            pictureBox1.BackgroundImage = SettingsData.Image;



        }
        private void PointToString(object sender, ConvertEventArgs cevent)
        {
            // The method converts only to string type. Test this using the DesiredType.
            if (cevent.DesiredType != typeof(string)) return;

            // Use the ToString method to format the value as currency ("c").
            var p = (Point)cevent.Value;
            cevent.Value = (p.X).ToString() + "," + p.Y.ToString();
        }

        private void StringToPoint(object sender, ConvertEventArgs cevent)
        {
            // The method converts back to decimal type only.
            try
            {
                cevent.Value = cevent.DesiredType != typeof(Point)
                    ? new Point() :
                new PointConverter().ConvertFromString((string)cevent.Value);
            }
            catch
            {
                cevent.Value = new Point();
            }
            // Converts the string back to decimal using the static Parse method.
        }

        private void selectFOnt_Click(object sender, EventArgs e)
        {
            var result = fontDialog1.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                textBoxFont.Text = fontDialog1.Font.Name;
            }
        }
    }
    
}
