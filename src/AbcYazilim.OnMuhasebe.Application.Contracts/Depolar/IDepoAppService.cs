using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Depolar;

public interface IDepoAppService : ICrudAppService<SelectDepoDto, ListDepoDto,
    DepoListParameterDto, CreateDepoDto, UpdateDepoDto, DepoCodeParameterDto>
{
}