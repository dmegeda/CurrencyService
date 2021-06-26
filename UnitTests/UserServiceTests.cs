using AuthService.Data;
using AuthService.Services;
using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {
        [Test]
        [TestCase("currency..@gmail.com")]
        [TestCase(".CURrency.@gmail.com")]
        [TestCase("currency.gmail.com")]
        [TestCase("@")]
        public void EmailValidationTest_EmailInvalid_ReturnFalse(string email)
        {
            UserService auth = new UserService(new UserContext());

            Assert.IsFalse(auth.IsEmailCorrect(email));
        }

        [Test]
        [TestCase("currency123@gmail.com")]
        [TestCase("currency.Rate123@gmail.com")]
        public void EmailValidationTest_EmailValid_ReturnTrue(string email)
        {
            UserService auth = new UserService(new UserContext());

            Assert.IsTrue(auth.IsEmailCorrect(email));
        }

        [Test]
        [TestCase("1")]
        [TestCase("ab")]
        [TestCase("password_incorrect_password_incorrect_password_incorrect_password_incorrect_" +
            "password_incorrect_password_incorrect_password_incorrect_password_incorrect_" +
            "password_incorrect_password_incorrect_password_incorrect_password_incorrect_")]
        public void PasswordValidationTest_PasswordInvalid_ReturnFalse(string password)
        {
            UserService auth = new UserService(new UserContext());

            Assert.IsFalse(auth.IsPasswordCorrect(password));
        }
    }
}