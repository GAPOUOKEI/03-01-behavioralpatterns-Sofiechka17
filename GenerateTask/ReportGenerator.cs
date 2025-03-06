using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateTask
{
    abstract class ReportGenerator
    {
        protected List<Sale> SalesData;

        protected abstract void ValidateData();
        protected abstract void FormatData();
        protected abstract void SaveReport();

        public void GenerateReport()
        {
            SalesData = GetSalesData();
            ValidateData();
            FormatData();
            SaveReport();
        }

        protected virtual List<Sale> GetSalesData()
        {
            return new List<Sale>
            {
                new Sale { Id = 1, ProductName = "Ноутбук", Price = 120000, Quantity = 7, SaleDate = DateTime.Now },
                new Sale { Id = 2, ProductName = "Телефон", Price = 80000, Quantity = 5, SaleDate = DateTime.Now },
                new Sale { Id = 3, ProductName = "Планшет", Price = 60000, Quantity = 10, SaleDate = DateTime.Now }
            };
        }
    }
}