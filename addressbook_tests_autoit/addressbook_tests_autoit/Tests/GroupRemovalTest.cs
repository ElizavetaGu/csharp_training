using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if (!app.Groups.DoesGroupExist())
            {
                GroupData newGroup = new GroupData()
                {
                    Name = "test"
                };
                app.Groups.Add(newGroup);
                oldGroups = app.Groups.GetGroupList();
            }

            //GroupData toBeRemoved = oldGroups[0];
            app.Groups.Remove((oldGroups.Count-1).ToString());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            //Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            oldGroups.RemoveAt(oldGroups.Count-1);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
