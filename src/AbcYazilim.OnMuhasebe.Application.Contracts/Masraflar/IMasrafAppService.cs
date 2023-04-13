using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Masraflar;

public interface IMasrafAppService : ICrudAppService<SelectMasrafDto, ListMasrafDto,
    MasrafListParameterDto, CreateMasrafDto, UpdateMasrafDto, CodeParameterDto>
{
}