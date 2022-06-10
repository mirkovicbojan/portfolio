using System.ComponentModel.DataAnnotations;

namespace TimeSheet.Models
{
    public class Client
    {
        [Key]
        public int clientID { get; set; }
        public string clientName { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public int zip { get; set; }
        public string? country { get; set; }

        //One-Many relationship with Project.cs saved for future use.
       
        //public ICollection<Project>? Projects { get; set; }
        
    }
}