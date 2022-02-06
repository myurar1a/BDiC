using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDiC
{
    public class TimeRelated
    {
        // 現在時刻
        private DateTime current;
        // 今日の曜日
        private DayOfWeek todayDayOfWeek;
        
        // BDから取得した日付
        private String latestDate;
        // 今日と更新日の差
        private int dateCompare;

        // 処理開始時間 (引数値)
        private DateTime before;

        public void ExctionDateDecision()
        {
            current = DateTime.Now;
            todayDayOfWeek = current.DayOfWeek;
            if (todayDayOfWeek != DayOfWeek.Saturday || todayDayOfWeek != DayOfWeek.Sunday)
            {
                Console.WriteLine("今日は週末なので、お休みです！");
                Environment.Exit(0);
            }
            else
                return;
        }

        public bool ConfirmationOfArrivalDate(String latestDateRaw)
        {
            this.latestDate = latestDateRaw.Trim();
            current = DateTime.Now;
            dateCompare = DateTime.Compare(current, DateTime.Parse(latestDate));

            if (dateCompare == 0)
                return true;
            else
                return false;

        }
    }
}
