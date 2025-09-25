using ElectivaProcesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectivaProcesData.Services
{
    public class OrderServ
    {
        private readonly csvS _csvHandler;

        public OrderServ(csvS csvHandler)
        {
            _csvHandler = csvHandler;
        }

        public List<Orders> LoadAndClean(string filePath, List<Customers> customerList)
        {
            var orderList = _csvHandler.LoadCsv<Orders>(filePath);

            orderList = orderList
                .GroupBy(o => o.OrderID)
                .Select(g => g.First())
                .ToList();

            foreach (var order in orderList)
            {
                order.OrderDate = order.OrderDate.Date;
                order.Status = order.Status.Trim().ToLower();
            }

            orderList = orderList
                .Where(o => customerList.Any(c => c.CustomerID == o.CustomerID))
                .ToList();

            return orderList;
        }
    }
}
