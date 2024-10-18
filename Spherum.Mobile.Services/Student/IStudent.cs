namespace Spherum.Mobile.Services.Student;

/// <summary>
/// Represents the service for retrieving student data.
/// </summary>
public interface IStudent
{
    /// <summary>
    /// Asynchronously retrieves a collection of students with a specified page size.
    /// </summary>
    /// <param name="pageSize">The number of students to retrieve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains
    /// an <see cref="ObservableCollection{T}"/> of <see cref="Common.Models.Student"/> objects.
    /// </returns>
    Task<ObservableCollection<Common.Models.Student>> GetStudentsAsync(int pageSize);
}