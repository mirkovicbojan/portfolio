using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Receipt_Micro_Service.DataAccess;
using Receipt_Micro_Service.Models;
using Receipt_Micro_Service.Repisotory.Interfaces;

namespace Receipt_Micro_Service.Repisotory
{
    public class ReceiptRepository:Repository<Receipt>, IReceiptRepository
    {
        private ReceiptDbContext _dbContext;

        public ReceiptRepository(ReceiptDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}