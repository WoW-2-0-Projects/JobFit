namespace JobFit.Application.Employees.Models;

public sealed record EmployeeDto
{
    /// <summary>
    /// Gets employee first name
    /// </summary>
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Gets employee last name
    /// </summary>
    public string LastName { get; init; } = string.Empty;
    
    /// <summary>
    /// Gets the phone number of the employee.
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    
    /// <summary>
    /// Gets the Email address of the employee.
    /// </summary>
    public string EmailAddress { get; set; } = default!;
}