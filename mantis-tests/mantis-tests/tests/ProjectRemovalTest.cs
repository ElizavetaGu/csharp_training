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
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            DateTime now = DateTime.Now;

            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            var oldProjects = app.API.GetAllProjectsList(account);

            if (oldProjects.Count == 0)
            {
                ProjectData newProject = new ProjectData()
                {
                    Name = "testproject" + now.ToString("hhmmssddMMyyyy")
                };

                app.API.CreateNewProject(account, newProject);
                oldProjects = app.API.GetAllProjectsList(account);
            }

            var toBeRemoved = oldProjects[0];

            app.Project.RemoveProject(1);

            var newProjects = app.API.GetAllProjectsList(account);

            Assert.AreEqual(oldProjects.Count - 1, newProjects.Count);
            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);

            foreach (var project in newProjects)
            {
                Assert.AreNotEqual(project.ID, toBeRemoved.ID);
            }
        }

        [Test]
        public void ProjectRemovalTestGUI()
        {
            DateTime now = DateTime.Now;

            var oldProjects = app.Project.GetProjectsFromGUI();

            if (oldProjects.Count == 0)
            {
                ProjectData newProject = new ProjectData()
                {
                    Name = "testproject" + now.ToString("hhmmssddMMyyyy")
                };

                app.Project.CreateProject(newProject);
                oldProjects = app.Project.GetProjectsFromGUI();
            }

            app.Project.RemoveProject(1);

            var newProjects = app.Project.GetProjectsFromGUI();

            Assert.AreEqual(oldProjects.Count - 1, newProjects.Count);
            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}