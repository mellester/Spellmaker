using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading.Tasks;

namespace Spellmaker.fakeGui
{
    class Form1
    {
        private bool useGui;
        private SettingsData settingsData;
        private questGui.Form1 form;

        public Form1(bool noGui, SettingsData settingsData)
        {
            this.useGui = !noGui;
            this.settingsData = settingsData;
            this.form = new questGui.Form1(settingsData);
        }

        public IList<string> Names
        {
            set => form.Names = value;
        }
    public TaskCompletionSource<bool> promise
        {
            get => form.promise;
        }
        public void Run() {
            Application.Run(this.form);
        }

    }
}
