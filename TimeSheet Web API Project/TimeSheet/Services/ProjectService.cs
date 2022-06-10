using TimeSheet.CustomExceptions;
using TimeSheet.DTO_models;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services.Interfaces;

namespace TimeSheet.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;

        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            var retVal = _projectRepository.GetAll().Select(item => ProjectDTO.ToProjectDTO(item));

            if (retVal.Count() == 0)
            {
                throw new EmptyListException("No Projects were found in database.");
            }

            return retVal;
        }

        public Project GetOne(int id)
        {
            var retVal = _projectRepository.GetById(id);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Project with id: {id} wasn't found.");
            }

            return retVal;
        }

        public void DeleteOne(Project obj)
        {
            var retVal = _projectRepository.GetById(obj.projectID);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Project with id: {obj.projectID} wasn't found.");
            }

            _projectRepository.Delete(obj);
        }

        public Project UpdateOne(Project obj)
        {
            var retVal = _projectRepository.GetById(obj.projectID);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Project with id: {obj.projectID} wasn't found.");
            }

            return _projectRepository.Edit(obj);
        }

        public Project Save(Project obj)
        {
            
            if (string.IsNullOrEmpty(obj.projectName))
            {
                throw new InvalidObjectParamsException("Project name cannot be empty.");
            }

            return _projectRepository.Save(obj);
        }
    }
}