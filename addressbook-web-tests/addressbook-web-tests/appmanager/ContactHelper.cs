﻿using System;
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

        public bool AreAllContactsInAllGroups()
        {
            int numberOfContactGroupPairs = GroupContactRelation.GetAll().Count();
            int numberOfContacts = ContactData.GetAll().Count();
            int numberOfGroups = GroupData.GetAll().Count();
            if (numberOfContactGroupPairs == numberOfContacts * numberOfGroups)
            {
                return true;
            }
            return false;
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            SelectGroup(group.Name);
            SelectContact(contact.ID);
            CommitContactRemovalFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void CommitContactRemovalFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void SelectGroup(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.ID);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public bool DoesContactInGroupExist()
        {
            List<GroupContactRelation> groupWithC = GroupContactRelation.GetAll();
            if (groupWithC.Count == 0)
            {
                return false;
            }
            else return true;
        }

        private void SelectContact(string contactID)
        {
            driver.FindElement(By.Id(contactID)).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        internal ContactData GetContactInfoFromPropertiesForm(int index)
        {
            manager.Navigator.OpenHomePage();
            SeeContactProperties(index);
            string allProperties = driver.FindElement(By.Id("content")).Text;
            //allProperties = allProperties.Replace("\r\n", "");
            //allProperties = Regex.Replace(allProperties, "[ \r\n]", "");
            return new ContactData()
            {
                AllProperties = allProperties
            };
        }

        public ContactData GetContactInfoFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification();
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            string bday = driver.FindElement(By.Name("bday")).GetAttribute("value");
            string bmonth = driver.FindElement(By.Name("bmonth")).GetAttribute("value");
            string byear = driver.FindElement(By.Name("byear")).GetAttribute("value");
            string aday = driver.FindElement(By.Name("aday")).GetAttribute("value");
            string amonth = driver.FindElement(By.Name("amonth")).GetAttribute("value");
            string ayear = driver.FindElement(By.Name("ayear")).GetAttribute("value");
            string address2 = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string phone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");

            return new ContactData(firstName)
            {
                Surname = lastName,
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3,

                MiddleName = middleName,
                Nickname = nickname,
                Company = company,
                Title = title,
                Fax = fax,
                Homepage = homepage,
                DayOfBirth = bday,
                MonthOfBirth = bmonth,
                YearOfBirth = byear,
                DayOfAnn = aday,
                MonthOfAnn = amonth,
                YearOfAnn = ayear,
                Address2 = address2,
                Phone2 = phone2,
                Notes = notes
            };
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

        public void Remove(ContactData contact)
        {

            SelectContact(contact.ID);
            DeleteContact();
            ConfirmContactRemoval();
            manager.Navigator.OpenHomePage();
        }

        public ContactHelper Modify(ContactData contact, ContactData toBeModified)
        {

            SelectContact(toBeModified.ID);
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
            contactCashe = null;
            return this;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElements(By.Name("entry"))[0]
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
