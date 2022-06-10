using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services.Interfaces;
using TimeSheet.DTO_models;
using TimeSheet.CustomExceptions;

namespace TimeSheet.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;

        }

        public IEnumerable<ClientDTO> GetAll()
        {
            var retVal = _clientRepository.GetAll()
            .Select(item => ClientDTO.ToClientDTO(item));

            if (retVal.Count() == 0)
            {
                throw new EmptyListException("No Clients were found in database");
            }

            return retVal;
        }

        public Client GetOne(int id)
        {
            var retVal = _clientRepository.GetById(id);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Client with id: {id} wasn't found.");
            }

            return retVal;
        }

        public void DeleteOne(Client obj)
        {
            var retVal = _clientRepository.GetById(obj.clientID);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Client with id: {obj.clientID} wasn't found.");
            }

            _clientRepository.Delete(obj);
        }

        public Client UpdateOne(Client obj)
        {
            var retVal = _clientRepository.GetById(obj.clientID);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Client with id: {obj.clientID} wasn't found.");
            }
            
            return _clientRepository.Edit(obj);
        }

        public Client Save(Client obj)
        {
            if (string.IsNullOrEmpty(obj.clientName))
            {
                throw new InvalidObjectParamsException("Client name cannot be empty.");
            }
            return _clientRepository.Save(obj);
        }
    }
}