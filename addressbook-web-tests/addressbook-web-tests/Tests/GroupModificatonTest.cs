using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            GroupData modificationData = new GroupData("modifiedName");
            modificationData.Header = null;
            modificationData.Footer = null;

            if (!app.Groups.DoesGroupExist())
            {
                GroupData newGroup = new GroupData("name");
                app.Groups.Create(newGroup);
            }

            List<GroupData> oldGroups = GroupData.GetAll();

            //сохраняем группу, которую будем менять, для проверки по ID в конце теста
            GroupData toBeModified = oldGroups[0];

            app.Groups.Modify(modificationData, toBeModified);

            //проверка, что количество групп не изменилось
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();

            //заменяем имя группы в изначальном списке групп
            //чтобы можно было сравнить новый и старый списки групп
            oldGroups[0].Name = modificationData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            //проверка, что имя изменилось именно у той группы, которую меняли
            foreach (GroupData element in newGroups)
            {
                if (element.ID == toBeModified.ID) //нашли ID измененной группы
                {
                    Assert.AreEqual(modificationData.Name, element.Name);
                }
            }
            app.Auth.Logout();
        }
    }
}
