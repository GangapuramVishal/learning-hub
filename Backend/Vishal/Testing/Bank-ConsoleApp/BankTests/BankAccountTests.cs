using Bank;
using Newtonsoft.Json.Bson;

namespace BankTests
{
    [TestFixture]
    public class BankAccountTests
    {
        private BankAccount account;

        [SetUp]
        public void Setup()
        {
            //Arrange
            account = new BankAccount(1000);
        }

        [Test]
        public void Adding_Funds_updates_Balance()
        {
            //Arrange
            //var account = new BankAccount(1000);

            //Act
            account.Add(500);

            //Assert
            Assert.AreEqual(1500, account.Balance);
        }
        [Test]
        public void Adding_Negative_Funds()
        {
            //Arrange
            //var account = new BankAccount(1000);

            //Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Add(-500));
        }

        [Test]
        public void WithDrawing_Funds_Update_Balance()
        {
            //Arrange
            //var account = new BankAccount(500);

            //Act
            account.Withdraw(300);

            //Assert
            Assert.AreEqual(700, account.Balance);
        }

        [Test]
        public void WithDraw_Funds_MoreThan_Balance()
        {
            //Arrange
            //var account = new BankAccount(500);

            //Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Withdraw(-800));
        }

        [Test]
        public void Transfering_Funds_Update_BothAccounts()
        {
            //Arrange
            //var account1 = new BankAccount(700);
            var account2 = new BankAccount();

            //Act
            account.TransferFundsTo(account2, 300);
            //Assert
            Assert.AreEqual(700, account.Balance);
            Assert.AreEqual(300, account2.Balance);
        }

        [Test]
        public void Transfering_Funds_To_NonExisiting_Account()
        {
            //Arrange
            //var account1 = new BankAccount(700);

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() => account.TransferFundsTo(null, 2000));
        }
    }
}