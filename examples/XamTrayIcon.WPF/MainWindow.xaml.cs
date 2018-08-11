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
            app.TrayIcon = new XamTop.DesktopTrayIcon.TrayIconImplementation();
            app.ContextMenu = new XamTop.ContextMenu.ContextMenuFacade();
            app.InitPlugins();
            LoadApplication(app);
        }
    }
}
