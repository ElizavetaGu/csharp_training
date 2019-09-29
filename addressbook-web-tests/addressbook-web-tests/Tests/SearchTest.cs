using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
        [Test]

        public void SearchTest()
        {
            System.Console.Out.Write(app.Contacts.GetNumberOfSearchResults());
        }
    }
}