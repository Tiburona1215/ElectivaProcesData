using ElectivaProcesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectivaProcesData.Services
{
    public class ProductServ
    {
        private readonly csvS _csvHandler;

        public ProductServ(csvS csvHandler)
        {
            _csvHandler = csvHandler;
        }

        public List<Products> LoadAndValidate(string filePath)
        {
            var productList = _csvHandler.LoadCsv<Products>(filePath);

            productList = productList
                .GroupBy(p => p.ProductID)
                .Select(g => g.First())
                .ToList();

            foreach (var product in productList)
            {
                product.ProductName = product.ProductName.Trim();
                product.Category = product.Category.Trim().ToLower();
            }

            productList = productList
                .Where(p => !string.IsNullOrEmpty(p.ProductName) && p.Price > 0)
                .ToList();

            return productList;
        }
    }
}
