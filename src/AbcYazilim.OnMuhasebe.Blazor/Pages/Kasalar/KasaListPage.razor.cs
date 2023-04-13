using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Blazor.Services;
using AbcYazilim.OnMuhasebe.Kasalar;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.Kasalar;

public partial class KasaListPage
{
    public AppService AppService { get; set; }

    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new KasaListParameterDto
        {
            SubeId = AppService.FirmaParametre.SubeId,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }

    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectKasaDto
        {
            Kod = await GetCodeAsync(new KasaCodeParameterDto
            {
                SubeId = AppService.FirmaParametre.SubeId,
                Durum = Service.IsActiveCards
            }),
            SubeId = AppService.FirmaParametre.SubeId,
            Durum = Service.IsActiveCards
        };

        Service.ShowEditPage();
    }
}