namespace Spherum.Mobile;

public static partial class MauiProgram
{
    static void RegisterServices(this IServiceCollection services)
    {
        // singleton
        services.TryAddSingleton<IShell>(s => new ShellService(s.ResolveLogger<ShellService>()));

        services.TryAddSingleton<IStudent>(s => new StudentService(s.ResolveLogger<StudentService>()));

        // transient

        // scoped
    }
}