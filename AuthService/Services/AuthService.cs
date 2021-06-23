﻿using AuthService.Abstracts;
using AuthService.Entities;
using AuthService.Infrastructure;
using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class AuthService : IAuthService
    {
        IUserContext _context;

        public AuthService(IUserContext context)
        {
            _context = context;
        }

        public void Register(UserModel userModel)
        {
            _context.LoadData();
            string email = userModel.Email;
            string passwordHash = Hash(userModel.Password);
            User user = new User(email, passwordHash);

            if (IsUserExists(userModel))
            {
                throw new ArgumentException("User already exists!");
            }

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
            //to do: validating
            return false;
        }

        public bool IsEmailCorrect(string email)
        {
            //to do: validating
            return false;
        }

        private string Hash(string password)
        {
            var bytes = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
