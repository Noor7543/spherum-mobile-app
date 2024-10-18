namespace Spherum.Mobile.ViewModels.StudentDetails;

public sealed partial class StudentDetailsPageViewModel : BaseViewModel
{
    public StudentDetailsPageViewModel(IShell shell) : base(shell)
    {
    }

    [ObservableProperty]
    Student? _selectedStudent;

    public override void ApplyQueryAttributes(IDictionary<string, object?> query)
    {
        if (!query.Any())
        {
            return;
        }

        if (!query.TryGetValue("SelectedStudent", out var student))
        {
            return;
        }

        if (student is not Student selectedStudent)
        {
            return;
        }

        SelectedStudent = selectedStudent;
    }
}