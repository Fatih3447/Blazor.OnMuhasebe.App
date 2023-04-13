using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Birimler;

public interface IBirimAppService : ICrudAppService<SelectBirimDto, ListBirimDto,
    BirimListParameterDto, CreateBirimDto, UpdateBirimDto, CodeParameterDto>
{
}