using ElectivaProcesData.DB;
using ElectivaProcesData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectivaProcesData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var csvHandler = new csvS();

            var productHandler = new ProductServ(csvHandler);
            var orderHandler = new OrderServ(csvHandler);
            var orderDetailHandler = new DetailServ(csvHandler);
            var clientHandler = new CustomrServ(csvHandler);

            var productList = productHandler.LoadAndValidate("Data/products.csv");
            var customerList = clientHandler.LoadAndClean("Data/customers.csv");
            var orderList = orderHandler.LoadAndClean("Data/orders.csv", customerList);
            var orderDetailList = orderDetailHandler.LoadAndValidate("Data/order_details.csv", orderList, productList);

            var dbContext = new DBsalesContext();

            var dataLoader = new LpServ(dbContext);
            dataLoader.InsertData(productList, customerList, orderList, orderDetailList);

            Console.WriteLine($"{productList.Count} products loaded correctly");
            Console.WriteLine($"{customerList.Count} clients loaded correctly");
            Console.WriteLine($"{orderList.Count} orders loaded correctly");
            Console.WriteLine($"{orderDetailList.Count} order details uploaded successfully");
            Console.WriteLine("Data successfully loaded");
        }
    }
}