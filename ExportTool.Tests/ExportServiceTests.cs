using BankSystem.App.Interfaces;
using BankSystem.Data.Storages;
using BankSystem.Models;

namespace ExportTool.Tests
{
    public class ExportServiceTests
    {
        private BankSystemDbContext _context = new BankSystemDbContext();

        private readonly string PathToDirecnory = Path.Combine("csv");
        private readonly string CSVFileName = "clients.csv";

        private string PathDirectoryDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private string nameFileClients = "clients.json";
        private string nameFileEmployees = "employees.json";

        [Fact]
        public void WriteClientsToCSVFile()
        {
            var clients = _context
                .Clients
                .ToList();

            var exporterToCSV = new ExportService(PathToDirecnory, CSVFileName);

            exporterToCSV.WriteClientToCsv(clients);
        }

        [Fact]
        public void ReadClientsFromCSVFile()
        {
            var exporterToCSV = new ExportService(PathToDirecnory, CSVFileName);

            var clients = exporterToCSV.ReadClientToCsv();

            Assert.NotEmpty(clients);
        }

        [Fact]
        public void AddClientsFromCSVFileToDataBase()
        {
            var exporterToCSV = new ExportService(PathToDirecnory, CSVFileName);

            var clients = exporterToCSV.ReadClientToCsv();

            clients.ForEach(x=>
            {
                x.Id = Guid.Empty;
            });
            
            _context.AddRange(clients);
        }

        [Fact]
        public void UpdateClientsFromCSVFileToDataBase()
        {
            var exporterToCSV = new ExportService(PathToDirecnory, CSVFileName);

            var clients = exporterToCSV.ReadClientToCsv();
            
            clients.ForEach(x =>
            {
                _context.UpdateRange(x);
            });
        }

        [Fact]
        public void SerializeCollectionClientsToJSON()
        {
            var exporterToJSON = new ExportService();

            var pathToFile = Path.Combine(PathDirectoryDesktop, nameFileClients);

            var clients = _context
                .Clients
                .ToList();

            exporterToJSON.SerializationCollectionToJSON<Client>(pathToFile, clients);
        }

        [Fact]
        public void DeserializeCollectionClientsFromJSON()
        {
            var exporterToJSON = new ExportService();

            var pathToFile = Path.Combine(PathDirectoryDesktop, nameFileClients);
            
            var clients = exporterToJSON.DeserializationCollectionFtomJSON<Client>(pathToFile);

            Assert.NotEmpty(clients);
        }

        [Fact]
        public void SerializeCollectionEmployeesToJSON()
        {
            var exporterToJSON = new ExportService();

            var pathToFile = Path.Combine(PathDirectoryDesktop, nameFileEmployees);

            var employees = _context
                .Employees
                .ToList();

            exporterToJSON.SerializationCollectionToJSON<Employee>(pathToFile, employees);
        }

        [Fact]
        public void DeserializeCollectionEmployeesFromJSON()
        {
            var exporterToJSON = new ExportService();

            var pathToFile = Path.Combine(PathDirectoryDesktop, nameFileEmployees);

            var employees = exporterToJSON.DeserializationCollectionFtomJSON<Employee>(pathToFile);

            Assert.NotEmpty(employees);
        }

        [Fact]
        public void SerializeClientToJSON()
        {
            var exporterToJSON = new ExportService();

            var client = _context
                .Clients
                .ToList()[0];

            var nameFile = client.Id + "_" + client.FullName() + "_клиент.json";
            var pathToFile = Path.Combine(PathDirectoryDesktop, nameFile);
            
            exporterToJSON.SerializationToJSON<Client>(pathToFile, client);
        }

        [Fact]
        public void DeserializeClientFromJSON()
        {
            var exporterToJSON = new ExportService();

            var client = _context
                .Clients
                .ToList()[0];
            
            var nameFile = client.Id + "_" + client.FullName() + "_клиент.json";
            var pathToFile = Path.Combine(PathDirectoryDesktop, nameFile);

            var clientDeserialize = exporterToJSON.DeserializationFtomJSON<Client>(pathToFile);

            Assert.NotNull(clientDeserialize);
        }

        [Fact]
        public void SerializeEmployeeToJSON()
        {
            var exporterToJSON = new ExportService();

            var employee = _context
                .Employees
                .ToList()[0];
            
            var nameFile = employee.Id + "_" + employee.FullName() + "_работник.json";
            var pathToFile = Path.Combine(PathDirectoryDesktop, nameFile);
            
            exporterToJSON.SerializationToJSON<Employee>(pathToFile, employee);
        }

        [Fact]
        public void DeserializeEmployeeFromJSON()
        {
            var exporterToJSON = new ExportService();

            var employee = _context
                .Employees
                .ToList()[0];
            
            var nameFile = employee.Id + "_" + employee.FullName() + "_работник.json";
            var pathToFile = Path.Combine(PathDirectoryDesktop, nameFile);

            var clientDeserialize = exporterToJSON.DeserializationFtomJSON<Employee>(pathToFile);

            Assert.NotNull(clientDeserialize);
        }
    }
}