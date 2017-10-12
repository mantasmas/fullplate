using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.DataContracts.Responses;

namespace fullPlate.Services.Interfaces
{
    public interface IRestaurantsService
    {
        List<RestaurantResponse>GetAll();
    }
}
