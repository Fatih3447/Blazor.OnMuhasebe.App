using AbcYazilim.Blazor.Core.Models;
using AbcYazilim.OnMuhasebe.BankaHesaplar;
using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class BankaHesapService : BaseService<ListBankaHesapDto, SelectBankaHesapDto>,
    IScopedDependency
{
    public BankaHesapTuru? HesapTuru { get; set; }

    public void BankaHesapTuruSelectedItemChanged(ComboBoxEnumItem<BankaHesapTuru> 
        selectedItem)
    {
        DataSource.HesapTuru = selectedItem.Value;
    }
    
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectMakbuzHareketDto makbuzHareket:
                makbuzHareket.BankaHesapId = SelectedItem.Id;
                makbuzHareket.BankaHesapAdi = SelectedItem.Ad;
                break;
            
            case SelectMakbuzDto makbuz:
                makbuz.BankaHesapId = SelectedItem.Id;
                makbuz.BankaHesapAdi = SelectedItem.Ad;
                break;
        }
    }
}