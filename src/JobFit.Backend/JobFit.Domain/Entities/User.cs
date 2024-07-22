using JobFit.Domain.Common.Entities;

namespace JobFit.Domain.Entities;

public class User : EntityBase
{
    /// <summary>
    /// Gets or sets user first name
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets user last name
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets user phone number
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the email address of the user
    /// </summary>
    public string EmailAddress { get; set; } = default!;
}