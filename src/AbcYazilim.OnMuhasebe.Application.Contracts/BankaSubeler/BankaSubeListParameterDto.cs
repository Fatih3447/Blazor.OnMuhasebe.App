using System;
using AbcYazilim.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.BankaSubeler;

public class BankaSubeListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid BankaId { get; set; }
    public bool Durum { get; set; }
}