using AppKit;
using ContextMenu;
using DesktopTrayIcon;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

namespace XamTrayIcon.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        NSWindow window;
        public AppDelegate()
        {
            var style = NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Titled;
            var rect = new CoreGraphics.CGRect(200, 1000, 1024, 768);
            window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            window.Title = "Xamarin Forms macOS Tray Icon";
            window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            Forms.Init();
            var app = new Core.App();
            app.TrayIcon = new TrayIconImplementation();
            app.ContextMenu = new ContextMenu.ContextMenu();
            app.InitPlugins();
            LoadApplication(app);
            base.DidFinishLaunching(notification);
        }

        public override NSWindow MainWindow => window;

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
