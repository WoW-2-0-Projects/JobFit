using JobFit.Domain.Common.Entities;

namespace JobFit.Domain.Entities;

public class Employee : User 
{
    // Navigation property
    public ICollection<StorageFile> Documents { get; set; } = new List<StorageFile>();
}