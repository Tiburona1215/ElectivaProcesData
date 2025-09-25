using ElectivaProcesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectivaProcesData.Services
{
    public class CustomrServ
    {
        private readonly csvS _csvHandler;

        public CustomrServ(csvS csvHandler)
        {
            _csvHandler = csvHandler;
        }

        public List<Customers> LoadAndClean(string filePath)
        {
            var clients = _csvHandler.LoadCsv<Customers>(filePath);

            clients = clients
                .GroupBy(c => c.CustomerID)
                .Select(g => g.First())
                .ToList();

            foreach (var client in clients)
            {
                client.FirstName = client.FirstName.Trim();
                client.LastName = client.LastName.Trim();
                client.Email = client.Email.Trim().ToLower();
                client.City = client.City.Trim();
                client.Country = client.Country.Trim().ToLower();
                client.Phone = FormatPhone(client.Phone);
            }

            clients = clients
                .Where(c => !string.IsNullOrEmpty(c.Email) && c.Email.Contains("@") && c.Email.Contains("."))
                .ToList();

            return clients;
        }

        public string FormatPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return "";

            phone = phone.Trim().ToLower();

            int extPos = phone.IndexOf('x');
            if (extPos >= 0) phone = phone.Substring(0, extPos);

            extPos = phone.IndexOf("ext");
            if (extPos >= 0) phone = phone.Substring(0, extPos);

            var digitsOnly = new List<char>();
            foreach (char c in phone)
            {
                if (char.IsDigit(c))
                    digitsOnly.Add(c);
            }

            if (digitsOnly.Count > 20)
                digitsOnly = digitsOnly.Take(20).ToList();

            return new string(digitsOnly.ToArray());
        }

    }
}
