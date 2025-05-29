using LombardSystem.Facades;
using LombardSystem.Models;

namespace TestProject0
{
    [TestClass]
    public class LombardFacadeTests
    {
        private LombardFacade _facade;
        private TestDatabaseFixture _dbFixture;

        [TestInitialize]
        public void Setup()
        {
            _dbFixture = new TestDatabaseFixture();
            _facade = new LombardFacade();
        }

        [TestMethod]
        public void Authenticate_ValidCredentials_ReturnsUser()
        {
            // Act
            var user = _facade.Authenticate("admin", "1");

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual("Owner", user.Role);
        }

        [TestMethod]
        public void Authenticate_InvalidCredentials_ReturnsNull()
        {
            // Act
            var user = _facade.Authenticate("admin", "wrongpassword");

            // Assert
            Assert.IsNull(user);
        }

        [TestMethod]
        public void AddClient_ValidData_ClientAddedToDatabase()
        {
            // Arrange
            var fullName = "Петров Петр";
            var contactInfo = "+79219876543";
            var passportData = "4511 654321";

            // Act
            _facade.AddClient(fullName, contactInfo, passportData);

            // Assert
            var client = _dbFixture.Context.Clients.FirstOrDefault(c => c.FullName == fullName);
            Assert.IsNotNull(client);
            Assert.AreEqual(passportData, client.PassportData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateContract_InvalidDates_ThrowsException()
        {
            // Arrange
            var clientId = 1;
            var itemId = 1;
            var signDate = DateTime.UtcNow;
            var expiryDate = signDate.AddDays(-1); // Неправильная дата окончания

            // Act & Assert (должно выбросить исключение)
            _facade.CreateContract(clientId, itemId, signDate, expiryDate, 10000m);
        }
    }
}