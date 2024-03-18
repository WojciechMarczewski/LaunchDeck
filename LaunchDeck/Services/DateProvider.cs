using LaunchDeck.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LaunchDeck.Services
{
    public class DateProvider : IDateProvider
    {
        private DateTime _date;


        public DateTime GetDate()
        {
            return _date;
        }
        public string GetDayOfWeek()
        {
            string dayOfWeekName = _date.ToString("dddd", CultureInfo.GetCultureInfo("pl-PL"));
            dayOfWeekName = dayOfWeekName[0].ToString().ToUpper() + dayOfWeekName.Substring(1);
            return dayOfWeekName;
        }

        public void SetDate(DateTime date)
        {
            _date = date;
        }

        public DateProvider()
        {
            _date = DateTime.Now;
        }
        public DateProvider(DateTime date)
        {
            _date = date;
        }
        public string GetDateFormattedYearFirst()
        {
            return _date.ToString("yyyy.MM.dd");
        }
    }
}
