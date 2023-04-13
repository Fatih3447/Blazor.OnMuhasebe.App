using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using AbcYazilim.OnMuhasebe.Faturalar;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.FaturaHareketler;

public class EfCoreFaturaHareketRepository : EfCoreCommonRepository<FaturaHareket>, 
    IFaturaHareketRepository
{
    public EfCoreFaturaHareketRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}