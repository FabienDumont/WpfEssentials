using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using WpfDemo.ViewModels;
using WpfEssentials.Services;
using Xunit;

namespace WpfDemoTests.ViewModels;

public class AnotherVmTests
{
  private readonly IDialogService _dialogServiceMock;
  private readonly INavigationService _navigationServiceMock;
  private readonly AnotherVm _anotherVm;

  public AnotherVmTests()
  {
    _dialogServiceMock = A.Fake<IDialogService>();
    _navigationServiceMock = A.Fake<INavigationService>();
    _anotherVm = new AnotherVm(_dialogServiceMock, _navigationServiceMock);
  }

  [Fact]
  public void AnotherVm_ShouldInitializeWithDependencies()
  {
    // Assert
    _anotherVm.Should().NotBeNull();
    _anotherVm.NavigationService.Should().BeSameAs(_navigationServiceMock);
    _anotherVm.DialogService.Should().BeSameAs(_dialogServiceMock);
    _anotherVm.Messages.Should().BeEmpty();
    _anotherVm.MessageToAdd.Should().BeEmpty();
  }

  [Fact]
  public async Task LoadDataCommand_ShouldCallShowMessage()
  {
    // Act
    await _anotherVm.LoadDataCommand.ExecuteAsync(null);

    // Assert
    A.CallTo(() => _dialogServiceMock.ShowMessage("Data loaded!")).MustHaveHappenedOnceExactly();
  }

  [Fact]
  public void AddMessageCommand_ShouldAddMessage_AndResetMessageToAdd()
  {
    // Arrange
    _anotherVm.MessageToAdd = "Test Message";

    // Act
    _anotherVm.AddMessageCommand.Execute(null);

    // Assert
    _anotherVm.Messages.Should().ContainSingle().Which.Should().Be("Test Message");
    _anotherVm.MessageToAdd.Should().BeEmpty();
  }

  [Fact]
  public void AddMessageCommand_ShouldNotAdd_WhenMessageToAddIsEmpty()
  {
    // Arrange
    _anotherVm.MessageToAdd = "";

    // Act
    _anotherVm.AddMessageCommand.Execute(null);

    // Assert
    _anotherVm.Messages.Should().BeEmpty();
  }

  [Fact]
  public void AddSpecificMessageCommand_ShouldAddMessage_WhenGivenString()
  {
    // Act
    _anotherVm.AddSpecificMessageCommand.Execute("Specific Message");

    // Assert
    _anotherVm.Messages.Should().ContainSingle().Which.Should().Be("Specific Message");
  }

  [Fact]
  public void AddSpecificMessageCommand_ShouldNotAddMessage_WhenGivenNull()
  {
    // Act
    _anotherVm.AddSpecificMessageCommand.Execute(null);

    // Assert
    _anotherVm.Messages.Should().BeEmpty();
  }

  [Fact]
  public void AddMessageCommand_ShouldBeDisabled_WhenMessagesCountIs10()
  {
    // Arrange
    for (int i = 0; i < 10; i++)
    {
      _anotherVm.Messages.Add($"Message {i}");
    }

    // Act
    var canExecute = _anotherVm.AddMessageCommand.CanExecute(null);

    // Assert
    canExecute.Should().BeFalse();
  }

  [Fact]
  public void AddSpecificMessageCommand_ShouldBeDisabled_WhenMessagesCountIs10()
  {
    // Arrange
    for (int i = 0; i < 10; i++)
    {
      _anotherVm.Messages.Add($"Message {i}");
    }

    // Act
    var canExecute = _anotherVm.AddSpecificMessageCommand.CanExecute("New Message");

    // Assert
    canExecute.Should().BeFalse();
  }

  [Fact]
  public void NavigateBackCommand_ShouldCallGoBack()
  {
    // Act
    _anotherVm.NavigateBackCommand.Execute(null);

    // Assert
    A.CallTo(() => _navigationServiceMock.GoBack()).MustHaveHappenedOnceExactly();
  }
}
