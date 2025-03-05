using System;
using Microsoft.Extensions.DependencyInjection;

namespace WpfEssentials.Core;

/// <summary>
///   Service Locator for MVVM pattern applications.
/// </summary>
public class ServiceLocator : IServiceProvider
{
  #region Fields

  private static ServiceLocator? _serviceLocator;
  private static IServiceProvider? _serviceProvider;
  private readonly IServiceProvider _provider;

  #endregion

  #region Ctors

  /// <summary>
  ///   Initializes a new instance of the <see cref="ServiceLocator" /> class.
  /// </summary>
  /// <param name="provider">The provider.</param>
  /// <exception cref="ArgumentNullException">provider</exception>
  public ServiceLocator(IServiceProvider? provider)
  {
    _provider = provider ?? throw new ArgumentNullException(nameof(provider));
  }

  #endregion

  #region Properties

  /// <summary>
  ///   Gets the current service locator.
  /// </summary>
  /// <value>
  ///   The current.
  /// </value>
  public static ServiceLocator Current => _serviceLocator ??= new ServiceLocator(_serviceProvider);

  /// <summary>
  ///   Gets a value indicating whether this instance is location provider set.
  /// </summary>
  /// <value>
  ///   <c>true</c> if this instance is locator provider set; otherwise, <c>false</c>.
  /// </value>
  public static bool IsLocationProviderSet => _serviceLocator != null;

  #endregion

  #region Methods

  /// <summary>
  ///   Sets the locator provider.
  /// </summary>
  /// <param name="serviceProvider">The service provider.</param>
  /// <exception cref="ArgumentNullException">serviceProvider</exception>
  public static void SetLocatorProvider(IServiceProvider? serviceProvider)
  {
    _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    _serviceLocator = new ServiceLocator(_serviceProvider);
  }

  /// <summary>
  ///   Gets the instance.
  /// </summary>
  /// <typeparam name="TService">The type of the service.</typeparam>
  public TService? GetInstance<TService>()
  {
    return _provider.GetService<TService>();
  }

  #endregion

  #region Implementation of IServiceProvider

  /// <summary>Gets the service object of the specified type.</summary>
  /// <param name="serviceType">An object that specifies the type of service object to get.</param>
  /// <returns>
  ///   A service object of type <paramref name="serviceType" />.
  ///   -or-
  ///   <see langword="null" /> if there is no service object of type <paramref name="serviceType" />.
  /// </returns>
  public object? GetService(Type serviceType)
  {
    return _provider.GetService(serviceType);
  }

  #endregion
}
