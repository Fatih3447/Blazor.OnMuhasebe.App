using AbcYazilim.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.Birimler;

public class BirimListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public bool Durum { get; set; }
}