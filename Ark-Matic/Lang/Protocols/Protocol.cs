using System.Collections.Generic;
using System.Drawing;

namespace Ark_Matic.Lang
{
    /// <summary>
    ///     Is a protocol and contains its information
    /// </summary>
    class Protocol
    {        
        List<string> items = new List<string>();
        private Point dropAllBtnLocation;
        private Point searchBarLocation;

        public Point SearchBarLocation
        {
            get { return searchBarLocation; }
            set { searchBarLocation = value; }
        }

        public Point DropAllBtnLocation
        {
            get { return dropAllBtnLocation; }
            set { dropAllBtnLocation = value; }
        }

        public List<string> Items
        {
            get { return items; }
            set { items = value; }
        }                
    }
}
