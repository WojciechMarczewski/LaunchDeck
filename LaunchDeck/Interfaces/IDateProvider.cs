using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchDeck.Interfaces
{
    public interface IDateProvider
    {
        void SetDate(DateTime dateOnly);
        DateTime GetDate();
        string GetDayOfWeek();
        string GetDateFormattedYearFirst();
    }
}
