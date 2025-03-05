using System.Linq;
using WpfEssentials.Helpers;

namespace WpfEssentials.Core;

using System;
using System.Windows.Controls;

public class NavigationFrame : INavigationFrame
{
  private static Frame? Frame
  {
    get
    {
      if (System.Windows.Application.Current.MainWindow == null)
      {
        return null;
      }

      return VisualTreeHelperExtensions.FindChildren<Frame>(System.Windows.Application.Current.MainWindow)
        .FirstOrDefault();
    }
  }

  public bool CanGoBack => Frame?.CanGoBack ?? false;

  public void GoBack()
  {
    if (Frame?.CanGoBack == true)
    {
      Frame.GoBack();
    }
  }

  public void Navigate(Uri pageUri)
  {
    Frame?.Navigate(pageUri);
  }

  public void Navigate(Uri pageUri, object parameter)
  {
    Frame?.Navigate(pageUri, parameter);
  }
}
