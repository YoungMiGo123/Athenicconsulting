using AthenicConsulting.Core.Core.Interfaces;
using AthenicConsulting.Core.Core.Interfaces.Services;
using AthenicConsulting.Core.Entity;

namespace AthenicConsulting.Core.Core.Models.Services
{
    public class LeadService : ILeadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Lead>> GetLeads()
        {
            var leads = await _unitOfWork.LeadRepository.GetAllAsync<Lead>();
            return leads.ToList();
        }
    }
}
