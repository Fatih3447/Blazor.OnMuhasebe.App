using System;
using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.Cariler;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class CariHareketService : BaseService<ListCariHareketDto, SelectMakbuzHareketDto>, IScopedDependency
{
    public Guid CariId { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        IsPopupListPage = true;
        CariId = (Guid)prm[0];
    }
}