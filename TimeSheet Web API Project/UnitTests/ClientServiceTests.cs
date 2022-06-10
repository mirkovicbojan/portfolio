using Moq;
using TimeSheet.CustomExceptions;
using TimeSheet.DTO_models;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services;

namespace UnitTests
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> repo = new Mock<IClientRepository>();

        [Fact]
        public void GetAll_ReturnsAllClients()
        {
            //Arrange
            var clients = new List<Client>();
            Client clientOne = new Client
            {
                clientID = 1,
                clientName = "Client One",
                address = "ClientOne Adress 1/1",
                city = "Client City",
                zip = 1,
                country = "Client Country"
            };
            Client clientTwo = new Client
            {
                clientID = 2,
                clientName = "Client Two",
                address = "ClientTwo Adress 1/1",
                city = "Client City",
                zip = 1,
                country = "Client Country"
            };
            clients.Add(clientOne);
            clients.Add(clientTwo);
            repo.Setup(c => c.GetAll()).Returns(clients);
            ClientService clientService = new ClientService(repo.Object);
            //Act
            List<ClientDTO> clientList = clientService.GetAll().ToList();
            //Assert
            Assert.Equal(1, clientList[0].clientID);
            Assert.Equal(2, clientList[1].clientID);
            Assert.NotEmpty(clientList);
        }

        [Fact]
        public void GetOne_ExistingIdPassed_ReturnsCorrectClient()
        {
            //Arrange
            var expectedClient = new Client
            {
                clientID = 1,
                clientName = "Client One",
                address = "ClientOne Adress 1/1",
                city = "Client City",
                zip = 1,
                country = "Client Country"
            };
            repo.Setup(c => c.GetById(1)).Returns(expectedClient);
            ClientService clientService = new ClientService(repo.Object);
            //Act
            var client = clientService.GetOne(1);
            //Assert
            Assert.Equal(expectedClient.clientName, client.clientName);
        }

        [Fact]
        public void GetOne_UnknownIdPassed_ReturnsKeyNotFoundException()
        {
            //Arrange
            var expectedClient = new Client
            {
                clientID = 1,
                clientName = "Client One",
                address = "ClientOne Adress 1/1",
                city = "Client City",
                zip = 1,
                country = "Client Country"
            };
            repo.Setup(c => c.GetById(1)).Returns(expectedClient);
            ClientService clientService = new ClientService(repo.Object);
            //Act
            Action act = () => clientService.GetOne(5);
            //Assert
            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(act);
            Assert.Equal("Client with id: 5 wasn't found.", exception.Message);

        }
        
        [Fact]
        public void Save_NewClient_ReturnsClientObject()
        {
            //Arrange
            var Client = new Client
            {
                clientID = 1,
                clientName = "Client One",
                address = "ClientOne Adress 1/1",
                city = "Client City",
                zip = 1,
                country = "Client Country"
            };
            repo.Setup(c => c.Save(Client)).Returns(Client);
            ClientService clientService = new ClientService(repo.Object);
            //Act
            var newClient = clientService.Save(Client);
            //Assert
            repo.Verify(c => c.Save(Client), Times.Once);
            Assert.Equal(Client.clientName, newClient.clientName);
        }

        [Fact]
        public void Save_InvalidObject_ReturnsInvalidObjectParamsException()
        {
            //Arrange
            var Client = new Client
            {
                clientID = 1,
                address = "ClientOne Adress 1/1",
                city = "Client City",
                zip = 1,
                country = "Client Country"
            };
            repo.Setup(c => c.Save(Client)).Returns(Client);
            ClientService clientService = new ClientService(repo.Object);
            //Act
            Action act = () => clientService.Save(Client);
            //Assert
            InvalidObjectParamsException exception = Assert.Throws<InvalidObjectParamsException>(act);
            Assert.Equal("Client name cannot be empty.", exception.Message);
        }

        [Fact]
        public void UpdateOne_ObjectExists_ReturnsObject()
        {
            //Arrange
            var updatedClient = new Client
            {
                clientID = 1,
                clientName = "Client One",
                address = "ClientOne Adress 1/1",
                city = "Client City",
                zip = 1,
                country = "Client Country"
            };
            repo.Setup(c => c.Edit(updatedClient)).Returns(updatedClient);
            repo.Setup(c => c.GetById(1)).Returns(updatedClient);
            ClientService clientService = new ClientService(repo.Object);
            //Act
            var updated = clientService.UpdateOne(updatedClient);
            //Assert
            repo.Verify(c => c.Edit(updatedClient), Times.Once);
            Assert.Equal(updatedClient.clientID, updated.clientID);
        }

        [Fact]
        public void DeleteOne_ExistingObject_CallsRepository()
        {
            //Arrange
            var forDeletion = new Client
            {
                clientID = 1,
                clientName = "Client One",
                address = "ClientOne Adress 1/1",
                city = "Client City",
                zip = 1,
                country = "Client Country"
            };
            repo.Setup(c => c.Edit(forDeletion));
            repo.Setup(c => c.GetById(1)).Returns(forDeletion);
            ClientService clientService = new ClientService(repo.Object);
            //Act
            clientService.DeleteOne(forDeletion);
            //Assert
            repo.Verify(f => f.Delete(forDeletion), Times.Once);
        }
    }
}