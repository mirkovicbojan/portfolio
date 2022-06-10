using CsvHelper.Configuration.Attributes;
using TimeSheet.Models;

namespace TimeSheet.DTO_Models
{
    public class TimeSheetDTO
    {
        public int? sheetID { get; set; }
        public string? clientName { get; set; }

        public string? memberName { get; set; }
        public string? projectName { get; set; }
        public string? categoryName { get; set; }
        public string? description { get; set; }
        public double time { get; set; }
        public double overtime { get; set; }

        [Format("yyyy-MM-dd")] //Format for CSV writing not database annotation
        public DateTime date { get; set; }

        public static TimeSheetDTO ToTimeSheetDTO(TimeSheetClass timeSheet)
        {
            var retVal = new TimeSheetDTO();
            retVal.sheetID = timeSheet.sheetID;
            retVal.description = timeSheet.description;
            retVal.time = timeSheet.time;
            retVal.overtime = timeSheet.overtime;
            retVal.memberName = timeSheet.Member?.memberName;
            retVal.projectName = timeSheet.Project?.projectName;
            retVal.categoryName = timeSheet.Category?.categoryName;
            retVal.clientName = timeSheet.Client?.clientName;
            retVal.date = timeSheet.date;

            return retVal;
        }
    }
}