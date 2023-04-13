using System;
using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Faturalar;
using AbcYazilim.OnMuhasebe.Hizmetler;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class HizmetHareketService: BaseService<ListHizmetHareketDto, SelectFaturaHareketDto>, IScopedDependency
{
    public Guid HizmetId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        HareketTuru = (FaturaHareketTuru)prm[0];
        HizmetId = (Guid)prm[1];
        IsPopupListPage = true;
    }
}