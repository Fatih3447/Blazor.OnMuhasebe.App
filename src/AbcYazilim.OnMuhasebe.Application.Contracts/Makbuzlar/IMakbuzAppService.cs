using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Makbuzlar;

public interface IMakbuzAppService : ICrudAppService<SelectMakbuzDto, ListMakbuzDto,
    MakbuzListParameterDto, CreateMakbuzDto, UpdateMakbuzDto, MakbuzNoParameterDto>
{
}