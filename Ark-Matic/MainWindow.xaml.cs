using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Threading;
using MouseCursor = System.Windows.Forms.Cursor;
using Point = System.Drawing.Point;
using Ark_Matic.Pages;
using Ark_Matic.Data;
using Ark_Matic.Lang;

/*

    Purpose: 
        Provides quality of life functionalities for ark players.

    Functionalities:
        Auto Clicker,
        Drop All (searches item; drops all of zed item)
 
*/

namespace Ark_Matic
{    
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static event Action OnDeactivatedExtension;

        // Handle to the window instance
        IntPtr windowHandle;
        // Class that is a wrapper for the win32 window 
        private HwndSource source;


        /// 
        ///     Called when the window loses focus
        /// 
        protected override void OnDeactivated(EventArgs e)
        {
            this.Topmost = true;

            OnDeactivatedExtension?.Invoke();

            base.OnDeactivated(e);
        }


        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            Main.Content = new MainPage();

            windowHandle = new WindowInteropHelper(this).Handle;
            source = HwndSource.FromHwnd(windowHandle);
            source.AddHook(VirtualKeyUtil.ToggleHooks);

            var clicker = new AutoClicker(windowHandle, "Testing");

            // Testing the creation of a autoclicker as a class
            StoredProtocols.Protocols.Add(Tuple.Create(clicker.Name, "F6"), Tuple.Create(nameof(AutoClicker), (object)clicker));

            VirtualKeyUtil.RegisterHotKey(windowHandle, VirtualKeyUtil.HOTKEY_ID, VirtualKeyUtil.MOD_NONE, (uint)Keys.F8);
        }
    }
}
