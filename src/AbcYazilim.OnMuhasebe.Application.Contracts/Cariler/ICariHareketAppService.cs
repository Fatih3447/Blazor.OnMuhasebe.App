using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Cariler;

public interface ICariHareketAppService : ICrudAppService<SelectMakbuzHareketDto, ListCariHareketDto,
CariHareketListParameterDto, MakbuzHareketDto, MakbuzHareketDto, MakbuzNoParameterDto>
{
    
}