using BankSystem.Models;
using BankSystem.App.Services;
using System.IO;
using System.Text;
using System.IO.Pipes;
using System.Globalization;
using System.Text.Json;
using System.Diagnostics.Metrics;
using BankSystem.Domain.Models;

namespace ExportTool.Tests
{
    public class ThreadAndTaskTests
    {
        private string pathDirectoryDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string nameFolder = "Export";
        private string nameFileClients = "_clients.json";
        private readonly object locker = new();
        private readonly ExportService exporterToJSON = new ExportService();

        [Fact]
        public void SerializeCollectionClientsToJSON()
        {
            var _storage = new TestDataGeneratorServise().GenerateListClient(10000);
            var currentFile = 1;

            var pathToDirectory = Path.Combine(pathDirectoryDesktop, nameFolder);

            if (!Directory.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }

            var maxSize = 100000;

            var thread = new Thread(() => Serialize(_storage, pathToDirectory, maxSize));
           
            thread.Start();
            thread.Join();
        }

        private void Serialize(List<Client> _storage, string pathToDirectory, int maxSizeFile)
        {
            var currentSize = 0;
            var currentFile = 1;
            var pathToFile = "";

            var clients = new List<Client>();
            
            foreach (var client in _storage)
            {
                var json = JsonSerializer.Serialize(client);
                var jsonSize = Encoding.UTF8.GetByteCount(json);
                pathToFile = Path.Combine(pathToDirectory, $"{currentFile}" + nameFileClients);

                currentSize += jsonSize;

                clients.Add(client);

                if (currentSize <= maxSizeFile) continue;

                lock (locker)
                {
                    exporterToJSON.SerializationCollectionToJSON(pathToFile, clients );
                }

                currentFile++;
                pathToFile = Path.Combine(pathToDirectory, $"{currentFile}" + nameFileClients);

                currentSize = 0;
                clients.Clear();
            }

            if (clients.Count <= 0) return;

            lock (locker)
            {
                exporterToJSON.SerializationCollectionToJSON(pathToFile, clients);
            }
        }

        [Fact]
        public void DeserializeCollectionClientsFromJSON()
        {
            var exporterToJSON = new ExportService();

            var pathToDirectory = Path.Combine(pathDirectoryDesktop, nameFolder);

            if (!Directory.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }

            var semaphore = new Semaphore(1, 1);
            IEnumerable<string> allfiles = Directory.EnumerateFiles(pathToDirectory);

             var currentFile = allfiles.Count();

            //var currentFile = 1;

            var clients = new List<Client>();

            ThreadPool.QueueUserWorkItem(_ =>
            {
                semaphore.WaitOne();
                
                for (var i = 1; i <= currentFile; i++)
                {
                    var pathToFile = Path.Combine(pathToDirectory, $"{i}_clients.json");

                    clients.AddRange(exporterToJSON.DeserializationFtomJSON<List<Client>>(pathToFile));
                    File.Delete(Path.Combine(pathToDirectory, $"{currentFile}_clients.json"));
                }
                
                semaphore.Release();
            });
        }

        [Fact]
        public void AddAmountToAccountTest()
        {
            Account account = new Account();

            
            for (int i = 1; i <= 2; i++)
            {
                Thread thread = new(AddAmount);
                thread.Name = $"{i} thread";
                thread.Start();
            }

            Thread.Sleep(1000);
           
            void AddAmount()
            {
                for (int i = 0; i < 10; i++)
                {
                    lock (locker)
                    {
                        account.Amount += 100;
                    }
                    Thread.Sleep(100);
                }
            }
        }
    }
}
