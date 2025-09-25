using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectivaProcesData.Services
{
    public class csvS
    {
        public List<T> LoadCsv<T>(string path)
        {
            var fileReader = new StreamReader(path);
            var csvParser = new CsvReader(fileReader, CultureInfo.InvariantCulture);
            return csvParser.GetRecords<T>().ToList();
        }
    }
}
