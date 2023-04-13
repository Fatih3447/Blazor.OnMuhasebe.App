using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.OdemeBelgeleri;

public class EfCoreOdemeBelgesiRepository : EfCoreCommonRepository<OdemeBelgesi>, IOdemeBelgesiRepository
{
    public EfCoreOdemeBelgesiRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {        
    }
}