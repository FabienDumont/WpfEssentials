using WpfEssentials.Controls;

namespace WpfEssentials.Services;

public class ToastService : IToastService
{
  public bool ShowToast(string message, DialogImage image)
  {
    var toast = new ToastMessageBox { Message = message, Image = image };
    toast.Show();
    return true;
  }
}
