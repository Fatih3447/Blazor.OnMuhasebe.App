using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace AbcYazilim.OnMuhasebe.Bankalar;

public class BankaManager : DomainService
{
    private readonly IBankaRepository _bankaRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public BankaManager(IBankaRepository bankaRepository, IOzelKodRepository ozelKodRepository)
    {
        _bankaRepository = bankaRepository;
        _ozelKodRepository = ozelKodRepository;
    }

    public async Task CheckCreateAsync(string kod, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _bankaRepository.KodAnyAsync(kod, x => x.Kod == kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Banka);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Banka);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, Banka entity,
        Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _bankaRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Banka, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Banka, entity.OzelKod2Id != ozelKod2Id);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        await _bankaRepository.RelationalEntityAnyAsync(
            x => x.BankaSubeler.Any(y => y.BankaId == id) ||
                 x.MakbuzHareketler.Any(y => y.CekBankaId == id));
    }
}