using System.Threading.Tasks;

namespace AbcYazilim.Blazor.Core.Services;

public interface ICoreHareketService<TDataGridItem> : ICoreDataGridService<TDataGridItem>,
    ICoreEditPageService<TDataGridItem>, ICoreListPageService, ICoreMessageService,
    ICoreCommonService
{
    public TDataGridItem TempDataSource { get; set; }
    void GetTotal();
    void BeforeInsert();
    void BeforeUpdate();
    Task DeleteAsync();
    void OnSubmit();
    void InsertOrUpdate();
    void Hesapla(object value, string propertyName);
}