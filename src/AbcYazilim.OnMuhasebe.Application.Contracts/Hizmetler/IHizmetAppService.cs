using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Hizmetler;

public interface IHizmetAppService : ICrudAppService<SelectHizmetDto, ListHizmetDto,
    HizmetListParameterDto, CreateHizmetDto, UpdateHizmetDto, CodeParameterDto>
{
}