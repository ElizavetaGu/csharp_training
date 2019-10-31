using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    [TestFixture]
    public class APIProjectRemovalTests : TestBase
    {
        [Test]
        public void APIProjectRemovalTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            Mantis.ProjectData[] projects = app.API.GetAllProjectsList(account);

            if (projects.Length == 0)
            {
                ProjectData newProject = new ProjectData()
                {
                    Name = "TestProject01"
                };

                app.API.CreateNewProject(account, newProject);
            }

            Mantis.ProjectData project = app.API.GetAllProjectsList(account)[0];
            app.API.RemoveProject(account, project.id);
        }

    }
}