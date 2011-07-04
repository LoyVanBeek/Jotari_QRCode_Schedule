using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;

namespace BarcodeScanner.Data
{
    /// <summary>
    /// Represents the timeline of activities for a single group. 
    /// </summary>
    public class ActivityTimeLine : List<Activity>, INotifyCollectionChanged
    {
        public ActivityTimeLine()
        {
        }

        public Activity getCurrent(DateTime now)
        {
            this.Sort(new ActivitySortComparer());

            Activity act = new Activity("",DateTime.Now, DateTime.Now, "");
            foreach(Activity activity in this)
            {
                //if (Utilities.Later(now, activity.StartTime) && Utilities.Later(activity.EndTime, now))
                if ((now > activity.StartTime && activity.EndTime > now))
                {
                    act = activity;
                    break;
                }
            }
            return act;
        }
        public Activity this[DateTime start]
        {
            get
            {
                return getCurrent(start);
            }
            set
            {
                this.Add(value);
                this.Sort(new ActivitySortComparer());
            }
        }

        private class ActivitySortComparer : IComparer<Activity>
        {

            #region IComparer<Activity> Members

            public int Compare(Activity x, Activity y)
            {
                return x.StartTime.CompareTo(y.StartTime);
            }

            #endregion
        }

        public static ActivityTimeLine FromDataTable(DataTable table, int column)
        {
            ActivityTimeLine atl = new ActivityTimeLine();

            DataRow[] rows = table.Select();

            for(int i=0;i<table.Rows.Count;i++)
            {
                DataRow row = table.Rows[i];
                #region row time
                string rowTotalTimeStr = row[0].ToString();
                
                DateTime rowTime = new DateTime();
                try
                {
                    rowTime = DateTime.Parse(rowTotalTimeStr);
                }
                catch (FormatException)
                {}
                #endregion row time

                string name = row[column] as string;


                DateTime nextRowTime = rowTime + new TimeSpan(0, 20, 0);
                try 
	            {	        
		            DataRow next = table.Rows[i+1];
                    #region row time
                    string nextRowTimeStr = next[0].ToString();
                    
                    try
                    {
                        nextRowTime = DateTime.Parse(nextRowTimeStr);
                    }
                    catch (FormatException)
                    {}
                    #endregion row time
	            }
	            catch (IndexOutOfRangeException)
	            {}

                if (rowTime != new DateTime())
                {
                    Activity act = new Activity(name, rowTime, nextRowTime, "");
                    atl.Add(act); 
                }
            }
            atl.Sort(new ActivitySortComparer());
            return atl;
        }

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion
    }
}
