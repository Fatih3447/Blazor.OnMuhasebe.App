using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Faturalar;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Stoklar;

public interface IStokHareketAppService : ICrudAppService<SelectFaturaHareketDto,
    ListStokHareketDto, StokHareketListParameterDto, FaturaHareketDto, FaturaHareketDto,
    FaturaNoParameterDto>
{
}