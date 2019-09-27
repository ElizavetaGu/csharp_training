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
            }
            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Modify(contact, 0);
            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts[0].Name = contact.Name;
            oldContacts[0].Surname = contact.Surname;

            oldContacts.Sort();
            newContacts.Sort();

            app.Auth.Logout();
        }
    }
}
