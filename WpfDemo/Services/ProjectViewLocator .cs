using System;
using System.Collections.Generic;
using WpfDemo.ViewModels;
using WpfDemo.Views;
using WpfEssentials.Core;

namespace WpfDemo.Services;

public class ProjectViewLocator : IViewLocator
{
  private static readonly Dictionary<Type, Type> ViewModelToViewMapping = new()
  {
    {typeof(ModalVm), typeof(ModalView)}
  };

  public Type? GetViewType(Type viewModelType)
  {
    return ViewModelToViewMapping.GetValueOrDefault(viewModelType);
  }
}
