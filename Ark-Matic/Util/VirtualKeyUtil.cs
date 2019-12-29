using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ark_Matic.Pages;
using Ark_Matic.Data;
using Ark_Matic.Util;

namespace Ark_Matic
{
    public static class VirtualKeyUtil
    {
        /// <summary>
        ///     Executes a click using the win32 dll
        /// </summary>
        public static void PerformMouseClick(Point _location)
        {
            SetCursorPosition(_location.X, _location.Y);
            ExecuteMouseEvent(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, _location.X, _location.Y, 0, 0);
        }

        /// <summary>
        ///     Presses a specified key using the win32 dll
        /// </summary>
        public static void PerformKeyboardClick(Keys _key)
        {
            byte key = (byte)_key;

            keybd_event(key, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(key, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }


        /// <summary>
        ///     Called when a hook (hotkey) is clicked
        /// </summary>
        public static IntPtr ToggleHooks(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == MW_HOTKEY && wParam.ToInt32() == HOTKEY_ID)
            {

                uint clickedKey = ((uint)lParam >> 16) & 0xFFFF;
                switch (clickedKey)
                {
                    case (uint)Keys.F6:

                        var timer = GeneralUtil.GetTimerFromAutoClicker(Keys.F6);

                        if (timer.Enabled)
                        {
                            timer.Stop();
                        }
                        else
                        {
                            timer.Start();
                        }
                        break;
                    //case (uint)Keys.F6 when !MainPage.clickTimer.Enabled:
                    //    MainPage.clickTimer.Start();
                    //    break;
                    //case (uint)Keys.F7 when MainPage.clickTimer.Enabled:
                    //    MainPage.clickTimer.Stop();
                    //    break;
                    case (uint)Keys.F8:
                        // DropAllButMetalAsync(null, null);
                        break;
                }
                handled = true;
            }
            return IntPtr.Zero;
        }

        #region Mouse Consts

        private const int MOUSEEVENTF_LEFT = 0x01;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;  // Value of the press of a click
        private const int MOUSEEVENTF_LEFTUP = 0x04;    // Value of the press up of a click

        #endregion Mouse Consts

        #region Keyboard Consts

        public const int HOTKEY_ID = 9000;     // Used to register a hotkey
        public const int MW_HOTKEY = 0x0312;
        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;

        public const uint MOD_NONE = 0x0000;
        private const byte BKey = 0x42;
        private const byte SKey = 0x53;
        private const byte WKey = 0x57;
        private const byte TKey = 0x54;
        private const uint F6 = 0x75;
        private const uint F7 = 0x76;
        private const uint F8 = 0x77;

        #endregion Keyboard Consts  

        #region win32 External Methods

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        static extern bool SetCursorPosition(int x, int y);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        static extern void ExecuteMouseEvent(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        #endregion win32 External Methods 
    }
}
