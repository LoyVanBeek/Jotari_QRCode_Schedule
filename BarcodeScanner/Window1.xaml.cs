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
        DataSet data;

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

            data = GetData();
        }

        void scanner_CodeRead(string code)
        {
            Dispatcher.BeginInvoke(
                  DispatcherPriority.Background,
                  new Action(
                      () => HandleCodeRead(code)));
        }

        private void HandleCodeRead(string code)
        {
            string[] parts = code.Split(':');
            if (parts[0].ToUpper() == "QR-CODE")
            {
                string idStr = parts[1];
                int id = int.Parse(idStr);

                try
                {
                    DataTable prijzen = data.Tables[0];
                    DataRow row = prijzen.Rows[id];
                    object item = row[0];

                    PrijsDisplay.Text = item.ToString();
                }
                catch (Exception e)
                {
                    PrijsDisplay.Text = "Helaas, geen prijs";
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.InnerException);
                }
            }
        }

        private DataSet GetData()
        {
            DataSet set = ExcelDataReader.exceldata(@"Prizes.xlsx");

            DataTable klein = set.Tables["Klein"];
            
            return set;
        }
    }
}
