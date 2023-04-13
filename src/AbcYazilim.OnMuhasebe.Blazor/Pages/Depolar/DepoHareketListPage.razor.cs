using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Blazor.Services;
using AbcYazilim.OnMuhasebe.Depolar;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.Depolar;

public partial class DepoHareketListPage
{
    public AppService AppService { get; set; }//Property Injection
    
    protected override async Task GetListDataSourceAsync()
    {
        Service.ListDataSource = (await GetListAsync(new DepoHareketListParameterDto
        {
            DepoId = Service.DepoId,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
        })).Items.ToList();

        Service.IsLoaded = true;
    }
}