using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager) { }

        public void Login(AccountData account)
        {
            if (IsloggedIn())
            {
                if (IsloggedIn(account))
                {
                    return;
                }

                Logout();
            }
            Type(By.Name("username"), account.Name);
            driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
        }

        public void Logout()
        {
            if (IsloggedIn())
            {
                driver.FindElement(By.XPath("//i[2]")).Click();
                driver.FindElement(By.LinkText("Выход")).Click();
            }

            int attempt = 0;
            while (IsloggedIn() && attempt < 10000)
            {
                System.Threading.Thread.Sleep(2);
                attempt++;
            }
        }

        public bool IsloggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }

        public bool IsloggedIn(AccountData account)
        {
            return IsloggedIn()
                && GetLoggedUserName() == account.Name;
        }

        public string GetLoggedUserName()
        {
            string text = driver.FindElement((By.ClassName("user-info"))).Text;
            System.Console.WriteLine(text);
            return text.Substring(1, text.Length - 2);
        }
    }
}
