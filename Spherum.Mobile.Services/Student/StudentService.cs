namespace Spherum.Mobile.Services.Student;

public sealed class StudentService : IStudent
{
    readonly ILogger<StudentService> _logger;

    public StudentService(ILogger<StudentService> logger)
    {
        _logger = logger;
    }

    public async Task<ObservableCollection<Common.Models.Student>> GetStudentsAsync(int pageSize)
    {
        _logger.LogInformation("Getting students ...");

        await Task.Delay(1000);

        var students = FakeStudent.GetStudents(pageSize);

        _logger.LogInformation("Successfully retrieved students {@students}", students);

        return new ObservableCollection<Common.Models.Student>(students);
    }
}