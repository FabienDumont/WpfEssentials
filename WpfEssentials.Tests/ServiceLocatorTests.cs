using System;
using FakeItEasy;
using FluentAssertions;
using WpfEssentials.Core;
using WpfEssentials.Services;
using Xunit;

namespace WpfEssentials.Tests;

public class ServiceLocatorTests
{
  [Fact]
  public void Constructor_ShouldThrowArgumentNullException_WhenProviderIsNull()
  {
    // Act
    Action act = () => new ServiceLocator(null);

    // Assert
    act.Should().Throw<ArgumentNullException>().WithMessage("*provider*");
  }

  [Fact]
  public void Current_ShouldReturnSingletonInstance()
  {
    // Arrange
    var fakeProvider = A.Fake<IServiceProvider>();
    ServiceLocator.SetLocatorProvider(fakeProvider);

    // Act
    var instance1 = ServiceLocator.Current;
    var instance2 = ServiceLocator.Current;

    // Assert
    instance1.Should().BeSameAs(instance2);
  }

  [Fact]
  public void IsLocationProviderSet_ShouldReturnTrue_AfterSettingLocatorProvider()
  {
    // Arrange
    var fakeProvider = A.Fake<IServiceProvider>();

    // Act
    ServiceLocator.SetLocatorProvider(fakeProvider);

    // Assert
    ServiceLocator.IsLocationProviderSet.Should().BeTrue();
  }

  [Fact]
  public void GetInstance_ShouldReturnService_WhenRegistered()
  {
    // Arrange
    var fakeProvider = A.Fake<IServiceProvider>();
    var fakeService = A.Fake<IDisposable>();

    A.CallTo(() => fakeProvider.GetService(typeof(IDisposable))).Returns(fakeService);
    var serviceLocator = new ServiceLocator(fakeProvider);

    // Act
    var result = serviceLocator.GetInstance<IDisposable>();

    // Assert
    result.Should().BeSameAs(fakeService);
  }

  [Fact]
  public void GetInstance_ShouldReturnNull_WhenServiceIsNotRegistered()
  {
    // Arrange
    var fakeProvider = A.Fake<IServiceProvider>();
    A.CallTo(() => fakeProvider.GetService(typeof(IDisposable))).Returns(null);
    var serviceLocator = new ServiceLocator(fakeProvider);

    // Act
    var result = serviceLocator.GetInstance<IDisposable>();

    // Assert
    result.Should().BeNull();
  }

  [Fact]
  public void GetService_ShouldReturnService_WhenRegistered()
  {
    // Arrange
    var fakeProvider = A.Fake<IServiceProvider>();
    var fakeService = A.Fake<IDisposable>();

    A.CallTo(() => fakeProvider.GetService(typeof(IDisposable))).Returns(fakeService);
    var serviceLocator = new ServiceLocator(fakeProvider);

    // Act
    var result = serviceLocator.GetService(typeof(IDisposable));

    // Assert
    result.Should().BeSameAs(fakeService);
  }

  [Fact]
  public void GetService_ShouldReturnNull_WhenServiceIsNotRegistered()
  {
    // Arrange
    var fakeProvider = A.Fake<IServiceProvider>();
    A.CallTo(() => fakeProvider.GetService(typeof(IDisposable))).Returns(null);
    var serviceLocator = new ServiceLocator(fakeProvider);

    // Act
    var result = serviceLocator.GetService(typeof(IDisposable));

    // Assert
    result.Should().BeNull();
  }
}
