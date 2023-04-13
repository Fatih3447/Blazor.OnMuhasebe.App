using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.Depolar;

[Authorize(OnMuhasebePermissions.Depo.Default)]
public class DepoAppService : OnMuhasebeAppService, IDepoAppService
{
    private readonly IDepoRepository _depoRepository;
    private readonly DepoManager _depoManager;

    public DepoAppService(IDepoRepository depoRepository, DepoManager depoManager)
    {
        _depoRepository = depoRepository;
        _depoManager = depoManager;
    }

    public virtual async Task<SelectDepoDto> GetAsync(Guid id)
    {
        var entity = await _depoRepository.GetAsync(id, x => x.Id == id, x => x.OzelKod1,
            x => x.OzelKod2);

        return ObjectMapper.Map<Depo, SelectDepoDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListDepoDto>> GetListAsync(
        DepoListParameterDto input)
    {
        var entities = await _depoRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.SubeId == input.SubeId && x.Durum == input.Durum,
            x => x.Kod);

        var totalCount = await _depoRepository.CountAsync(x => x.SubeId == input.SubeId &&
          x.Durum == input.Durum);

        return new PagedResultDto<ListDepoDto>(
            totalCount,
            ObjectMapper.Map<List<Depo>, List<ListDepoDto>>(entities)
            );
    }

    [Authorize(OnMuhasebePermissions.Depo.Create)]
    public virtual async Task<SelectDepoDto> CreateAsync(CreateDepoDto input)
    {
        await _depoManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id,
            input.SubeId);

        var entity = ObjectMapper.Map<CreateDepoDto, Depo>(input);
        await _depoRepository.InsertAsync(entity);
        return ObjectMapper.Map<Depo, SelectDepoDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Depo.Update)]
    public virtual async Task<SelectDepoDto> UpdateAsync(Guid id, UpdateDepoDto input)
    {
        var entity = await _depoRepository.GetAsync(id, x => x.Id == id);
        
        await _depoManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id, 
            input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _depoRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Depo, SelectDepoDto>(mappedEntity);
    }

    [Authorize(OnMuhasebePermissions.Depo.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _depoManager.CheckDeleteAsync(id);
        await _depoRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(DepoCodeParameterDto input)
    {
        return await _depoRepository.GetCodeAsync(x => x.Kod,
            x => x.SubeId == input.SubeId && x.Durum == input.Durum);
    }
}