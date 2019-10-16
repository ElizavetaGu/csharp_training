using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using addressbook_web_tests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            if (dataType == "group")
            {
                GroupDataGenerator(args);
            }

            else if (dataType == "contact")
            {
                ContactDataGenerator(args);
            }
            else
            {
                System.Console.Out.Write("Unrecognised data type " + dataType);
            }    
        }

        static void GroupDataGenerator(string[] args)
        {
            int count = Convert.ToInt32(args[1]);
            string fileName = args[2];
            string format = args[3];
            List<GroupData> groups = new List<GroupData>();
            
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(100),
                    Footer = TestBase.GenerateRandomString(100)
                });
            }

            if (format == "excel")
            {
                WriteGroupsToExcelFile(groups, fileName);
            }
            else
            {
                StreamWriter writer = new StreamWriter(fileName);
                if (format == "csv")
                {
                    WriteGroupsToCSVFile(groups, writer);
                }
                else if (format == "xml")
                {
                    WriteGroupsToXMLFile(groups, writer);
                }
                else if (format == "json")
                {
                    WriteGroupsToJSONFile(groups, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognised format " + format);
                }
                writer.Close();
            }
        }

        static void ContactDataGenerator(string[] args)
        {
            int count = Convert.ToInt32(args[1]);
            string fileName = args[2];
            string format = args[3];
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(10))
                {
                    AllProperties = TestBase.GenerateRandomString(100),
                    AllPhones = TestBase.GenerateRandomString(100),
                    AllEmails = TestBase.GenerateRandomString(100)
                });

        }

            if (format == "excel")
            {
                WriteContactsToExcelFile(contacts, fileName);
            }
            else
            {
                StreamWriter writer = new StreamWriter(fileName);
                if (format == "csv")
                {
                    WriteContactsToCSVFile(contacts, writer);
                }
                else if (format == "xml")
                {
                    WriteContactsToXMLFile(contacts, writer);
                }
                else if (format == "json")
                {
                    WriteContactsToJSONFile(contacts, writer);
                }
                else
                {
                    System.Console.Out.Write("Unrecognised format " + format);
                }
                writer.Close();
            }

        }

        static void WriteGroupsToJSONFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToJSONFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteGroupsToExcelFile(List<GroupData> groups, string fileName)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            sheet.Cells[1, 1] = "test";

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            SaveAndCloseExcel(fileName, app, wb);
        }
        static void WriteContactsToExcelFile(List<ContactData> contacts, string fileName)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            sheet.Cells[1, 1] = "test";

            int row = 1;
            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.Name;
                sheet.Cells[row, 2] = contact.AllProperties;
                sheet.Cells[row, 3] = contact.AllPhones;
                sheet.Cells[row, 4] = contact.AllEmails;
                row++;
            }
            SaveAndCloseExcel(fileName, app, wb);
        }

        private static void SaveAndCloseExcel(string fileName, Excel.Application app, Excel.Workbook wb)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void WriteGroupsToCSVFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void WriteContactsToCSVFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0}", contact.Name));
            }
        }

        static void WriteGroupsToXMLFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups); 
        }

        static void WriteContactsToXMLFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }
    }
}
