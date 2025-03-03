namespace WpfEssentials.Services;

public interface INavigationService
{
  #region Methods

  void GoBack();
  void NavigateTo(string pageKey);
  void NavigateTo(string pageKey, object parameter);
  T? TryGetParameter<T>(string pageKey);

  #endregion
}
