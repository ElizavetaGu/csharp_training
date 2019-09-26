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

            List<ContactData> oldGroups = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);

            List<ContactData> newGroups = app.Contacts.GetContactsList();
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
            
            
            app.Auth.Logout();
         }

    }
} 
