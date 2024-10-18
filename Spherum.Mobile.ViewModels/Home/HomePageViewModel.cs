namespace Spherum.Mobile.ViewModels.Home;

public sealed partial class HomePageViewModel : BaseViewModel
{
    readonly IShell _shell;
    readonly ILogger<HomePageViewModel> _logger;
    readonly IStudent _student;

    public HomePageViewModel(IShell shell, ILogger<HomePageViewModel> logger, IStudent student) : base(shell)
    {
        _shell = shell;
        _logger = logger;
        _student = student;

        UseObtrusiveOverlay = true;
    }

    [ObservableProperty]
    ObservableCollection<Student>? _studentsList = [];

    [ObservableProperty]
    Student? _selectedStudent;

    [ObservableProperty]
    bool _alreadyLoaded;

    public override void ApplyQueryAttributes(IDictionary<string, object?> query)
    {
        if (!query.Any())
        {
            return;
        }

        if (!query.TryGetValue("NewStudent", out var student))
        {
            return;
        }

        if (student is not Student newStudent)
        {
            return;
        }

        StudentsList?.Insert(0, newStudent);
    }

    public override async Task InitAsync()
    {
        if (AlreadyLoaded)
        {
            return;
        }

        StudentsList = await GetStudentsAsync();
    }

    [RelayCommand]
    async Task FetchMoreStudentsAsync()
    {
        var students = await GetStudentsAsync();

        foreach (var item in students)
        {
            StudentsList?.Add(item);
        }
    }

    [RelayCommand]
    async Task NavigateToStudentFormAsync()
    {
        _logger.LogDebug("Navigating to add new student page...");

        LoadingMessage = AppStrings.NavigatingToStudentFormMessage;

        IsBusy = true;

        await _shell.GotoAsync("AddNewStudentPage");

        IsBusy = false;
    }

    [RelayCommand]
    async Task NavigateToStudentDetailPageAsync()
    {
        _logger.LogDebug("Navigating to student detail page...");

        LoadingMessage = AppStrings.NavigatingToStudentDetailsMessage;

        IsBusy = true;
        var parameter =  new Dictionary<string, object?> { { nameof(SelectedStudent), SelectedStudent } };

        await _shell.GotoAsync("StudentDetailsPage", parameter);

        IsBusy = false;
    }

    async Task<ObservableCollection<Student>> GetStudentsAsync()
    {
        AlreadyLoaded = true;

        IsBusy = true;

        _logger.LogDebug("Getting students please wait...");

        LoadingMessage = AppStrings.GettingStudentsMessage;

        var students = await _student.GetStudentsAsync(50);

        _logger.LogDebug("Retrieved students, count: {students}", students.Count);

        IsBusy = false;

        return students;
    }
}