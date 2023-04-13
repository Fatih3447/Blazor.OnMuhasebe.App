using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.Makbuzlar;

[Authorize(OnMuhasebePermissions.Makbuz.Default)]
public class MakbuzAppService : OnMuhasebeAppService, IMakbuzAppService
{
    private readonly IMakbuzRepository _makbuzRepository;
    private readonly MakbuzManager _makbuzManager;
    private readonly MakbuzHareketManager _makbuzHareketManager;

    public MakbuzAppService(IMakbuzRepository makbuzRepository, MakbuzManager makbuzManager,
        MakbuzHareketManager makbuzHareketManager)
    {
        _makbuzRepository = makbuzRepository;
        _makbuzManager = makbuzManager;
        _makbuzHareketManager = makbuzHareketManager;
    }

    public virtual async Task<SelectMakbuzDto> GetAsync(Guid id)
    {
        var entity = await _makbuzRepository.GetAsync(id, x => x.Id == id);
        var mappedDto = ObjectMapper.Map<Makbuz, SelectMakbuzDto>(entity);

        mappedDto.MakbuzHareketler.ForEach(x =>
        {
            x.OdemeTuruAdi = L[$"Enum:OdemeTuru:{(byte)x.OdemeTuru}"];
            x.BelgeDurumuAdi = L[$"Enum:BelgeDurumu:{(byte)x.BelgeDurumu}"];
        });

        return mappedDto;
    }

    public virtual async Task<PagedResultDto<ListMakbuzDto>> GetListAsync(
        MakbuzListParameterDto input)
    {
        var entities = await _makbuzRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.MakbuzTuru == input.MakbuzTuru && x.SubeId == input.SubeId &&
                 x.DonemId == input.DonemId && x.Durum == input.Durum,
            x => x.MakbuzNo,
            x => x.Cari, x => x.Kasa, x => x.BankaHesap, x => x.OzelKod1, x => x.OzelKod2);

        var totalCount = await _makbuzRepository.CountAsync(
            x => x.MakbuzTuru == input.MakbuzTuru && x.SubeId == input.SubeId &&
                 x.DonemId == input.DonemId && x.Durum == input.Durum);

        return new PagedResultDto<ListMakbuzDto>(
            totalCount,
            ObjectMapper.Map<List<Makbuz>, List<ListMakbuzDto>>(entities)
            );
    }

    [Authorize(OnMuhasebePermissions.Makbuz.Create)]
    public virtual async Task<SelectMakbuzDto> CreateAsync(CreateMakbuzDto input)
    {
        await _makbuzManager.CheckCreateAsync(input.MakbuzNo, input.MakbuzTuru.Value,
            input.CariId, input.KasaId, input.BankaHesapId, input.OzelKod1Id, input.OzelKod2Id,
            input.SubeId, input.DonemId);

        foreach (var makbuzHareket in input.MakbuzHareketler)
        {
            await _makbuzHareketManager.CheckCreateAsync(makbuzHareket.CekBankaId,
                makbuzHareket.CekBankaSubeId, makbuzHareket.KasaId,
                makbuzHareket.BankaHesapId);
        }

        var entity = ObjectMapper.Map<CreateMakbuzDto, Makbuz>(input);
        await _makbuzRepository.InsertAsync(entity);
        return ObjectMapper.Map<Makbuz, SelectMakbuzDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Makbuz.Update)]
    public virtual async Task<SelectMakbuzDto> UpdateAsync(Guid id, UpdateMakbuzDto input)
    {
        var entity = await _makbuzRepository.GetAsync(id, x => x.Id == id,
            x => x.MakbuzHareketler);

        await _makbuzManager.CheckUpdateAsync(id, input.MakbuzNo, entity, input.CariId,
            input.KasaId, input.BankaHesapId, input.OzelKod1Id, input.OzelKod2Id);

        foreach (var makbuzHareketDto in input.MakbuzHareketler)
        {
            await _makbuzHareketManager.CheckUpdateAsync(makbuzHareketDto.CekBankaId,
                makbuzHareketDto.CekBankaSubeId, makbuzHareketDto.KasaId,
                makbuzHareketDto.BankaHesapId);

            var makbuzHareket = entity.MakbuzHareketler.FirstOrDefault(
                x => x.Id == makbuzHareketDto.Id);

            if (makbuzHareket == null)
            {
                entity.MakbuzHareketler.Add(ObjectMapper.Map<MakbuzHareketDto,
                    MakbuzHareket>(makbuzHareketDto));
                continue;
            }

            ObjectMapper.Map(makbuzHareketDto, makbuzHareket);
        }

        var deletedEntities = entity.MakbuzHareketler.Where(
            x => input.MakbuzHareketler.Select(y => y.Id).ToList().IndexOf(x.Id) == -1);

        entity.MakbuzHareketler.RemoveAll(deletedEntities);

        ObjectMapper.Map(input, entity);
        await _makbuzRepository.UpdateAsync(entity);
        return ObjectMapper.Map<Makbuz, SelectMakbuzDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Makbuz.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _makbuzRepository.GetAsync(id, x => x.Id == id,
            x => x.MakbuzHareketler);

        await _makbuzRepository.DeleteAsync(entity);
        entity.MakbuzHareketler.RemoveAll(entity.MakbuzHareketler);
    }

    public virtual async Task<string> GetCodeAsync(MakbuzNoParameterDto input)
    {
        return await _makbuzRepository.GetCodeAsync(x => x.MakbuzNo,
            x => x.MakbuzTuru == input.MakbuzTuru && x.SubeId == input.SubeId &&
                 x.DonemId == input.DonemId && x.Durum == input.Durum);
    }
}