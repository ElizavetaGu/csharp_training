using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData contact = new ContactData("AnnaModified2");
            contact.Surname = null;

            if (!app.Contacts.DoesContactExist())
            {
                ContactData newContact = new ContactData("name");
                app.Contacts.Create(newContact);
                app.Contacts.Modify(contact);
            }

            else
            {
                 app.Contacts.Modify(contact);
            }

            app.Auth.Logout();
        }
    }
}
