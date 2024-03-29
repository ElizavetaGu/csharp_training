﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Remove(int index)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(index);
            RemoveGroup();
            ReturnToGroupsPage(); 
        }

        public void Remove(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(group.ID);
            RemoveGroup();
            ReturnToGroupsPage();
        }

        private List<GroupData> groupCashe = null;
        
        public List<GroupData> GetGroupList()
        {
            if (groupCashe == null)
            {
                groupCashe = new List<GroupData>();
                manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCashe.Add(new GroupData(element.Text) {
                        //задаем одно св-во через конструктор
                        ID = element.FindElement(By.TagName("input")).GetAttribute("value")
                        //при конструировании объекта можно сразу указать ему доп. свойство
                    });
                }
            }
            return new List<GroupData>(groupCashe);
            //возвращает не сам кэш, а его копию, которая постоена из старого
        }

        public GroupData FindGroupWithContact()
        {
            GroupData groupWithContact = new GroupData();
            if (!manager.Contacts.DoesContactInGroupExist())
            {
                if (!DoesGroupExist())
                {
                    groupWithContact = new GroupData("name");
                    Create(groupWithContact);
                }
                groupWithContact = GroupData.GetAll()[0];

                if (!manager.Contacts.DoesContactExist())
                {
                    ContactData newContact = new ContactData("name");
                    manager.Contacts.Create(newContact);
                }
                List<ContactData> oldListAdd = groupWithContact.GetContacts();
                ContactData contactN = ContactData.GetAll().Except(oldListAdd).First();
                manager.Contacts.AddContactToGroup(contactN, groupWithContact);
            }

            GroupContactRelation groupWithC = GroupContactRelation.GetAll()[0];

            List<GroupData> allGroups = GroupData.GetAll();
            
            foreach (GroupData group in allGroups)
            {
                if (group.ID == groupWithC.GroupID)
                {
                    groupWithContact = group;
                    break;
                }
            }
            return groupWithContact;
        }

        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        public bool DoesGroupExist()
        {
            manager.Navigator.GoToGroupsPage();
            return IsElementPresent(By.Name("selected[]"));
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Modify(GroupData group, int index)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(index);
            InitGroupModification();
            FillGroupForm(group);
            UpdateGroup();
            ReturnToGroupsPage();
            
            return this;
        }
        public GroupHelper Modify(GroupData modificationData, GroupData toBeModified)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(toBeModified.ID);
            InitGroupModification();
            FillGroupForm(modificationData);
            UpdateGroup();
            ReturnToGroupsPage();

            return this;
        }
        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.XPath("(//input[@name='edit'])[2]")).Click();
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCashe = null;
            return this;
        }
        public GroupHelper UpdateGroup()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCashe = null;
            return this;
        }
        public void ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }
        public GroupHelper SelectGroup(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='"+id+"'])")).Click();
            return this;
        }
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCashe = null;
            return this;
        }


    }
}
