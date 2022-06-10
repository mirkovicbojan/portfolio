using TimeSheet.DTO_Models;
using TimeSheet.Models;

namespace TimeSheet.Services.Interfaces
{
    public interface ITimeSheetService
    {
        public IEnumerable<TimeSheetDTO> GetAll();

        public TimeSheetClass GetOne(int id);

        public void DeleteOne(TimeSheetClass obj);

        public TimeSheetClass UpdateOne(TimeSheetClass obj);

        public TimeSheetClass Save(TimeSheetClass obj);

    }
}