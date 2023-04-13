using System;
using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.MakbuzHareketler;

public class MakbuzHareketListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid EntityId { get; set; }
    public OdemeTuru OdemeTuru { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
}