using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Abstracts
{
    public interface IUserContext
    {
        List<User> Users { get; set; }
        void LoadData();
        void WriteData();
    }
}
