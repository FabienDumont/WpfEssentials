using System;

namespace WpfEssentials.Services;

public interface IViewLocator
{
  Type? GetViewType(Type viewModelType);
}
