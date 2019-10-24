using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            if (!app.Groups.DoesGroupExist())
            {
                GroupData newGroup = new GroupData("name");
                app.Groups.Create(newGroup);
            }
            if (!app.Contacts.DoesContactExist())
            {
                ContactData newContact = new ContactData("name");
                app.Contacts.Create(newContact);
            }
            //получить первую группу
            GroupData group = GroupData.GetAll()[0];
            //получить все контакты, которые добавлены в эту группу
            List<ContactData> oldList = group.GetContacts();
            //найти контакт, который в данной группе не находится
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, group);

            //actions
            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort(); 
            oldList.Sort();

            Assert.AreEqual(oldList, newList);

        }
    }
}
