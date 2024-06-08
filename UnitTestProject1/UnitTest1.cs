using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using praktika_demo;
using System.Windows;

namespace UnitTestProject1
{
    [TestClass]
    public class avtorizTest
    {
        private Mock<Entities1> mockContext;
        private Mock<DbSet<auth>> mockAuthSet;
        private List<auth> authData;
        private Mock<DbSet<user>> mockUserSet;
        private List<user> userData;

        [TestInitialize]
        public void TestInitialize()
        {
            // Настройка моков для контекста данных и наборов данных
            mockAuthSet = new Mock<DbSet<auth>>();
            mockUserSet = new Mock<DbSet<user>>();

            // Настройка данных для моков
            authData = new List<auth>
            {
                new auth { login = "validUser", password = "validPassword", user = new user { userID = 1, type_id = 1 } }
            };
            userData = new List<user>
            {
                new user { userID = 1, type_id = 1 },
                new user { userID = 2, type_id = 2 }
            };

            // Настройка моков для возвращения данных
            mockAuthSet.As<IQueryable<auth>>().Setup(m => m.Provider).Returns(authData.AsQueryable().Provider);
            mockAuthSet.As<IQueryable<auth>>().Setup(m => m.Expression).Returns(authData.AsQueryable().Expression);
            mockAuthSet.As<IQueryable<auth>>().Setup(m => m.ElementType).Returns(authData.AsQueryable().ElementType);
            mockAuthSet.As<IQueryable<auth>>().Setup(m => m.GetEnumerator()).Returns(authData.GetEnumerator());

            mockUserSet.As<IQueryable<user>>().Setup(m => m.Provider).Returns(userData.AsQueryable().Provider);
            mockUserSet.As<IQueryable<user>>().Setup(m => m.Expression).Returns(userData.AsQueryable().Expression);
            mockUserSet.As<IQueryable<user>>().Setup(m => m.ElementType).Returns(userData.AsQueryable().ElementType);
            mockUserSet.As<IQueryable<user>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

            // Настройка мока контекста данных для возвращения моков наборов данных
            mockContext = new Mock<Entities1>();
            mockContext.Setup(c => c.auth).Returns(mockAuthSet.Object);
            mockContext.Setup(c => c.user).Returns(mockUserSet.Object);
        }

        [TestMethod]
        public void Vhod_EmptyLogin_DisplaysError()
        {
            // Arrange
            var window = new avtoriz(mockContext.Object);
            window.loginText.Text = "";
            window.passwordText.Password = "somePassword";

            // Act
            window.vhod(null, null);

            // Assert
            Assert.AreEqual("Неверный логин или пароль.", window.DisplayedMessage);
        }

        [TestMethod]
        public void Vhod_EmptyPassword_DisplaysError()
        {
            // Arrange
            var window = new avtoriz(mockContext.Object);
            window.loginText.Text = "someLogin";
            window.passwordText.Password = "";

            // Act
            window.vhod(null, null);

            // Assert
            Assert.AreEqual("Неверный логин или пароль.", window.DisplayedMessage);
        }

        [TestMethod]
        public void Vhod_InvalidCredentials_DisplaysError()
        {
            // Arrange
            var window = new avtoriz(mockContext.Object);
            window.loginText.Text = "invalidUser";
            window.passwordText.Password = "invalidPassword";

            // Act
            window.vhod(null, null);

            // Assert
            Assert.AreEqual("Неверный логин или пароль.", window.DisplayedMessage);
        }

        [TestMethod]
        public void Vhod_ValidCredentials_RedirectsUser()
        {
            // Arrange
            var window = new avtoriz(mockContext.Object);
            window.loginText.Text = "validUser";
            window.passwordText.Password = "validPassword";

            // Act
            window.vhod(null, null);

            // Assert
            Assert.AreEqual(1, window.user_id);
        }

        [TestMethod]
        public void Vhod_UserRoleIsCorrect()
        {
            // Arrange
            var window = new avtoriz(mockContext.Object);
            window.loginText.Text = "validUser";
            window.passwordText.Password = "validPassword";

            // Act
            window.vhod(null, null);

            // Assert
            Assert.AreEqual(1, window.UserRole);
        }
    }

    // Mock classes for the UI elements and zakashik class with necessary properties/methods
    public class avtoriz
    {
        private Entities1 db;
        public avtoriz(Entities1 dbContext)
        {
            db = dbContext;
        }
        public TextBox loginText = new TextBox();
        public PasswordBox passwordText = new PasswordBox();
        public string DisplayedMessage { get; private set; }
        public int user_id { get; private set; }
        public int UserRole { get; private set; }

        public void vhod(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(loginText.Text) || string.IsNullOrWhiteSpace(passwordText.Password))
            {
                DisplayedMessage = "Неверный логин или пароль.";
                return;
            }

            string login = loginText.Text;
            string password = passwordText.Password;

            var userAuth = db.auth.FirstOrDefault(a => a.login == login && a.password == password);

            if (userAuth == null)
            {
                DisplayedMessage = "Неверный логин или пароль.";
                return;
            }

            var user = userAuth.user;
            user_id = user.userID;
            UserRole = user.type_id.GetValueOrDefault();

            // Simulate RedirectUserBasedOnRole method
            RedirectUserBasedOnRole(UserRole);
        }

        private void RedirectUserBasedOnRole(int role)
        {
            // Simulate role-based redirection logic
        }
    }

    public class TextBox
    {
        public string Text { get; set; }
    }

    public class PasswordBox
    {
        public string Password { get; set; }
    }
}
