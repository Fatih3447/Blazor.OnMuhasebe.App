using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.BankaSubeler;

public class EfCoreBankaSubeRepository : EfCoreCommonRepository<BankaSube>, IBankaSubeRepository
{
    public EfCoreBankaSubeRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}