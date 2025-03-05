using System;
using System.Threading.Tasks;

namespace WpfEssentials.Services;

public interface IDialogService
{
  #region Methods

  Task ShowWarning(string message);
  Task ShowError(string message);
  Task ShowError(Exception error);
  Task ShowMessage(string message);
  bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : class;

  #endregion
}
