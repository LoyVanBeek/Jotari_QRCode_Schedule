using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace BarcodeScanner.DataLookup
{
    public class ExcelDataReader : IDataReader
    {
        const int STARTTIMEINDEX = 0;
        const int ENDTIMEINDEX = 1;

        const int RADIO = 2;
        const int JOTA = 3;
        const int TV = 4;
        const int JOTI = 5;
        const int KLIMTOREN = 6;
        const int VELD = 7;
        const int BOS = 8;

        Dictionary<int, string> columnActivities;

       public string Filename {get; private set;}
        DataSet data;

        int blockDuration = 20;
        int columnOffset = 1;

        public ExcelDataReader(string filename)
        {
            this.Filename = filename;
            data = exceldata(filename);

            columnActivities = new Dictionary<int, string>();
            columnActivities.Add(2, "Radio");
            columnActivities.Add(3, "Jota");
            columnActivities.Add(4, "TV");
            columnActivities.Add(5, "Joti");
            columnActivities.Add(6, "Klimtoren");
            columnActivities.Add(7, "Veld");
            columnActivities.Add(8, "Bos");
        }

        #region IDataReader Members

        public string Lookup(int groupnumber, DateTime time)
        {
            DataTable table = data.Tables["Klein"];

            #region get row for time
            int rowIndex = -1;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                object start = table.Rows[i][STARTTIMEINDEX];
                object end = table.Rows[i][ENDTIMEINDEX];
                DateTime? iStartTime = start as DateTime?;
                DateTime? iEndTime = end as DateTime?;

                if (iStartTime != null && iEndTime != null)
                {
                    if (time >= iStartTime && time < iEndTime)
                    {
                        rowIndex = i;
                        break;
                    }
                }
            } 
            #endregion

            string activity = "Onbekend, vraag de leiding";

            //if the next row does not contain numbers of groups, the activity is the non-numeric string in the cell.
            //If a cell contains the groups number, that is their activity.
            foreach (int column in columnActivities.Keys)
            {
                List<int> groupsForColumn = ParseCellContents((string)table.Rows[rowIndex][column]);
                if (groupsForColumn == null)
                {
                    activity = (string)table.Rows[rowIndex][column];
                    break;
                }
                else if (groupsForColumn.Contains(groupnumber))
                {
                    activity = columnActivities[column];
                    break;
                }
            }

            return activity;
        }

        public static List<int> ParseCellContents(string p)
        {
            if (p.Contains('+'))//if the cell contains a +, then in contains groups
            {
                //p = p.Replace(' ', ''); //remove spaces
                List<string> parts = new List<string>(p.Split('+'));
                List<int> numbers = new List<int>();
                foreach (string part in parts)
                {
                    string numStr = part.Trim();
                    int num;
                    bool ok = int.TryParse(numStr, out num);
                    if (ok)
                    {
                        numbers.Add(num);
                    }
                }
                return numbers;
            }
            else
            {
                return null;
            }
        }

        #endregion

        public static DataSet exceldata(string filelocation)
        {
            DataSet ds = new DataSet();

            string excelConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
            OleDbConnection excelConn = new OleDbConnection(excelConnStr);
            excelConn.Open();

            #region programma klein
            OleDbCommand kleinCommand = new OleDbCommand("SELECT * FROM [Kleine speltakken$]", excelConn);

            OleDbDataAdapter kleinDA = new OleDbDataAdapter(kleinCommand);

            DataTable kleinSchedule = new DataTable();
            kleinDA.Fill(kleinSchedule);

            kleinSchedule.TableName = "Klein";
            ds.Tables.Add(kleinSchedule);
            #endregion programma klein

            #region programma groot
            OleDbCommand grootCommand = new OleDbCommand("SELECT * FROM [Grote speltakken$]", excelConn);

            OleDbDataAdapter grootDA = new OleDbDataAdapter(grootCommand);

            DataTable grootSchedule = new DataTable();
            grootDA.Fill(grootSchedule);

            grootSchedule.TableName = "Groot";
            ds.Tables.Add(grootSchedule);
            #endregion programma groot

            #region leiding
            OleDbCommand leidingCommand = new OleDbCommand("SELECT * FROM [Leiding Kleine speltakken$]", excelConn);

            OleDbDataAdapter leidingDA = new OleDbDataAdapter(leidingCommand);

            DataTable leidingSchedule = new DataTable();
            leidingDA.Fill(leidingSchedule);

            leidingSchedule.TableName = "Leiding Kleine speltakken";
            ds.Tables.Add(leidingSchedule);
            #endregion leiding
            excelConn.Close();

            return ds;
        }
    }
}
