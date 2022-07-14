using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food_Delivery_App.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string username { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string address { get; set; }
        
    }
}