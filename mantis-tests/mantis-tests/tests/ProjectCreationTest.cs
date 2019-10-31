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
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            DateTime now = DateTime.Now;

            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData()
            {
                Name = "testproject" + now.ToString("hhmmssddMMyyyy")
            };

            var oldProjects = app.API.GetAllProjectsList(account);

            app.Project.CreateProject(project);

            var newProjects = app.API.GetAllProjectsList(account);
            int number = newProjects.Count - 1;

            Assert.AreEqual(oldProjects.Count + 1, newProjects.Count);
            project.ID = newProjects[number].ID;
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }

    }
}