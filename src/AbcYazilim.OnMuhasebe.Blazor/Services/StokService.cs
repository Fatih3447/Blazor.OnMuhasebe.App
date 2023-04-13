using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Stoklar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class StokService : BaseService<ListStokDto, SelectStokDto>, IScopedDependency
{
    public override void SelectEntity(IEntityDto targetEntity)
    {
        if (targetEntity is SelectFaturaHareketDto hareket)
        {
            hareket.StokId = SelectedItem.Id;
            hareket.StokKodu = SelectedItem.Kod;
            hareket.StokAdi = SelectedItem.Ad;
            hareket.BirimAdi = SelectedItem.BirimAdi;
            hareket.BirimFiyat = SelectedItem.BirimFiyat;
            hareket.KdvOrani = SelectedItem.KdvOrani;
        }
    }
}