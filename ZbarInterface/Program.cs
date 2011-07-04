using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace ZbarInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            //readProcess();

            foreach (string result in yieldResults())
            {
                Console.WriteLine(result);
            }
        }

        private static void readProcess()
        {
            ProcessStartInfo zbarStart = new ProcessStartInfo(@"C:\Program Files\ZBar\bin\zbarcam.exe");
            zbarStart.RedirectStandardOutput = true;
            zbarStart.UseShellExecute = false;

            Process zbar = Process.Start(zbarStart);
            while (true)
            {
                try
                {
                    string line = zbar.StandardOutput.ReadLine();

                    System.Console.WriteLine(line);
                }
                catch (IOException)
                {
                    break;
                }
            }
        }

        private static IEnumerable<string> yieldResults()
        {
            ProcessStartInfo zbarStart = new ProcessStartInfo(@"C:\Program Files\ZBar\bin\zbarcam.exe");
            zbarStart.RedirectStandardOutput = true;
            zbarStart.UseShellExecute = false;

            Process zbar = Process.Start(zbarStart);
            while (true)
            {
                string line = "";
                try
                {
                    line = zbar.StandardOutput.ReadLine();

                    //System.Console.WriteLine(line);
                }
                catch (IOException)
                {
                    break;
                }

                if (!String.IsNullOrEmpty(line))
                {
                    yield return line;
                }
            }
        }
    }
}
