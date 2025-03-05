﻿using Microsoft.Extensions.DependencyInjection;
using WpfEssentials.Core;
using WpfEssentials.Services;

namespace WpfEssentials;

public static class ServiceCollectionExtensions
{
  #region Methods

  public static IServiceCollection AddWpf(this IServiceCollection services)
  {
    services.AddSingleton<IToastService, ToastService>();
    services.AddSingleton<IDialogService, DialogService>();
    services.AddSingleton<INavigationService, NavigationService>();
    services.AddSingleton<INavigationFrame, NavigationFrame>();

    return services;
  }

  #endregion
}
