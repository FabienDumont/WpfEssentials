using System;
using FakeItEasy;
using FluentAssertions;
using WpfEssentials.Core;
using WpfEssentials.Services;
using Xunit;

namespace WpfEssentials.Tests;

public class NavigationServiceTests
{
  private readonly INavigationPageResolver _pageResolverMock;
  private readonly INavigationFrame _navigationFrameMock;
  private readonly NavigationService _navigationService;

  public NavigationServiceTests()
  {
    _pageResolverMock = A.Fake<INavigationPageResolver>();
    _navigationFrameMock = A.Fake<INavigationFrame>();
    _navigationService = new NavigationService(_pageResolverMock, _navigationFrameMock);
  }

  [Fact]
  public void NavigateTo_ShouldThrowException_WhenPageKeyIsInvalid()
  {
    // Arrange
    A.CallTo(() => _pageResolverMock.GetPageUri("InvalidKey")).Returns(null);

    // Act
    Action act = () => _navigationService.NavigateTo("InvalidKey");

    // Assert
    act.Should().Throw<ArgumentOutOfRangeException>().WithMessage("*No registered page for key: InvalidKey*");
  }

  [Fact]
  public void NavigateTo_ShouldNavigateWithoutParameter_WhenPageUriIsFound()
  {
    // Arrange
    var pageUri = new Uri("http://example.com");
    A.CallTo(() => _pageResolverMock.GetPageUri("ValidKey")).Returns(pageUri);

    // Act
    _navigationService.NavigateTo("ValidKey");

    // Assert
    A.CallTo(() => _navigationFrameMock.Navigate(pageUri)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void NavigateTo_ShouldNavigateWithParameter_WhenParameterIsProvided()
  {
    // Arrange
    var pageUri = new Uri("http://example.com");
    var parameter = new {Name = "TestParam"};
    A.CallTo(() => _pageResolverMock.GetPageUri("ValidKey")).Returns(pageUri);

    // Act
    _navigationService.NavigateTo("ValidKey", parameter);

    // Assert
    A.CallTo(() => _navigationFrameMock.Navigate(pageUri, parameter)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void NavigateTo_ShouldStoreParameter()
  {
    // Arrange
    var pageUri = new Uri("http://example.com");
    var parameter = new {Name = "StoredParam"};
    A.CallTo(() => _pageResolverMock.GetPageUri("ValidKey")).Returns(pageUri);

    // Act
    _navigationService.NavigateTo("ValidKey", parameter);
    var retrievedParameter = _navigationService.TryGetParameter<object>("ValidKey");

    // Assert
    retrievedParameter.Should().BeSameAs(parameter);
  }

  [Fact]
  public void GoBack_ShouldCallNavigationFrameGoBack_WhenPossible()
  {
    // Arrange
    A.CallTo(() => _navigationFrameMock.CanGoBack).Returns(true);

    // Act
    _navigationService.GoBack();

    // Assert
    A.CallTo(() => _navigationFrameMock.GoBack()).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void GoBack_ShouldNotCallGoBack_WhenFrameCannotGoBack()
  {
    // Arrange
    A.CallTo(() => _navigationFrameMock.CanGoBack).Returns(false);

    // Act
    _navigationService.GoBack();

    // Assert
    A.CallTo(() => _navigationFrameMock.GoBack()).MustNotHaveHappened();
  }

  [Fact]
  public void GoBack_ShouldRemoveStoredParameter()
  {
    // Arrange
    var pageUri = new Uri("http://example.com");
    var parameter = new {Name = "OldParam"};
    A.CallTo(() => _pageResolverMock.GetPageUri("ValidKey")).Returns(pageUri);
    A.CallTo(() => _navigationFrameMock.CanGoBack).Returns(true);

    _navigationService.NavigateTo("ValidKey", parameter);

    // Act
    _navigationService.GoBack();
    var retrievedParameter = _navigationService.TryGetParameter<object>("ValidKey");

    // Assert
    retrievedParameter.Should().BeNull();
  }
}
