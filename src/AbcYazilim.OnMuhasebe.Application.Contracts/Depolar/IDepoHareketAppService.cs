using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Faturalar;
using AbcYazilim.OnMuhasebe.Services;
using AbcYazilim.OnMuhasebe.Stoklar;

namespace AbcYazilim.OnMuhasebe.Depolar;

public interface IDepoHareketAppService : ICrudAppService<SelectFaturaHareketDto,
    ListStokHareketDto, DepoHareketListParameterDto, FaturaHareketDto, FaturaHareketDto,
    FaturaNoParameterDto>
{
    
}