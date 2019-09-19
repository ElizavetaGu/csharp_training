﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
 
        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Groups.DoesGroupExist())
            {
                GroupData group = new GroupData("name");
                app.Groups.Create(group);
                app.Groups.Remove(0);
            }

            else
            {
                List<GroupData> oldGroups = app.Groups.GetGroupList();
                app.Groups.Remove(0);

                List<GroupData> newGroups = app.Groups.GetGroupList();

                oldGroups.RemoveAt(0);
                Assert.AreEqual(oldGroups, newGroups);
            }
            
            app.Auth.Logout();
        }

    }
}

