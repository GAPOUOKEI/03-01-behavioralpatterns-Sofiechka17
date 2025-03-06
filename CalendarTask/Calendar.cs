using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalendarTask
{
    public class Calendar
    {
        private ICalendarState _currentState;
        public List<string> Events { get; } = new List<string>();

        public Calendar(ICalendarState initialState)
        {
            _currentState = initialState;
        }

        public void SetState(ICalendarState newState)
        {
            _currentState = newState;
        }

        public void AddEvent(string eventDetails, DateTime selectedDate)
        {
            _currentState.AddEvent(this, eventDetails, selectedDate);
        }

        public void EditEvent(int index, string eventDetails)
        {
            _currentState.EditEvent(this, index, eventDetails);
        }

        public void DeleteEvent(int index)
        {
            _currentState.DeleteEvent(this, index);
        }

        public void ViewEvents()
        {
            _currentState.ViewEvents(this);
        }
    }
}