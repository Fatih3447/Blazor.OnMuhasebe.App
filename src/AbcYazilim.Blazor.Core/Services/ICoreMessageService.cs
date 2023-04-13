using System;
using System.Threading.Tasks;

namespace AbcYazilim.Blazor.Core.Services;

public interface ICoreMessageService
{
    Task ConfirmMessage(string message, Action action, string title = null);
}