using System.Windows;

namespace Spellmaker.fakeGui
{
    class Helpers
    {
        private Settings settings;


        public Helpers(Settings settings)
        {
            this.settings = settings;
        }

        public MessageBoxResult AskMessage(string v)
        {
            if (this.settings != null && !this.settings.noGui )
            {
                return MessageBoxResult.OK;
            }
            return MessageBox.Show(v, "oke", MessageBoxButton.OKCancel);
        }
    }
    
}