using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;

namespace WpfEssentials.Controls;

public partial class ToastMessageBox
{
  #region Dependencies

  public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
    nameof(Image), typeof(DialogImage), typeof(ToastMessageBox),
    new UIPropertyMetadata(DialogImage.None, OnImagePropertyChanged)
  );

  private static void OnImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is ToastMessageBox messageBox && e.NewValue is DialogImage image)
    {
      if (image == DialogImage.None)
      {
        messageBox.Tag = PackIconKind.None;
        messageBox.Background = new SolidColorBrush(Color.FromRgb(200, 213, 220));
        messageBox.Foreground = new SolidColorBrush(Color.FromRgb(34, 34, 34));
      }
      else if (image == DialogImage.Error)
      {
        messageBox.Tag = PackIconKind.Error;
        messageBox.Background = new SolidColorBrush(Color.FromRgb(199, 69, 69));
      }
      else if (image == DialogImage.Warning)
      {
        messageBox.Tag = PackIconKind.Warning;
        messageBox.Background = new SolidColorBrush(Color.FromRgb(249, 169, 55));
      }
      else if (image == DialogImage.Information)
      {
        messageBox.Tag = PackIconKind.Information;
        messageBox.Background = new SolidColorBrush(Color.FromRgb(104, 165, 191));
      }
    }
  }

  public DialogImage Image
  {
    get => (DialogImage) GetValue(ImageProperty);
    set => SetValue(ImageProperty, value);
  }

  public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
    nameof(Message), typeof(string), typeof(ToastMessageBox)
  );

  public string Message
  {
    get => (string) GetValue(MessageProperty);
    init => SetValue(MessageProperty, value);
  }

  #endregion

  #region Fields

  private DispatcherTimer? _dispatcherTimer = new() {Interval = TimeSpan.FromSeconds(15)};

  #endregion

  #region Ctors

  public ToastMessageBox()
  {
    InitializeComponent();
  }

  #endregion

  #region Methods

  protected override void OnClosed(EventArgs e)
  {
    if (_dispatcherTimer != null)
    {
      _dispatcherTimer.Stop();
      _dispatcherTimer = null;
    }

    base.OnClosed(e);
  }

  private void ToastMessageBox_OnLoaded(object sender, RoutedEventArgs e)
  {
    var window = Application.Current.MainWindow;
    if (window != null)
    {
      var toasts = Application.Current.Windows.OfType<ToastMessageBox>().Where(_ => !Equals(this, _)).ToList();

      var top = toasts.Count == 0 ? 0d : toasts.Max(_ => _.Top);
      var lastToast = toasts.LastOrDefault(_ => _.Top.Equals(top)) ?? toasts.LastOrDefault();

      var owner = (FrameworkElement) window.Content;
      var scale = PresentationSource.FromVisual(owner)!.CompositionTarget?.TransformToDevice.M11 ?? 1d;

      var location = owner.PointToScreen(new Point(0, 0));
      Left = location.X / scale + GetWidth(owner) - 455;
      Top = GetHeight(lastToast, top == 0d ? location.Y / scale : top) + 5;
    }

    if (_dispatcherTimer == null)
    {
      return;
    }

    _dispatcherTimer.Tick += (_, _) =>
    {
      _dispatcherTimer.Stop();
      _dispatcherTimer = null;
      Close();
    };

    _dispatcherTimer.Start();
  }

  /// <summary>
  ///   Gets the width.
  /// </summary>
  /// <param name="window">The window.</param>
  private static double GetWidth(FrameworkElement? window)
  {
    if (window == null)
    {
      return 0d;
    }

    return double.IsNaN(window.Width) ? window.ActualWidth : window.Width;
  }

  private static double GetHeight(Window? window, double top)
  {
    if (window == null)
    {
      return top;
    }

    return (double.IsNaN(window.Height) ? window.ActualHeight : window.Height) + top;
  }

  private void ToastMessageBox_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
  {
    Close();
  }

  #endregion
}
