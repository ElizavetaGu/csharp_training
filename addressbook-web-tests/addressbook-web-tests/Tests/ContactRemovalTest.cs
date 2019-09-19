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
    public class ContactRemovalTests : AuthTestBase
    {

        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contacts.DoesContactExist())
            {
                ContactData newContact = new ContactData("name");
                app.Contacts.Create(newContact);
                app.Contacts.Remove();

            }
            else
            {

                app.Contacts.Remove();

            }

            app.Auth.Logout();
        }
    }
}
