using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using questGui;

namespace Spellmaker
{
    class FilerWatcher
    {
        private readonly Settings settings;
        private readonly Spellmaker.fakeGui.FileForm1 fileForm;

        public FilerWatcher(Settings settings, Spellmaker.fakeGui.FileForm1 fileForm)
        {
            this.settings = settings;
            this.fileForm = fileForm;
            fileForm.Invoke((MethodInvoker)(() =>
            {
                fileForm.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - fileForm.Width, fileForm.Top);
            }));
        }
        public void Run()
        {

        }
    }
}
