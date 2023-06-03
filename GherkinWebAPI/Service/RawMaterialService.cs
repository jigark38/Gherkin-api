using GherkinWebAPI.Core;
using GherkinWebAPI.CustomExceptions;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Models;
using GherkinWebAPI.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service
{
    public class RawMaterialService : IRawMaterialService
    {
        public IRawMaterialRepository _repository { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public RawMaterialService(IRawMaterialRepository repository)
        {
            this._repository = repository;
        }

        public Task<List<RawMaterialMasterDto>> GetRawmaterialMaster()
        {
            return _repository.GetRawMaterialMaster();
        }

        public Task CreateRawMaterialMaster(RawMaterialMaster rawMaterialGroupMaster)
        {
            return _repository.CreateRawMaterialGroup(rawMaterialGroupMaster);
        }
        public Task UpdateRawMaterialMaster(RawMaterialMaster rawMaterialGroupMaster)
        {
            return _repository.UpdateRawMaterialGroup(rawMaterialGroupMaster);
        }
        public Task<RawMaterialDetailsDto> CreateRawMaterialDetails(RawMaterialDetails rawMaterialDetails)
        {
            return _repository.CreateRawMaterialDetails(rawMaterialDetails);
        }

        public Task<List<RawMaterialDetailsDto>> GetRawmaterialDetails()
        {
            return _repository.GetRawmaterialDetails();
        }

        public Task<RawMaterialDetailsDto> UpdateRawMaterialDetails(string id, RawMaterialDetailsRequest rawMaterialDetailsReq)
        {
            return _repository.UpdateRawMaterialDetails(id, rawMaterialDetailsReq);
        }

        public async Task<List<RawMaterialDetailsDto>> GetRMDeatilsCodeNameByGroupCode(string rawMaterialGroupCode)
        {
            return await _repository.GetRMDeatilsCodeNameByGroupCode(rawMaterialGroupCode);
        }

        public async Task<RMUom> CreateRawMaterialUOM(RMUom rMUomDto)
        {
            return await _repository.CreateRawMaterialUOM(rMUomDto);
        }

        public async Task<List<RMUom>> GetAllUom()
        {
            return await _repository.GetAllUom();
        }
    }
}
