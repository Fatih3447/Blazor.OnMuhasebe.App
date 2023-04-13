using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.OzelKodlar;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.OzelKodlar;

public partial class OzelKodListPage
{
    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new OzelKodListParameterDto
        {
            KodTuru = Service.KodTuru,
            KartTuru = Service.KartTuru,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }

    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectOzelKodDto
        {
            Kod = await GetCodeAsync(new OzelKodCodeParameterDto
            {
                KodTuru = Service.KodTuru,
                KartTuru = Service.KartTuru,
                Durum = Service.IsActiveCards
            }),
            KodTuru = Service.KodTuru,
            KartTuru = Service.KartTuru,
            Durum = Service.IsActiveCards
        };

        Service.ShowEditPage();
    }
}