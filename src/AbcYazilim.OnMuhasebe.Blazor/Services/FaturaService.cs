using System;
using System.Collections.Generic;
using AbcYazilim.Blazor.Core.Services;
using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.Faturalar;
using Volo.Abp.DependencyInjection;

namespace AbcYazilim.OnMuhasebe.Blazor.Services;

public class FaturaService : BaseService<ListFaturaDto, SelectFaturaDto>,
    IScopedDependency
{
    public override void FillTable<TItem>(ICoreHareketService<TItem> hareketService,
        Action hasChanged)
    {
        if (hareketService is FaturaHareketService faturaHareketService)
        {
            faturaHareketService.ListDataSource = DataSource.FaturaHareketler ??
                new List<FaturaHareketler.SelectFaturaHareketDto>();
            faturaHareketService.HasChanged = hasChanged;
            faturaHareketService.GetTotal();
        }
    }
}