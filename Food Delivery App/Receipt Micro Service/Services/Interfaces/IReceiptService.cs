

using Receipt_Micro_Service.Models;

namespace Receipt_Micro_Service.Services.Interfaces
{
    public interface IReceiptService
    {
        public void emailReceipt(Receipt order);
    }
}