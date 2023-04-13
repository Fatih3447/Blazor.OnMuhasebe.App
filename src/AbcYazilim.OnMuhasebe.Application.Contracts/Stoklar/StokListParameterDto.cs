using AbcYazilim.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.Stoklar;

public class StokListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public bool Durum { get; set; }
}