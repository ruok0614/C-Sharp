using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace TickTacToe.Views.Messengers
{
    public class DialogService
    {
        private Windows.UI.Xaml.Controls.ContentDialog dialog;

        public async void ShowMessage(string title, string msg)
        {
            if (dialog == null)
            {
                dialog = new Windows.UI.Xaml.Controls.ContentDialog();
                dialog.Title = title;
                dialog.PrimaryButtonText = "OK";
            }
            dialog.Content = msg;

            await dialog.ShowAsync();
        }
    }
}
