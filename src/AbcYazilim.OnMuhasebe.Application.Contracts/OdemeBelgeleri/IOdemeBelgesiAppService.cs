using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.OdemeBelgeleri;

public interface IOdemeBelgesiAppService : ICrudAppService<SelectMakbuzHareketDto, ListOdemeBelgesiDto,
    OdemeBelgesiListParameterDto, MakbuzHareketDto, MakbuzHareketDto, MakbuzNoParameterDto>
{
}