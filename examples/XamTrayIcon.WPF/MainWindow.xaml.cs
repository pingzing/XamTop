using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace XamTrayIcon.WPF
{
    public partial class MainWindow : FormsApplicationPage
    {        
        public MainWindow()
        {           
            InitializeComponent();
            Forms.Init();
            var app = new Core.App();
            app.TrayIcon = new DesktopTrayIcon.TrayIconImplementation();
            app.ContextMenu = new ContextMenu.ContextMenuImplementation();
            app.InitPlugins();
            LoadApplication(app);
        }
    }
}
