using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BarcodeScanner.Scanner
{
    public class ZBarInterface : ICodeReader, IDisposable
    {
        public event CodeReadHandler CodeRead;

        string processString = @"ZbarBin\zbarcam.exe";//@"C:\Program Files\ZBar\bin\zbarcam.exe";

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
            reader.Close();
        }

        public void Start()
        {
            worker = new Thread(Run);
            worker.Start();
        }

        public void Stop()
        {
            worker.Abort();
            try
            {
                reader.Kill();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
            }
            reader.Close();
        }

        private void Run()
        {
            FileInfo fi = new FileInfo(processString);

            if (fi.Exists)
            {
                ProcessStartInfo zbarStart = new ProcessStartInfo(fi.FullName);
                zbarStart.RedirectStandardOutput = true;
                zbarStart.UseShellExecute = false;
                zbarStart.CreateNoWindow = false;

                try
                {
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
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                    throw ioe;
                }
                finally
                {
                    reader.Close();
                }
            }
            else
            {
                throw new FileNotFoundException("ZBar could not be found, so no codes can be read.", fi.FullName);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}
