
using MailKit.Net.Smtp;
using MimeKit;
using Receipt_Micro_Service.Models;
using Receipt_Micro_Service.Repisotory.Interfaces;
using Receipt_Micro_Service.Services.Interfaces;

namespace Receipt_Micro_Service.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _repository;

        private readonly IConfiguration _config;
        public ReceiptService(IConfiguration config, IReceiptRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public void emailReceipt(Receipt order)
        {
            var receiptString = createReceiptString(order);
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse($"{order.userEmail}"));
            email.Subject = "Food App Order Receipt";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text){Text = receiptString};

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

             _repository.Save(order);

        }

        private string createReceiptString(Receipt order)
        {
            string receipt = "Your latest food app order: \n "+
            $"{order.Id} \n"+
            $"{order.createdOn}";
            foreach(var item in order.foods)
            {
                receipt = receipt + "\n" + $"{item}";
            }
            receipt += $"\n Delivery Fee: {order.deliveryFee} \n Total: {order.priceSum}";
            return receipt;
        }
    }
}