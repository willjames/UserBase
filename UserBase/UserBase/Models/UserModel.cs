using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserBase.Features.User;

namespace UserBase.Models
{
    public class UserModel
    {
        public UserRecord userRecord { get; set; }
        public List<UserRecord> userRecords { get; set; }

        public bool UserCreationSuccessful { get; set; }
    }
}