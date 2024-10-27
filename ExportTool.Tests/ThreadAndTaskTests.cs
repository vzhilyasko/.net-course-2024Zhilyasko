using System.Text;
using System.Text.Json;
using BankSystem.App.Services;
using BankSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ExportTool.Tests
{
    public class ThreadAndTaskTests
    {
        private static TestDataGeneratorServise _dataGenerator = new TestDataGeneratorServise();
        private readonly List<Client> _storage = _dataGenerator.GenerateListClient(10000).ToList();
        ExportService _serializeToJSON = new ExportService();

        private string _pathDirectoryDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string _nameFolder = "Export";
        private string _nameFileClients = "clients.json";
        private readonly object _locker = new();
        
        [Fact]
        public void SerializeToJsonThread()
        {
            var pathToDirectory = Path.Combine(_pathDirectoryDesktop, _nameFolder);
            int currentFile = 1;
            var maxSizeFile = 10000;

            if (!File.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }

            var threadSerializeToJSON = new Thread(() => SerializeInJSON(_storage, pathToDirectory, maxSizeFile));

            threadSerializeToJSON.Start();
            threadSerializeToJSON.Join();
        }

        private void SerializeInJSON(List<Client> _storage, string pathToDirectory, int maxSize)
        {
            var sizeFile = 0;
            var currentFile = 1;
            var pathFile = Path.Combine(pathToDirectory, $"{currentFile}_" + _nameFileClients);
            
            
            var clients = new List<Client>();

            _storage.ForEach(x =>
            {
                var json = JsonSerializer.Serialize(x);
                var sizeJSON = Encoding.UTF8.GetByteCount(json);
                sizeFile += sizeJSON;

                clients.Add(x);

                if (sizeFile <= maxSize) return;

                lock (_locker)
                {
                    _serializeToJSON.SerializationCollectionToJSON(pathFile, clients);
                }

                pathFile = Path.Combine(pathToDirectory, $"{currentFile}_" + _nameFileClients);
                sizeFile = 0;

                currentFile++;
                clients.Clear();
            });

            if (_storage.Count <= 0) return;

            lock (_locker)
            {
                _serializeToJSON.SerializationCollectionToJSON(pathFile, clients);
            }
        }




        [Fact]
        public void DeserializeClientFromJSON()
        {
            var exporterToJSON = new ExportService();
            var pathFolder = Path.Combine(_pathDirectoryDesktop, _nameFolder);

            var countFileInDirectory = Directory.GetFiles(pathFolder).Count();

            var clients = new List<Client>();

            for (int i = 1; i <= countFileInDirectory; i++)
            {
                var pathFile = Path.Combine(pathFolder, $"{i}_" + _nameFileClients);
                clients.AddRange(_serializeToJSON.DeserializationCollectionFtomJSON<Client>(pathFile));
            }
        }

        [Fact]
        public void AddAmountInThreads()
        {
            var account = _dataGenerator.GenerateListAccount()[0];
           
            var threadAdd1 = new Thread(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    account.Amount += 100;
                }
            });

            var threadAdd2 = new Thread(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    account.Amount += 100;
                }
            });

            threadAdd1.Start();
            threadAdd2.Start();

            threadAdd1.Join();
            threadAdd2.Join();
        }
    }
}
