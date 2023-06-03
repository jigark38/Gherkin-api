using GherkinWebAPI.DTO;
using GherkinWebAPI.Entities;
using GherkinWebAPI.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IRawMaterialRepository
    {
        Task<List<RawMaterialMasterDto>> GetRawMaterialMaster();

        Task<List<RawMaterialDetailsDto>> GetRawmaterialDetails();

        Task CreateRawMaterialGroup(RawMaterialMaster rawMaterialGroupMaster);
        Task UpdateRawMaterialGroup(RawMaterialMaster rawMaterialGroupMaster);

        Task<RawMaterialDetailsDto> CreateRawMaterialDetails(RawMaterialDetails rawMaterialDetails);

        Task<RawMaterialDetailsDto> UpdateRawMaterialDetails(string id, RawMaterialDetailsRequest rawMaterialDetailsReq);

        Task<List<RawMaterialDetailsDto>> GetRMDeatilsCodeNameByGroupCode(string rawMaterialGroupCode);
        Task<RMUom> CreateRawMaterialUOM(RMUom rMUomDto);
        Task<List<RMUom>> GetAllUom();
    }
}
