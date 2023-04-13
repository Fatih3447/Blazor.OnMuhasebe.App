using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.BankaHesaplar;

[Authorize(OnMuhasebePermissions.BankaHesap.Default)]
public class BankaHesapAppService : OnMuhasebeAppService, IBankaHesapAppService
{
    private readonly IBankaHesapRepository _bankaHesapRepository;
    private readonly BankaHesapManager _bankaHesapManager;

    public BankaHesapAppService(IBankaHesapRepository bankaHesapRepository,
        BankaHesapManager bankaHesapManager)
    {
        _bankaHesapRepository = bankaHesapRepository;
        _bankaHesapManager = bankaHesapManager;
    }

    public virtual async Task<SelectBankaHesapDto> GetAsync(Guid id)
    {
        var entity = await _bankaHesapRepository.GetAsync(id, x => x.Id == id,
            x => x.BankaSube, x => x.BankaSube.Banka, x => x.OzelKod1, x => x.OzelKod2);

        var mappedDto = ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(entity);
        mappedDto.HesapTuruAdi = L[$"Enum:BankaHesapTuru:{(byte)mappedDto.HesapTuru}"];

        return mappedDto;
    }

    public virtual async Task<PagedResultDto<ListBankaHesapDto>> GetListAsync(
        BankaHesapListParameterDto input)
    {
        var entities = await _bankaHesapRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount, x => input.HesapTuru == null ? x.SubeId == input.SubeId &&
            x.Durum == input.Durum : x.HesapTuru == input.HesapTuru &&
            x.SubeId == input.SubeId && x.Durum == input.Durum,
            x => x.Kod,
            x => x.BankaSube, x => x.BankaSube.Banka, x => x.OzelKod1, x => x.OzelKod2,
            x => x.MakbuzHareketler);

        var totalCount = await _bankaHesapRepository.CountAsync(x => input.HesapTuru == null ?
              x.SubeId == input.SubeId &&
              x.Durum == input.Durum : x.HesapTuru == input.HesapTuru &&
              x.SubeId == input.SubeId && x.Durum == input.Durum);

        var mappedDtos = ObjectMapper.Map<List<BankaHesap>, List<ListBankaHesapDto>>(entities);

        mappedDtos.ForEach(x =>
        {
            x.HesapTuruAdi = L[$"Enum:BankaHesapTuru:{(byte)x.HesapTuru}"];
            x.Borc = x.MakbuzHareketler.Where(y => y.BelgeDurumu == BelgeDurumu.TahsilEdildi ||
                     y.OdemeTuru == OdemeTuru.Pos && y.BelgeDurumu == BelgeDurumu.Portfoyde)
                      .Sum(y => y.Tutar);

            x.Alacak = x.MakbuzHareketler.Where(y => y.BelgeDurumu == BelgeDurumu.Odendi)
                        .Sum(y => y.Tutar);
        });

        return new PagedResultDto<ListBankaHesapDto>(totalCount, mappedDtos);
    }

    [Authorize(OnMuhasebePermissions.BankaHesap.Create)]
    public virtual async Task<SelectBankaHesapDto> CreateAsync(CreateBankaHesapDto input)
    {
        await _bankaHesapManager.CheckCreateAsync(input.Kod, input.BankaSubeId,
            input.OzelKod1Id, input.OzelKod2Id, input.SubeId);

        var entity = ObjectMapper.Map<CreateBankaHesapDto, BankaHesap>(input);
        await _bankaHesapRepository.InsertAsync(entity);
        return ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.BankaHesap.Update)]
    public virtual async Task<SelectBankaHesapDto> UpdateAsync(Guid id,
        UpdateBankaHesapDto input)
    {
        var entity = await _bankaHesapRepository.GetAsync(id, x => x.Id == id);

        await _bankaHesapManager.CheckUpdateAsync(id, input.Kod, entity, input.BankaSubeId,
            input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _bankaHesapRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(mappedEntity);
    }

    [Authorize(OnMuhasebePermissions.BankaHesap.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _bankaHesapManager.CheckDeleteAsync(id);
        await _bankaHesapRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(BankaHesapCodeParameterDto input)
    {
        return await _bankaHesapRepository.GetCodeAsync(x => x.Kod,
            x => x.SubeId == input.SubeId && x.Durum == input.Durum);
    }
}