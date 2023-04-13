using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using AbcYazilim.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.OdemeBelgeleri;

[Authorize(OnMuhasebePermissions.OdemeBelgesi.Default)]
public class OdemeBelgesiAppService : OnMuhasebeAppService, IOdemeBelgesiAppService
{
    private readonly IOdemeBelgesiRepository _repository;

    public OdemeBelgesiAppService(IOdemeBelgesiRepository repository)
    {
        _repository = repository;
    }

    public virtual async Task<PagedResultDto<ListOdemeBelgesiDto>> GetListAsync(OdemeBelgesiListParameterDto input)
    {
        IList<OdemeBelgesi> _odemeBelgeleri;

        if (input.Sql == "IslemYapilabilecekOdemeBelgeleri")
            _odemeBelgeleri = await _repository.FromSqlRawAsync($"{input.Sql} " +
                                                                $"@SubeId='{input.SubeId}', " +
                                                                $"@DonemId='{input.DonemId}', " +
                                                                $"@KendiBelgemiz={input.KendiBelgemiz}, " +
                                                                $"@OdemeTurleri='{input.OdemeTurleri}'");
        else
            _odemeBelgeleri = await _repository.FromSqlRawAsync($"{input.Sql} " +
                                                                $"@SubeId='{input.SubeId}', " +
                                                                $"@DonemId='{input.DonemId}', " +
                                                                $"@OdemeTurleri='{input.OdemeTurleri}'");

        var mappedEntities = ObjectMapper.Map<List<OdemeBelgesi>, List<ListOdemeBelgesiDto>>(
            _odemeBelgeleri.ToList());
        
        mappedEntities.ForEach(x =>
        {
            x.OdemeTuruAdi = L[$"Enum:OdemeTuru:{(byte)x.OdemeTuru}"];
            x.BelgeDurumuAdi = L[$"Enum:BelgeDurumu:{(byte)x.BelgeDurumu}"];
        });

        return new PagedResultDto<ListOdemeBelgesiDto>
        {
            TotalCount = _odemeBelgeleri.Count,
            Items = mappedEntities
        };
    }

    public Task<SelectMakbuzHareketDto> GetAsync(Guid id) => throw new NotImplementedException();

    public Task<SelectMakbuzHareketDto> CreateAsync(MakbuzHareketDto input) => throw new NotImplementedException();

    public Task<SelectMakbuzHareketDto> UpdateAsync(Guid id, MakbuzHareketDto input) =>
        throw new NotImplementedException();

    public Task DeleteAsync(Guid id) => throw new NotImplementedException();

    public Task<string> GetCodeAsync(MakbuzNoParameterDto input) => throw new NotImplementedException();
}