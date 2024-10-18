namespace Spherum.Mobile.Common.Models;

public sealed class AcademicInfo
{
    public string? EnrollmentNumber { get; set; }

    public string? Course { get; set; }

    public string? Department { get; set; }

    public string? Year { get; set; }

    public double? GPA { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public List<string>? Subjects { get; set; }
}