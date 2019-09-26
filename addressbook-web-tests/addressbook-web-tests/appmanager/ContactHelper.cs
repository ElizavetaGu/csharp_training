using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class ContactHelper : HelperBase
    {
        public string baseURL;

        public ContactHelper(ApplicationManager manager) : base (manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {

            AddNewContactPage();
            FillContactForm(contact);
            SubmitConactCreation();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public List<ContactData> GetContactsList()
        {
            manager.Navigator.OpenHomePage();
            List<ContactData> contacts = new List<ContactData>();
            
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));
            
            foreach (IWebElement element in elements)
            {
                ContactData contact = new ContactData(element.Text);
                contacts.Add(contact);
            }
            return contacts;
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
            InitContactModification();
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
            return this;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
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
            return this;
        }

        public ContactHelper UpdateContact()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }
    }
}
