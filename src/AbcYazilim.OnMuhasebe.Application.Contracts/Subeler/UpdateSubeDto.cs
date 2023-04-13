using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.Subeler;

public class UpdateSubeDto : IEntityDto
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}