using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace AbcYazilim.OnMuhasebe.OzelKodlar;

public class OzelKodManager : DomainService
{
    private readonly IOzelKodRepository _ozelKodRepository;

    public OzelKodManager(IOzelKodRepository ozelKodRepository)
    {
        _ozelKodRepository = ozelKodRepository;
    }

    public async Task CheckCreateAsync(string kod, OzelKodTuru? kodTuru, KartTuru? kartTuru)
    {
        await _ozelKodRepository.KodAnyAsync(kod, x => x.Kod == kod && x.KodTuru == kodTuru &&
                                                       x.KartTuru == kartTuru);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, OzelKod entity)
    {
        await _ozelKodRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod &&
         x.KodTuru == entity.KodTuru && x.KartTuru == entity.KartTuru,
         entity.Kod != kod);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        await _ozelKodRepository.RelationalEntityAnyAsync(
            x => x.OzelKod1Bankalar.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Bankalar.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1BankaSubeler.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2BankaSubeler.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1BankaHesaplar.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2BankaHesaplar.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1Birimler.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Birimler.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1Cariler.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Cariler.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1Depolar.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Depolar.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1Faturalar.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Faturalar.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1Hizmetler.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Hizmetler.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1Kasalar.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Kasalar.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1Makbuzlar.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Makbuzlar.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1Masraflar.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Masraflar.Any(y => y.OzelKod2Id == id) ||
                 x.OzelKod1Stoklar.Any(y => y.OzelKod1Id == id) ||
                 x.OzelKod2Stoklar.Any(y => y.OzelKod2Id == id));
    }
}