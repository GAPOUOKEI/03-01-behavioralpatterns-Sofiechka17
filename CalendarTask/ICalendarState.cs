using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarTask
{
    public interface ICalendarState
    {
        void AddEvent(Calendar calendar, string eventDetails, DateTime selectedDate);
        void EditEvent(Calendar calendar, int index, string eventDetails);
        void DeleteEvent(Calendar calendar, int index);
        void ViewEvents(Calendar calendar);
    }
}