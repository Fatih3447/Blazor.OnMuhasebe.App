using System;
using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.OzelKodlar;

public class ListOzelKodDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
}