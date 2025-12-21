using Microsoft.Maui.LifecycleEvents;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Windows.Graphics;
#endif

namespace World.Execute;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseSkiaSharp();
		// .ConfigureFonts(fonts =>
		// {
		// 	fonts.AddFont("PxPlus_IBM_VGA_8x16.ttf", "VGA");
		// });

#if WINDOWS
		builder.ConfigureLifecycleEvents(events =>
		{
			events.AddWindows(wndLifeCycleBuilder =>
			{
				wndLifeCycleBuilder.OnWindowCreated(window =>
				{
					IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
					WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);


					AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
					winuiAppWindow.SetPresenter(AppWindowPresenterKind.Overlapped);
					if (winuiAppWindow.Presenter is OverlappedPresenter p)
					{

						p.IsMaximizable = false;
						p.IsMinimizable = false;
						p.IsResizable = false;
						p.SetBorderAndTitleBar(false, false);
						// p.IsAlwaysOnTop = true;
						// p.IsModal = true;

						p.Maximize();
					}
					else
					{
						const int width = 1920;
						const int height = 1080;
						winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
					}
				});
			});
		});
#endif
		return builder.Build();
	}
}
