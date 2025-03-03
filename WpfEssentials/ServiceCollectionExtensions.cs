using Microsoft.Extensions.DependencyInjection;
using WpfEssentials.Services;

namespace WpfEssentials;

public static class ServiceCollectionExtensions
{
  #region Methods

  public static IServiceCollection AddWpf(this IServiceCollection services)
  {
    services.AddSingleton<IDialogService, DialogService>();
    services.AddSingleton<INavigationService, NavigationService>();

    return services;
  }

  #endregion
}
