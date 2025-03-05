using System;
using System.Threading.Tasks;
using System.Windows;
using WpfEssentials.Core;

namespace WpfEssentials.Services;

public class DialogService(IViewLocator viewLocator, IToastService toastService)
  : IDialogService
{
  #region Implementation of IDialogService

  public async Task ShowWarning(string message)
  {
    await Task.FromResult(toastService.ShowToast(message, DialogImage.Warning)).ConfigureAwait(false);
  }

  public async Task ShowError(string message)
  {
    await Task.FromResult(toastService.ShowToast(message, DialogImage.Error)).ConfigureAwait(false);
  }

  public async Task ShowError(Exception error)
  {
    var message = error.Message;
    var innerException = error.InnerException;

    while (innerException != null)
    {
      message += string.IsNullOrEmpty(innerException.Message) ? string.Empty : innerException.Message;
      innerException = innerException.InnerException;
    }

    await Task.FromResult(toastService.ShowToast(message, DialogImage.Error)).ConfigureAwait(false);
  }

  public async Task ShowMessage(string message)
  {
    await Task.FromResult(toastService.ShowToast(message, DialogImage.Information)).ConfigureAwait(false);
  }

  public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : class
  {
    var viewType = viewLocator.GetViewType(typeof(TViewModel));
    if (viewType == null)
    {
      throw new InvalidOperationException($"No registered view for {typeof(TViewModel).Name}");
    }

    var window = (Window) Activator.CreateInstance(viewType)!;
    window.DataContext = viewModel;

    if (Application.Current.MainWindow != null)
    {
      window.Owner = Application.Current.MainWindow;
      window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
    }

    return window.ShowDialog();
  }

  #endregion
}
