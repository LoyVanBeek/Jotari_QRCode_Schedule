using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BarcodeScanner.Scanner
{
    public class ZBarInterface : ICodeReader
    {
        public event CodeReadHandler CodeRead;

        string processString = @"C:\Program Files\ZBar\bin\zbarcam.exe";

        Thread worker;
        private Process reader;

        public ZBarInterface()
        {
        }
        public ZBarInterface(string process)
        {
            processString = process;
        }

        ~ZBarInterface()
        {
            worker.Abort();
            reader.Close();
        }

        public void Start()
        {
            worker = new Thread(Run);
            worker.Start();
        }

        public void Stop()
        {
            try
            {
                reader.Kill();
                reader.Close();
                worker.Abort();
            }
            catch (Exception e)
            {
            }
        }

        private void Run()
        {
            FileInfo zbarfile = new FileInfo(processString);
            if (!zbarfile.Exists)
            {
                throw new FileNotFoundException("Het programma ZBar was niet gevonden op "+processString, processString);
            }
            
            ProcessStartInfo zbarStart = new ProcessStartInfo(processString);
            zbarStart.RedirectStandardOutput = true;
            zbarStart.UseShellExecute = false;
            zbarStart.CreateNoWindow = false;

            try
            {
                reader = Process.Start(zbarStart);
            }
            catch (Exception e)
            {
                throw e;
            }

            while (true)
            {
                string line = "";
                try
                {
                    line = reader.StandardOutput.ReadLine();

                    //System.Console.WriteLine(line);
                }
                catch (IOException)
                {
                    break;
                }

                if (!String.IsNullOrEmpty(line))
                {
                    if (CodeRead != null)
                    {
                        CodeRead(line);
                    }
                }
            }
            reader.Close();
        }
    }
}
