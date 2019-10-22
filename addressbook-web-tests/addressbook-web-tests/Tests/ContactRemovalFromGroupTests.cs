using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class ContactRemovalFromGroupTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalFromGroupTest()
        {
            //найти группу, в которую добавлен хотя бы один контакт
            GroupData groupWithContact = app.Groups.FindGroupWithContact();
            ContactData contactToRemove = groupWithContact.GetContacts().First();
            List<ContactData> oldList = groupWithContact.GetContacts();

            app.Contacts.RemoveContactFromGroup(contactToRemove, groupWithContact);

            //actions
            List<ContactData> newList = groupWithContact.GetContacts();
            newList.Add(contactToRemove);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
