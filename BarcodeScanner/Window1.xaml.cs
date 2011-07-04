using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using BarcodeScanner.Data;
using BarcodeScanner.DataLookup;
using BarcodeScanner.Scanner;
using System.Windows.Controls;

namespace BarcodeScanner
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Dictionary<string, ActivityTimeLine> schedule;

        ICodeReader scanner;

        public Window1()
        {
            Loaded += new RoutedEventHandler(Window1_Loaded);
            InitializeComponent();

            Closing += new CancelEventHandler(Window1_Closing);
        }

        void Window1_Closing(object sender, CancelEventArgs e)
        {
            scanner.Stop();
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            scanner = new ZBarInterface();
            scanner.CodeRead += new CodeReadHandler(scanner_CodeRead);
            scanner.Start();

            schedule = GetSchedule();
        }

        void scanner_CodeRead(string code)
        {
            Dispatcher.BeginInvoke(
                  DispatcherPriority.Background,
                  new Action(
                      () => HandleCodeRead(code)));
        }

        private void HandleCodeRead(string idStr)
        {
            string[] parts = idStr.Split(':');
            if (parts[0].ToUpper() == "QR-CODE")
            {
                string id = parts[1];
                DateTime now = DateTime.Now;

                string day = (comboBox1.SelectedValue as ComboBoxItem).Content.ToString();
                if (day == "Zaterdag")
                {
                    now = new DateTime(2011, 10, 15, now.Hour, now.Minute, now.Second);
                }
                else if (day == "Zondag")
                {
                    now = new DateTime(2011, 10, 16, now.Hour, now.Minute, now.Second);
                }
                else
                {
                    //Act like sunday
                    now = new DateTime(2011, 10, 16, now.Hour, now.Minute, now.Second);
                }

                Activity acti= null;
                try
                {
                    ActivityTimeLine atl = schedule[id];
                    acti = atl[now];
                }
                catch (KeyNotFoundException)
                {
                    acti = new Activity("Onbekend, vraag de leiding", new DateTime(), new DateTime(), "");
                }

                Console.Beep();

                displayActivity(id, acti);
            }
        }

        private void displayActivity(string group, Activity activity)
        {
            string groupNr = group.Replace("Groot", "");
            groupNr = group.Replace("Klein", ""); //Remove Klein and Groot
            groupDisplay.Content = groupNr;

            ActivityDisplay.Text = activity.Name;

            ActivityTimeLine atl = schedule[group];

            Activity next = atl[activity.EndTime + new TimeSpan(0, 1, 0)];

            NextActivityDisplay.Text = next.Name;
        }

        private Dictionary<string, ActivityTimeLine> GetSchedule()
        {
            Dictionary<string, ActivityTimeLine> schedule = new Dictionary<string, ActivityTimeLine>();

            DataSet set = ExcelDataReader.exceldata(@"C:\Documents and Settings\L.vBeek\My Documents\Scouting\Jotari\jotari planning 2011 laatste versie.xlsx");

            DataTable klein = set.Tables["Klein"];
            DataRow kleinGroupNrs = klein.Rows[1]; //this is the 2nd filled row

            for (int groupCol = 2; groupCol < kleinGroupNrs.ItemArray.Length; groupCol++) //start from 2, for column C
            {
                ActivityTimeLine atl = ActivityTimeLine.FromDataTable(klein, groupCol);

                string groupNr = kleinGroupNrs[groupCol].ToString();

                schedule.Add("Klein"+groupNr, atl);
            }

            DataTable groot = set.Tables["Groot"];
            DataRow grootGroupNrs = groot.Rows[45]; //this is the 2nd filled row

            for (int groupCol = 2; groupCol < grootGroupNrs.ItemArray.Length; groupCol++) //start from 2, for column C
            {
                ActivityTimeLine atl = ActivityTimeLine.FromDataTable(groot, groupCol);

                string groupNr = grootGroupNrs[groupCol].ToString();

                schedule.Add("Groot" + groupNr, atl);
            }

            return schedule;
        }
    }
}
