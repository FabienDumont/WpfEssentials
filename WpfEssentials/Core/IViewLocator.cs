using System;

namespace WpfEssentials.Core;

public interface IViewLocator
{
  Type? GetViewType(Type viewModelType);
}
