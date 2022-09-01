using AthenicConsulting.Core.Entity;

namespace AthenicConsulting.Core.Core.Interfaces.Services
{
    public interface ILeadService
    {
        Task<List<Lead>> GetLeads();
    }
}
