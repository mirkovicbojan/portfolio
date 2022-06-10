using TimeSheet.Contexts;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;

namespace TimeSheet.Repository
{
    public class ClientRepository:Repository<Client>, IClientRepository
    {
        private TimeSheetContext dbContext;
        

        public ClientRepository(TimeSheetContext context):base(context)
        {
            dbContext = context;
        }
    }
}