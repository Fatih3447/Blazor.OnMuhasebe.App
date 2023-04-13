using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Kasalar;

public interface IKasaAppService : ICrudAppService<SelectKasaDto, ListKasaDto,
    KasaListParameterDto, CreateKasaDto, UpdateKasaDto, KasaCodeParameterDto>
{
}