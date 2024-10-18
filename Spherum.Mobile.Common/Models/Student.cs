namespace Spherum.Mobile.Common.Models;

public sealed partial class Student : ObservableObject
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Nationality { get; set; }

    public Contact? ContactInformation { get; set; }

    public AcademicInfo? AcademicInformation { get; set; }

    public AdditionalInfo? AdditionalInformation { get; set; }

    [JsonIgnore]
    [ObservableProperty]
    bool _isSelected;
}