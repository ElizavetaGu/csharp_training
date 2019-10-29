using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class UnitTest1 : TestBase
    {
        [Test]
        public void UnitTest()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser1",
                Password = "password"
            };
            Assert.IsFalse(app.James.Verify(account));
            app.James.Add(account);
            Assert.IsTrue(app.James.Verify(account));
            app.James.Delete(account);
            Assert.IsFalse(app.James.Verify(account));

        }

    }
}
