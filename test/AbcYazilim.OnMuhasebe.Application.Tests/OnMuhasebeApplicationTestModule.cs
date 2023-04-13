using Volo.Abp.Modularity;

namespace AbcYazilim.OnMuhasebe
{
    [DependsOn(
        typeof(OnMuhasebeApplicationModule),
        typeof(OnMuhasebeDomainTestModule)
        )]
    public class OnMuhasebeApplicationTestModule : AbpModule
    {

    }
}