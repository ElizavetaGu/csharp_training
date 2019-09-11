using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData contact = new ContactData("AnnaModified");
            contact.Surname = "SmithModified";

            app.Contacts.Modify(contact);
            app.Auth.Logout();
        }
        
    }
}
