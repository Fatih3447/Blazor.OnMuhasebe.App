using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.Donemler;

[Authorize(OnMuhasebePermissions.Donem.Default)]
public class DonemAppService : OnMuhasebeAppService, IDonemAppService
{
    private readonly IDonemRepository _donemRepository;
    private readonly DonemManager _donemManager;

    public DonemAppService(IDonemRepository donemRepository, DonemManager donemManager)
    {
        _donemRepository = donemRepository;
        _donemManager = donemManager;
    }

    public virtual async Task<SelectDonemDto> GetAsync(Guid id)
    {
        var entity = await _donemRepository.GetAsync(id, x => x.Id == id);
        return ObjectMapper.Map<Donem, SelectDonemDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListDonemDto>> GetListAsync(
        DonemListParameterDto input)
    {
        var entities = await _donemRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,
            x => x.Kod);

        var totalCount = await _donemRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListDonemDto>(
            totalCount,
            ObjectMapper.Map<List<Donem>, List<ListDonemDto>>(entities)
            );
    }

    [Authorize(OnMuhasebePermissions.Donem.Create)]
    public virtual async Task<SelectDonemDto> CreateAsync(CreateDonemDto input)
    {
        await _donemManager.CheckCreateAsync(input.Kod);

        var entity = ObjectMapper.Map<CreateDonemDto, Donem>(input);
        await _donemRepository.InsertAsync(entity);
        return ObjectMapper.Map<Donem, SelectDonemDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Donem.Update)]
    public virtual async Task<SelectDonemDto> UpdateAsync(Guid id, UpdateDonemDto input)
    {
        var entity = await _donemRepository.GetAsync(id, x => x.Id == id);
        await _donemManager.CheckUpdateAsync(id, input.Kod, entity);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _donemRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Donem, SelectDonemDto>(mappedEntity);
    }

    [Authorize(OnMuhasebePermissions.Donem.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _donemManager.CheckDeleteAsync(id);
        await _donemRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _donemRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}