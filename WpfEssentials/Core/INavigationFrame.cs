using System;

namespace WpfEssentials.Core;

public interface INavigationFrame
{
  bool CanGoBack { get; }
  void GoBack();
  void Navigate(Uri pageUri);
  void Navigate(Uri pageUri, object parameter);
}
