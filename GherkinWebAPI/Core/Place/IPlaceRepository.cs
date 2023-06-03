using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IPlaceRepository
    {
        Task<Place> CreatePlace(Place place);

        Task<List<Place>> GetAllPlaces();

        Task UpdatePlace(int placeCode, Place place);

        Task DeletePlace(int placeCode);

        Task<List<Place>> GetPlacesByStateCode(int stateCode);

        Task<List<Place>> GetPlacesByDistrictCode(int distCode);

    }
}
