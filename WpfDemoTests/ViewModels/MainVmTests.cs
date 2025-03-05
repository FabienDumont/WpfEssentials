using FakeItEasy;
using FluentAssertions;
using WpfDemo.ViewModels;
using WpfEssentials.Services;
using Xunit;

namespace WpfDemoTests.ViewModels;

public class MainVmTests
{
  private readonly IDialogService _dialogServiceMock;
  private readonly INavigationService _navigationServiceMock;
  private readonly MainVm _mainVm;

  public MainVmTests()
  {
    _dialogServiceMock = A.Fake<IDialogService>();
    _navigationServiceMock = A.Fake<INavigationService>();
    _mainVm = new MainVm(_dialogServiceMock, _navigationServiceMock);
  }

  [Fact]
  public void Loading_ShouldNavigateToHomePage()
  {
    // Act
    _mainVm.LoadingCommand.Execute(null);

    // Assert
    A.CallTo(() => _navigationServiceMock.NavigateTo("HomePage")).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void MainVm_ShouldInitializeWithDependencies()
  {
    // Assert
    _mainVm.Should().NotBeNull();
    _mainVm.NavigationService.Should().BeSameAs(_navigationServiceMock);
    _mainVm.DialogService.Should().BeSameAs(_dialogServiceMock);
  }
}
