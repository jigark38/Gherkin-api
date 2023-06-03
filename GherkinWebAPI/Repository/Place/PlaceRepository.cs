using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.Models;
using GherkinWebAPI.Persistence;
using GherkinWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public class PlaceRepository : RepositoryBase<Place>, IPlaceRepository
    {
        private RepositoryContext _context;
        public PlaceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public async Task<Place> CreatePlace(Place place)
        {
            var placeDetail = _context.Places.Add(place);
            var result = await _context.SaveChangesAsync();

            if (result == 1)
            {
                return placeDetail;
            }

            return new Place();
        }

        public async Task DeletePlace(int placeCode)
        {
            Place place = await _context.Places.AsNoTracking().FirstOrDefaultAsync(x => x.PlaceCode == placeCode);
            if (place != null)
            {
                _context.Places.Remove(place);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomException("Place doesn't exist to delete");
            }
        }

        public async Task<List<Place>> GetAllPlaces()
        {
            return await _context.Places.ToListAsync();
        }

        public async Task UpdatePlace(int placeCode, Place place)
        {
            Place Place = await _context.Places.AsNoTracking().FirstOrDefaultAsync(x => x.PlaceCode == placeCode);
            if (Place != null)
            {
                Place.PlaceName = place.PlaceName.ToUpper();
                Place.StateCode = place.StateCode;
                Place.CountryCode = place.CountryCode;
                Place.DistrictCode = place.DistrictCode;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomException("Place doesn't exist");
            }
        }

        public async Task<List<Place>> GetPlacesByStateCode(int stateCode)
        {
            var placeDetails = await (from places in _context.Places.AsNoTracking()
                                      where places.StateCode == stateCode
                                      select places
                                                       ).ToListAsync();
            return placeDetails;
        }

        public async Task<List<Place>> GetPlacesByDistrictCode(int distCode)
        {
            return await _context.Places.AsNoTracking().Where(x => x.DistrictCode == distCode).Select(x => x).ToListAsync();
        }
    }
}
