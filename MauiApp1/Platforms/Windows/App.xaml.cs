using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using Windows.UI.ViewManagement;
using WinRT.Interop;
using static PInvoke.User32;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiApp1.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
	/// <summary>
	/// Initializes the singleton application object.  This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
		this.InitializeComponent();
    }

	protected override void OnLaunched(LaunchActivatedEventArgs args) {
		base.OnLaunched(args);


        var currentWindow = Application.Windows[0].Handler.PlatformView;
        IntPtr _windowHandle = WindowNative.GetWindowHandle(currentWindow);
        var windowId = Win32Interop.GetWindowIdFromWindow(_windowHandle);

        //// https://docs.microsoft.com/en-us/windows/apps/winui/winui3/desktop-winui3-app-with-basic-interop
        //SetWindowDetails(_windowHandle, 1920, 1080);
        //m_window.Activate();
       
        AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
        appWindow.TitleBar.ExtendsContentIntoTitleBar = false;

        //appWindow.Resize(new SizeInt32(1920, 1080));
        //
        appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
    }

    private static void SetWindowDetails(IntPtr hwnd, int width, int height) {
        var dpi = GetDpiForWindow(hwnd);
        float scalingFactor = (float)dpi / 96;
        width = (int)(width * scalingFactor);
        height = (int)(height * scalingFactor);

        _ = SetWindowPos(hwnd, SpecialWindowHandles.HWND_TOP,
                                    0, 0, width, height,
                                    SetWindowPosFlags.SWP_NOMOVE);
        _ = SetWindowLong(hwnd,
               WindowLongIndexFlags.GWL_STYLE,
               (SetWindowLongFlags)(GetWindowLong(hwnd,
                  WindowLongIndexFlags.GWL_STYLE) &
                  ~(int)SetWindowLongFlags.WS_MINIMIZEBOX &
                  ~(int)SetWindowLongFlags.WS_MAXIMIZEBOX));
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

