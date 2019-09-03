using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            AddNewContactPage();
            ContactData contact = new ContactData("Anna");
            contact.Surname = "Smith";
            FillContactForm(contact);
            SubmitConactCreation();
            OpenHomePage();
            Logout();
         }
    }
}
