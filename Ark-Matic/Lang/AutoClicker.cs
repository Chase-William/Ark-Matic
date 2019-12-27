using System;
using System.Timers;

namespace Ark_Matic.Lang
{
    public class AutoClicker
    {
        private int interval;
        private string name;

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

        public Timer Timer = new Timer();

        public AutoClicker(IntPtr _windowHandle, string _name)
        {
            Name = _name;
            // These would be settable by the user during creation
            VirtualKeyUtil.RegisterHotKey(_windowHandle, VirtualKeyUtil.HOTKEY_ID, VirtualKeyUtil.MOD_NONE, (uint)Keys.F6);
        }
    }
}
