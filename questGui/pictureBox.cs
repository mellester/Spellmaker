using System;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spellmaker;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
 

namespace questGui
{
    public partial class pictureBox : UserControl
    {
        private SettingsData settingsData;
        private Dictionary<Control, float> controlDict = new Dictionary<Control, float>();
        Timer fade = new Timer();

        public pictureBox(SettingsData settingsData)
        {
            this.settingsData = settingsData;
            settingsData.pathsToWatch.CollectionChanged += this.update;
            settingsData.doneImage.CollectionChanged += this.DeckCreated;
            lock (SettingsData.loc)
            {
                SettingsData._dispacther = System.Windows.Threading.Dispatcher.CurrentDispatcher;
                SettingsData.observableImageSet.CollectionChanged += this.CollectionChanged;
            }
            InitializeComponent();

            FileSystemWatcher watcher = fileSystemWatcher1;
            watcher.Filter = "*.png";

            // Add event handlers.
            watcher.Changed += this.OnChanged;
            watcher.Created += this.OnChanged;
            watcher.Deleted += this.OnChanged;
            watcher.EnableRaisingEvents = true;

            
            watcher.Path = settingsData.pathsToWatch.FirstOrDefault();
            fade.Interval = (100);
            fade.Tick += new EventHandler(fade_Tick);
            fade.Start();

        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    PictureCreated(sender, e);
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                    PictureReplaced(sender, e);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    PicturRemoved(sender, e);
                    break;
            }
        }

        private void PictureReplaced(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newitems = e.NewItems.Cast<Tuple<object, Image, Bitmap>>() ;
            foreach (var item in e.OldItems)
            {
                var img = ((Tuple<object, Image, Bitmap>)item).Item2;
                var controls = flowLayoutPanel1.Controls.Cast<PictureBox>();
                var box = controls.Where((A) => A.Image.Equals(img)).FirstOrDefault();
                if (box == null)
                {
                    PictureCreated(sender, e);
                    return;
                }
                box.Image.Dispose();
                box.Image = newitems.First().Item2;
                
            }
        }

        private void PictureCreated(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender is ObservableCollection<Object>)
            {
                foreach (var item in e.NewItems)
                {
                    var t = new Action<pictureBox>((send) =>
                    {
                        var p = new PictureBox()
                        {
                            Size = new Size(200, 250),
                            SizeMode = PictureBoxSizeMode.StretchImage,
                        };
                        //p.Image.Dispose();
                        p.WaitOnLoad = false;
                        //if (item is KeyValuePair<object, Image>)
                            p.Image = ((Tuple<object, Image, Bitmap>)item).Item2;
                        flowLayoutPanel1.Controls.Add(p);
                        p.Show();
                    });
                    this.BeginInvoke(t, this);
                }
            }
        }

        private void DeckCreated(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var newItem in e.NewItems)
            {
                this.Invoke(new Action(()=>
                {
                    var tuple = newItem as Tuple<string, string>;
                    var f = new DoneForm(tuple.Item2, settingsData,
                        tuple.Item1);
                    f.Show();
                }));
            }
        }

        private void PicturRemoved(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach(var item in e.OldItems)
            {
                var img = ((Tuple<object, Image, Bitmap>)item).Item2;
                var controls = flowLayoutPanel1.Controls.Cast<PictureBox>();
                var box = controls.Where((A) => A.Image.Equals(img)).FirstOrDefault();
                flowLayoutPanel1.Controls.Remove(box);
                box.Image.Dispose();
            }

        }

        private void update(object sender, NotifyCollectionChangedEventArgs e)
        {
           string path = (sender as ObservableCollection<string>).Last();
          //  settingsData.imageSet[path].CollectionChanged += this.OnChanged1;
        }

        private void OnChanged1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender is ObservableCollection<string>)
            {
                foreach(var item in  e.NewItems)
                {
                    var t = new Action<pictureBox>((send) =>
                    {
                        var p = new PictureBox()
                        {
                            Size = new Size(200, 250),
                            SizeMode = PictureBoxSizeMode.StretchImage,

                        };
                        //p.Image.Dispose();
                        p.WaitOnLoad = false;
                        while (p.Image == null)
                            try
                            {
                                using (var s = new System.IO.FileStream((string)item, System.IO.FileMode.Open))
                                    p.Image = Image.FromStream(s);
                            }
                            catch (OutOfMemoryException)
                            {
                                    MessageBox.Show("out of memmory");
                                Thread.Sleep(300);
                            }
                        flowLayoutPanel1.Controls.Add(p);
                        int index = flowLayoutPanel1.Controls.Count - 1;
                        controlDict.Add(p,1.5f);
                        p.Show();
                        flowLayoutPanel1.ScrollControlIntoView(p);
                        var count = flowLayoutPanel1.Controls.Count;
                        if (count > 12)
                        {
                            var key = controlDict.Keys.First();
                            controlDict.Remove(key);
                            flowLayoutPanel1.Controls.Remove(key);
                        }
                    });
                    
                        flowLayoutPanel1.BeginInvoke(t, this);
                }
                return;
            }
            throw new NotImplementedException();
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (!flowLayoutPanel1.Controls.OfType<PictureBox>().Any(P => P.Name == e.Name))
            {
                var file = e.Name;
                var path = e.FullPath;
                if (flowLayoutPanel1.Controls.ContainsKey(file))
                    return;
                var p = new PictureBox()
                {
                    Name = file,
                    Size = new Size(200, 250),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    
                };
                //p.Image.Dispose();
                p.WaitOnLoad = false;
                var ful = (Path.Combine(Directory.GetCurrentDirectory() , path));
                p.ImageLocation = ful;
                
                p.Text = e.ChangeType.ToString() + " " + e.Name;
                flowLayoutPanel1.Controls.Add(p);
                p.Show();
                foreach (PictureBox box in flowLayoutPanel1.Controls.OfType<PictureBox>())
                    box.Invalidate();
            }

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void fade_Tick(object sender, EventArgs e)
        {
            List<Control> keys = new List<Control>(controlDict.Keys);
            foreach (var key in keys )
            {
                var value = controlDict[key] - 0.03f;
                controlDict[key] = value;
                if (value < 1)
                {
                    var p = ((PictureBox)key);
                
                }
                if (value < 0)
                {
                    flowLayoutPanel1.Controls.Remove(key);

                }

            }
        }
        public static Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix
                    {

                        //set the opacity  
                        Matrix33 = opacity
                    };

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
