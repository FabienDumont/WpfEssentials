using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfEssentials.Services;

namespace WpfEssentials;

public abstract class BaseVm : ObservableRecipient
{
  #region Ctors

  protected BaseVm(IDialogService dialogService, INavigationService navigationService)
  {
    DialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
    NavigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
  }

  #endregion

  #region Properties

  protected IDialogService DialogService { get; }
  protected INavigationService NavigationService { get; }

  public bool IsLoading
  {
    get;
    set
    {
      if (SetProperty(ref field, value))
      {
        RaiseCanChanged();
      }
    }
  }

  #endregion

  #region Methods

  protected virtual void RaiseCanChanged()
  {
  }

  protected async Task ShowErrorAsync(Exception error)
  {
    await DialogService.ShowError(error).ConfigureAwait(true);
  }

  #endregion
}
