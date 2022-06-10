using TimeSheet.Models;

namespace TimeSheet.DTO_models
{
    public class ClientDTO
    {
        public int? clientID { get; set; }
        public string? clientName { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public int zip { get; set; }
        public string? country { get; set; }

        public static ClientDTO ToClientDTO(Client client)
        {
            return new ClientDTO()
            {
                clientID = client.clientID,
                clientName = client.clientName,
                address = client.address,
                city = client.city,
                zip = client.zip,
                country = client.country
            };
        }
    }
}
