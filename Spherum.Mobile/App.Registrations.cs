namespace Spherum.Mobile;

public partial class App
{
    void RegisterRoutes()
    {
        // register all view routes here...
        Routing.RegisterRoute(nameof(StudentDetailsPage), typeof(StudentDetailsPage));
        Routing.RegisterRoute(nameof(AddNewStudentPage), typeof(AddNewStudentPage));
    }
}