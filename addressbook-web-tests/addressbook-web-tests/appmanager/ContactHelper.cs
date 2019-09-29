using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using System.Reflection;

namespace addressbook_web_tests
{
    public class ContactHelper : HelperBase
    {
        public string baseURL;

        public ContactHelper(ApplicationManager manager) : base (manager)
        {
        }

        public ContactData GetContactInfoFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName)
            {
                Surname = lastName,
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        internal ContactData GetContactInfoFromPropertiesForm(int index)
        {
            manager.Navigator.OpenHomePage();
            SeeContactProperties(index);
            string allProperties = driver.FindElement(By.Id("content")).Text;
            allProperties = allProperties.Replace("\r\n", "");
            return new ContactData()
            {
                AllProperties = allProperties
            };
        }

        public ContactData GetContactInfoFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName)
            {
                Surname = lastName,
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3
            };
        }

        public string GetAllProperties(ContactData contact)
        {
            PropertyInfo[] listOfProperties = contact.GetType().GetProperties();
            return "";
        }

        public ContactHelper Create(ContactData contact)
        {

            AddNewContactPage();
            FillContactForm(contact);
            SubmitConactCreation();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }

        private List<ContactData> contactCashe = null;

        public List<ContactData> GetContactsList()
        {
            if (contactCashe == null)
            {
                contactCashe = new List<ContactData>();

                manager.Navigator.OpenHomePage();
                List<ContactData> contacts = new List<ContactData>();

                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));

                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                    ContactData contact = new ContactData(cells[2].Text);
                    contact.Surname = cells[1].Text;
                    contactCashe.Add(contact);
                }
            }
            return new List<ContactData>(contactCashe);
        }

        public void Remove(int index)
        {

            SelectContact(index);
            DeleteContact();
            ConfirmContactRemoval();
            manager.Navigator.OpenHomePage();
        }

        public ContactHelper Modify(ContactData contact, int index)
        {

            SelectContact(index);
            InitContactModification(index);
            FillContactForm(contact);
            UpdateContact();
            manager.Navigator.OpenHomePage();

            return this;
        }

        public bool DoesContactExist()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public ContactHelper AddNewContactPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), (contact.Name));
            Type(By.Name("lastname"), (contact.Surname));
            /*driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Name);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Surname);*/
            return this;
        }
        public ContactHelper SubmitConactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            contactCashe = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper SeeContactProperties(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper ConfirmContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCashe = null;
            return this;
        }

        public ContactHelper UpdateContact()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            contactCashe = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            return Int32.Parse(driver.FindElement(By.CssSelector("strong span")).Text);
            /*Match m = new Regex(@"\d+").Match(text); //создаем РВ и применяем его к тексту
            return Int32.Parse(m.Value); //забрали ту часть текста, которая удовлетворяет РВ*/
        }
    }
}
