/*! HTMLdecode (http://kionier.com/) | (c) 2015 Kionier | Licensed GNU GPL/LGPL http://www.gnu.org/licenses/ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLdecode
{
    public class Tag
    {
        public enum EnumFlavor
        {
            Open,
            Close,
            Self,
            Text,
            Special,
            Commit,
            Script
        }

        public EnumFlavor Flavor { get; set; }
        public String Value { get; set; }
        public String Name { get; set; }

        public override string ToString()
        {
            return Name + "; "  + Flavor.ToString() + "; " + Value;
        }
    }
}
