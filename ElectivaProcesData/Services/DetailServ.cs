using ElectivaProcesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectivaProcesData.Services
{
    public class DetailServ
    {
        private readonly csvS _csvHandler;

        public DetailServ(csvS csvHandler)
        {
            _csvHandler = csvHandler;
        }

        public List<OrdersDetails> LoadAndValidate(string filePath, List<Orders> orderList, List<Products> productList)
        {
            var details = _csvHandler.LoadCsv<OrdersDetails>(filePath);

            details = details
                .GroupBy(t => new { t.OrderID, t.ProductID })
                .Select(q => q.First())
                .ToList();

            details = details
                .Where(t => orderList.Any(o => o.OrderID == t.OrderID)
                         && productList.Any(p => p.ProductID == t.ProductID))
                .ToList();

            return details;
        }
    }
}
