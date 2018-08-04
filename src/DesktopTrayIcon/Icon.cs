using System;
using DesktopTrayIcon.Abstractions;

namespace DesktopTrayIcon
{
    // This class is "Added As Link" to all the platform-specific projects.
    public class Icon
    {
        private static Lazy<ITrayIcon> implementation = new Lazy<ITrayIcon>(() => CreateTrayIcon(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static ITrayIcon Current
        {
            get
            {
                var value = implementation.Value;
                if (value == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return value;
            }
        }

        private static ITrayIcon CreateTrayIcon()
        {
#if NETSTANDARD1_0
            // If this directive is defined, this class is being called directly, which won't ever work.
            return null;
#else
            return new TrayIconImplementation();
#endif

        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            throw new NotImplementedException("This functionality is not implemented in " +
                "the portable version of this assembly. You should reference the NuGet " +
                "package from your main application project in order to reference the " +
                "platform-specific implementation.");
    }
}
