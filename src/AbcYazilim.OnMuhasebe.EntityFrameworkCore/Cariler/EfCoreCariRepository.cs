using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.Cariler;

public class EfCoreCariRepository : EfCoreCommonRepository<Cari>, ICariRepository
{
    public EfCoreCariRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}