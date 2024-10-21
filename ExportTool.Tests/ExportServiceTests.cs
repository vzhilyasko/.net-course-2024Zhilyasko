using BankSystem.App.Interfaces;
using BankSystem.Data.Storages;

namespace ExportTool.Tests
{
    public class ExportServiceTests
    {
        private BankSystemDbContext _context = new BankSystemDbContext();

        private readonly string PathToDirecnory = Path.Combine("csv");
        private readonly string CSVFileName = "clients.csv";

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
    }
}