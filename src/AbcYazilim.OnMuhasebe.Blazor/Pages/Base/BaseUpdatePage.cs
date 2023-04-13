using System;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.Localization;
using AbcYazilim.OnMuhasebe.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.Base;

public abstract class BaseUpdatePage<TGetOutputDto, TGetListOutputDto, TGetListInput,
    TCreateInput, TUpdateInput> : AbpComponentBase
    where TGetOutputDto : class, IEntityDto
    where TGetListOutputDto : class, IEntityDto
    where TGetListInput : class, IEntityDto
    where TCreateInput : class, IEntityDto
    where TUpdateInput : class, IEntityDto  
{
    public BaseUpdatePage()
    {
        LocalizationResource = typeof(OnMuhasebeResource);
    }

    #region Services

    protected ICrudAppService<TGetOutputDto, TGetListOutputDto, TGetListInput,
              TCreateInput, TUpdateInput> BaseCrudService { get; set; }

    #endregion

    #region Crud Functions

    protected async Task<TGetOutputDto> GetAsync(Guid id)
    {
        try
        {
            return await BaseCrudService.GetAsync(id);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }

        return default;
    }

    protected async Task<TGetOutputDto> CreateAsync(TCreateInput input)
    {
        try
        {
            return await BaseCrudService.CreateAsync(input);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }

        return default;
    }

    protected async Task<TGetOutputDto> UpdateAsync(Guid id, TUpdateInput input)
    {
        try
        {
            return await BaseCrudService.UpdateAsync(id, input);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }

        return default;
    }

    #endregion
}