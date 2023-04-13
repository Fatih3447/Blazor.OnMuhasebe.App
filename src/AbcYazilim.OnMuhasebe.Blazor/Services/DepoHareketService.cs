using System;
using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.FaturaHareketler;
using AbcYazilim.OnMuhasebe.Stoklar;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class DepoHareketService : BaseService<ListStokHareketDto, SelectFaturaHareketDto>, IScopedDependency
{
    public Guid DepoId { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        DepoId = (Guid)prm[0];
        IsPopupListPage = true;
    }
}