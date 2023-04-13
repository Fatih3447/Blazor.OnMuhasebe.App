using AbcYazilim.OnMuhasebe.Localization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.Base;

public abstract class BaseEditPage : AbpComponentBase
{
    public BaseEditPage()
    {
        LocalizationResource = typeof(OnMuhasebeResource);
    }

    [Parameter] public EventCallback OnSubmit { get; set; }
}