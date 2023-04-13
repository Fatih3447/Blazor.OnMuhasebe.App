using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.BankaSubeler;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.BankaSubeler;

public partial class BankaSubeListPage
{
    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new BankaSubeListParameterDto
        {
            BankaId = Service.BankaId,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }

    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectBankaSubeDto
        {
            Kod = await GetCodeAsync(new BankaSubeCodeParameterDto
            {
                BankaId = Service.BankaId,
                Durum = Service.IsActiveCards
            }),
            BankaId = Service.BankaId,
            Durum = Service.IsActiveCards
        };

        Service.ShowEditPage();
    }
}