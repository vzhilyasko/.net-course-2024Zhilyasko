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
        private readonly  List<Client> _storage = _dataGenerator.GenerateListClient(10000).ToList();
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
            var maxFileSize = 15000;

            if (!File.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }
            
            var threadSerializeToJSON = new Thread(() => SerializeInJSON(_storage, pathToDirectory, maxFileSize));
            var threadSerializeToJSON1 = new Thread(() => SerializeInJSON(_storage, pathToDirectory, maxFileSize));

            threadSerializeToJSON.Start();
            threadSerializeToJSON1.Start();
            threadSerializeToJSON.Join();
            threadSerializeToJSON1.Join();
        }

        private void SerializeInJSON(List<Client> _storage, string pathToDirectory, int maxFileSize)
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

                if (sizeFile <= maxFileSize) return;

                lock (_locker)
                {
                    _serializeToJSON.SerializationCollectionToJSON(pathFile, clients);
                }

                pathFile = Path.Combine(pathToDirectory, $"{currentFile}_" + _nameFileClients);
                sizeFile = 0;

                currentFile++;
                clients.Clear();
            });
            
            lock (_locker)
            {
                _serializeToJSON.SerializationCollectionToJSON(Path.Combine(pathToDirectory, $"{currentFile--}_" + _nameFileClients), clients);
            }
        }
        
        [Fact]
        public void DeserializeClientFromJSON()
        {
            //Arrange 
            var exporterToJSON = new ExportService();
            var pathFolder = Path.Combine(_pathDirectoryDesktop, _nameFolder);

            var countFileInDirectory = Directory.GetFiles(pathFolder).Count();

            var clients = new List<Client>();

            //Act
            for (int i = 1; i <= countFileInDirectory; i++)
            {
                var pathFile = Path.Combine(pathFolder, $"{i}_" + _nameFileClients);
                clients.AddRange(_serializeToJSON.DeserializationCollectionFtomJSON<Client>(pathFile));
            }

            //Assert
            Assert.Equal(clients.Count,10000);
        }

        [Fact]
        public void AddAmountInThreads()
        {
            //Arrange  
            var account = _dataGenerator.GenerateListAccount()[0];
           
            //Act
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


            //Assert
            Assert.Equal(account.Amount, 2000);
        }
    }
}
