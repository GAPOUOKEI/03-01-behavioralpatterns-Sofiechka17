using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CsvHelper;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace GenerateTask
{
    class SalesReport : ReportGenerator
    {
        private iTextSharp.text.Document _pdfDocument;
        private string _filePath;

        protected override void ValidateData()
        {
            Console.WriteLine("Проверка данных о продажах...");

            foreach (var sale in SalesData)
            {
                if (sale.Price <= 0 || sale.Quantity <= 0)
                {
                    throw new InvalidDataException("Некорректные данные о продажах: цена или количество не могут быть меньше или равны нулю.");
                }

                if (string.IsNullOrEmpty(sale.ProductName))
                {
                    throw new InvalidDataException("Некорректные данные о продажах: название товара не может быть пустым.");
                }
            }

            Console.WriteLine("Данные прошли проверку.");
        }

        protected override void FormatData()
        {
            Console.WriteLine("Форматирование данных о продажах...");

            _filePath = "sales_report.pdf";
            _pdfDocument = new iTextSharp.text.Document();
            PdfWriter.GetInstance(_pdfDocument, new FileStream(_filePath, FileMode.Create));
            _pdfDocument.Open();

            // Устанавливаем шрифт с поддержкой кириллицы
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "segoeui.ttf");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var font = new Font(baseFont, 12, Font.NORMAL);
            var boldFont = new Font(baseFont, 15, Font.BOLD);

            var title = new Paragraph("Отчет о продажах", boldFont);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20f; 
            _pdfDocument.Add(title);

            // Добавляем таблицу с данными
            var table = new PdfPTable(5);
            table.WidthPercentage = 100;

            table.AddCell(new PdfPCell(new Phrase("ID", font)));
            table.AddCell(new PdfPCell(new Phrase("Товар", font)));
            table.AddCell(new PdfPCell(new Phrase("Цена", font)));
            table.AddCell(new PdfPCell(new Phrase("Количество", font)));
            table.AddCell(new PdfPCell(new Phrase("Дата продажи", font)));

            // Данные о продажах
            foreach (var sale in SalesData)
            {
                table.AddCell(new PdfPCell(new Phrase(sale.Id.ToString(), font)));
                table.AddCell(new PdfPCell(new Phrase(sale.ProductName, font)));
                table.AddCell(new PdfPCell(new Phrase(sale.Price.ToString(), font)));
                table.AddCell(new PdfPCell(new Phrase(sale.Quantity.ToString(), font)));
                table.AddCell(new PdfPCell(new Phrase(sale.SaleDate.ToShortDateString(), font)));
            }

            _pdfDocument.Add(table);

            Console.WriteLine("Данные отформатированы.");
        }

        protected override void SaveReport()
        {
            Console.WriteLine("Сохранение отчета о продажах...");
            _pdfDocument.Close();

            Console.WriteLine($"PDF-отчет сохранен в файл: {_filePath}");

            SaveCsvReport();
        }

        private void SaveCsvReport()
        {
            string csvFilePath = "sales_report.csv";

            using (var writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(SalesData);
            }

            Console.WriteLine($"CSV-отчет сохранен в файл: {csvFilePath}");
        }
    }
}