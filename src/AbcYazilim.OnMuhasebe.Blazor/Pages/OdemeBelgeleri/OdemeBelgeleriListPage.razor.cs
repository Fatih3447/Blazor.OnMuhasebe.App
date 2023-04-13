using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Blazor.Services;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using AbcYazilim.OnMuhasebe.OdemeBelgeleri;
using DevExpress.Blazor.Internal;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.OdemeBelgeleri;

public partial class OdemeBelgeleriListPage
{
    public AppService AppService { get; set; }

    protected override async Task GetListDataSourceAsync()
    {
        if (!Service.IsPopupListPage)
        {
            Service.MakbuzService.MakbuzTuru = 0;
            Service.OdemeTurleri = $"{(byte)OdemeTuru.Cek}, {(byte)OdemeTuru.Senet}, " +
                                   $"{(byte)OdemeTuru.Pos}";
            Service.KendiBelgemiz = false;
        }

        var listDataSource = (await GetListAsync(new OdemeBelgesiListParameterDto
        {
            Sql = Service.MakbuzService.MakbuzTuru == MakbuzTuru.BankaIslem ||
                  Service.MakbuzService.MakbuzTuru == MakbuzTuru.KasaIslem ? "IslemYapilabilecekTumOdemeBelgeleri" :
                  Service.MakbuzService.MakbuzTuru == MakbuzTuru.Odeme ? "IslemYapilabilecekOdemeBelgeleri" :
                  "OdemeBelgeleri",
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
            KendiBelgemiz = Service.KendiBelgemiz,
            OdemeTurleri = Service.OdemeTurleri
        }))?.Items.ToList();

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;

        Service.ExcludeListItems.ForEach(x =>
        {
            var entity = Service.ListDataSource.FirstOrDefault(y => y.TakipNo == x);
            Service.ListDataSource.Remove(entity);
        });

        Service.ExcludeListItems = null;
        Service.IsLoaded = true;
    }
}