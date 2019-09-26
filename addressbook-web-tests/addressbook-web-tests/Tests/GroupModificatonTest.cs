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

            app.Groups.Modify(group);
            app.Auth.Logout();
        }


    }
}
