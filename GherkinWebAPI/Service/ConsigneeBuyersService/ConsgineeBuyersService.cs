using GherkinWebAPI.Core;
using GherkinWebAPI.DTO;
using GherkinWebAPI.Models;
using GherkinWebAPI.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GherkinWebAPI.Service
{
    /// <summary>
    /// Defines the <see cref="ConsgineeBuyersService" />
    /// </summary>
    public class ConsgineeBuyersService : IConsgineeBuyersService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public IConsgineeBuyersRepository _repository { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ConsgineeBuyersService"/> class.
        /// </summary>
        /// <param name="ConsgineeBuyersRepository">The ConsgineeBuyersRepository<see cref="IConsgineeBuyersRepository"/></param>
        public ConsgineeBuyersService(IRepositoryWrapper repositoryWrapper, IConsgineeBuyersRepository repository)
        {
            _repositoryWrapper = repositoryWrapper;
            _repository = repository;
        }


        public Task<List<ConsgineeBuyersDto>> GetAllConsgineeBuyersDetails()
        {
            return _repositoryWrapper.ConsgineeBuyersRepository.GetAllConsgineeBuyersDetails();

        }
        public Task<ConsgineeBuyersDto> GetConsgineeBuyersDetailsId(string consgType,string CBCode)
        {
            return _repository.GetConsgineeBuyersDetailsId(consgType,CBCode);
        }

        public async Task<ConsigneeBuyersDetails> AddConsgineeBuyersDetails(ConsigneeBuyersDetails consigneeBuyersDetails)
        {
            consigneeBuyersDetails.Created_Date = DateTime.UtcNow;
            consigneeBuyersDetails.Modified_Date = DateTime.UtcNow;
            return await _repositoryWrapper.ConsgineeBuyersRepository.AddConsgineeBuyersDetails(consigneeBuyersDetails);
            //throw new NotImplementedException();
        }


        public async Task<DocumentUpload> AddDocumentDetails(DocumentUpload document)
        {
            return await _repositoryWrapper.ConsgineeBuyersRepository.AddDocumentDetails(document);
        }

       
        public Task<ConsgineeBuyersDto> UpdateConsgineeDetails(string id, ConsgineeBuyersDto consigneeBuyersDetails)
        {
            return _repository.UpdateConsgineeDetails(id, consigneeBuyersDetails);
        }

        public Task<List<ConsgineeBuyersDto>> GetConsgineeNameByConsType(string consgType)
        {
            return _repository.GetConsgineeNameByConsType(consgType);
        }
        
    }
}