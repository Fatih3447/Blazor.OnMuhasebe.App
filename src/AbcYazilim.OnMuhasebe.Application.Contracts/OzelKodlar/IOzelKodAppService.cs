using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.OzelKodlar;

public interface IOzelKodAppService : ICrudAppService<SelectOzelKodDto, ListOzelKodDto,
    OzelKodListParameterDto, CreateOzelKodDto, UpdateOzelKodDto, OzelKodCodeParameterDto>
{
}