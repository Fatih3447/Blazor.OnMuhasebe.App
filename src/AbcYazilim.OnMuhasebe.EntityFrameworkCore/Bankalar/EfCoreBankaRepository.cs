using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.Bankalar;

public class EfCoreBankaRepository : EfCoreCommonRepository<Banka>, IBankaRepository
{
    public EfCoreBankaRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}