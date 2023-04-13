using System;
using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Faturalar;
using AbcYazilim.OnMuhasebe.Masraflar;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class MasrafHareketService : BaseService<ListMasrafHareketDto, SelectFaturaHareketDto>, IScopedDependency
{
    public Guid MasrafId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        HareketTuru = (FaturaHareketTuru)prm[0];
        MasrafId = (Guid)prm[1];
        IsPopupListPage = true;
    }
}