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
            ProjectData project = new ProjectData();
            project.Name = "testproject" + now.ToString("hhmmssddMMyyyy");

            app.Project.CreateProject(project);
        }

    }
}