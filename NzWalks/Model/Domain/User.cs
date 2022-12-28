using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NzWalks.Model.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }
        
        public List<User_Role> UserRoles { get; set; }
    }
}
