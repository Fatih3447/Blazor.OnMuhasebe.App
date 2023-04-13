using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace AbcYazilim.OnMuhasebe.Hizmetler;

public class HizmetManager : DomainService
{
    private readonly IHizmetRepository _hizmetRepository;
    private readonly IBirimRepository _birimRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public HizmetManager(IHizmetRepository hizmetRepository, IBirimRepository birimRepository,
        IOzelKodRepository ozelKodRepository)
    {
        _hizmetRepository = hizmetRepository;
        _birimRepository = birimRepository;
        _ozelKodRepository = ozelKodRepository;
    }

    public async Task CheckCreateAsync(string kod, Guid? birimId, Guid? ozelKod1Id,
        Guid? ozelKod2Id)
    {
        await _hizmetRepository.KodAnyAsync(kod, x => x.Kod == kod);
        await _birimRepository.EntityAnyAsync(birimId, x => x.Id == birimId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Hizmet);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Hizmet);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, Hizmet entity,
        Guid? birimId, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _hizmetRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);

        await _birimRepository.EntityAnyAsync(birimId, x => x.Id == birimId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Hizmet, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Hizmet, entity.OzelKod2Id != ozelKod2Id);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        await _hizmetRepository.RelationalEntityAnyAsync(
            x => x.FaturaHareketler.Any(y => y.HizmetId == id));
    }
}