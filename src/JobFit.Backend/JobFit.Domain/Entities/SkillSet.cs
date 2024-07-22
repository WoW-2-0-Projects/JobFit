using JobFit.Domain.Common.Entities;

namespace JobFit.Domain.Entities;

public class SkillSet : EntityBase
{
    public List<string>? Languages { get; set; }
    
    public List<string>? WorkExperiences { get; set; }
    
    public List<string>? Education { get; set; }
    
    public List<string>? Skills { get; set; }
    
    public Guid RecruiterId { get; set; }
    
    public Recruiter? Recruiter { get; set; }
}