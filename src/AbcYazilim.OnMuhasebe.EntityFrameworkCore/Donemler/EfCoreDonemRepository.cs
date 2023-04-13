using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.Donemler;

public class EfCoreDonemRepository : EfCoreCommonRepository<Donem>, IDonemRepository
{
    public EfCoreDonemRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}