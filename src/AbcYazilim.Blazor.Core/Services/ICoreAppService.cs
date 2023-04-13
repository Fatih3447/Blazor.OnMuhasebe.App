using System;

namespace AbcYazilim.Blazor.Core.Services;

public interface ICoreAppService
{
    public Action HasChanged { get; set; }
    public bool ShowFirmaParametreEditPage { get; set; }
    public bool ShowSubeDonemEditPage { get; set; }
}