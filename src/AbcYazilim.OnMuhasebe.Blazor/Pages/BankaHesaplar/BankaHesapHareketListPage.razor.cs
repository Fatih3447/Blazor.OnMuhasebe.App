using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Blazor.Services;
using AbcYazilim.OnMuhasebe.Faturalar;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.BankaHesaplar;

public partial class BankaHesapHareketListPage
{
    public AppService AppService { get; set; }//Property Injection
    
    protected override async Task GetListDataSourceAsync()
    {
        Service.ListDataSource = (await GetListAsync(new MakbuzHareketListParameterDto
        {
            OdemeTuru = Service.OdemeTuru,
            EntityId = Service.EntityId,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
        })).Items.ToList();

        Service.IsLoaded = true;
    }
}