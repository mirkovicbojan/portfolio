using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TimeSheet.Models
{
    public class TimeSheetClass
    {


        [Key]
        public int sheetID { get; set; }
        public string? description { get; set; }
        public double time { get; set; }
        public double overtime { get; set; }

        public DateTime date { get; set; }


        [ForeignKey("clientID")]
        public int? clientID { get; set; }
        public virtual Client? Client { get; set; }


        [ForeignKey("projectID")]
        public int? projectID { get; set; }
        public virtual Project? Project { get; set; }


        [ForeignKey("categoryID")]
        public int? categoryID { get; set; }
        public virtual Category? Category { get; set; }


        [ForeignKey("memberID")]
        public int? memberID { get; set; }
        public virtual Member? Member { get; set; }


    }
}