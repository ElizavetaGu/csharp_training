using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;

namespace mantis_tests 
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.ID;
            client.mc_issue_add(account.Name, account.Password, issue);

        }

        public List<ProjectData> GetAllProjectsList(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] projects = client.mc_projects_get_user_accessible(account.Name, account.Password);
                        
            List<ProjectData> listOfProjects = new List<ProjectData>();
            
            foreach (Mantis.ProjectData m in projects)
            {
                listOfProjects.Add(new ProjectData()
                {
                    Name = m.name,
                    ID = m.id
                });
            }

            return listOfProjects;
        }

        public void CreateNewProject(AccountData account, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            client.mc_project_add(account.Name, account.Password, project);
        }

        public void RemoveProject(AccountData account, string projectID)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            
            client.mc_project_delete(account.Name, account.Password, projectID);
        }
    }
}
