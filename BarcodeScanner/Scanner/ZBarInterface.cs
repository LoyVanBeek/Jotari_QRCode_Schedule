﻿using System;
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

        public void Start()
        {
            worker = new Thread(Run);
            worker.Start();
        }

        public void Stop()
        {
            worker.Abort();
        }

        private void Run()
        {
            ProcessStartInfo zbarStart = new ProcessStartInfo(processString);
            zbarStart.RedirectStandardOutput = true;
            zbarStart.UseShellExecute = false;
            zbarStart.CreateNoWindow = false;

            reader = Process.Start(zbarStart);
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