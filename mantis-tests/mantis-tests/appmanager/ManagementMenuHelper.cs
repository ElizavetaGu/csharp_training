using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        private string baseURL;
        public ManagementMenuHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }
            public void GoToHomePage()
            {
                if (driver.Url == baseURL)
                {
                    return;
                }
                driver.Navigate().GoToUrl(baseURL);
            }

            public void GoToProjectManagementPage()
            {
                if (driver.Url == baseURL + "/manage_proj_page.php")
                {
                    return;
                }
                GoToManageOverviewPage();
                driver.FindElement(By.XPath("(//a[@href='/mantisbt-2.22.1/manage_proj_page.php'])")).Click();
            }

            private void GoToManageOverviewPage()
            {
                if (driver.Url == baseURL + "/manage_overview_page.php")
                {
                    return;
                }
                driver.FindElement(By.XPath("(//a[@href='/mantisbt-2.22.1/manage_overview_page.php'])")).Click();
            }
        }
    }
