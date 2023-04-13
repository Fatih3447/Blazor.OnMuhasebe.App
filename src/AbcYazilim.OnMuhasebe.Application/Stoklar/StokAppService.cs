using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.Stoklar;

[Authorize(OnMuhasebePermissions.Stok.Default)]
public class StokAppService : OnMuhasebeAppService, IStokAppService
{
    private readonly IStokRepository _stokRepository;
    private readonly StokManager _stokManager;

    public StokAppService(IStokRepository stokRepository, StokManager stokManager)
    {
        _stokRepository = stokRepository;
        _stokManager = stokManager;
    }

    public virtual async Task<SelectStokDto> GetAsync(Guid id)
    {
        var entity = await _stokRepository.GetAsync(id, x => x.Id == id, x => x.Birim,
            x => x.OzelKod1, x => x.OzelKod2);

        return ObjectMapper.Map<Stok, SelectStokDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListStokDto>> GetListAsync(
        StokListParameterDto input)
    {
        var entities = await _stokRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,
            x => x.Kod);

        var totalCount = await _stokRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListStokDto>(
            totalCount,
            ObjectMapper.Map<List<Stok>, List<ListStokDto>>(entities)
            );
    }

    [Authorize(OnMuhasebePermissions.Stok.Create)]
    public virtual async Task<SelectStokDto> CreateAsync(CreateStokDto input)
    {
        await _stokManager.CheckCreateAsync(input.Kod, input.BirimId, input.OzelKod1Id,
            input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateStokDto, Stok>(input);
        await _stokRepository.InsertAsync(entity);
        return ObjectMapper.Map<Stok, SelectStokDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Stok.Update)]
    public virtual async Task<SelectStokDto> UpdateAsync(Guid id, UpdateStokDto input)
    {
        var entity = await _stokRepository.GetAsync(id, x => x.Id == id);

        await _stokManager.CheckUpdateAsync(id, input.Kod, entity, input.BirimId,
            input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _stokRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Stok, SelectStokDto>(mappedEntity);
    }

    [Authorize(OnMuhasebePermissions.Stok.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _stokManager.CheckDeleteAsync(id);
        await _stokRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _stokRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}