using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarTask
{
    public class AddEventState : ICalendarState
    {
        public void AddEvent(Calendar calendar, string eventDetails, DateTime selectedDate)
        {
            if (!string.IsNullOrEmpty(eventDetails))
            {
                calendar.Events.Add($"{selectedDate.ToShortDateString()}: {eventDetails}");
            }
            else
            {
                throw new InvalidOperationException("Нельзя добавить пустое событие.");
            }
        }

        public void EditEvent(Calendar calendar, int index, string eventDetails)
        {
            throw new InvalidOperationException("Невозможно редактировать событие в режиме добавления.");
        }

        public void DeleteEvent(Calendar calendar, int index)
        {
            throw new InvalidOperationException("Невозможно удалить событие в режиме добавления.");
        }

        public void ViewEvents(Calendar calendar)
        {
            throw new InvalidOperationException("Невозможно просматривать события в режиме добавления.");
        }
    }
}