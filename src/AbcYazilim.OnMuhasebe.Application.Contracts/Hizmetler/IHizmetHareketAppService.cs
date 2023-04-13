using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Faturalar;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Hizmetler;

public interface IHizmetHareketAppService : ICrudAppService<SelectFaturaHareketDto,
    ListHizmetHareketDto, HizmetHareketListParameterDto, FaturaHareketDto, FaturaHareketDto,
    FaturaNoParameterDto>
{
    
}