namespace Spherum.Mobile.Views.AddNewStudent;

public partial class AddNewStudentPage
{
    public AddNewStudentPage(AddNewStudentPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}