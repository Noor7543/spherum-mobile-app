namespace Spherum.Mobile.Common.Fake;

public static class FakeStudent
{
    public static List<Student> GetStudents(int count)
    {
        var fakeStudent = GenerateFakeStudent();

        return fakeStudent.Generate(count);
    }

    public static Student GetAStudent()
    {
        return GenerateFakeStudent().Generate();
    }

    public static AcademicInfo GetAcademicInfo()
    {
        return new Faker<AcademicInfo>().RuleFor(a => a.EnrollmentNumber, f => f.Random.AlphaNumeric(10))
                                        .RuleFor(a => a.Course,
                                                 f => f.PickRandom("Computer Science", "Mathematics", "Physics",
                                                                   "Engineering"))
                                        .RuleFor(a => a.Department, f => f.Company.CompanyName())
                                        .RuleFor(a => a.Year, f => f.PickRandom("1st", "2nd", "3rd", "4th"))
                                        .RuleFor(a => a.GPA, f => Math.Round(f.Random.Double(2.0, 4.0), 2))
                                        .RuleFor(a => a.EnrollmentDate, f => f.Date.Past(3))
                                        .RuleFor(a => a.Subjects,
                                                 f => f.Make(
                                                     5,
                                                     () => f.PickRandom("Algebra", "Data Structures",
                                                                        "Operating Systems", "Machine Learning",
                                                                        "Linear Algebra")))
                                        .Generate();
    }

    public static AdditionalInfo GetAdditionalInfo()
    {
        return new Faker<AdditionalInfo>().RuleFor(a => a.EmergencyContactName, f => f.Name.FullName())
                                          .RuleFor(a => a.EmergencyContactPhone, f => f.Phone.PhoneNumber())
                                          .RuleFor(a => a.GuardianName, f => f.Name.FullName())
                                          .RuleFor(a => a.GuardianRelation,
                                                   f => f.PickRandom("Father", "Mother", "Guardian"))
                                          .RuleFor(a => a.GuardianContact, f => f.Phone.PhoneNumber())
                                          .Generate();
    }

    public static Contact GetContact()
    {
        return new Faker<Contact>().RuleFor(c => c.Email, f => f.Internet.Email())
                                   .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
                                   .RuleFor(c => c.Address, f => f.Address.StreetAddress())
                                   .RuleFor(c => c.City, f => f.Address.City())
                                   .RuleFor(c => c.State, f => f.Address.State())
                                   .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
                                   .RuleFor(c => c.Country, f => f.Address.Country())
                                   .Generate();
    }

    static Faker<Student> GenerateFakeStudent()
    {
        return new Faker<Student>().RuleFor(s => s.Id, f => f.Random.Number(10000000, 99999999))
                                   .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                                   .RuleFor(s => s.LastName, f => f.Name.LastName())
                                   .RuleFor(s => s.DateOfBirth, f => f.Date.Past(25, DateTime.Now.AddYears(-18)))
                                   .RuleFor(s => s.Gender, f => f.PickRandom("Male", "Female", "Non-binary"))
                                   .RuleFor(s => s.Nationality, f => f.Address.Country())
                                   .RuleFor(s => s.ContactInformation, f => GetContact())
                                   .RuleFor(s => s.AcademicInformation, f => GetAcademicInfo())
                                   .RuleFor(s => s.AdditionalInformation, f => GetAdditionalInfo());
    }
}