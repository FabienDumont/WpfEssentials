using System;
using System.Collections.Generic;
using WpfEssentials.Services;

namespace WpfDemo.Services;

public class ProjectNavigationPageResolver : INavigationPageResolver
{
  #region Properties

  public static readonly IDictionary<string, Uri> Pages = new Dictionary<string, Uri>
  {
    {
      "HomePage",
      new Uri("pack://application:,,,/Views/HomePage.xaml", UriKind.Absolute)
    },
    {
      "AnotherPage",
      new Uri("pack://application:,,,/Views/AnotherPage.xaml", UriKind.Absolute)
    },
    {
      "ModalPage",
      new Uri("pack://application:,,,/Views/ModalView.xaml", UriKind.Absolute)
    }
  };

  #endregion

  public Uri? GetPageUri(string pageKey)
  {
    return Pages.TryGetValue(pageKey, out var uri) ? uri : null;
  }
}
