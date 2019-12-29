﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark_Matic.Data
{
    public static class StoredProtocols
    {
        // Key   == Tuple < name, hotkey >
        // Value == Tuple < type, protocol >
        public static MultiKeyDictionary<string, Keys, Tuple<Type, object>> Protocols = new MultiKeyDictionary<string, Keys, Tuple<Type, object>>();

        
    }
}
