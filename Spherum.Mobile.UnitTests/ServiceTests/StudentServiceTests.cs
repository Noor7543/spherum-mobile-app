namespace Spherum.Mobile.UnitTests.ServiceTests;

public class StudentServiceTests
{
    readonly ILogger<StudentService> _logger = Substitute.For<ILogger<StudentService>>();
    readonly StudentService _sut;

    public StudentServiceTests()
    {
        _sut = new StudentService(_logger);
    }

    [Fact]
    public async Task GetStudentsAsync_ShouldReturnStudents()
    {
        // Arrange
        const int PageSize = 10;

        // Act
        var students = await _sut.GetStudentsAsync(PageSize);

        // Assert
        _logger.AssertLoggerIsCalled(LogLevel.Information, 2);
        students.Should().NotBeNullOrEmpty();
        students.Count.Should().Be(PageSize);

        foreach (var student in students)
        {
            student.Id.Should().BePositive();
            student.FirstName.Should().NotBeNullOrWhiteSpace();
            student.LastName.Should().NotBeNullOrWhiteSpace();
            student.DateOfBirth.Should().BeBefore(DateTime.Now);
            student.Gender.Should().NotBeNullOrWhiteSpace();
            student.Nationality.Should().NotBeNullOrWhiteSpace();

            student.ContactInformation.Should().NotBeNull();
            student.ContactInformation?.Email.Should().NotBeNullOrWhiteSpace();
            student.ContactInformation?.Phone.Should().NotBeNullOrWhiteSpace();

            student.AcademicInformation.Should().NotBeNull();
            student.AcademicInformation?.Course.Should().NotBeNullOrWhiteSpace();
            student.AcademicInformation?.Department.Should().NotBeNullOrWhiteSpace();
            student.AcademicInformation?.EnrollmentDate.Should().BeBefore(DateTime.Now);

            student.AdditionalInformation.Should().NotBeNull();
            student.AdditionalInformation?.GuardianName.Should().NotBeNullOrWhiteSpace();
            student.AdditionalInformation?.EmergencyContactPhone.Should().NotBeNullOrWhiteSpace();
        }
    }
}