using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarcodeScanner.DataLookup
{
    public interface IDataReader
    {
        string Lookup(int groupnumber, DateTime time);
    }
}
