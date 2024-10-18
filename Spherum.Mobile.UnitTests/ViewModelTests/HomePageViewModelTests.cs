namespace Spherum.Mobile.UnitTests.ViewModelTests;

public class HomePageViewModelTests
{
    readonly IShell _shell = Substitute.For<IShell>();
    readonly ILogger<HomePageViewModel> _logger = Substitute.For<ILogger<HomePageViewModel>>();
    readonly IStudent _student = Substitute.For<IStudent>();
    readonly HomePageViewModel _sut;

    public HomePageViewModelTests()
    {
        _sut = new HomePageViewModel(_shell, _logger, _student);
    }

    [Fact]
    public void ApplyQueryAttributes_WhenQueryIsEmpty_ShouldNotProceed()
    {
        // Arrange
        var query = new Dictionary<string, object?>();

        //Act
        _sut.ApplyQueryAttributes(query);

        // Assert
        query.Should().BeEmpty();
    }

    [Fact]
    public void ApplyQueryAttributes_WhenQueryDoesNotContainNewStudent_ShouldReturn()
    {
        // Arrange
        var query = new Dictionary<string, object?>();

        //Act
        _sut.ApplyQueryAttributes(query);

        // Assert
        query.ContainsKey("NewStudent").Should().BeFalse();
    }

    [Fact]
    public void ApplyQueryAttributes_WhenStudentIsNotOfTypeStudent_ShouldReturn()
    {
        // Arrange
        var query = new Dictionary<string, object?> { { "NewStudent", "Other" } };

        //Act
        _sut.ApplyQueryAttributes(query);

        // Assert
        query["NewStudent"].Should().NotBeOfType<Student>();
    }

    [Fact]
    public void ApplyQueryAttributes_ShouldInsertStudent()
    {
        // Arrange
        var fakeStudent = FakeStudent.GetAStudent();
        var query = new Dictionary<string, object?> { { "NewStudent",  fakeStudent} };
        _sut.StudentsList = new ObservableCollection<Student>(FakeStudent.GetStudents(10));

        //Act
        _sut.ApplyQueryAttributes(query);

        // Assert
        _sut.StudentsList.Should().Contain(fakeStudent);
        _sut.StudentsList.First().Should().Be(fakeStudent);
    }

    [Fact]
    public async Task InItAsync_ShouldShouldNotGetStudents_WhenAlreadyLoaded()
    {
        // Arrange
        _sut.AlreadyLoaded = true;

        //Act
        await _sut.InitAsync();

        // Assert
        await _student.DidNotReceive().GetStudentsAsync(default);
    }

    [Fact]
    public async Task InItAsync_ShouldShouldGetStudents()
    {
        // Arrange
        _sut.AlreadyLoaded = false;
        var fakeStudents = new ObservableCollection<Student>(FakeStudent.GetStudents(50));
        _student.GetStudentsAsync(50).Returns(fakeStudents);

        //Act
        await _sut.InitAsync();

        // Assert
        await _student.Received(1).GetStudentsAsync(50);
        _sut.IsBusy.Should().BeFalse();
        _sut.StudentsList.Should().BeEquivalentTo(fakeStudents);
        _sut.AlreadyLoaded.Should().BeTrue();
        _logger.AssertLoggerIsCalled(LogLevel.Debug, 2);
    }

    [Fact]
    public async Task FetchMoreStudentsCommand_ShouldReturnMoreStudents()
    {
        // Arrange
        var fakeStudents = new ObservableCollection<Student>(FakeStudent.GetStudents(50));
        _student.GetStudentsAsync(50).Returns(fakeStudents);

        // Act
        _sut.FetchMoreStudentsCommand.Execute(null);

        // Assert
        await _student.Received(1).GetStudentsAsync(50);
        _sut.IsBusy.Should().BeFalse();
        _sut.StudentsList.Should().Contain(fakeStudents);
        _sut.AlreadyLoaded.Should().BeTrue();
        _logger.AssertLoggerIsCalled(LogLevel.Debug, 2);
    }

    [Fact]
    public async Task FetchMoreStudentsCommand_ShouldHandleEmptyResult()
    {
        // Arrange
        _student.GetStudentsAsync(50).Returns([]);

        // Act
        _sut.FetchMoreStudentsCommand.Execute(null);

        // Assert
        await _student.Received(1).GetStudentsAsync(50);
        _sut.StudentsList.Should().NotBeNull();
        _sut.StudentsList.Should().BeEmpty();
    }

    [Fact]
    public async Task NavigateToStudentFormCommand_ShouldNavigateToStudentForm()
    {
        // Arrange + Act
        _sut.NavigateToStudentFormCommand.Execute(null);

        // Assert
        _logger.AssertLoggerIsCalledWithExactMessage(LogLevel.Debug, 1,"Navigating to add new student page...");
        _sut.LoadingMessage.Should().Be("Navigating to student form, please wait...");
        await _shell.Received().GotoAsync(Arg.Any<string>());
        _sut.IsBusy.Should().BeFalse();
    }

    [Fact]
    public async Task NavigateToStudentDetailPageCommand_ShouldNavigateToStudentDetailPage()
    {
        // Arrange + Act
        _sut.NavigateToStudentDetailPageCommand.Execute(null);

        // Assert
        _logger.AssertLoggerIsCalledWithExactMessage(LogLevel.Debug, 1,"Navigating to student detail page...");
        _sut.LoadingMessage.Should().Be("Navigating to student details page, please wait...");
        await _shell.Received().GotoAsync(Arg.Any<string>(), Arg.Any<Dictionary<string, object?>>());
        _sut.IsBusy.Should().BeFalse();
    }
}