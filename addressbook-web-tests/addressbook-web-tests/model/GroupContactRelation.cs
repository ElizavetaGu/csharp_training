﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace addressbook_web_tests
{ 
    [Table(Name = "address_in_groups")]
    public class GroupContactRelation
    {
        [Column(Name = "group_id")]
        public string GroupID { get; set; }

        [Column(Name = "id")]
        public string ContactID { get; set; }

        public static List<GroupContactRelation> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from gcr in db.GCR select gcr).ToList();
            }
        }
    }


}
