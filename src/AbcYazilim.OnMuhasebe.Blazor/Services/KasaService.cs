using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.Kasalar;
using AbcYazilim.OnMuhasebe.MakbuzHareketler;
using AbcYazilim.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class KasaService : BaseService<ListKasaDto, SelectKasaDto>, IScopedDependency
{
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectMakbuzHareketDto makbuzHareket:
                makbuzHareket.KasaId = SelectedItem.Id;
                makbuzHareket.KasaAdi = SelectedItem.Ad;
                break;
            
            case SelectMakbuzDto makbuz:
                makbuz.KasaId = SelectedItem.Id;
                makbuz.KasaAdi = SelectedItem.Ad;
                break;
        }
    }
}