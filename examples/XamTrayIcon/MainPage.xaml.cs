using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopTrayIcon.Abstractions;
using Xamarin.Forms;

namespace XamTrayIcon.Core
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void ShowIconButton_Clicked(object sender, EventArgs e)
        {
            ((App)App.Current).TrayIcon.Show();
        }

        public void HideIconButton_Clicked(object sender, EventArgs e)
        {
            ((App)App.Current).TrayIcon.Hide();
        }

        public void ShowContextMenuButton_Clicked(object sender, EventArgs e)
        {
            ((App)App.Current).TrayIcon.ShowContextMenu();
            Device.StartTimer(TimeSpan.FromSeconds(3), () => {
                ((App)App.Current).TrayIcon.HideContextMenu();
                return false;
            });
        }

        public void HideContextMenuButton_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}
