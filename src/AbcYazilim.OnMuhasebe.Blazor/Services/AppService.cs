using System;
using AbcYazilim.Blazor.Core.Services;
using AbcYazilim.OnMuhasebe.Parametreler;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class AppService : ICoreAppService, IScopedDependency
{
    public SelectFirmaParametreDto FirmaParametre { get; set; } = new SelectFirmaParametreDto();    
    public Action HasChanged { get; set; }
    public bool ShowFirmaParametreEditPage { get; set; }
    public bool ShowSubeDonemEditPage { get; set; }
}