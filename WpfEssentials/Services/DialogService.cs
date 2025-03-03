using System;
using System.Threading.Tasks;
using System.Windows;
using WpfEssentials.Controls;

namespace WpfEssentials.Services;

public class DialogService : IDialogService
{
  #region Fields

  private readonly IViewLocator _viewLocator;

  #endregion

  #region Methods

  #region Ctors

  public DialogService(IViewLocator viewLocator)
  {
    _viewLocator = viewLocator;
  }

  #endregion

  public static bool ShowToast(string message, DialogImage image)
  {
    var toast = new ToastMessageBox {Message = message, Image = image};
    toast.Show();
    return true;
  }

  #endregion

  #region Implementation of IDialogService

  public async Task ShowWarning(string message)
  {
    await Task.FromResult(ShowToast(message, DialogImage.Warning)).ConfigureAwait(false);
  }

  public async Task ShowError(string message)
  {
    await Task.FromResult(ShowToast(message, DialogImage.Error)).ConfigureAwait(false);
  }

  public async Task ShowError(Exception error)
  {
    var message = error.Message;
    var innerException = error.InnerException;

    while (innerException != null)
    {
      message += (string.IsNullOrEmpty(innerException.Message) ? string.Empty : innerException.Message);

      innerException = innerException.InnerException;
    }

    await Task.FromResult(ShowToast(message, DialogImage.Error)).ConfigureAwait(false);
  }

  public async Task ShowMessage(string message)
  {
    await Task.FromResult(ShowToast(message, DialogImage.Information)).ConfigureAwait(false);
  }

  public async Task ShowMessage(string message, Action afterHideCallback)
  {
    await Task.FromResult(ShowToast(message, DialogImage.Information)).ConfigureAwait(false);
  }

  public Task<bool> ShowMessage(string message, string title)
  {
    return Task.FromResult(
      Application.Current.MainWindow != null && MessageBox.Show(
        Application.Current.MainWindow, message, title, MessageBoxButton.YesNo, MessageBoxImage.Question,
        MessageBoxResult.Yes
      ) == MessageBoxResult.Yes
    );
  }

  public async Task ShowMessageBox(string message)
  {
    await Task.FromResult(ShowToast(message, DialogImage.Information)).ConfigureAwait(false);
  }

  public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : class
  {
    var viewType = _viewLocator.GetViewType(typeof(TViewModel));
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
