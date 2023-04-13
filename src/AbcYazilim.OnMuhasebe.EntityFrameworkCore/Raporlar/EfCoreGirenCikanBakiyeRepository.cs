using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.Raporlar;

public class EfCoreGirenCikanBakiyeRepository : EfCoreCommonNoKeyRepository<GirenCikanBakiye>,
    IGirenCikanBakiyeRepository
{
    public EfCoreGirenCikanBakiyeRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}