using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarcodeScanner.Zbar
{
    public class ZBarInterface
    {
        public event CodeReadHandler CodeRead;
        public delegate void CodeReadHandler(string code);


    }
}
