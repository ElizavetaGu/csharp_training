using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;


namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(20))
                {
                    Surname = GenerateRandomString(30)
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"contact.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    Name = range.Cells[i, 1].Value,
                    Surname = range.Cells[i, 2].Value,
                    AllPhones = range.Cells[i, 3].Value,
                    AllEmails = range.Cells[i, 4].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromJSONFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contact.json"));
        }


        [Test, TestCaseSource("ContactDataFromExcelFile")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = ContactData.GetAll();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.Add(contact);

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
            
            app.Auth.Logout();
         }

    }
} 
