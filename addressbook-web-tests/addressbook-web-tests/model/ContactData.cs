using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace addressbook_web_tests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string allProperties;
        public string allPhones;
        public string allEmails;

        public ContactData(string name)
        {
            Name = name;
        }
        public ContactData()
        {
            AllProperties = allProperties;
        }

        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Homepage { get; set; }
        public string DayOfBirth { get; set; }
        public string MonthOfBirth { get; set; }
        public string YearOfBirth { get; set; }
        public string DayOfAnn { get; set; }
        public string MonthOfAnn { get; set; }
        public string YearOfAnn { get; set; }
        public string Address2 { get; set; }
        public string Phone2 { get; set; }
        public string Notes { get; set; }

        public string NumberOfFullYears { get; set; }
        public string AllProperties
        {
            get
            {
                if (allProperties != null)
                {
                    return allProperties;
                }
                else
                {
                    return GetAllProperties();
                }
            }
            set
            {
                allProperties = value;
            }
        }

        public string GetAllProperties()
        {
            string fullName = (Name == "" ? ("") : Name) 
                + (MiddleName == "" ? ("") : MiddleName) 
                + (Surname == "" ? ("") : Surname);

            string phones = (HomePhone == "" ? ("") : "H:" + HomePhone)
                + (MobilePhone == "" ? ("") : "M:" + MobilePhone)
                + (WorkPhone == "" ? ("") : "W:" + WorkPhone)
                + (Fax == "" ? ("") : "F:" + Fax);

            string homepage = Homepage == "" ? ("") : "Homepage:" + Homepage;

            string dateOfBirth = "";
            if (DayOfBirth != "-" || MonthOfBirth != "-" || YearOfBirth != "")
            {
                dateOfBirth = "Birthday "
                    + (DayOfBirth == "-"||DayOfBirth == "0" ? ("") : DayOfBirth + ".")
                    + (MonthOfBirth == "-" ? ("") : MonthOfBirth + "")
                    + (YearOfBirth == "" ? ("") : YearOfBirth + "(" + 
                    (DateTime.Now.Year - Convert.ToInt32(YearOfBirth)).ToString() + ")");
            }

            string dateOfAnn = "";
            if (DayOfAnn != "-" || MonthOfAnn != "-" || YearOfAnn != "")
            {
                dateOfAnn = "Anniversary "
                    + (DayOfAnn == "-" ? ("") : DayOfAnn + ".")
                    + (MonthOfAnn == "-" ? ("") : MonthOfAnn)
                    + (YearOfAnn == "" ? ("") : YearOfAnn + "(" +
                    (DateTime.Now.Year - Convert.ToInt32(YearOfAnn)).ToString() + ")");
            }

            string phone2 = Phone2 == "" ? ("") : "P:" + Phone2;

            string[] allPropertiesList = { fullName, Nickname, Company,
                Title, Address, phones, Email1, Email2,
                Email3, homepage, dateOfBirth, dateOfAnn, Address2, phone2, Notes };

            string allProperties = "";

            foreach (string property in allPropertiesList)
            {
                //удаление всех пробелов из текст
                allProperties = Regex.Replace(String.Concat(allProperties, property), "[ ]", "");
            }

            return allProperties;
        }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUpEmail(Email1) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUpEmail(string email)
        {
            email = email.Trim(); //добавлено на случай, если строка состоит из одних пробелов
            if (email == null || email == "")
            {
                return "";
            }
            return email + "\r\n"; 
            //нет регулярки даже на пробелы, потому что один пробел в теле емейла
            //не убирается в таблице на главной странице
        }

        private string CleanUp(string phone)
        {
            phone = phone.Trim(); //добавлено на случай, если строка состоит из одних пробелов
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
        }
         
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name && Surname == other.Surname;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + Name + "\nsurname=" + Surname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Surname.CompareTo(other.Surname) == 0)
            {
                return Name.CompareTo(other.Name);
            }

            return Surname.CompareTo(other.Surname);
        }
    }

  
}
