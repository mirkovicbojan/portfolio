using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet.Models
{
    public class Project
    {
        public int projectID { get; set; }
        public string? projectName { get; set; }
        public string? projectDescription { get; set; }


        [ForeignKey("ClientID")]
        public int? currentclientID { get; set; }
        public virtual Client? currentClient { get; set; }

        
        [ForeignKey("memberID")]
        public int? memberID {get;set;}
        
        public virtual Member? Member { get; set; }

    }
}