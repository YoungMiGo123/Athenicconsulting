using AthenicConsulting.Core;

namespace AthenicConsulting.Seeder.Seeder.Interfaces
{
    public interface ISeederService
    {
        public bool SeedBrands(string path);
        public Task<bool> SeedCampaigns();
    }
}
