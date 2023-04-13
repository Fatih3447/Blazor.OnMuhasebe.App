using System;
using Microsoft.AspNetCore.Components;

namespace AbcYazilim.Blazor.Core.Services;

public interface ICoreCommonService
{
    public Action HasChanged { get; set; }
    public ComponentBase ActiveEditComponent { get; set; }
}