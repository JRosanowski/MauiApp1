using Microsoft.Extensions.Logging;

namespace MauiApp1;

public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    try
    {
      var builder = MauiApp.CreateBuilder();
      builder
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
          fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
          fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

#if DEBUG
    builder.Logging.AddDebug();
#endif
      
      return builder.Build();
    }
    catch (Exception ex)
    {
      string path = Path.Combine(GetScopedStorageFolder(), "MauiApp1.txt");
      File.AppendAllText(path, $"MESSAGE: {ex.InnerException.Message}\nSTACK: {ex.InnerException.StackTrace}\n\n");
      //throw;
      return null;
    }
    
    static string GetScopedStorageFolder()
    {
      // If there's no access to scoped storage then just revert to the internal drive. This
      // only occurs on some old devices like the MM50. In this case we're just trying to prevent
      // any crashes rather than totally solve the problem.
      Java.IO.File file = Android.App.Application.Context.GetExternalFilesDir(null);
      return file == null
        ? Path.Combine(Android.App.Application.Context.FilesDir.AbsolutePath, "Scoped")
        : file.AbsolutePath;
    }
  }
}