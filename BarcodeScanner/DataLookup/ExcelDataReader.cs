using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.OleDb;

namespace BarcodeScanner.DataLookup
{
    public class ExcelDataReader : IDataReader
    {
        public string Filename {get; private set;}
        Application excel;
        DataSet data;

        int blockDuration = 20;
        int columnOffset = 1;

        public ExcelDataReader(string filename)
        {
            this.Filename = filename;
            data = exceldata(filename);
        }

        #region IDataReader Members

        public string Lookup(int groupnumber, DateTime time)
        {
            //row 3 has the groupnumbers
            //column A shows the times

            int groupColumn = groupnumber + columnOffset; //Offset to jump to the right row. 
            DataRow currentTimeRow = null;

            DataRow[] rows = data.Tables[0].Select();
            foreach (DataRow row in rows)
            {
                string rowTotalTimeStr = row[0].ToString();
                DateTime rowTime = new DateTime();
                try
                {
                    rowTime = DateTime.Parse(rowTotalTimeStr);
                }
                catch (FormatException)
                {}

                //if (rowTime.Hour==time.Hour && rowTime.Minute == time.Minute)
                if (Utilities.Later(time, rowTime) && Utilities.Later(rowTime.AddMinutes(blockDuration), time))
                {
                    currentTimeRow = row;
                    break;
                }
            }

            if (currentTimeRow != null)
            {
                object activity = currentTimeRow[groupColumn];
                return activity as string;
            }
            else
            {
                //throw new DataException("Datarow for current time not found");
                return "Onbekend, vraag de leiding";
            }
        }

        /// <summary>
        /// True if questioned is later than t2
        /// </summary>
        /// <param name="questioned"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        //private bool Later(DateTime questioned, DateTime compareTo)
        //{
        //    bool minuteOK = questioned.Minute > compareTo.Minute;
        //    if (questioned.Hour > compareTo.Hour)
        //    {
        //        return true;
        //    }
        //    else if(questioned.Hour == compareTo.Hour)
        //    {
        //        return minuteOK;
        //    }
        //    else //if(questioned.Hour < compareTo.Hour)
        //    {
        //        return false;
        //    }
        //}

        #endregion

        public static DataSet exceldata(string filelocation)
        {
            DataSet ds = new DataSet();

            OleDbCommand excelCommand = new OleDbCommand(); 
            OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter();

            string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
            //string excelConnStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + filelocation + "; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            OleDbConnection excelConn = new OleDbConnection(excelConnStr);

            excelConn.Open();

            System.Data.DataTable dtPatterns = new System.Data.DataTable();
            excelCommand = new OleDbCommand("SELECT * FROM [klein$]", excelConn);

            excelDataAdapter.SelectCommand = excelCommand;

            excelDataAdapter.Fill(dtPatterns);

            dtPatterns.TableName = "Klein";

            ds.Tables.Add(dtPatterns);

            excelConn.Close();

            return ds;

        }
    }
}
