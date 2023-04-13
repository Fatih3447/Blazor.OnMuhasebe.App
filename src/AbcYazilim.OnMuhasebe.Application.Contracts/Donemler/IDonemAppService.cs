using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Donemler;

public interface IDonemAppService : ICrudAppService<SelectDonemDto, ListDonemDto,
    DonemListParameterDto, CreateDonemDto, UpdateDonemDto, CodeParameterDto>
{
}