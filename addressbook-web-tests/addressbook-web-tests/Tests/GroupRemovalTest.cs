using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
 
        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Groups.DoesGroupExist())
            {
                GroupData group = new GroupData("name");
                app.Groups.Create(group);
            }

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeRemoved = oldGroups[0];
            app.Groups.Remove(toBeRemoved);

            List<GroupData> newGroups = GroupData.GetAll();
            
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());
            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            //проверяем, что удаленная группа действительно удалилась и ее ID нет в новом списке групп
            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.ID, toBeRemoved.ID);
            }
            
            app.Auth.Logout();
        }

    }
}

