using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            GroupData group = new GroupData("modifiedName");
            group.Header = "modifiedHeader";
            group.Footer = "modifiedFooter";
            
            app.Groups.Modify(group);
            app.Auth.Logout();
        }


    }
}
