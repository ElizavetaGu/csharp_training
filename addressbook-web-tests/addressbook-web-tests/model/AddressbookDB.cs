using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace addressbook_web_tests
{
    public class AddressbookDB : LinqToDB.Data.DataConnection
    {
        public AddressbookDB() : base("AddressBook") { }
        //конструктор, который вызывает конструктор базового класса
        //куда в качестве параметра передеается название БД из AppConfig

        public ITable<GroupData> Groups { get { return GetTable<GroupData>(); } }
        public ITable<ContactData> Contacts { get { return GetTable<ContactData>(); } }
        public ITable<GroupContactRelation> GCR { get { return GetTable<GroupContactRelation>(); } }
    }
}
