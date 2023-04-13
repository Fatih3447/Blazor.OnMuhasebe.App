using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Faturalar;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbcYazilim.OnMuhasebe.Cariler;

[Authorize(OnMuhasebePermissions.Cari.Default)]
public class CariAppService : OnMuhasebeAppService, ICariAppService
{
    private readonly ICariRepository _cariRepository;
    private readonly CariManager _cariManager;

    public CariAppService(ICariRepository cariRepository, CariManager cariManager)
    {
        _cariRepository = cariRepository;
        _cariManager = cariManager;
    }

    public virtual async Task<SelectCariDto> GetAsync(Guid id)
    {
        var entity = await _cariRepository.GetAsync(id, x => x.Id == id, x => x.OzelKod1,
            x => x.OzelKod2);

        return ObjectMapper.Map<Cari, SelectCariDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListCariDto>> GetListAsync(
        CariListParameterDto input)
    {
        var entities = await _cariRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,
            x => x.Kod,
            x => x.OzelKod1, x => x.OzelKod2, x => x.Faturalar, x => x.Makbuzlar);

        var totalCount = await _cariRepository.CountAsync(x => x.Durum == input.Durum);
        var mappedDtos = ObjectMapper.Map<List<Cari>, List<ListCariDto>>(entities);

        mappedDtos.ForEach(x =>
        {
            x.Alacak = x.Faturalar.Where(y => y.FaturaTuru == FaturaTuru.Alis)
             .Sum(y => y.NetTutar);

            x.Alacak += x.Makbuzlar.Where(y => y.MakbuzTuru == MakbuzTuru.Tahsilat)
             .Sum(y => y.CekToplam + y.SenetToplam + y.PosToplam + y.NakitToplam +
              y.BankaToplam);

            x.Borc = x.Faturalar.Where(y => y.FaturaTuru == FaturaTuru.Satis)
             .Sum(y => y.NetTutar);

            x.Borc += x.Makbuzlar.Where(y => y.MakbuzTuru == MakbuzTuru.Odeme)
             .Sum(y => y.CekToplam + y.SenetToplam + y.PosToplam + y.NakitToplam +
              y.BankaToplam);
        });

        return new PagedResultDto<ListCariDto>(totalCount, mappedDtos);
    }

    [Authorize(OnMuhasebePermissions.Cari.Create)]
    public virtual async Task<SelectCariDto> CreateAsync(CreateCariDto input)
    {
        await _cariManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateCariDto, Cari>(input);
        await _cariRepository.InsertAsync(entity);
        return ObjectMapper.Map<Cari, SelectCariDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Cari.Update)]
    public virtual async Task<SelectCariDto> UpdateAsync(Guid id, UpdateCariDto input)
    {
        var entity = await _cariRepository.GetAsync(id, x => x.Id == id);

        await _cariManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id,
            input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _cariRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Cari, SelectCariDto>(mappedEntity);
    }

    [Authorize(OnMuhasebePermissions.Cari.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _cariManager.CheckDeleteAsync(id);
        await _cariRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _cariRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}