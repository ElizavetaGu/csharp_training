using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            GroupData group = new GroupData("modifiedName");
            group.Header = null;
            group.Footer = null;

            if (!app.Groups.DoesGroupExist())
            {
                GroupData newGroup = new GroupData("name");
                app.Groups.Create(newGroup);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            //сохраняем группу, которую будем менять, для проверки по ID в конце теста
            GroupData oldData = oldGroups[0];

            app.Groups.Modify(group, 0);

            //проверка, что количество групп не изменилось
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();

            //заменяем имя группы в изначальном списке групп
            //чтобы можно было сравнить новый и старый списки групп
            oldGroups[0].Name = group.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            //проверка, что имя изменилось именно у той группы, которую меняли
            foreach (GroupData element in newGroups)
            {
                if (element.ID == oldData.ID) //нашли ID измененной группы
                {
                    Assert.AreEqual(group.Name, element.Name);
                }
            }
            app.Auth.Logout();
        }
    }
}
