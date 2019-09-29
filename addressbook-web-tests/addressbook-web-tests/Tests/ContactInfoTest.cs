using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactInfoTests : AuthTestBase
    {

        [Test]
        public void ContactInfoEditPageTest()
        {
            ContactData fromTable = app.Contacts.GetContactInfoFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInfoFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

    }
}
