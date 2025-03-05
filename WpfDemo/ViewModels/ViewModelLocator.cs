using System.Diagnostics;
using WpfEssentials;
using WpfEssentials.Core;
using WpfEssentials.Services;

namespace WpfDemo.ViewModels;

[DebuggerNonUserCode]
public class ViewModelLocator
{
  public MainVm? MainVm => ServiceLocator.Current.GetInstance<MainVm>();
  public HomeVm? HomeVm => ServiceLocator.Current.GetInstance<HomeVm>();
  public AnotherVm? AnotherVm => ServiceLocator.Current.GetInstance<AnotherVm>();
  public ModalVm? ModalVm => ServiceLocator.Current.GetInstance<ModalVm>();
}
