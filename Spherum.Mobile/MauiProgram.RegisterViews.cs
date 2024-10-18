namespace Spherum.Mobile;

public static partial class MauiProgram
{
    static void RegisterViews(this IServiceCollection services)
    {
        // singleton
        services.TryAddSingleton<HomePage>(s => new HomePage(s.ResolveService<HomePageViewModel>()));

        // transient
        services.TryAddTransient<StudentDetailsPage>();
        services.TryAddTransient<AddNewStudentPage>();

        // scoped

    }
}