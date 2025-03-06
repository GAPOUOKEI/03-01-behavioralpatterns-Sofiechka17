using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CalendarTask
{
    public class ViewEventState : ICalendarState
    {
        public void AddEvent(Calendar calendar, string eventDetails, DateTime selectedDate)
        {
            throw new InvalidOperationException("Невозможно добавить событие в режиме просмотра.");
        }

        public void EditEvent(Calendar calendar, int index, string eventDetails)
        {
            throw new InvalidOperationException("Невозможно редактировать событие в режиме просмотра.");
        }

        public void DeleteEvent(Calendar calendar, int index)
        {
            if (index < 0 || index >= calendar.Events.Count)
                throw new InvalidOperationException("Некорректный индекс события.");

            calendar.Events.RemoveAt(index);
        }

        public void ViewEvents(Calendar calendar)
        {
            var allEvents = string.Join(Environment.NewLine, calendar.Events);

            if (string.IsNullOrWhiteSpace(allEvents))
            {
                MessageBox.Show("Событий нет.", "Список событий");
            }
            else
            {
                MessageBox.Show(allEvents, "Список событий");
            }
        }
    }
}