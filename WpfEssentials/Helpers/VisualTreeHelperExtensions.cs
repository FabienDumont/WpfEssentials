using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfEssentials.Helpers;

public static class VisualTreeHelperExtensions
{
  public static IEnumerable<T> FindChildren<T>(DependencyObject? parent) where T : DependencyObject
  {
    if (parent == null) yield break;

    for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
    {
      var child = VisualTreeHelper.GetChild(parent, i);

      if (child is T t) yield return t;

      foreach (var descendant in FindChildren<T>(child)) yield return descendant;
    }
  }
}
