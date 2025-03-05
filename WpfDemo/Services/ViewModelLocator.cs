using System.Diagnostics;
using WpfDemo.ViewModels;
using WpfEssentials.Core;

namespace WpfDemo.Services;

[DebuggerNonUserCode]
public class ViewModelLocator
{
  public static MainVm? MainVm => ServiceLocator.Current.GetInstance<MainVm>();
  public static HomeVm? HomeVm => ServiceLocator.Current.GetInstance<HomeVm>();
  public static AnotherVm? AnotherVm => ServiceLocator.Current.GetInstance<AnotherVm>();
  public static ModalVm? ModalVm => ServiceLocator.Current.GetInstance<ModalVm>();
}
