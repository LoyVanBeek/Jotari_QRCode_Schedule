using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarcodeScanner
{
    public class Utilities
    {
        public static bool Later(DateTime questioned, DateTime compareTo)
        {
            bool minuteOK = questioned.Minute > compareTo.Minute;
            if (questioned.Hour > compareTo.Hour)
            {
                return true;
            }
            else if (questioned.Hour == compareTo.Hour)
            {
                return minuteOK;
            }
            else //if(questioned.Hour < compareTo.Hour)
            {
                return false;
            }
        }
    }
}
