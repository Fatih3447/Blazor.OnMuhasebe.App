using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Faturalar;

public interface IFaturaAppService : ICrudAppService<SelectFaturaDto, ListFaturaDto,
    FaturaListParameterDto, CreateFaturaDto, UpdateFaturaDto, FaturaNoParameterDto>
{
}