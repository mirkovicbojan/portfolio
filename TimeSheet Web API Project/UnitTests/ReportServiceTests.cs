using Moq;
using TimeSheet.DTO_models;
using TimeSheet.DTO_Models;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services;

namespace UnitTests
{
    public class ReportServiceTests
    {
        Mock<IReportRepository> repo = new Mock<IReportRepository>();
        Mock<ITimeSheetRepository> tsRepo = new Mock<ITimeSheetRepository>();

        private static IReportRepository CreateRepoforMethod_GetAll()
        {
            var stubRepository = new Mock<IReportRepository>();
            IEnumerable<TimeSheetClass> tsList = new List<TimeSheetClass>
            {
                new TimeSheetClass{
                    sheetID = 1,
                    description = "Description 1",
                    time = 8,
                    overtime = 0,
                    Member = new Member{},
                    Project = new Project{},
                    Category = new Category{},
                    Client = new Client{},
                    date = Convert.ToDateTime("05/05/2022"),
                },
                new TimeSheetClass
                {
                    sheetID = 2,
                    description = "Description 2",
                    time = 8,
                    overtime = 0,
                    Member = new Member{memberName = "MemberT2"},
                    Project = new Project{projectName="ProjectT2"},
                    Category = new Category{categoryName="CategoryT2"},
                    Client = new Client{clientName="ClientT2"},
                    date = Convert.ToDateTime("06/06/2022")

                }
            };
            stubRepository.Setup(r => r.GetAll()).Returns(tsList);
            return stubRepository.Object;
        }

        [Fact]
        public void Search_Returns_AllTimeSheets()
        {
            //Arrange
            var report = new ReportSearchDTO { };
            ReportService reportService = new ReportService(CreateRepoforMethod_GetAll());
            //Act
            IEnumerable<ReportDTO> searchReturn = reportService.Search(report);
            //Asser
            Assert.NotEmpty(searchReturn);
        }

        [Fact]
        public void Search_ByMember_ReturnsDTOList()
        {
            //Arrange
            var report = new ReportSearchDTO
            {
                memberName = "MemberT2"
            };
            ReportService reportService = new ReportService(CreateRepoforMethod_GetAll());
            //Act
            IEnumerable<ReportDTO> searchReturn = reportService.Search(report);
            //Assert
            foreach (var returnedReport in searchReturn)
            {
                Assert.True(report.memberName == returnedReport.memberName);
            }
        }

        [Fact]
        public void Search_ByProject_ReturnsDTOList()
        {
            //Arrange
            var report = new ReportSearchDTO
            {
                projectName = "ProjectT2"
            };
            ReportService reportService = new ReportService(CreateRepoforMethod_GetAll());
            //Act
            IEnumerable<ReportDTO> searchReturn = reportService.Search(report);
            //Assert
            foreach (var returnedReport in searchReturn)
            {
                Assert.True(report.projectName == returnedReport.projectName);
            }
        }

        [Fact]
        public void Search_ByCategory_ReturnsDTOList()
        {
            //Arrange
            var report = new ReportSearchDTO
            {
                categoryName = "CategoryT2"
            };
            ReportService reportService = new ReportService(CreateRepoforMethod_GetAll());
            //Act
            IEnumerable<ReportDTO> searchReturn = reportService.Search(report);
            //Assert
            foreach (var returnedReport in searchReturn)
            {
                Assert.True(report.categoryName == returnedReport.categoryName);
            }
        }

        [Fact]
        public void Search_ByStartDate_ReturnsDTOList()
        {
            //Arrange
            var report = new ReportSearchDTO
            {
                startDate = "04/04/2022"
            };
            ReportService reportService = new ReportService(CreateRepoforMethod_GetAll());
            //Act
            IEnumerable<ReportDTO> searchReturn = reportService.Search(report);
            var first = searchReturn.FirstOrDefault();
            //Assert
            foreach (var returnedReport in searchReturn)
            {
                Assert.True(Convert.ToDateTime(report.startDate) < Convert.ToDateTime(returnedReport.Date));
            }
        }

        [Fact]
        public void Search_ByEndDate_ReturnsDTOList()
        {
            //Arrange
            var report = new ReportSearchDTO
            {
                endDate = "07/07/2022"
            };
            ReportService reportService = new ReportService(CreateRepoforMethod_GetAll());
            //Act
            IEnumerable<ReportDTO> searchReturn = reportService.Search(report);
            var first = searchReturn.FirstOrDefault();
            //Assert
            foreach (var returnedReport in searchReturn)
            {
                Assert.True(Convert.ToDateTime(report.endDate) > Convert.ToDateTime(returnedReport.Date));
            }
        }

        [Fact]
        public void Search_ByClientName_ReturnsDTOList()
        {
            //Arrange
            var report = new ReportSearchDTO
            {
                clientName = "ClientT2"
            };
            ReportService reportService = new ReportService(CreateRepoforMethod_GetAll());
            //Act
            IEnumerable<ReportDTO> searchReturn = reportService.Search(report);
            //Assert
            foreach (var returnedReport in searchReturn)
            {
                Assert.True(report.clientName == returnedReport.clientName);
            }
        }

        [Fact]
        public void writeToPDF_Returns_ByteArray()
        {
            //Arrange
            IEnumerable<TimeSheetClass> reports = new List<TimeSheetClass> { };
            repo.Setup(r => r.GetAll()).Returns(reports);
            ReportService reportService = new ReportService(repo.Object);
            //Act
            var stream = reportService.writeToPDF(reports.Select(item => ReportDTO.ToReportDTO(TimeSheetDTO.ToTimeSheetDTO(item))));
            //Assert
            Assert.NotEmpty(stream);
        }
        
        [Fact]
        public void writeToCSV_Returns_ByteArray()
        {
            //Arrange
            IEnumerable<TimeSheetClass> reports = new List<TimeSheetClass> { };
            repo.Setup(r => r.GetAll()).Returns(reports);
            ReportService reportService = new ReportService(repo.Object);
            //Act
            var stream = reportService.writeToCSV(reports.Select(item => ReportDTO.ToReportDTO(TimeSheetDTO.ToTimeSheetDTO(item))));
            //Assert
            Assert.NotEmpty(stream);
        }
    }
}