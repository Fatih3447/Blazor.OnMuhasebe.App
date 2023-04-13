using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.BankaHesaplar;

public interface IBankaHesapAppService : ICrudAppService<SelectBankaHesapDto,
    ListBankaHesapDto, BankaHesapListParameterDto, CreateBankaHesapDto,
    UpdateBankaHesapDto, BankaHesapCodeParameterDto>
{
}