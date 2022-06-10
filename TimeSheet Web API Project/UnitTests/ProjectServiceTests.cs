using Moq;
using TimeSheet.DTO_models;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services;
using TimeSheet.CustomExceptions;

namespace UnitTests
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectRepository> repo = new Mock<IProjectRepository>();

        [Fact]
        public void GetAll_ReturnsAllProjects()
        {
            //Arrange
            var projects = new List<Project>();

            Project projectOne = new Project
            {
                projectID = 1,
                projectName = "Project One",
                projectDescription = "Project One description",
                currentclientID = 1,
                currentClient = null,
                memberID = 1,
                Member = null
            };
            Project projectTwo = new Project
            {
                projectID = 2,
                projectName = "Project Two",
                projectDescription = "Project Two description",
                currentclientID = 1,
                currentClient = null,
                memberID = 1,
                Member = null
            };
            projects.Add(projectOne);
            projects.Add(projectTwo);
            repo.Setup(p => p.GetAll()).Returns(projects);
            //Act
            ProjectService projectService = new ProjectService(repo.Object);
            IEnumerable<ProjectDTO> projectList = projectService.GetAll();
            //Assert
            Assert.NotEmpty(projectList);
        }

        [Fact]
        public void GetOne_ExistingIdPassed_ReturnsCorrectProject()
        {
            //Arrange
            Project projectOne = new Project
            {
                projectID = 1,
                projectName = "Project One",
                projectDescription = "Project One description",
                currentclientID = 1,
                currentClient = null,
                memberID = 1,
                Member = null
            };
            repo.Setup(p => p.GetById(1)).Returns(projectOne);
            ProjectService projectService = new ProjectService(repo.Object);
            //Act
            var project = projectService.GetOne(1);
            //Assert
            Assert.Equal(projectOne.projectName, project.projectName);
        }

        [Fact]
        public void GetOne_UnknownIdPassed_ReturnsKeyNotFoundException()
        {
            //Arrange
            Project projectOne = new Project
            {
                projectID = 1,
                projectName = "Project One",
                projectDescription = "Project One description",
                currentclientID = 1,
                currentClient = null,
                memberID = 1,
                Member = null
            };
            repo.Setup(p => p.GetById(1)).Returns(projectOne);
            ProjectService projectService = new ProjectService(repo.Object);
            //Act
            Action act = () => projectService.GetOne(2);
            //Assert
            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(act);
            Assert.Equal("Project with id: 2 wasn't found.", exception.Message);
        }

        [Fact]
        public void Save_Project_ReturnsProjectObject()
        {
            //Arrange
            Project projectOne = new Project
            {
                projectID = 1,
                projectName = "Project One",
                projectDescription = "Project One description",
                currentclientID = 1,
                currentClient = null,
                memberID = 1,
                Member = null
            };
            repo.Setup(p => p.Save(projectOne)).Returns(projectOne);
            ProjectService projectService = new ProjectService(repo.Object);
            //Act
            var project = projectService.Save(projectOne);
            //Assert
            Assert.Equal(projectOne.projectID, project.projectID);
        }

        [Fact]
        public void Save_InvalidObject_ReturnsInvalidObjectParamsException()
        {
            //Arrange
            Project projectOne = new Project
            {
                projectID = 1,
                projectDescription = "Project One description",
                currentclientID = 1,
                currentClient = null,
                memberID = 1,
                Member = null
            };
            repo.Setup(p => p.Save(projectOne)).Returns(projectOne);
            ProjectService projectService = new ProjectService(repo.Object);
            //Act
            Action act = () => projectService.Save(projectOne);
            //Assert
            InvalidObjectParamsException exc = Assert.Throws<InvalidObjectParamsException>(act);
            Assert.Equal("Project name cannot be empty.",exc.Message);
        }

        [Fact]
        public void UpdateOne_ObjectExists_ReturnsObject()
        {
            //Arrange
            Project updatedProject = new Project
            {
                projectID = 1,
                projectDescription = "Project One description",
                currentclientID = 1,
                currentClient = null,
                memberID = 1,
                Member = null
            };
            repo.Setup(p => p.Edit(updatedProject)).Returns(updatedProject);
            repo.Setup(p => p.GetById(1)).Returns(updatedProject);
            ProjectService projectService = new ProjectService(repo.Object);
            //Act   
            var project = projectService.UpdateOne(updatedProject);
            //Assert
            Assert.Equal(updatedProject.projectName, project.projectName);
        }

        [Fact]
        public void DeleteOne_ExistingObject_CallsRepository()
        {
            //Arrange
            Project forDeletion = new Project
            {
                projectID = 1,
                projectDescription = "Project One description",
                currentclientID = 1,
                currentClient = null,
                memberID = 1,
                Member = null
            };
            repo.Setup(p => p.Delete(forDeletion));
            repo.Setup(p => p.GetById(1)).Returns(forDeletion);
            ProjectService projectService = new ProjectService(repo.Object);
            //Act
            projectService.DeleteOne(forDeletion);
            //Assert
            repo.Verify(f => f.Delete(forDeletion), Times.Once);
        }
    }
}