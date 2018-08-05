using System;
using DesktopTrayIcon.Abstractions;

namespace DesktopTrayIcon
{
    public class TrayIcon
    {
        private static Lazy<ITrayIcon> implementation = new Lazy<ITrayIcon>(() => CreateTrayIcon(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static ITrayIcon Current
        {
            get
            {
                var ret = implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return ret;
            }
        }

        private static ITrayIcon CreateTrayIcon()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#else
            return new TrayIconImplementation();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly()
        {
            return new NotImplementedException("This functionality is not implemented in the netstandard version of this assembly.  " +
                "You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
