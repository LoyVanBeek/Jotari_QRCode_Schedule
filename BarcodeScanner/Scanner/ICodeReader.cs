using System;
namespace BarcodeScanner.Scanner
{
    interface ICodeReader
    {
        event CodeReadHandler CodeRead;
        void Start();
        void Stop();
    }
    
    public delegate void CodeReadHandler(string code);
}
