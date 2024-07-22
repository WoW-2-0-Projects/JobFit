namespace JobFit.Domain.Entities;

public class Recruiter: User
{

    public ICollection<SkillSet>? SkillSets { get; set; } = new List<SkillSet>();
}