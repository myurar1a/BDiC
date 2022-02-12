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
        private DateTime _current;
        // 今日の曜日
        private DayOfWeek _todayDayOfWeek;

        // 今日の日付
        private DateTime _date;
        // BDから取得した日付
        private String? _latestDate;
        // 今日と更新日の差
        private int _dateCompare;

        // 処理開始時間 (引数値)
        private DateTime _before;
        // 現在と処理開始時間の差分
        private TimeSpan _delta;

        public void ExctionDateDecision()
        {
            this._current = DateTime.Now;
            this._todayDayOfWeek = this._current.DayOfWeek;
            if (this._todayDayOfWeek != DayOfWeek.Saturday || this._todayDayOfWeek != DayOfWeek.Sunday)
            {
                Console.WriteLine("今日は週末なので、お休みです！");
                Environment.Exit(0);
            }
            else
                return;
        }

        public bool ConfirmationOfArrivalDate(String latestDateRaw)
        {
            this._latestDate = latestDateRaw.Trim();
            this._date = DateTime.Today;
            this._dateCompare = DateTime.Compare(this._date, DateTime.Parse(this._latestDate));
            // 今日と同日なら0、前の日なら1

            if (this._dateCompare == 0)
                return true;
            else
                return false;
        }

        public TimeSpan WaitTime(DateTime before)
        {
            this._before = before;
            this._current = DateTime.Now;
            this._delta = this._current - before;

            return new TimeSpan(0, 0, 0, 5) - this._delta;
        }
    }
}
