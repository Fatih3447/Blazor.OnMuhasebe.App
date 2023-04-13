using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AbcYazilim.OnMuhasebe
{
    [DependsOn(
        typeof(OnMuhasebeEntityFrameworkCoreTestModule)
        )]
    public class OnMuhasebeDomainTestModule : AbpModule
    {

    }
}