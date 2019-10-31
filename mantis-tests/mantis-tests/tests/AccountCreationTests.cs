using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp] 
        public void SetUpConfig()
        {
            app.FTP.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.FTP.Upload("/config_inc.php", localFile);
            }
        }

        [Test]
        public void AccountCreationTest()
        {
            

            AccountData account = new AccountData()
            {
                Name = "testuser6", 
                Password = "password",
                Email = "testuser6@localhost.localdomain"
            };

            List<AccountData> accounts = app.Admin.GetAllAccounts();
            AccountData existingAccount = accounts.Find(x => x.Name == account.Name);
            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }

        [TestFixtureTearDown]

        public void RestoreConfig()
        {
            app.FTP.RestoreBackedupFile("/config_inc.php");
        }
    }
}
