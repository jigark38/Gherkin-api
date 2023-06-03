namespace GherkinWebAPI.Repository
{
    using AutoMapper;
    using GherkinWebAPI.Core;
    using GherkinWebAPI.DTO;
    using GherkinWebAPI.Models;
    using GherkinWebAPI.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
	using System.Runtime.CompilerServices;
	using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CropRepository" />
    /// </summary>
    public class CropRepository : RepositoryBase<CropGroup>, ICropRepository
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private readonly RepositoryContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CropRepository"/> class.
        /// </summary>
        /// <param name="repositoryContext">The repositoryContext<see cref="RepositoryContext"/></param>
        public CropRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        /// <summary>
        /// The AddCrop
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <returns>The <see cref="Task{Crop}"/></returns>
        public async Task<Crop> AddCrop(Crop crop)
        {
            if( _context.Crops.Any())
            {
                var maxId = _context.Crops.OrderByDescending(c => c.CropId).Take(1).FirstOrDefault().CropId;
                crop.CropCode = $"CNC_{maxId + 1}";
            }
            else
            {
                crop.CropCode = $"CNC_{1}";
            }
           
            var result = _context.Crops.Add(crop);
            await _context.SaveChangesAsync();
            return  result;
        }

        /// <summary>
        /// The AddCropAndScheme
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <param name="scheme">The scheme<see cref="CropScheme"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddCropAndScheme(Crop crop, CropScheme scheme)
        {
            var result = _context.Crops.Add(crop);
            scheme.CropCode = result.CropCode;
            _context.CropSchemes.Add(scheme);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// The AddCropGroup
        /// </summary>
        /// <param name="group">The group<see cref="CropGroup"/></param>
        /// <returns>The <see cref="Task{CropGroup}"/></returns>
        public async Task<CropGroup> AddCropGroup(CropGroup group)
        {
            //_context.Database.Log = message => Debug.WriteLine(message);

            if (_context.CropGroups.Any())
            {
                var maxId = _context.CropGroups.OrderByDescending(c => c.CropGroupId).Take(1).FirstOrDefault().CropGroupId;
                group.CropGroupCode = $"CGC_{maxId + 1}";
            }
            else
            {
                group.CropGroupCode = $"CGC_{1}";
            }

            var result = _context.CropGroups.Add(group);
            await  _context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// The AddCropScheme
        /// </summary>
        /// <param name="cropScheme">The cropScheme<see cref="List{CropScheme}"/></param>
        /// <returns>The <see cref="Task{IEnumerable{CropScheme}}"/></returns>
        public async Task<IEnumerable<CropScheme>> AddCropScheme(List<CropScheme> cropScheme)
        {
            if (_context.CropSchemes.Any())
            {
                var maxId = _context.CropSchemes.OrderByDescending(c => c.CropSchemeId).Take(1).FirstOrDefault().CropSchemeId;

                cropScheme.ForEach(crop =>
                {
                    maxId++;
                    crop.Code = $"CSC_{maxId}";
                });
            }
            else
            {
                var maxId = 0;
                cropScheme.ForEach(crop =>
                {
                    maxId++;
                    crop.Code = $"CSC_{maxId}";
                });
                
            }

            var result = _context.CropSchemes.AddRange(cropScheme);
            await _context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// The GetAllCropGroup
        /// </summary>
        /// <returns>The <see cref="Task{List{CropGroup}}"/></returns>
        public async Task<List<CropGroup>> GetAllCropGroup()
        {
            return await FindAll().ToListAsync();
        }

        /// <summary>
        /// The GetAllCrops
        /// </summary>
        /// <returns>The <see cref="Task{List{Crop}}"/></returns>
        public async Task<List<Crop>> GetAllCrops()
        {
            return await _context.Crops.ToListAsync();
        }

        public async Task<List<SchemeDto>> GetCropSchemes(string cropCode)
        {
            var query = from scheme in _context.CropSchemes
                        select scheme;
            if(!string.IsNullOrEmpty(cropCode))
            {
                query = query.Where(i => i.CropCode == cropCode);
            }

            var res = await query.ToListAsync();

            return  (Mapper.Map<List<SchemeDto>>(res));
        }

        /// <summary>
        /// The SearchCrop
        /// </summary>
        /// <param name="groupCode">The groupCode<see cref="string"/></param>
        /// <param name="cropName">The cropName<see cref="string"/></param>
        /// <returns>The <see cref="Task{CropSchemeDto}"/></returns>
        public async Task<CropSchemeDto> SearchCrop(string groupCode, string cropName)
        {
            var crop = (await (from c in _context.Crops
                              join grp in _context.CropGroups on c.CropGroupCode equals grp.CropGroupCode
                              where c.Name == cropName && c.CropGroupCode == groupCode
                              select new CropSchemeDto()
                              {
                                  CropName = c.Name,
                                  GroupCode = grp.CropGroupCode,
                                  CropCode = c.CropCode,
                                  EntryDate = grp.EntryDate,
                                  UserName = grp.UserId
                              }).ToListAsync()).FirstOrDefault();
            if (crop != null)
            {
                var cropSchemes = _context.CropSchemes.Where(i => i.CropCode == crop.CropCode);
                crop.Schemes = Mapper.Map<List<SchemeDto>>(cropSchemes);

                return crop;
            }

            return null;
        }

        /// <summary>
        /// The UpDateCrop
        /// </summary>
        /// <param name="crop">The crop<see cref="Crop"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpDateCrop(Crop crop)
        {
            var existingCrop = await _context.Crops.FirstOrDefaultAsync(i => i.CropGroupCode == crop.CropGroupCode && i.CropCode == crop.CropCode);
            if (existingCrop == null)
            {
                throw new KeyNotFoundException($"No crop found for the crop code {crop.CropCode} and crop group code {crop.CropGroupCode}");
            }

            existingCrop.Name = crop.Name;
            existingCrop.CropGroupCode = crop.CropGroupCode;
            _context.Entry(existingCrop).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// The UpdateCropScheme
        /// </summary>
        /// <param name="cropSchemes">The cropSchemes<see cref="List{CropScheme}"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpdateCropScheme(List<CropScheme> cropSchemes)
        {
            cropSchemes.ForEach(async scheme =>
            {
                var cropScheme = await _context.CropSchemes.FirstOrDefaultAsync(i => i.CropCode == scheme.CropCode && i.Code == scheme.Code);
                if (cropScheme != null)
                {
                    cropScheme.Count = scheme.Count;
                    cropScheme.From = scheme.From;
                    cropScheme.Sign = scheme.Sign;

                    _context.Entry(cropScheme).State = EntityState.Modified;
                }
            });

            await _context.SaveChangesAsync();
        }

        public async Task<List<Crop>> GetCropListByCropGroupCode(string cropGroupCode)
        {
            var cropList = await _context.Crops.Where(x => x.CropGroupCode.StartsWith(cropGroupCode)).ToListAsync();
            return cropList;
        }
    }
}
