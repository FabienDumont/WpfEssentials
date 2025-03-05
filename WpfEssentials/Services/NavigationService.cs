using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WpfEssentials.Core;
using WpfEssentials.Helpers;

namespace WpfEssentials.Services;

public class NavigationService(INavigationPageResolver pageResolver, INavigationFrame navigationFrame)
  : INavigationService
{
  private static readonly Dictionary<string, object> Parameters = new();
  private string? _currentPageKey;

  public T? TryGetParameter<T>(string pageKey)
  {
    return Parameters.TryGetValue(pageKey, out var parameter) && parameter is T p ? p : default;
  }

  public void GoBack()
  {
    if (_currentPageKey != null && Parameters.ContainsKey(_currentPageKey))
    {
      Parameters.Remove(_currentPageKey);
    }

    if (navigationFrame.CanGoBack)
    {
      navigationFrame.GoBack();
    }
  }

  public void NavigateTo(string pageKey)
  {
    NavigateTo(pageKey, null);
  }

  public void NavigateTo(string pageKey, object? parameter)
  {
    var pageUri = pageResolver.GetPageUri(pageKey);
    if (pageUri == null)
    {
      throw new ArgumentOutOfRangeException(nameof(pageKey), $"No registered page for key: {pageKey}");
    }

    if (parameter == null)
    {
      navigationFrame.Navigate(pageUri);
    }
    else
    {
      navigationFrame.Navigate(pageUri, parameter);
      Parameters[pageKey] = parameter;
    }

    _currentPageKey = pageKey;
  }
}
