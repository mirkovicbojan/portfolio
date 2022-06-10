using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services.Interfaces;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using TimeSheet.DTO_models;
using iText.Kernel.Geom;
using CsvHelper;
using TimeSheet.DTO_Models;
using iText.Kernel.Colors;
using TimeSheet.CustomExceptions;

namespace TimeSheet.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly Color HeaderColor = new iText.Kernel.Colors.DeviceRgb(242, 106, 64);
        private readonly Color CellColor = new iText.Kernel.Colors.DeviceRgb(236, 236, 236);

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public IEnumerable<ReportDTO> Search(ReportSearchDTO obj)
        {
            IEnumerable<TimeSheetDTO> foundList = _reportRepository
                .GetAll()
                .Select(item => TimeSheetDTO.ToTimeSheetDTO(item));

            if (!string.IsNullOrEmpty(obj.memberName))
            {
                foundList = foundList.Where(e => e.memberName == obj.memberName);
            }
            if (!string.IsNullOrEmpty(obj.clientName))
            {
                foundList = foundList.Where(e => e.clientName == obj.clientName);
            }
            if (!string.IsNullOrEmpty(obj.projectName))
            {
                foundList = foundList.Where(e => e.projectName == obj.projectName);
            }
            if (!string.IsNullOrEmpty(obj.categoryName))
            {
                foundList = foundList.Where(e => e.categoryName == obj.categoryName);

            }
            if (!string.IsNullOrEmpty(obj.startDate))
            {
                DateTime converted = Convert.ToDateTime(obj.startDate);
                foundList = foundList.Where(e => (e.date.CompareTo(converted) > 0));
            }
            if (!string.IsNullOrEmpty(obj.endDate))
            {
                DateTime converted = Convert.ToDateTime(obj.endDate);
                foundList = foundList.Where(e => (e.date.CompareTo(converted) < 0));
            }
            if (foundList.Count() == 0)
            {
                throw new EmptyListException("No Reports matching the filters were found.");
            }

            return foundList.Select(item => ReportDTO.ToReportDTO(item));
        }

        public byte[] writeToPDF(IEnumerable<ReportDTO> reports)
        {
            var stream = new MemoryStream();

            using (var writer = new PdfWriter(stream))
            using (var pdf = new PdfDocument(writer))
            using (var document = new Document(pdf, PageSize.A4, false))
            {
                document.Add(createHeader("REPORTS"));
                document.Add(createTable(reports));
            }

            if (stream.ToArray().Count() == 0)
            {
                throw new EmptyStreamException("Unable to create a PDF document.");
            }

            return stream.ToArray();
        }

        public byte[] writeToCSV(IEnumerable<ReportDTO> reports)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(reports);
            }

            if (stream.ToArray().Count() == 0)
            {
                throw new EmptyStreamException("Unable to write to CSV.");
            }

            return stream.ToArray();
        }

        public Table createTable(IEnumerable<ReportDTO> reports)
        {
            Table table = new Table(6, false);

            table.AddHeaderCell(createCell(HeaderColor, "Date"));
            table.AddHeaderCell(createCell(HeaderColor, "Team Member"));
            table.AddHeaderCell(createCell(HeaderColor, "Project"));
            table.AddHeaderCell(createCell(HeaderColor, "Category"));
            table.AddHeaderCell(createCell(HeaderColor, "Description"));
            table.AddHeaderCell(createCell(HeaderColor, "Time"));

            foreach (var report in reports)
            {
                table.AddCell(createCell(CellColor, string.Format("{0:d}", report.Date)));
                table.AddCell(createCell(CellColor, report.memberName));
                table.AddCell(createCell(CellColor, report.projectName));
                table.AddCell(createCell(CellColor, report.categoryName));
                table.AddCell(createCell(CellColor, report.description));
                table.AddCell(createCell(CellColor, report.time.ToString()));
            }

            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            return table;
        }

        public Cell createCell(Color cellColor, String cellText)
        {
            List<Cell> cellList = new List<Cell>();

            Cell tableCell = new Cell(1, 1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontColor(ColorConstants.BLACK)
                .SetBackgroundColor(cellColor)
                .SetFontSize(8);

            tableCell.Add(new Paragraph(cellText ?? ""));

            return tableCell;
        }

        public Paragraph createHeader(string input)
        {
            
            Paragraph header = new Paragraph(input)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            return header;
        }
    }
}