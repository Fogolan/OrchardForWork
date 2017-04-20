namespace AllowedRoles.Models
{
    public class AllowedRoleRecord
    {
        public virtual int Id { get; set; }
        public virtual string AllowedRoleName { get; set; }
        public virtual string RoleName { get; set; }
    }
}