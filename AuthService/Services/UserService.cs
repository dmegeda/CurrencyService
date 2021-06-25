using AuthService.Abstracts;
using AuthService.Entities;
using AuthService.Infrastructure;
using AuthService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class UserService : IUserService
    {
        IUserContext _context;

        public UserService(IUserContext context)
        {
            _context = context;
        }

        public void Register(UserModel userModel)
        {
            _context.LoadData();

            if (!IsEmailCorrect(userModel.Email))
            {
                throw new ArgumentException("Email is invalid!");
            }

            if (!IsPasswordCorrect(userModel.Password))
            {
                throw new ArgumentException("Password is invalid");
            }
            
            if (IsUserExists(userModel))
            {
                throw new ArgumentException("User already exists!");
            }

            string passwordHash = Hash(userModel.Password);
            User user = new User(userModel.Email, passwordHash);

            _context.Users.Add(user);
            _context.WriteData();
        }

        public void Login(UserModel userModel)
        {
            _context.LoadData();
            if (!IsUserExists(userModel))
            {
                throw new ArgumentException("User not found!");
            }

            var user = _context.Users.FirstOrDefault(x => x.Email == userModel.Email);
            string passwordHash = Hash(userModel.Password);

            if (user.PasswordHash != passwordHash)
            {
                throw new ArgumentException("Passwords do not match!");
            }
        }

        public bool IsUserExists(UserModel userModel)
        {
            return _context.Users.FirstOrDefault(x => x.Email == userModel.Email) != null;
        }

        public bool IsPasswordCorrect(string password)
        {
            if (password.Length < 3 || password.Length > 100) return false;

            return true;
        }

        public bool IsEmailCorrect(string email)
        {
            if (email == null) return false;

            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.
                    [a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+
                    [a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            return Regex.IsMatch(email, pattern);
        }

        private string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
