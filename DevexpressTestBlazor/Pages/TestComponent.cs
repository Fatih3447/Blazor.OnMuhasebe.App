using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace DevexpressTestBlazor.Pages;

public class TestComponent : ComponentBase
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<DxGridLayoutItem>(0);
        builder.AddAttribute(1, "Column", ColumnIndex);
        builder.AddAttribute(2, "ColumnSpan", ColumnSpan);
        builder.AddAttribute(3, "CssClass", CssClass);
        builder.AddAttribute(4, "Row", RowIndex);
        builder.AddAttribute(5, "RowSpan", RowSpan);
        builder.AddAttribute(6, "Visible", Visible);
        builder.AddAttribute(7, "Template", (RenderFragment)((builder1) =>
        {
            builder1.AddContent(8, Caption);
        }));

        builder.CloseComponent();
    }

    [Parameter, EditorRequired] public string Caption { get; set; }
    [Parameter, EditorRequired] public int ColumnIndex { get; set; }
    [Parameter] public int ColumnSpan { get; set; }
    [Parameter] public string CssClass { get; set; } = "caption";
    [Parameter, EditorRequired] public int RowIndex { get; set; }
    [Parameter] public int RowSpan { get; set; }
    [Parameter] public bool Visible { get; set; } = true;
}
