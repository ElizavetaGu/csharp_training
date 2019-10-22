using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contacts.DoesContactExist())
            {
                ContactData newContact = new ContactData("name");
                app.Contacts.Create(newContact);
            }

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];
            app.Contacts.Remove(toBeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(0);

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            app.Auth.Logout();
        }
    }
}
