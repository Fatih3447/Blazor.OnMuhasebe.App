using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.BankaSubeler;

[Authorize(OnMuhasebePermissions.BankaSube.Default)]
public class BankaSubeAppService : OnMuhasebeAppService, IBankaSubeAppService
{
    private readonly IBankaSubeRepository _bankaSubeRepository;
    private readonly BankaSubeManager _bankaSubeManager;

    public BankaSubeAppService(IBankaSubeRepository bankaSubeRepository,
        BankaSubeManager bankaSubeManager)
    {
        _bankaSubeRepository = bankaSubeRepository;
        _bankaSubeManager = bankaSubeManager;
    }

    public virtual async Task<SelectBankaSubeDto> GetAsync(Guid id)
    {
        var entity = await _bankaSubeRepository.GetAsync(id, bs => bs.Id == id,
            x => x.Banka, x => x.OzelKod1, x => x.OzelKod2);

        return ObjectMapper.Map<BankaSube, SelectBankaSubeDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListBankaSubeDto>> GetListAsync(
        BankaSubeListParameterDto input)
    {
        var entities = await _bankaSubeRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.BankaId == input.BankaId && x.Durum == input.Durum,
            x => x.Kod,
            x => x.Banka, x => x.OzelKod1, x => x.OzelKod2);

        var totalCount = await _bankaSubeRepository.CountAsync(x =>
          x.BankaId == input.BankaId && x.Durum == input.Durum);

        return new PagedResultDto<ListBankaSubeDto>(
            totalCount,
            ObjectMapper.Map<List<BankaSube>, List<ListBankaSubeDto>>(entities)
            );
    }

    [Authorize(OnMuhasebePermissions.BankaSube.Create)]
    public virtual async Task<SelectBankaSubeDto> CreateAsync(CreateBankaSubeDto input)
    {
        await _bankaSubeManager.CheckCreateAsync(input.Kod, input.BankaId,
            input.OzelKod1Id, input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateBankaSubeDto, BankaSube>(input);
        await _bankaSubeRepository.InsertAsync(entity);
        return ObjectMapper.Map<BankaSube, SelectBankaSubeDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.BankaSube.Update)]
    public virtual async Task<SelectBankaSubeDto> UpdateAsync(Guid id, UpdateBankaSubeDto input)
    {
        var entity = await _bankaSubeRepository.GetAsync(id, bs => bs.Id == id);

        await _bankaSubeManager.CheckUpdateAsync(id, input.Kod, entity, 
            input.OzelKod1Id, input.OzelKod1Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _bankaSubeRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<BankaSube, SelectBankaSubeDto>(mappedEntity);
    }

    [Authorize(OnMuhasebePermissions.BankaSube.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _bankaSubeManager.CheckDeleteAsync(id);
        await _bankaSubeRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(BankaSubeCodeParameterDto input)
    {
        return await _bankaSubeRepository.GetCodeAsync(x => x.Kod,
            x => x.BankaId == input.BankaId && x.Durum == input.Durum);
    }
}