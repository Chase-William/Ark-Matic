using Ark_Matic.Pages;
using System;
using System.Timers;

namespace Ark_Matic.Lang
{
    public class AutoClicker
    {
        private int interval;
        private string name;

        private string hotkey;

        public string Hotkey
        {
            get { return hotkey; }
            set { hotkey = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Interval
        {
            get { return interval; }
            set { interval = value; }
        }       

        public Timer Timer = new Timer(10);

        public AutoClicker(IntPtr _windowHandle, string _name, string _hotkey)
        {
            Name = _name;
            Hotkey = _hotkey;
            // These would be settable by the user during creation
            VirtualKeyUtil.RegisterHotKey(_windowHandle, VirtualKeyUtil.HOTKEY_ID, VirtualKeyUtil.MOD_NONE, (uint)Keys.F6);

            Timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            VirtualKeyUtil.PerformMouseClick(MainPage.CurrentCursorLocation);
        }
    }
}
