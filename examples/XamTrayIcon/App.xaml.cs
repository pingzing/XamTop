using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamTop.ContextMenu;
using XamTop.ContextMenu.Abstractions;
using XamTop.DesktopTrayIcon;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamTrayIcon.Core
{
    public partial class App : Application
    {
        public TrayIcon TrayIcon { get; set; } = new TrayIcon();
        public IContextMenu ContextMenu { get; set; } = new ContextMenu();

        public App ()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public void InitPlugins()
        {
            if (Device.RuntimePlatform == Device.WPF)
            {
                TrayIcon.IconPath = Environment.CurrentDirectory + "/Assets/trayicon.ico";
            }
            else if (Device.RuntimePlatform == Device.macOS)
            {
                TrayIcon.IconPath = "TrayIcon";
            }
            TrayIcon.TrayTooltip = "XamTrayIcon";
            TrayIcon.Show();
            TrayIcon.Click += Current_Click;            
        }

        private void Current_Click(object sender, EventArgs e)
        {
            TrayIcon.ShowContextMenu();
        }

        private void HideIconButton_Clicked(object sender, EventArgs e)
        {
            TrayIcon.Hide();
        }

        private void ShowIconButton_Clicked(object sender, EventArgs e)
        {
            TrayIcon.Show();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}
