using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public GroupHelper(ApplicationManager manager) : base(manager)
        {

        }

        internal bool DoesGroupExist()
        {
            List<GroupData> groups = GetGroupList();
            if (groups.Count > 1)
            { return true; }
            return false; 
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();

            OpenGroupsDialogue();
            string count = aux.ControlTreeView(GROUPWINTITLE, "", 
                "WindowsForms10.SysTreeView32.app.0.2c908d51", 
                "GetItemCount", "#0", "");
           for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "",
                    "WindowsForms10.SysTreeView32.app.0.2c908d51",
                    "GetText", "#0|#"+i, "");
                list.Add(new GroupData()
                {
                    Name = item
                });
            }

            CloseGroupsDialogue();
            return list;
        }

        internal void Remove(string index)
        {
            OpenGroupsDialogue();
            SelectGroup(index);
            InitGroupRemoval();
            DeleteGroupWithCont();
            ConfirmGroupRemoval();
            CloseGroupsDialogue();
        }

        private void ConfirmGroupRemoval()
        {
            aux.ControlClick("Delete group", "", "WindowsForms10.BUTTON.app.0.2c908d53");
        }

        private void DeleteGroupWithCont()
        {
            aux.ControlClick("Delete group", "", "WindowsForms10.BUTTON.app.0.2c908d51");
        }

        private void InitGroupRemoval()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");
            aux.WinWait("Delete group");
        }

        private void SelectGroup(string index)
        {
            aux.ControlTreeView(GROUPWINTITLE, "",
                                "WindowsForms10.SysTreeView32.app.0.2c908d51",
                                "Select", "#0|#" + index, "");
        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialogue();
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
            CloseGroupsDialogue();
        }

        private void CloseGroupsDialogue()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        private void OpenGroupsDialogue()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            aux.WinWait(GROUPWINTITLE);
        }
    }
}