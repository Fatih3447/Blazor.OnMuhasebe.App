using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Faturalar;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Masraflar;

public interface IMasrafHareketAppService: ICrudAppService<SelectFaturaHareketDto,
    ListMasrafHareketDto, MasrafHareketListParameterDto, FaturaHareketDto, FaturaHareketDto,
    FaturaNoParameterDto>
{
    
}