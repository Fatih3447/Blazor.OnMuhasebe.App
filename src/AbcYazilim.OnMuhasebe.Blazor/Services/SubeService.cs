using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.Parametreler;
using AbcYazilim.OnMuhasebe.Subeler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class SubeService : BaseService<ListSubeDto, SelectSubeDto>, IScopedDependency
{
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectFirmaParametreDto firmaParametre:
                firmaParametre.SubeId = SelectedItem.Id;
                firmaParametre.SubeAdi = SelectedItem.Ad;
                break;
        }
    }
}