using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;



namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Anna");
            contact.Surname = "Smith";

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts.Add(contact);

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
            
            app.Auth.Logout();
         }

    }
} 
