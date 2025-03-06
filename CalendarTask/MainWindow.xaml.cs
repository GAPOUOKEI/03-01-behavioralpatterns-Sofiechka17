using CalendarTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CalendarTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Calendar _calendar;
        private int _selectedEventIndex = -1; // Индекс выбранного события для редактирования
        public MainWindow()
        {
            InitializeComponent();
            _calendar = new Calendar(new ViewEventState()); // Начальное состояние — просмотр
            UpdateEventsDisplay();
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _calendar.SetState(new AddEventState());
                DateTime selectedDate = EventCalendar.SelectedDate ?? DateTime.Now;
                _calendar.AddEvent(EventDetails.Text, selectedDate);
                UpdateEventsDisplay();
                EventDetails.Text = ""; // Очищаем поле ввода после добавления
                _selectedEventIndex = -1; // Сбрасываем индекс после добавления
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedEventIndex != -1 && _selectedEventIndex < _calendar.Events.Count)
                {
                    _calendar.SetState(new EditEventState());
                    _calendar.EditEvent(_selectedEventIndex, EventDetails.Text);
                    UpdateEventsDisplay();
                    EventDetails.Text = "";
                    _selectedEventIndex = -1;
                }
                else
                {
                    MessageBox.Show("Выберите событие для редактирования.");
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedEventIndex != -1 && _selectedEventIndex < _calendar.Events.Count)
                {
                    _calendar.SetState(new ViewEventState());
                    _calendar.DeleteEvent(_selectedEventIndex);
                    UpdateEventsDisplay();
                    EventDetails.Text = "";
                    _selectedEventIndex = -1; 
                }
                else
                {
                    MessageBox.Show("Выберите событие для удаления.");
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Обновление отображения событий
        private void UpdateEventsDisplay()
        {
            EventsPanel.Children.Clear();

            for (int i = 0; i < _calendar.Events.Count; i++)
            {
                var eventText = _calendar.Events[i];

                var textBlock = new TextBlock
                {
                    Text = eventText,
                    Margin = new Thickness(0, 5, 0, 5),
                    Cursor = Cursors.Hand
                };

                // Добавляем обработчик для клика по событию
                textBlock.MouseLeftButtonUp += (s, e) =>
                {
                    _selectedEventIndex = i;
                    // Отображаем текст события в TextBox
                    EventDetails.Text = eventText;
                    Console.WriteLine($"Вы выбрали событие: {eventText} с индексом {_selectedEventIndex}");
                };

                EventsPanel.Children.Add(textBlock);
            }
        }
    }
}