using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        
        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.AddNewContactPage();
            ContactData contact = new ContactData("Anna");
            contact.Surname = "Smith";
            app.Contacts.FillContactForm(contact);
            app.Contacts.SubmitConactCreation();
            app.Navigator.OpenHomePage();
            app.Auth .Logout();
         }

    }
} 
