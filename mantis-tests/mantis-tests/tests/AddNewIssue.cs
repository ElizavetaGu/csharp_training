using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssue : TestBase
    {
        [Test]
        public void AddNewIssueTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
            ProjectData project = new ProjectData()
            {
                ID = "2"
            };
            IssueData issue = new IssueData()
            {
                Summary = "some text",
                Description = "some long text",
                Category = "General"
            };
            app.API.CreateNewIssue(account, project, issue);
        }
    }
}
