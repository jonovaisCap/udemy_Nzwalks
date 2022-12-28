using System;

namespace NzWalks.Model.Domain
{
    public class User_Role
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        //Navigation Properties
        
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
