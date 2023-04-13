using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.Faturalar;

[Authorize(OnMuhasebePermissions.Fatura.Default)]
public class FaturaAppService : OnMuhasebeAppService, IFaturaAppService
{
    private readonly IFaturaRepository _faturaRepository;
    private readonly FaturaManager _faturaManager;
    private readonly FaturaHareketManager _faturaHareketManager;

    public FaturaAppService(IFaturaRepository faturaRepository, FaturaManager faturaManager,
        FaturaHareketManager faturaHareketManager)
    {
        _faturaRepository = faturaRepository;
        _faturaManager = faturaManager;
        _faturaHareketManager = faturaHareketManager;
    }

    public virtual async Task<SelectFaturaDto> GetAsync(Guid id)
    {
        var entity = await _faturaRepository.GetAsync(id, x => x.Id == id);

        var mappedDto = ObjectMapper.Map<Fatura, SelectFaturaDto>(entity);
        mappedDto.FaturaHareketler.ForEach(x =>
        {
            x.HareketTuruAdi = L[$"Enum:FaturaHareketTuru:{(byte)x.HareketTuru}"];
        });

        return mappedDto;
    }

    public virtual async Task<PagedResultDto<ListFaturaDto>> GetListAsync(
        FaturaListParameterDto input)
    {
        var entities = await _faturaRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.FaturaTuru == input.FaturaTuru && x.SubeId == input.SubeId &&
                 x.DonemId == input.DonemId && x.Durum == input.Durum,
            x => x.FaturaNo,
            x => x.Cari, x => x.OzelKod1, x => x.OzelKod2);

        var totalCount = await _faturaRepository.CountAsync(
            x => x.FaturaTuru == input.FaturaTuru && x.SubeId == input.SubeId &&
                 x.DonemId == input.DonemId && x.Durum == input.Durum);

        return new PagedResultDto<ListFaturaDto>(
            totalCount,
            ObjectMapper.Map<List<Fatura>, List<ListFaturaDto>>(entities)
            );
    }

    [Authorize(OnMuhasebePermissions.Fatura.Create)]
    public virtual async Task<SelectFaturaDto> CreateAsync(CreateFaturaDto input)
    {
        await _faturaManager.CheckCreateAsync(input.FaturaNo, input.CariId, input.OzelKod1Id,
            input.OzelKod2Id, input.SubeId, input.DonemId);

        foreach (var faturaHareket in input.FaturaHareketler)
        {
            await _faturaHareketManager.CheckCreateAsync(faturaHareket.StokId,
                faturaHareket.HizmetId, faturaHareket.MasrafId, faturaHareket.DepoId);
        }

        var entity = ObjectMapper.Map<CreateFaturaDto, Fatura>(input);
        await _faturaRepository.InsertAsync(entity);
        return ObjectMapper.Map<Fatura, SelectFaturaDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Fatura.Update)]
    public virtual async Task<SelectFaturaDto> UpdateAsync(Guid id, UpdateFaturaDto input)
    {
        var entity = await _faturaRepository.GetAsync(id, x => x.Id == id,
            x => x.FaturaHareketler);

        await _faturaManager.CheckUpdateAsync(id, input.FaturaNo, entity, input.CariId,
            input.OzelKod1Id, input.OzelKod2Id);

        foreach (var faturaHareketDto in input.FaturaHareketler)
        {
            await _faturaHareketManager.CheckUpdateAsync(faturaHareketDto.StokId,
                faturaHareketDto.HizmetId, faturaHareketDto.MasrafId, faturaHareketDto.DepoId);

            var faturaHareket = entity.FaturaHareketler.FirstOrDefault(
                x => x.Id == faturaHareketDto.Id);

            if (faturaHareket == null)
            {
                entity.FaturaHareketler.Add(
                    ObjectMapper.Map<FaturaHareketDto, FaturaHareket>(faturaHareketDto));
                continue;
            }

            ObjectMapper.Map(faturaHareketDto, faturaHareket);
        }

        var deletedEntities = entity.FaturaHareketler.Where(
            x => input.FaturaHareketler.Select(y => y.Id).ToList().IndexOf(x.Id) == -1);

        entity.FaturaHareketler.RemoveAll(deletedEntities);

        ObjectMapper.Map(input, entity);
        await _faturaRepository.UpdateAsync(entity);
        return ObjectMapper.Map<Fatura, SelectFaturaDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Fatura.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _faturaRepository.GetAsync(id, x => x.Id == id,
            x => x.FaturaHareketler);

        await _faturaRepository.DeleteAsync(entity);
        entity.FaturaHareketler.RemoveAll(entity.FaturaHareketler);
    }

    public virtual async Task<string> GetCodeAsync(FaturaNoParameterDto input)
    {
        return await _faturaRepository.GetCodeAsync(x => x.FaturaNo,
            x => x.FaturaTuru == input.FaturaTuru && x.SubeId == input.SubeId &&
                 x.DonemId == input.DonemId && x.Durum == input.Durum);
    }
}