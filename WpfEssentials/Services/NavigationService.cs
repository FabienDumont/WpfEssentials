using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using WpfEssentials.Helpers;

namespace WpfEssentials.Services;

public class NavigationService : INavigationService
{
  private readonly INavigationPageResolver _pageResolver;
  private static readonly IDictionary<string, object> Parameters = new Dictionary<string, object>();

  public NavigationService(INavigationPageResolver pageResolver)
  {
    _pageResolver = pageResolver;
  }

  private Frame? Frame =>
    VisualTreeHelperExtensions.FindChildren<Frame>(System.Windows.Application.Current.MainWindow).FirstOrDefault();

  public string? CurrentPageKey { get; private set; }

  public T? TryGetParameter<T>(string pageKey)
  {
    return Parameters.TryGetValue(pageKey, out var parameter) && parameter is T p ? p : default;
  }

  public void GoBack()
  {
    if (CurrentPageKey != null && Parameters.ContainsKey(CurrentPageKey))
    {
      Parameters.Remove(CurrentPageKey);
    }

    Frame?.NavigationService.GoBack();
  }

  public void NavigateTo(string pageKey)
  {
    NavigateTo(pageKey, null);
  }

  public void NavigateTo(string pageKey, object? parameter)
  {
    var pageUri = _pageResolver.GetPageUri(pageKey);
    if (pageUri == null)
    {
      throw new ArgumentOutOfRangeException(nameof(pageKey), $"No registered page for key: {pageKey}");
    }

    if (parameter == null)
    {
      Frame?.NavigationService.Navigate(pageUri);
    }
    else
    {
      Frame?.NavigationService.Navigate(pageUri, parameter);
      Parameters[pageKey] = parameter;
    }

    CurrentPageKey = pageKey;
  }
}
