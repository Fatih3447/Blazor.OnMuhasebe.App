using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using AbcYazilim.OnMuhasebe.OdemeBelgeleri;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.BankaHesaplar;

public interface IBankaHesapHareketAppService : ICrudAppService<SelectMakbuzHareketDto, ListOdemeBelgesiHareketDto,
    MakbuzHareketListParameterDto, MakbuzHareketDto, MakbuzHareketDto, MakbuzNoParameterDto>
{
}