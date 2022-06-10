using TimeSheet.CustomExceptions;
using TimeSheet.DTO_Models;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services.Interfaces;


namespace TimeSheet.Services
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly ITimeSheetRepository _timeSheetRepository;


        public TimeSheetService(ITimeSheetRepository timeSheetRepository)
        {
            _timeSheetRepository = timeSheetRepository;
        }

        public IEnumerable<TimeSheetDTO> GetAll()
        {
            var retVal = _timeSheetRepository.GetAll();

            if (retVal.Count() == 0)
            {
                throw new EmptyListException("No TimeSheets exist in database.");
            }

            return _timeSheetRepository.GetAll().Select(item => TimeSheetDTO.ToTimeSheetDTO(item));
        }

        public TimeSheetClass GetOne(int id)
        {
            var retVal = _timeSheetRepository.GetById(id);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"TimeSheet with id: {id} wasn't found.");
            }

            return retVal;
        }

        public void DeleteOne(TimeSheetClass sheet)
        {
            var retVal = _timeSheetRepository.GetById(sheet.sheetID);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"TimeSheet with id: {sheet.sheetID} wasn't found.");
            }

            _timeSheetRepository.Delete(sheet);
        }

        public TimeSheetClass UpdateOne(TimeSheetClass obj)
        {
            //Refactor so it updates from DTO to database
            //And returns DTO
            var retVal = _timeSheetRepository.GetById(obj.sheetID);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"TimeSheet with id: {obj.sheetID} wasn't found.");
            }

            return _timeSheetRepository.Edit(obj);
        }

        public TimeSheetClass Save(TimeSheetClass obj)
        {

            if (String.IsNullOrEmpty(obj.description))
            {
                throw new InvalidObjectParamsException("TimeSheet description is required.");
            }
            
            return _timeSheetRepository.Save(obj);
        }


    }
}