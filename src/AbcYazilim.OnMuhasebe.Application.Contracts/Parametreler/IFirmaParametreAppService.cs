using System;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Services;

namespace AbcYazilim.OnMuhasebe.Parametreler;

public interface IFirmaParametreAppService : ICrudAppService<SelectFirmaParametreDto,
    SelectFirmaParametreDto, FirmaParametreListParameterDto, CreateFirmaParametreDto,
    UpdateFirmaParametreDto>
{
    Task<bool> UserAnyAsync(Guid userId);
}