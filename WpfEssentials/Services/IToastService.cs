namespace WpfEssentials.Services;

public interface IToastService
{
  bool ShowToast(string message, DialogImage image);
}
