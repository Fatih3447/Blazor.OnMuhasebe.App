using System;

namespace AbcYazilim.Blazor.Core.Models;

public class ComboBoxEnumItem<TEnum> where TEnum : Enum
{
    public TEnum Value { get; set; }
    public string DisplayName { get; set; }
}