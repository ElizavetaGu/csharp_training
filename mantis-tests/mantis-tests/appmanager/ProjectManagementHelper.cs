using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }

        public void RemoveProject(int index)
        {
            manager.ManagementMenu.GoToProjectManagementPage();
            GoToProjectModificationPage(index);
            SubmitProjectRemoveing();
            ConfirmProjectRemoving();
        }
        public void DoesAnyProjectExist()
        {
            manager.ManagementMenu.GoToProjectManagementPage();

            if (!DoRecordsExist())
            {
                DateTime now = DateTime.Now;
                ProjectData project = new ProjectData();
                project.Name = "testproject" + now.ToString("hhmmssddMMyyyy");

                CreateProject(project);
            }
        }

        private void ConfirmProjectRemoving()
        {
            driver.FindElement(By.XPath("//input[4]")).Click();
        }

        private void SubmitProjectRemoveing()
        {
            driver.FindElement(By.XPath("//input[3]")).Click();
        }

        private void GoToProjectModificationPage(int index)
        {
            driver.FindElement(By.XPath("//td/a[" + index + "]")).Click();
        }

        public void CreateProject(ProjectData project)
        {
            manager.ManagementMenu.GoToProjectManagementPage();
            InitNewProjectCreation();
            FillNewProjectForm(project);
            SubmitProjectCreation();
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
        }

        private void FillNewProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
        }

        private void InitNewProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

    }
}