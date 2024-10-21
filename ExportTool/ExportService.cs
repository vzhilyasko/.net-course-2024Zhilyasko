using System.Globalization;
using System.Text;
using BankSystem.Data.Storages;
using BankSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ExportTool
{
    public class ExportService
    {
        private string PathToDirecory { get; set; }
        private string CSVFileName { get; set; }

        public ExportService(string pathToDirectory, string csvFileName)
        {
            PathToDirecory = pathToDirectory;
            CSVFileName = csvFileName;
        }

        public void WriteClientToCsv(List<Client> clients)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(PathToDirecory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(PathToDirecory, CSVFileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    using (var writer = new CsvHelper.CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        writer.WriteRecords(clients);
                        writer.Flush();
                    }
                }
            }
        }

        public List<Client> ReadClientToCsv()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(PathToDirecory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = Path.Combine(PathToDirecory, CSVFileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    using (var reader = new CsvHelper.CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        var clients = reader.GetRecords<Client>().ToList();
                        return clients;
                    }
                }
            }
        }
    }
}