using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Subeler;

public interface ISubeAppService : ICrudAppService<SelectSubeDto, ListSubeDto,
    SubeListParameterDto, CreateSubeDto, UpdateSubeDto, CodeParameterDto>
{
}