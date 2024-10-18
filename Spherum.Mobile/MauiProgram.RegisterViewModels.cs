namespace Spherum.Mobile;

public static partial class MauiProgram
{
    static void RegisterViewModels(this IServiceCollection services)
    {
        // singleton
        services.TryAddSingleton<HomePageViewModel>(s => new HomePageViewModel(s.ResolveService<IShell>(),
                                                                               s.ResolveLogger<HomePageViewModel>(),
                                                                               s.ResolveService<IStudent>()));

        // transient
        services.TryAddTransient<StudentDetailsPageViewModel>();
        services.TryAddTransient<AddNewStudentPageViewModel>();

        // scoped
    }
}