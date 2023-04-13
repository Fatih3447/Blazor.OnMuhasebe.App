using AbcYazilim.OnMuhasebe.Commons;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.MakbuzHareketler;

public class EfCoreMakbuzHareketRepository : EfCoreCommonRepository<MakbuzHareket>, IMakbuzHareketRepository
{
    public EfCoreMakbuzHareketRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}