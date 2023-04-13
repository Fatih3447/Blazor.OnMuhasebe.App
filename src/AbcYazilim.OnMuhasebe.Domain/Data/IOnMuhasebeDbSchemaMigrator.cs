using System.Threading.Tasks;

namespace AbcYazilim.OnMuhasebe.Data
{
    public interface IOnMuhasebeDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
