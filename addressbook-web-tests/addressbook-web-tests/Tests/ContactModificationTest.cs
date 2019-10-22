using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData madificationData = new ContactData("AnnaModified2");
            madificationData.Surname = null;

            if (!app.Contacts.DoesContactExist())
            {
                ContactData newContact = new ContactData("name");
                app.Contacts.Create(newContact);
            }
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeModified = oldContacts[0];

            app.Contacts.Modify(madificationData, toBeModified);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts[0].Name = madificationData.Name;
            oldContacts[0].Surname = madificationData.Surname;

            oldContacts.Sort();
            newContacts.Sort();

            app.Auth.Logout();
        }
    }
}
