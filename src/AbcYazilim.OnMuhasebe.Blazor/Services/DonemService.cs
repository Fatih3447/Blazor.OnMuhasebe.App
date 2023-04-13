using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.Donemler;
using AbcYazilim.OnMuhasebe.Parametreler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class DonemService : BaseService<ListDonemDto, SelectDonemDto>, IScopedDependency
{
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectFirmaParametreDto firmaParametre:
                firmaParametre.DonemId = SelectedItem.Id;
                firmaParametre.DonemAdi = SelectedItem.Ad;
                break;
        }
    }
}