using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Abstracts
{
    public interface IAuthService
    {
        public void Register(UserModel user);
        public void Login(UserModel user);
    }
}
