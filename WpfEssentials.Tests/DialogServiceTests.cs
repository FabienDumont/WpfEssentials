using System;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using WpfEssentials.Core;
using WpfEssentials.Services;
using Xunit;

namespace WpfEssentials.Tests;

public class DialogServiceTests
{
  private readonly IViewLocator _viewLocatorMock;
  private readonly IToastService _toastServiceMock;
  private readonly DialogService _dialogService;

  public DialogServiceTests()
  {
    _viewLocatorMock = A.Fake<IViewLocator>();
    _toastServiceMock = A.Fake<IToastService>();
    _dialogService = new DialogService(_viewLocatorMock, _toastServiceMock);
  }

  [Fact]
  public async Task ShowWarning_ShouldCallToastServiceWithWarning()
  {
    // Act
    await _dialogService.ShowWarning("Warning message");

    // Assert
    A.CallTo(() => _toastServiceMock.ShowToast("Warning message", DialogImage.Warning)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public async Task ShowError_ShouldCallToastServiceWithError()
  {
    // Act
    await _dialogService.ShowError("Error message");

    // Assert
    A.CallTo(() => _toastServiceMock.ShowToast("Error message", DialogImage.Error)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public async Task ShowError_WithException_ShouldCallToastServiceWithErrorMessage()
  {
    // Arrange
    var exception = new Exception("Outer", new Exception("Inner"));

    // Act
    await _dialogService.ShowError(exception);

    // Assert
    A.CallTo(() => _toastServiceMock.ShowToast("OuterInner", DialogImage.Error)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public async Task ShowMessage_ShouldCallToastServiceWithInformation()
  {
    // Act
    await _dialogService.ShowMessage("Info message");

    // Assert
    A.CallTo(() => _toastServiceMock.ShowToast("Info message", DialogImage.Information)).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void ShowDialog_WithUnregisteredView_ShouldThrowException()
  {
    // Arrange
    var fakeViewModel = new object();

    A.CallTo(() => _viewLocatorMock.GetViewType(typeof(object))).Returns(null);

    // Act
    Action act = () => _dialogService.ShowDialog(fakeViewModel);

    // Assert
    act.Should().Throw<InvalidOperationException>().WithMessage("No registered view for Object");
  }
}
