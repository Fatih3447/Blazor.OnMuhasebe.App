using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Masraflar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class MasrafService : BaseService<ListMasrafDto, SelectMasrafDto>, 
    IScopedDependency
{
    public override void SelectEntity(IEntityDto targetEntity)
    {
        if (targetEntity is SelectFaturaHareketDto hareket)
        {
            hareket.MasrafId = SelectedItem.Id;
            hareket.MasrafKodu = SelectedItem.Kod;
            hareket.MasrafAdi = SelectedItem.Ad;
            hareket.BirimAdi = SelectedItem.BirimAdi;
            hareket.BirimFiyat = SelectedItem.BirimFiyat;
            hareket.KdvOrani = SelectedItem.KdvOrani;
        }
    }
}