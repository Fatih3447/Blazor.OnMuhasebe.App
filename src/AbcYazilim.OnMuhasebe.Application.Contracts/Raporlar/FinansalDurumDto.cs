using Volo.Abp.Application.Dtos;

namespace AbcYazilim.OnMuhasebe.Raporlar;

public class FinansalDurumDto : IEntityDto
{
    public decimal Tutar { get; set; }
    public string Aciklama { get; set; }
}