using Moq;
using TimeSheet.CustomExceptions;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services;

namespace UnitTests
{
    public class TimeSheetServiceTests
    {
        private readonly Mock<ITimeSheetRepository> repo = new Mock<ITimeSheetRepository>();

        [Fact]
        public void GetAll_ReturnsFull_TimeSheetList()
        {
            //Arrange
            IEnumerable<TimeSheetClass> tsList = new List<TimeSheetClass>()
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
            //Act
            repo.Setup(t => t.GetAll()).Returns(tsList);
            TimeSheetService timeSheetService = new TimeSheetService(repo.Object);
            var TsheetList = timeSheetService.GetAll();
            //Assert
            Assert.NotEmpty(TsheetList);
        }

        [Fact]
        public void GetOne_ExistingIdPassed_ReturnsObject()
        {
            //Arrange
            TimeSheetClass tsheet1 = new TimeSheetClass
            {
                sheetID = 1,
                description = "Description 1",
                time = 8,
                overtime = 0,
                date = Convert.ToDateTime("05/05/2022")
            };
            repo.Setup(t => t.GetById(1)).Returns(tsheet1);
            TimeSheetService timeSheetService = new TimeSheetService(repo.Object);
            //Act
            var timeSheet = timeSheetService.GetOne(1);
            //Asser
            Assert.Equal(tsheet1.sheetID, timeSheet.sheetID);
        }
        [Fact]
        public void GetOne_UnknownIdPassed_ReturnsNull()
        {
            //Arrange
            TimeSheetClass tsheet1 = new TimeSheetClass
            {
                sheetID = 1,
                description = "Description 1",
                time = 8,
                overtime = 0,
                date = Convert.ToDateTime("05/05/2022")
            };
            repo.Setup(t => t.GetById(1)).Returns(tsheet1);
            TimeSheetService timeSheetService = new TimeSheetService(repo.Object);
            //Act
            Action act = () => timeSheetService.GetOne(2);
            //Assert
            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(act);
            Assert.Equal("TimeSheet with id: 2 wasn't found.",exception.Message);
        }
        [Fact]
        public void Save_NewSheet_ReturnsTimeSheetObject()
        {
            //Arrange
            TimeSheetClass tsheet1 = new TimeSheetClass
            {
                sheetID = 1,
                description = "Description 1",
                time = 8,
                overtime = 0,
                date = Convert.ToDateTime("05/05/2022")
            };
            repo.Setup(t => t.Save(tsheet1)).Returns(tsheet1);
            TimeSheetService timeSheetService = new TimeSheetService(repo.Object);
            //Act
            var timeSheet = timeSheetService.Save(tsheet1);
            //Assert
            repo.Verify(f => f.Save(tsheet1), Times.Once);
            Assert.Equal(tsheet1.sheetID, timeSheet.sheetID);
        }
        [Fact]
        public void Save_InvalidObject_ReturnsInvalidObjectParamsException()
        {
            //Arrange
            TimeSheetClass tsheet1 = new TimeSheetClass
            {
                sheetID = 1,
                overtime = 0,
                date = Convert.ToDateTime("05/05/2022")
            };
            repo.Setup(t => t.Save(tsheet1)).Returns(tsheet1);
            TimeSheetService timeSheetService = new TimeSheetService(repo.Object);
            //Act
            Action act = () => timeSheetService.Save(tsheet1);
            //Assert
            repo.Verify(f => f.Save(tsheet1), Times.Never);
            InvalidObjectParamsException exception = Assert.Throws<InvalidObjectParamsException>(act);
            Assert.Equal("TimeSheet description is required.",exception.Message);
        }
        [Fact]
        public void Delete_ValidObject_CallsRepository()
        {
            //Arrange
            TimeSheetClass forDeletion = new TimeSheetClass
            {
                sheetID = 1,
                description = "Description 1",
                overtime = 0,
                date = Convert.ToDateTime("05/05/2022")
            };
            repo.Setup(t => t.Delete(forDeletion));
            repo.Setup(t => t.GetById(1)).Returns(forDeletion);
            TimeSheetService timeSheetService = new TimeSheetService(repo.Object);
            //Act
            timeSheetService.DeleteOne(forDeletion);
            //Assert
            repo.Verify(f => f.Delete(forDeletion), Times.Once);
        }


        [Fact]
        public void UpdateOne_Returns_TimeSheetDTOObject()
        {
            //Arrange
            var updatedTimeSheet =  new TimeSheetClass{
                sheetID = 1,
                description = "Description Update 1",
                time = 8,
                overtime = 0,
                Member = new Member{},
                Project = new Project{},
                Category = new Category{},
                Client = new Client{},
                date = Convert.ToDateTime("05/05/2022"),
            };
            //Act
            repo.Setup(t => t.Edit(updatedTimeSheet)).Returns(updatedTimeSheet);
            repo.Setup(t => t.GetById(1)).Returns(updatedTimeSheet);
            TimeSheetService timeSheetService = new TimeSheetService(repo.Object);
            var updated = timeSheetService.UpdateOne(updatedTimeSheet);
            //Assert
            repo.Verify(t => t.Edit(updatedTimeSheet), Times.Once);
            Assert.Equal(updatedTimeSheet.description, updated.description);
        }
    }
}