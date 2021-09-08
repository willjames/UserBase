using System;

namespace UserBase.Features.User
{
    public class UserRecord
    {
        public Guid Id { get; set; }
        public DateTime DateRegistered { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
