using Microsoft.Extensions.DependencyInjection;
using WpfDemo.ViewModels;

namespace WpfDemo;

public static class ServiceCollectionExtensions
{
  #region Methods

  public static IServiceCollection AddViewModels(this IServiceCollection services)
  {
    services.AddSingleton<MainVm>().AddSingleton<HomeVm>().AddSingleton<AnotherVm>().AddSingleton<ModalVm>();
    return services;
  }

  #endregion
}
