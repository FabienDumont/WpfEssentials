using System.Diagnostics;
using WpfDemo.ViewModels;
using WpfEssentials.Services;

namespace WpfDemo.Services;

[DebuggerNonUserCode]
public class ViewModelLocator
{
  public MainVm? MainVm => ServiceLocator.Current.GetInstance<MainVm>();
  public HomeVm? HomeVm => ServiceLocator.Current.GetInstance<HomeVm>();
  public AnotherVm? AnotherVm => ServiceLocator.Current.GetInstance<AnotherVm>();
  public ModalVm? ModalVm => ServiceLocator.Current.GetInstance<ModalVm>();
}
