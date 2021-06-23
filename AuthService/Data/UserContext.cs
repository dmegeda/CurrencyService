using AuthService.Abstracts;
using AuthService.Entities;
using AuthService.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Data
{
    public class UserContext : IUserContext
    {
        public List<User> Users { get; set; }
        private string _usersPath;

        public UserContext()
        {
            _usersPath = Directory.GetCurrentDirectory() + "\\Data\\users.json";
            LoadData();
        }

        public void LoadData()
        {
            Users = JSONSerialization<User>.Deserialize(_usersPath);

            if (Users == null)
            {
                Users = new List<User>();
            }
        }

        public void WriteData()
        {
            JSONSerialization<User>.Serialize(Users, _usersPath);
        }
    }
}
