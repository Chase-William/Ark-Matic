using Ark_Matic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark_Matic.Util
{
    public static class GeneralUtil
    {
        public static System.Timers.Timer GetTimerFromAutoClicker(Keys _key)
        {
            return ((Ark_Matic.Lang.AutoClicker)StoredProtocols.Protocols[Keys.F6].Item2).Timer;
        }
    }
}
