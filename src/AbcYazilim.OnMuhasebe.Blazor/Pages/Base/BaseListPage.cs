using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.Blazor.Core.Helpers;
using AbcYazilim.OnMuhasebe.Blazor.Services.Base;
using AbcYazilim.OnMuhasebe.CommonDtos;
using AbcYazilim.OnMuhasebe.Localization;
using AbcYazilim.OnMuhasebe.Services;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components;

namespace AbcYazilim.OnMuhasebe.Blazor.Pages.Base;

public abstract class BaseListPage<TGetOutputDto, TGetListOutputDto, TGetListInput,
    TCreateInput, TUpdateInput, TGetCodeInput> : AbpComponentBase
    where TGetOutputDto : class, IEntityDto<Guid>, new()
    where TGetListOutputDto : class, IEntityDto<Guid>
    where TGetListInput : class, IEntityDto, IDurum, new()
    where TCreateInput : class, IEntityDto, new()
    where TUpdateInput : class, IEntityDto
    where TGetCodeInput : class, IEntityDto, IDurum, new()
{
    public BaseListPage()
    {
        LocalizationResource = typeof(OnMuhasebeResource);
    }

    public string DefaultPolicy { get; set; }
    public string CreatePolicy { get; set; }
    public string UpdatePolicy { get; set; }
    public string DeletePolicy { get; set; }
    public string PrintPolicy { get; set; }
    public string ReservePolicy { get; set; }

    #region Services

    protected ICrudAppService<TGetOutputDto, TGetListOutputDto, TGetListInput,
              TCreateInput, TUpdateInput, TGetCodeInput> BaseCrudService
    { get; set; }

    public BaseService<TGetListOutputDto, TGetOutputDto> BaseService { get; set; }

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

    protected async Task<PagedResultDto<TGetListOutputDto>> GetListAsync(
        TGetListInput input)
    {
        try
        {
            return await BaseCrudService.GetListAsync(input);
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

    protected async Task DeleteAsync(Guid id)
    {
        try
        {
            await BaseCrudService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected async Task<string> GetCodeAsync(TGetCodeInput input)
    {
        try
        {
            return await BaseCrudService.GetCodeAsync(input);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }

        return default;
    }

    #endregion

    protected override async Task OnParametersSetAsync()
    {
        if (DefaultPolicy != null) BaseService.IsGrantedDefault =
                await AuthorizationService.IsGrantedAsync(DefaultPolicy);

        if (CreatePolicy != null) BaseService.IsGrantedCreate =
                await AuthorizationService.IsGrantedAsync(CreatePolicy);

        if (UpdatePolicy != null) BaseService.IsGrantedUpdate =
                await AuthorizationService.IsGrantedAsync(UpdatePolicy);

        if (DeletePolicy != null) BaseService.IsGrantedDelete =
                await AuthorizationService.IsGrantedAsync(DeletePolicy);

        if (PrintPolicy != null) BaseService.IsGrantedPrint =
                await AuthorizationService.IsGrantedAsync(PrintPolicy);

        if (ReservePolicy != null) BaseService.IsGrantedReserve =
                await AuthorizationService.IsGrantedAsync(ReservePolicy);

        await GetListDataSourceAsync();
        BaseService.HasChanged = StateHasChanged;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        BaseService.ShowListPage(firstRender);
    }

    protected virtual async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new TGetListInput
        {
            Durum = BaseService.IsActiveCards
        }))?.Items.ToList();

        BaseService.IsLoaded = true;

        if (listDataSource != null)
            BaseService.ListDataSource = listDataSource;
    }

    protected virtual async Task DeleteAsync()
    {
        BaseService.SelectFirstDataRow = false;

        await BaseService.ConfirmMessage(L["DeleteConfirmMessage"], async () =>
         {
             await DeleteAsync(BaseService.SelectedItem.Id);
             var deletedEntityIndex = BaseService.ListDataSource.FindIndex(x =>
                 x.GetEntityId() == BaseService.SelectedItem.GetEntityId());

             await GetListDataSourceAsync();
             BaseService.HasChanged();

             if (BaseService.ListDataSource.Count > 0)
                 BaseService.SelectedItem = BaseService.ListDataSource.
                                            SetSelectedItem(deletedEntityIndex);

         }, L["DeleteConfirmMessageTitle"]);
    }

    protected virtual async Task BeforeInsertAsync()
    {
        BaseService.DataSource = new TGetOutputDto();

        var kod = typeof(TGetOutputDto).GetProperty("Kod");
        var durum = typeof(TGetOutputDto).GetProperty("Durum");

        if (kod != null)
            kod.SetValue(BaseService.DataSource, await GetCodeAsync(
                new TGetCodeInput { Durum = BaseService.IsActiveCards }));

        if (durum != null)
            durum.SetValue(BaseService.DataSource, BaseService.IsActiveCards);

        BaseService.ShowEditPage();
    }

    protected virtual async Task BeforeUpdateAsync()
    {
        if (!BaseService.IsGrantedUpdate)
        {
            BaseService.SelectFirstDataRow = false;
            return;
        }

        if (BaseService.ListDataSource.Count == 0) return;

        BaseService.SelectFirstDataRow = false;
        BaseService.DataSource = await GetAsync(BaseService.SelectedItem.Id);
        BaseService.EditPageVisible = true;
        await InvokeAsync(BaseService.HasChanged);
    }

    protected virtual async Task OnSubmit()
    {
        TGetOutputDto result;

        if (BaseService.DataSource.Id == Guid.Empty)
        {
            var createInput = ObjectMapper.Map<TGetOutputDto, TCreateInput>(
                BaseService.DataSource);

            result = await CreateAsync(createInput);
        }
        else
        {
            var updateInput = ObjectMapper.Map<TGetOutputDto, TUpdateInput>(
                BaseService.DataSource);

            result = await UpdateAsync(BaseService.DataSource.Id, updateInput);
        }

        if (result == null) return;

        var savedEntityIndex = BaseService.ListDataSource.FindIndex(
            x => x.Id == BaseService.DataSource.Id);

        await GetListDataSourceAsync();
        BaseService.HideEditPage();

        if (BaseService.DataSource.Id == Guid.Empty)
            BaseService.DataSource.Id = result.Id;

        if (savedEntityIndex > -1)
            BaseService.SelectedItem = BaseService.ListDataSource.
                SetSelectedItem(savedEntityIndex);
        else
            BaseService.SelectedItem = BaseService.ListDataSource.
                GetEntityById(BaseService.DataSource.Id);
    }

    protected virtual async Task PrintAsync()
    {
        if (BaseService.ListDataSource.Count == 0) return;

        BaseService.SelectFirstDataRow = false;
        BaseService.DataSource = await GetAsync(BaseService.SelectedItem.Id);

        BaseService.ShowReportSelectBox = true;
        await InvokeAsync(BaseService.HasChanged);
    }
}