using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace AbcYazilim.OnMuhasebe.Makbuzlar;

public class MakbuzHareketManager : DomainService
{
    private readonly IBankaRepository _bankaRepository;
    private readonly IBankaSubeRepository _bankaSubeRepository;
    private readonly IKasaRepository _kasaRepository;
    private readonly IBankaHesapRepository _bankaHesapRepository;

    public MakbuzHareketManager(IBankaRepository bankaRepository, 
        IBankaSubeRepository bankaSubeRepository, IKasaRepository kasaRepository, 
        IBankaHesapRepository bankaHesapRepository)
    {
        _bankaRepository = bankaRepository;
        _bankaSubeRepository = bankaSubeRepository;
        _kasaRepository = kasaRepository;
        _bankaHesapRepository = bankaHesapRepository;
    }

    public async Task CheckCreateAsync(Guid? cekBankaId, Guid? cekBankaSubeId, Guid? kasaId,
        Guid? bankaHesapId)
    {
        await _bankaRepository.EntityAnyAsync(cekBankaId, x => x.Id == cekBankaId);
        await _bankaSubeRepository.EntityAnyAsync(cekBankaSubeId, x => x.Id == cekBankaSubeId);
        await _kasaRepository.EntityAnyAsync(kasaId, x => x.Id == kasaId);
        await _bankaHesapRepository.EntityAnyAsync(bankaHesapId, x => x.Id == bankaHesapId);
    }

    public async Task CheckUpdateAsync(Guid? cekBankaId, Guid? cekBankaSubeId, Guid? kasaId,
        Guid? bankaHesapId)
    {
        await _bankaRepository.EntityAnyAsync(cekBankaId, x => x.Id == cekBankaId);
        await _bankaSubeRepository.EntityAnyAsync(cekBankaSubeId, x => x.Id == cekBankaSubeId);
        await _kasaRepository.EntityAnyAsync(kasaId, x => x.Id == kasaId);
        await _bankaHesapRepository.EntityAnyAsync(bankaHesapId, x => x.Id == bankaHesapId);
    }
}