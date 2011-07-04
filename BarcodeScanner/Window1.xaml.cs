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
        Process reader;

        BarcodeScanner.DataLookup.IDataReader data;

        Thread worker;

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
                int id = int.Parse(parts[1]);
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
                string activity = data.Lookup(id, now);

                ActivityTimeLine atl = schedule[id.ToString()];
                Activity acti = atl[now];

                Console.Beep();

                displayActivity(id.ToString(), acti);
            }
        }

        private void displayActivity(string group, Activity activity)
        {
            Console.WriteLine("Group {0} has {1}", group, activity);
            groupDisplay.Content = group;

            ActivityDisplay.Text = activity.Name;

            ActivityTimeLine atl = schedule[group];

            Activity next = atl[activity.EndTime + new TimeSpan(0, 1, 0)];

            NextActivityDisplay.Text = next.Name;

            //scheduleView.ItemsSource = atl;
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
