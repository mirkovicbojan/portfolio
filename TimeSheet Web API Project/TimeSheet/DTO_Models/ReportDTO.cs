namespace TimeSheet.DTO_Models
{
    public class ReportDTO
    {
        public string? memberName { get; set; }
        public string? clientName { get; set; }
        public string? projectName { get; set; }
        public string? categoryName { get; set; }
        public string? Date { get; set; }
        public string? description { get; set; }
        public double time { get; set; }

        public static ReportDTO ToReportDTO(TimeSheetDTO timeSheet)
        {
            var retVal = new ReportDTO();
            retVal.memberName = timeSheet.memberName;
            retVal.clientName = timeSheet.clientName;
            retVal.projectName = timeSheet.projectName;
            retVal.categoryName = timeSheet.categoryName;
            retVal.Date = timeSheet.date.ToShortDateString();
            retVal.description = timeSheet.description;
            retVal.time = timeSheet.time;

            return retVal;
        }
    }
}