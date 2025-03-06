namespace GenerateTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ReportGenerator report = new SalesReport();
                report.GenerateReport();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}