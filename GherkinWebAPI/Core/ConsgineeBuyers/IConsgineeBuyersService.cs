using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IConsgineeBuyersService
    {
        /// <summary>
        /// The GetAllConsgineeBuyersDetails
        /// </summary>
        /// <returns>The <see cref="Task{List{ConsigneeBuyersDetails}}"/></returns>
        Task<List<ConsgineeBuyersDto>> GetAllConsgineeBuyersDetails();
        Task<ConsgineeBuyersDto> GetConsgineeBuyersDetailsId(string consgType,string CBCode);
        Task<ConsigneeBuyersDetails> AddConsgineeBuyersDetails(ConsigneeBuyersDetails consigneeBuyers);

        Task<DocumentUpload>  AddDocumentDetails(DocumentUpload document);
        Task<ConsgineeBuyersDto> UpdateConsgineeDetails(string id, ConsgineeBuyersDto consigneeBuyers);
        Task<List<ConsgineeBuyersDto>> GetConsgineeNameByConsType(string consgType);
    }
}
