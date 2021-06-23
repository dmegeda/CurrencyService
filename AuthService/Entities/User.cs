using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AuthService.Entities
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PasswordHash { get; set; }

        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
