namespace TimeSheet.DTO_Models
{
    public class ReportSearchDTO
    {
        public string? memberName { get; set; }
        public string? clientName { get; set; }
        public string? projectName { get; set; }
        public string? categoryName { get; set; }
        public string? startDate{get;set;}
        public string? endDate{get;set;}

        
    }
}