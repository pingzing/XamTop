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
            if (Device.RuntimePlatform == Device.WPF)
            {
                DesktopTrayIcon.Icon.Current.IconPath = Environment.CurrentDirectory + "/Assets/trayicon.ico";
            }                        

            DesktopTrayIcon.Icon.Current.ContextMenuItems = new ITrayMenuItem[]
            {
                new TrayMenuButton { Label = "Show debug message", Clicked = (o, s) => Debug.WriteLine("Debug message!") },
                new TrayMenuButton { Label = "Close", Clicked = (o2, s2) => Debug.WriteLine("I'll implement close later.") }
            };
            DesktopTrayIcon.Icon.Current.TrayTooltip = "XamTrayIcon";
            DesktopTrayIcon.Icon.Current.Show();
            DesktopTrayIcon.Icon.Current.Click += Current_Click;
        }

        private void Current_Click(object sender, EventArgs e)
        {
            DesktopTrayIcon.Icon.Current.ShowContextMenu();
        }

        private void HideIconButton_Clicked(object sender, EventArgs e)
        {
            DesktopTrayIcon.Icon.Current.Hide();
        }

        private void ShowIconButton_Clicked(object sender, EventArgs e)
        {
            DesktopTrayIcon.Icon.Current.Show();
        }
    }
}
