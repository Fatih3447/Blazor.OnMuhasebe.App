using System;
using System.Collections.Generic;
using System.Text;
using AbcYazilim.OnMuhasebe.Localization;
using Volo.Abp.Application.Services;

namespace AbcYazilim.OnMuhasebe
{
    /* Inherit your application services from this class.
     */
    public abstract class OnMuhasebeAppService : ApplicationService
    {
        protected OnMuhasebeAppService()
        {
            LocalizationResource = typeof(OnMuhasebeResource);
        }
    }
}
