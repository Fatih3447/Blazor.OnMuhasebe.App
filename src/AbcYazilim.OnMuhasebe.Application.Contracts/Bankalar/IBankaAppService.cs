using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Bankalar;

public interface IBankaAppService : ICrudAppService<SelectBankaDto, ListBankaDto,
    BankaListParameterDto, CreateBankaDto, UpdateBankaDto, CodeParameterDto>
{
}