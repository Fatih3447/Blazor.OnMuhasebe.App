using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.BankaSubeler;

public interface IBankaSubeAppService : ICrudAppService<SelectBankaSubeDto, ListBankaSubeDto,
    BankaSubeListParameterDto, CreateBankaSubeDto, 
    UpdateBankaSubeDto, BankaSubeCodeParameterDto>
{
}