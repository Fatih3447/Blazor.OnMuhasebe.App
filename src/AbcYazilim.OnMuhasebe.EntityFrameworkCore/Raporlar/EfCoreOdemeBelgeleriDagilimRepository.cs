using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.Raporlar;

public class EfCoreOdemeBelgeleriDagilimRepository : EfCoreCommonNoKeyRepository<OdemeBelgeleriDagilim>,
    IOdemeBelgeleriDagilimRepository
{
    public EfCoreOdemeBelgeleriDagilimRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}