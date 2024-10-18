namespace Spherum.Mobile;

public partial class App
{
    public App()
    {
        InitializeComponent();
        RegisterRoutes();

        MainPage = new AppShell();
    }
}