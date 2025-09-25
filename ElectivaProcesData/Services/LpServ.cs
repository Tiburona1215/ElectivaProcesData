using ElectivaProcesData.DB;
using ElectivaProcesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectivaProcesData.Services
{
    public class LpServ
    {
        private readonly DBsalesContext _context;

        public LpServ(DBsalesContext context)
        {
            _context = context;
        }

        public void InsertData(
            List<Products> productList,
            List<Customers> customerList,
            List<Orders> orderList,
            List<OrdersDetails> detailList)
        {
            _context.Products.AddRange(productList);
            _context.Customers.AddRange(customerList);
            _context.Orders.AddRange(orderList);
            _context.OrderDetail.AddRange(detailList);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException? .Message);
                throw ex;
            }
            
        }
    }
}
