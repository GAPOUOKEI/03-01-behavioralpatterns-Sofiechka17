using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarTask
{
    public class EditEventState : ICalendarState
    {
        public void AddEvent(Calendar calendar, string eventDetails, DateTime selectedDate)
        {
            throw new InvalidOperationException("Невозможно добавить событие в режиме редактирования.");
        }

        public void EditEvent(Calendar calendar, int index, string eventDetails)
        {
            if (index < 0 || index >= calendar.Events.Count)
                throw new InvalidOperationException("Некорректный индекс события для редактирования.");

            if (string.IsNullOrEmpty(eventDetails))
                throw new InvalidOperationException("Содержимое события не может быть пустым.");

            // Обновляем событие
            calendar.Events[index] = $"{calendar.Events[index].Split(':')[0]}: {eventDetails}";
        }

        public void DeleteEvent(Calendar calendar, int index)
        {
            throw new InvalidOperationException("Невозможно удалить событие в режиме редактирования.");
        }

        public void ViewEvents(Calendar calendar)
        {
            throw new InvalidOperationException("Невозможно просматривать события в режиме редактирования.");
        }
    }
}