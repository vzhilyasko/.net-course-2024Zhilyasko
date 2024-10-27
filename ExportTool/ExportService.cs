using System.Globalization;
using System.Text;
using BankSystem.Models;
using Newtonsoft.Json;

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

        public ExportService()
        {
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

        public void SerializationToJSON<T>(string pathFile, T items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var serialize = JsonConvert.SerializeObject(items);
            File.WriteAllText(pathFile, serialize);
        }

        public void SerializationCollectionToJSON<T>(string pathFile, List<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            
            var serialize = JsonConvert.SerializeObject(items);
            File.WriteAllText(pathFile,serialize);
        }

        public T DeserializationFtomJSON <T>(string pathFile)
        {
            if (pathFile == null)
            {
                throw new ArgumentNullException("В пути к файлу null");
            }
            if (!File.Exists(pathFile))
            {
                throw new FileNotFoundException("Файл отсутствует");
            }
            
            var deserialize = JsonConvert.DeserializeObject<T>(File.ReadAllText(pathFile));
            return deserialize;
        }

        public List<T> DeserializationCollectionFtomJSON <T>(string pathFile)
        {
            if (pathFile == null)
            {
                throw new ArgumentNullException("В пути к файлу null");
            }
            if (!File.Exists(pathFile))
            {
                throw new FileNotFoundException("Файл отсутствует");
            }
            
            var deserialize = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(pathFile));
            return deserialize;
        }
    }
}