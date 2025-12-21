#if WINDOWS
using Microsoft.Maui.Platform;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Microsoft.Maui.Storage;
using SkiaSharp;
#endif

// App.xaml.cs
namespace World.Execute;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = new Window();

		var hostPage = new ContentPage
		{
			Padding = 0,
			BackgroundColor = Colors.Black
		};

		window.Page = hostPage;

		Microsoft.Maui.Controls.Application.Current!.UserAppTheme = AppTheme.Dark;

		window.Dispatcher.Dispatch(async () =>
		{
#if WINDOWS
			using var stream =
				await FileSystem.OpenAppPackageFileAsync("PxPlus_IBM_VGA_8x16.ttf");

			var typeface = SKTypeface.FromStream(stream);

			WorldInterface.Create(window, typeface);

			// WorldInterface.WriteLine("World.Execute(Me)");
			// WorldInterface.WriteLine("Running fullscreen.");

			await WorldRuntime.RunAsync();
#endif
		});

		return window;
	}

}
