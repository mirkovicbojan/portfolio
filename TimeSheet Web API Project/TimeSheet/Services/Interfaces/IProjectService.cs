using TimeSheet.DTO_models;
using TimeSheet.Models;

namespace TimeSheet.Services.Interfaces
{
    public interface IProjectService
    {
        public IEnumerable<ProjectDTO> GetAll();

        public Project GetOne(int id);

        public void DeleteOne(Project obj);

        public Project UpdateOne(Project obj);

        public Project Save(Project obj);
    }
}