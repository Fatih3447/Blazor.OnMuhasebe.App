using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace AbcYazilim.OnMuhasebe.Depolar;

public class DepoManager : DomainService
{
    private readonly IDepoRepository _depoRepository;
    private readonly IOzelKodRepository _ozelKodRepository;
    private readonly ISubeRepository _subeRepository;

    public DepoManager(IDepoRepository depoRepository, IOzelKodRepository ozelKodRepository,
        ISubeRepository subeRepository)
    {
        _depoRepository = depoRepository;
        _ozelKodRepository = ozelKodRepository;
        _subeRepository = subeRepository;
    }

    public async Task CheckCreateAsync(string kod, Guid? ozelKod1Id, Guid? ozelKod2Id,
        Guid? subeId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _depoRepository.KodAnyAsync(kod, x => x.Kod == kod && x.SubeId == subeId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Depo);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Depo);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, Depo entity,
        Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _depoRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod &&
        x.SubeId == entity.SubeId, entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Depo, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Depo, entity.OzelKod2Id != ozelKod2Id);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        await _depoRepository.RelationalEntityAnyAsync(
            x => x.FaturaHareketler.Any(y => y.DepoId == id));
    }
}