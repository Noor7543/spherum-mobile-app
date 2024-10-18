namespace Spherum.Mobile.ViewModels.AddNewStudent;

public sealed partial class AddNewStudentPageViewModel : BaseViewModel
{
    readonly IShell _shell;

    public AddNewStudentPageViewModel(IShell shell) : base(shell)
    {
        _shell = shell;

        UseObtrusiveOverlay = true;
    }

    [ObservableProperty]
    string? _firstName;

    [ObservableProperty]
    string? _lastName;

    [ObservableProperty]
    string? _nationality;

    [ObservableProperty]
    string? _streetAddress;

    [ObservableProperty]
    string? _city;

    [ObservableProperty]
    string? _state;

    [ObservableProperty]
    string? _country;

    [ObservableProperty]
    string? _zipCode;

    [ObservableProperty]
    string? _email;

    [ObservableProperty]
    string? _phone;

    [ObservableProperty]
    bool _firstNameHasError;

    [ObservableProperty]
    bool _lastNameHasError;

    [ObservableProperty]
    bool _nationalityHasError;

    [ObservableProperty]
    bool _streetAddressHasError;

    [ObservableProperty]
    bool _cityHasError;

    [ObservableProperty]
    bool _stateHasError;

    [ObservableProperty]
    bool _countryHasError;

    [ObservableProperty]
    bool _zipCodeHasError;

    [ObservableProperty]
    bool _emailHasError;

    [ObservableProperty]
    bool _phoneHasError;

    [RelayCommand]
    async Task AddNewStudent()
    {
        if (!ValidateEntries())
        {
            return;
        }

        var contact = new Contact
        {
            Address = StreetAddress,
            City = City,
            State = State,
            Country = Country,
            PostalCode = ZipCode,
            Email = Email,
            Phone = Phone
        };

        var student = new Student
        {
            FirstName = FirstName,
            LastName = LastName,
            Nationality = Nationality,
            ContactInformation = contact
        };

        IsBusy = true;

        await Task.Delay(1000);

        var parameter = new Dictionary<string, object?> { { "NewStudent", student } };

        await _shell.GotoAsync("..", parameter);

        await _shell.DisplayToastAsync(AppStrings.StudentAddedMessage);

        IsBusy = false;
    }

    bool ValidateEntries()
    {
        FirstNameHasError = CheckError(FirstName, out var firstNameValid);
        LastNameHasError = CheckError(LastName, out var lastNameValid);
        NationalityHasError = CheckError(Nationality, out var nationalityValid);
        StreetAddressHasError = CheckError(StreetAddress, out var streetAddressValid);
        CityHasError = CheckError(City, out var cityValid);
        StateHasError = CheckError(State, out var stateValid);
        CountryHasError = CheckError(Country, out var countryValid);
        ZipCodeHasError = CheckError(ZipCode, out var zipCodeValid);
        EmailHasError = !IsValidEmail(Email, out var emailValid);
        PhoneHasError = CheckError(Phone, out var phoneValid);

        return firstNameValid && lastNameValid && nationalityValid && streetAddressValid &&
               cityValid && stateValid && countryValid && zipCodeValid && emailValid && phoneValid;
    }

    static bool CheckError(string? input, out bool isValid)
    {
        isValid = !string.IsNullOrWhiteSpace(input);
        return !isValid;
    }

    static bool IsValidEmail(string? email, out bool isValid)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            isValid = false;
            return false;
        }

        isValid = MyRegex().IsMatch(email);
        return isValid;
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, "en-PK")]
    private static partial Regex MyRegex();
}