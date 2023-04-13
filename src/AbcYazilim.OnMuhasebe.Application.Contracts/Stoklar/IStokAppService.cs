using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Stoklar;

public interface IStokAppService : ICrudAppService<SelectStokDto, ListStokDto,
    StokListParameterDto, CreateStokDto, UpdateStokDto, CodeParameterDto>
{
}