using TimeSheet.DTO_models;
using TimeSheet.DTO_Models;
using iText.Layout.Element;

namespace TimeSheet.Services.Interfaces
{
    public interface IReportService
    {
        public IEnumerable<ReportDTO> Search(ReportSearchDTO obj);

        public byte[] writeToPDF(IEnumerable<ReportDTO> reports);

        public byte[] writeToCSV(IEnumerable<ReportDTO> reports);

        public Table createTable(IEnumerable<ReportDTO> reports);
    }
}