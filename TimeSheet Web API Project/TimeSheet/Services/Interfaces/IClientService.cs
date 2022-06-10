using TimeSheet.DTO_models;
using TimeSheet.Models;

namespace TimeSheet.Services.Interfaces
{
    public interface IClientService
    {
        public IEnumerable<ClientDTO> GetAll();

        public Client GetOne(int id);

        public void DeleteOne(Client obj);

        public Client UpdateOne(Client obj);

        public Client Save(Client obj);
    }
}