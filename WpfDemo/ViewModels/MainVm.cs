using CommunityToolkit.Mvvm.Input;
using WpfEssentials.Services;
using WpfEssentials.ViewModels;

namespace WpfDemo.ViewModels;

public sealed partial class MainVm(IDialogService dialogService, INavigationService navigationService)
  : BaseVm(dialogService, navigationService)
{
  #region Methods

  [RelayCommand]
  private void Loading()
  {
    NavigationService.NavigateTo("HomePage");
  }

  #endregion
}
