using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Blazor.Services;
using AbcYazilim.OnMuhasebe.Depolar;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.Depolar;

public partial class DepoListPage
{
    public AppService AppService { get; set; }

    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new DepoListParameterDto
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
        Service.DataSource = new SelectDepoDto
        {
            Kod = await GetCodeAsync(new DepoCodeParameterDto
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