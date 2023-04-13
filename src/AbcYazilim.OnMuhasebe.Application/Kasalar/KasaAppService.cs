using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.Kasalar;

[Authorize(OnMuhasebePermissions.Kasa.Default)]
public class KasaAppService : OnMuhasebeAppService, IKasaAppService
{
    private readonly IKasaRepository _kasaRepository;
    private readonly KasaManager _kasaManager;

    public KasaAppService(IKasaRepository kasaRepository, KasaManager kasaManager)
    {
        _kasaRepository = kasaRepository;
        _kasaManager = kasaManager;
    }

    public virtual async Task<SelectKasaDto> GetAsync(Guid id)
    {
        var entity = await _kasaRepository.GetAsync(id, x => x.Id == id,
            x => x.OzelKod1, x => x.OzelKod2);

        return ObjectMapper.Map<Kasa, SelectKasaDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListKasaDto>> GetListAsync(
        KasaListParameterDto input)
    {
        var entities = await _kasaRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.SubeId == input.SubeId && x.Durum == input.Durum,
            x => x.Kod,
            x => x.OzelKod1, x => x.OzelKod2, x => x.MakbuzHareketler);

        var totalCount = await _kasaRepository.CountAsync(x => x.SubeId == input.SubeId &&
                                                               x.Durum == input.Durum);

        return new PagedResultDto<ListKasaDto>(
            totalCount,
            ObjectMapper.Map<List<Kasa>, List<ListKasaDto>>(entities)
            );
    }

    [Authorize(OnMuhasebePermissions.Kasa.Create)]
    public virtual async Task<SelectKasaDto> CreateAsync(CreateKasaDto input)
    {
        await _kasaManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id,
            input.SubeId);

        var entity = ObjectMapper.Map<CreateKasaDto, Kasa>(input);
        await _kasaRepository.InsertAsync(entity);
        return ObjectMapper.Map<Kasa, SelectKasaDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Kasa.Update)]
    public virtual async Task<SelectKasaDto> UpdateAsync(Guid id, UpdateKasaDto input)
    {
        var entity = await _kasaRepository.GetAsync(id, x => x.Id == id);

        await _kasaManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id,
            input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _kasaRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Kasa, SelectKasaDto>(mappedEntity);
    }

    [Authorize(OnMuhasebePermissions.Kasa.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _kasaManager.CheckDeleteAsync(id);
        await _kasaRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(KasaCodeParameterDto input)
    {
        return await _kasaRepository.GetCodeAsync(x => x.Kod,
            x => x.SubeId == input.SubeId && x.Durum == input.Durum);
    }
}