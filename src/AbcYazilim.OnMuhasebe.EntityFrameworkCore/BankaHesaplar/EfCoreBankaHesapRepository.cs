using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.BankaHesaplar;

public class EfCoreBankaHesapRepository : EfCoreCommonRepository<BankaHesap>, IBankaHesapRepository
{
    public EfCoreBankaHesapRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}