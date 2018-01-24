using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.DataContracts.Requests;
using fullPlate.DataContracts.Responses;

namespace fullPlate.Services.Interfaces
{
    public interface ILunchService
    {
        List<LunchResponse> GetAllLunches();
        LunchResponse GetOneLunch(int lunchId);
        LunchResponse CreateNewLuch(LunchDataRequest newLunchRequest);
        bool RemoveLunch(int lunchId);
        bool EnableLunch(int lunchId);
        List<AvailableDishResponse> GetAllDishes();
        List<LunchResponse> GetAvailableLunches(string userId);
    }
}
