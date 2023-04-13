using System;
using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Faturalar;
using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.Stoklar;

public class StokHareketListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public FaturaHareketTuru? HareketTuru { get; set; }
    public Guid StokId { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
}