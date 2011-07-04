using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using com.google.zxing;
using System.Drawing;
using com.google.zxing.common;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Windows.Interop;
using System.Timers;
using System.ComponentModel;
using System.Windows.Threading;
using com.google.zxing.qrcode;
using System.Diagnostics;
using BarcodeScanner.DataLookup;
using System.Threading;
using System.Data;
using BarcodeScanner.Data;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Process reader;

        BarcodeScanner.DataLookup.IDataReader data;

        Thread worker;

        Dictionary<string, ActivityTimeLine> schedule;

        public Window1()
        {
            Loaded += new RoutedEventHandler(Window1_Loaded);
            InitializeComponent();

            Closing += new CancelEventHandler(Window1_Closing);
        }

        void Window1_Closing(object sender, CancelEventArgs e)
        {
            worker.Abort();
            if (!reader.HasExited)
            {
                reader.Kill();
                reader.Close(); 
            }
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            data = new ExcelDataReader(@"C:\Documents and Settings\L.vBeek\My Documents\Scouting\Jotari\jotari planning 2011 laatste versie.xlsx");
            worker = new Thread(Loop);
            worker.Start();

            schedule = GetSchedule();
        }

        private void Loop()
        {
            IEnumerable<string> ids = readProcess();
            foreach (string idStr in ids)
            {
                string[] parts = idStr.Split(':');
                if (parts[0].ToUpper() == "QR-CODE")
                {
                    int id = int.Parse(parts[1]);
                    DateTime now = DateTime.Now;

                    if (comboBox1.SelectedItem.ToString() == "Zaterdag")
                    {
                        now = new DateTime(2011, 10, 15, now.Hour, now.Minute, now.Second);
                    }
                    else if (comboBox1.SelectedItem.ToString() == "Zondag")
                    {
                        now = new DateTime(2011, 10, 16, now.Hour, now.Minute, now.Second);
                    }
                    else
                    {
                        //Act like sunday
                        now = new DateTime(2011, 10, 16, now.Hour, now.Minute, now.Second);
                    }
                    string activity = data.Lookup(id, now);

                    ActivityTimeLine atl = schedule[id.ToString()];
                    Activity acti = atl[now];

                    Console.Beep();

                    Dispatcher.BeginInvoke(
                      DispatcherPriority.Background,
                      new Action(
                          () => displayActivity(id.ToString(), acti)));
                }
            }
        }

        private void displayActivity(int id, string activity)
        {
            Console.WriteLine("Group {0} has {1}", id, activity);
            groupDisplay.Content = id;
            ActivityDisplay.Text = activity;
        }
        private void displayActivity(string group, Activity activity)
        {
            Console.WriteLine("Group {0} has {1}", group, activity);
            groupDisplay.Content = group;

            ActivityDisplay.Text = activity.Name;

            ActivityTimeLine atl = schedule[group];

            Activity next = atl[activity.EndTime+new TimeSpan(0,1,0)];

            NextActivityDisplay.Text = next.Name;

            //scheduleView.ItemsSource = atl;
        }

        private IEnumerable<string> readProcess()
        {
            ProcessStartInfo zbarStart = new ProcessStartInfo(@"C:\Program Files\ZBar\bin\zbarcam.exe");
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
                    yield return line;
                }
            }
            reader.Close();
        }

        private Dictionary<string, ActivityTimeLine> GetSchedule()
        {
            Dictionary<string, ActivityTimeLine> schedule = new Dictionary<string, ActivityTimeLine>();

            DataSet set = ExcelDataReader.exceldata(@"C:\Documents and Settings\L.vBeek\My Documents\Scouting\Jotari\jotari planning 2011 laatste versie.xlsx");

            DataTable klein = set.Tables["Klein"];
            DataRow groupNrs = klein.Rows[1]; //this is the 2nd filled row

            for (int groupCol = 2; groupCol < groupNrs.ItemArray.Length; groupCol++) //start from 2, for column C
            {
                ActivityTimeLine atl = ActivityTimeLine.FromDataTable(klein, groupCol);

                string groupNr = groupNrs[groupCol].ToString();

                schedule.Add(groupNr, atl);
            }

            return schedule;
        }
    }
}
