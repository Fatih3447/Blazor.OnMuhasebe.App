using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.Birimler;

[Authorize(OnMuhasebePermissions.Birim.Default)]
public class BirimAppService : OnMuhasebeAppService, IBirimAppService
{
    private readonly IBirimRepository _birimRepository;
    private readonly BirimManager _birimManager;

    public BirimAppService(IBirimRepository birimRepository, BirimManager birimManager)
    {
        _birimRepository = birimRepository;
        _birimManager = birimManager;
    }

    public virtual async Task<SelectBirimDto> GetAsync(Guid id)
    {
        var entity = await _birimRepository.GetAsync(id, x => x.Id == id,
            x => x.OzelKod1, x => x.OzelKod2);

        return ObjectMapper.Map<Birim, SelectBirimDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListBirimDto>> GetListAsync(
        BirimListParameterDto input)
    {
        var entities = await _birimRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,
            x => x.Kod,
            x => x.OzelKod1, x => x.OzelKod2);

        var totalCount = await _birimRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListBirimDto>(
            totalCount,
            ObjectMapper.Map<List<Birim>, List<ListBirimDto>>(entities)
            );
    }

    [Authorize(OnMuhasebePermissions.Birim.Create)]
    public virtual async Task<SelectBirimDto> CreateAsync(CreateBirimDto input)
    {
        await _birimManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateBirimDto, Birim>(input);
        await _birimRepository.InsertAsync(entity);
        return ObjectMapper.Map<Birim, SelectBirimDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Birim.Update)]
    public virtual async Task<SelectBirimDto> UpdateAsync(Guid id, UpdateBirimDto input)
    {
        var entity = await _birimRepository.GetAsync(id, x => x.Id == id);

        await _birimManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id, 
            input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _birimRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Birim, SelectBirimDto>(mappedEntity);
    }

    [Authorize(OnMuhasebePermissions.Birim.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _birimManager.CheckDeleteAsync(id);
        await _birimRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _birimRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}