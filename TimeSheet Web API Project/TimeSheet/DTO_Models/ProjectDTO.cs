using TimeSheet.Models;

namespace TimeSheet.DTO_models
{
    public class ProjectDTO
    {
        public int? projectID { get; set; }
        public string? projectName { get; set; }
        public string? projectDescription { get; set; }

        public int? currentclientID { get; set; }
        public int? memberID { get; set; }

        public static ProjectDTO ToProjectDTO(Project project)
        {
            return new ProjectDTO()
            {
                projectID = project.projectID,
                projectName = project.projectName,
                projectDescription = project.projectDescription,
                currentclientID = project.currentclientID,
                memberID = project.memberID
            };
        }
    }
}