using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spellmaker.fakeGui
{
    class FileForm1
    {
        private questGui.FileForm1 fileForm;
        private bool useGui;
        private SettingsData settingsData;

        public System.Drawing.Point Location
        {
            get => fileForm.Location;
            set => fileForm.Location = value;
        }

        public int Top
        {
            get => fileForm.Top;
            set => fileForm.Top = value;
        }

        public int Width
        {
            get => fileForm.Width;
            set => fileForm.Width = value;
        }

        public FileForm1(bool noGui , SettingsData settingsData)
        {
            this.useGui = !noGui;
            this.settingsData = settingsData;
            //if (this.useGui)
            {
                fileForm = new questGui.FileForm1(settingsData);
            }
        }

        public void Show()
        {
            fileForm.Show();
        }

        public void Invoke(Delegate @delegate)
        {
            fileForm.Invoke(@delegate);
        }
    }
}
