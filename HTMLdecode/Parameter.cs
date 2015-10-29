/*! HTMLdecode (http://kionier.com/) | (c) 2015 Kionier | Licensed GNU GPL/LGPL http://www.gnu.org/licenses/ */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLdecode
{
    public class Parameter
    {
        public String Name { get; set; }
        public String Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.Name, this.Value);
        }
    }
}
