using FakeItEasy;
using FluentAssertions;
using System.Linq;
using System.Windows;
using WpfDemo.ViewModels;
using WpfEssentials.Services;
using Xunit;

namespace WpfDemoTests.ViewModels;

public class ModalVmTests
{
  private readonly IDialogService _dialogServiceMock;
  private readonly INavigationService _navigationServiceMock;
  private readonly ModalVm _modalVm;

  public ModalVmTests()
  {
    _dialogServiceMock = A.Fake<IDialogService>();
    _navigationServiceMock = A.Fake<INavigationService>();
    _modalVm = new ModalVm(_dialogServiceMock, _navigationServiceMock);
  }

  [Fact]
  public void ModalVm_ShouldInitializeWithDependencies()
  {
    // Assert
    _modalVm.Should().NotBeNull();
    _modalVm.NavigationService.Should().BeSameAs(_navigationServiceMock);
    _modalVm.DialogService.Should().BeSameAs(_dialogServiceMock);
  }
}
