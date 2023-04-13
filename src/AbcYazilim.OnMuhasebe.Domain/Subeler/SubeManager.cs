using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace AbcYazilim.OnMuhasebe.Subeler;

public class SubeManager : DomainService
{
    private readonly ISubeRepository _subeRepository;

    public SubeManager(ISubeRepository subeRepository)
    {
        _subeRepository = subeRepository;
    }

    public async Task CheckCreateAsync(string kod)
    {
        await _subeRepository.KodAnyAsync(kod, x => x.Kod == kod);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, Sube entity)
    {
        await _subeRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        await _subeRepository.RelationalEntityAnyAsync(
            x => x.BankaHesaplar.Any(y => y.SubeId == id) ||
                 x.Depolar.Any(y => y.SubeId == id) ||
                 x.Faturalar.Any(y => y.SubeId == id) ||
                 x.Kasalar.Any(y => y.SubeId == id) ||
                 x.Makbuzlar.Any(y => y.SubeId == id) ||
                 x.FirmaParemetreler.Any(y => y.SubeId == id));
    }
}