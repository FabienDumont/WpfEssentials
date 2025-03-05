using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using WpfEssentials.Services;
using WpfEssentials.ViewModels;

namespace WpfDemo.ViewModels;

public class AnotherVm(IDialogService dialogService, INavigationService navigationService)
  : BaseVm(dialogService, navigationService)
{
  #region Properties

  public string MessageToAdd
  {
    get;
    set => SetProperty(ref field, value);
  } = string.Empty;

  public ObservableCollection<string> Messages { get; } = [];

  #endregion

  #region Commands

  public AsyncRelayCommand LoadDataCommand => new(LoadDataAsync);
  public RelayCommand AddMessageCommand => new(AddMessage, CanAddMessage);
  public RelayCommand<object> AddSpecificMessageCommand => new(AddSpecificMessage, CanAddSpecificMessage);
  public ICommand NavigateBackCommand => new RelayCommand(NavigateBack);

  #endregion

  #region Methods

  private async Task LoadDataAsync()
  {
    await Task.Delay(1000);
    await DialogService.ShowMessage("Data loaded!");
  }

  private bool CanAddMessage()
  {
    return Messages.Count < 10;
  }

  private void AddMessage()
  {
    if (!string.IsNullOrWhiteSpace(MessageToAdd))
    {
      Messages.Add(MessageToAdd);
      MessageToAdd = string.Empty;
    }
  }

  private bool CanAddSpecificMessage(object? arg)
  {
    return Messages.Count < 10;
  }

  private void AddSpecificMessage(object? parameter)
  {
    if (parameter is string message)
    {
      Messages.Add(message);
    }
  }

  private void NavigateBack()
  {
    NavigationService.GoBack();
  }

  #endregion
}
