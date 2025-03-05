using WpfDemo.ViewModels;
using WpfEssentials.Services;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace WpfDemoTests.ViewModels;

public class HomeVmTests
{
  private readonly IDialogService _dialogServiceMock;
  private readonly INavigationService _navigationServiceMock;
  private readonly ModalVm _modalVmMock;
  private readonly HomeVm _homeVm;

  public HomeVmTests()
  {
    _dialogServiceMock = A.Fake<IDialogService>();
    _navigationServiceMock = A.Fake<INavigationService>();
    _modalVmMock = A.Fake<ModalVm>();
    _homeVm = new HomeVm(_dialogServiceMock, _navigationServiceMock, _modalVmMock);
  }

  [Fact]
  public void HomeVm_ShouldInitializeWithDependencies()
  {
    // Assert
    _homeVm.Should().NotBeNull();
    _homeVm.NavigationService.Should().BeSameAs(_navigationServiceMock);
    _homeVm.DialogService.Should().BeSameAs(_dialogServiceMock);
  }

  [Fact]
  public void NavigateAnotherCommand_ShouldCallNavigateTo_AnotherPage()
  {
    // Act
    _homeVm.NavigateAnotherCommand.Execute(null);

    // Assert
    A.CallTo(() => _navigationServiceMock.NavigateTo("AnotherPage")).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void NavigateModalCommand_ShouldCallShowDialog_WithModalVm()
  {
    // Act
    _homeVm.NavigateModalCommand.Execute(null);

    // Assert
    A.CallTo(() => _dialogServiceMock.ShowDialog(_modalVmMock)).MustHaveHappenedOnceExactly();
  }
}
