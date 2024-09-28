using BankSystem.App.Services;
using BankSystem.Models;

namespace BankSystem.App.Test
{
    public class ClientTests
    {
        [Fact]
        public void GetHashCodeNecessityPositivTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();

            var client = generatedClient.ElementAt(1).Key;

            var newClient = new Client()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                MidlleName = client.MidlleName,
                BankAccount = client.BankAccount,
                Birthday = client.Birthday,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };
            
            var presenceKey = generatedClient.ContainsKey(newClient);

            Assert.True(presenceKey);
        }

        [Fact]
        public void GetEqualityOperatorNecessityPositivTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();

            var client = generatedClient.ElementAt(1).Key;

            var newClient = new Client()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                MidlleName = client.MidlleName,
                BankAccount = client.BankAccount,
                Birthday = client.Birthday,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };

           bool result =  generatedClient.ElementAt(1).Key == newClient;
            
           Assert.True(result);
        }

        [Fact]
        public void GetInequalityOperatorNecessityPositivTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();

            var client = generatedClient.ElementAt(1).Key;

            var newClient = new Client()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                MidlleName = client.MidlleName,
                BankAccount = client.BankAccount,
                Birthday = client.Birthday,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };

            newClient.FirstName = newClient.FirstName + "new";
            
            bool result = generatedClient.ElementAt(1).Key != newClient;

            Assert.True(result);
        }
    }
}