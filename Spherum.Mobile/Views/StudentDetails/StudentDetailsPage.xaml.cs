namespace Spherum.Mobile.Views.StudentDetails;

public partial class StudentDetailsPage
{
    public StudentDetailsPage(StudentDetailsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}