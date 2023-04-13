using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Cariler;

public interface ICariAppService : ICrudAppService<SelectCariDto, ListCariDto,
    CariListParameterDto, CreateCariDto, UpdateCariDto, CodeParameterDto>
{
}