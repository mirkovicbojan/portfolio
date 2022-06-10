namespace TimeSheet.Models
{
    public class Member
    {
        public int memberID { get; set; }
        public string? memberName { get; set; }
        public float hoursPerWeek { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public int status { get; set; }
        public int role { get; set; }

        /* Reserved for future use (Exporting all data)
        public List<TimeSheetClass>? timeSheets{get;set;}
        */
    }
}