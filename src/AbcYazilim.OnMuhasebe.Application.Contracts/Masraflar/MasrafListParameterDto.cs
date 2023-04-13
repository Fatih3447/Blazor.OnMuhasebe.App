using AbcYazilim.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.Masraflar;

public class MasrafListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public bool Durum { get; set; }
}