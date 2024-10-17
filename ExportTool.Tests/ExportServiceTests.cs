using BankSystem.Data.Storages;

namespace ExportTool.Tests
{
    public class ExportServiceTests
    {
        BankSystemDbContext _context = new BankSystemDbContext();

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
        public void UpdateClientsFromCSVFileToDataBase()
        {
            var exporterToCSV = new ExportService(PathToDirecnory, CSVFileName);

             exporterToCSV.UpdateClientsFromFile();
        }

        [Fact]
        public void AddNewClientsFromCSVFileToDataBase()
        {
            var exporterToCSV = new ExportService(PathToDirecnory, CSVFileName);

            exporterToCSV.AddClientsFromFile();
        }
    }
}