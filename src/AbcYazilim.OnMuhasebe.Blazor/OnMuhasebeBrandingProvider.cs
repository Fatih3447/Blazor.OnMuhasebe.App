using AbcYazilim.OnMuhasebe.Localization;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AbcYazilim.OnMuhasebe.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class OnMuhasebeBrandingProvider : DefaultBrandingProvider
    {
        private readonly IStringLocalizer<OnMuhasebeResource> _localizer;

        public OnMuhasebeBrandingProvider(IStringLocalizer<OnMuhasebeResource> localizer)
        {
            _localizer = localizer;
        }

        public override string AppName => $"Abc {_localizer["Software"]} {_localizer["Pre-Accounting"]}";
    }
}
