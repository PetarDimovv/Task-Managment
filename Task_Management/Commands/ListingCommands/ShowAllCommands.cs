using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;

namespace Task_Management.Commands.ListingCommands
{
    public class ShowAllCommands : BaseCommand
    {
        public ShowAllCommands(IRepository repository) : base(repository)
        {
        }
        public override string Execute()
        {

            
            var sb = new StringBuilder();
            sb.AppendLine("ALL COMMANDS:");
            sb.AppendLine();
            sb.AppendLine("Create Commands:");
            sb.AppendLine("1. Create New Member Command  ====  create member / name");
            sb.AppendLine("2. Create New Team Command  ====  create team / title");
            sb.AppendLine("3. Create New Board  ====  create board / board name / team name");
            sb.AppendLine("4. Create New Bug  ====  create bug / Board name / Title / Description / Steps to reproduce / Priority / Severity");
            sb.AppendLine("5. Create New Story  ====  create story / Board name / Title / Description / Priority / Severity / Status");
            sb.AppendLine("6. Create New Feedback  ====  create feedback / Board name / Title / Description / Rating / Status");
            sb.AppendLine();                                     
            sb.AppendLine("                  ----------------"); 
            sb.AppendLine();                                     
            sb.AppendLine("Add Commands:");
            sb.AppendLine("1. Add Member To Team  ====  add member to team / Member name / Team name");
            sb.AppendLine("2. Add Comment To Task  ====  add comment to task / Comment / Task title");
            sb.AppendLine("3.Add Assignee To Task ==== assign task / Member name / Task name");
            sb.AppendLine("4.Remove Assignee From a Task ==== unassign task / Member name / Task name");
            sb.AppendLine();
            sb.AppendLine("                  ----------------");
            sb.AppendLine();
            sb.AppendLine("Listing Commands:");
            sb.AppendLine("1. Show All Members Command  ====  show all members");
            sb.AppendLine("2. Show All Teams Command  ====  show all teams");
            sb.AppendLine("3. Show All Team Members Command  ====  show all team members / Team name");
            sb.AppendLine("4. Show All Team Boards Command  ====  show all team boards / Team name");
            sb.AppendLine("5. Show Member's Activity Command   ====  show member activity / Member name");
            sb.AppendLine("6. Show Board's Activity Command   ====  show board activity / Board name");
            sb.AppendLine("7. Show Team's Activity Command   ====  show team activity / Team name"); 
            sb.AppendLine();
            sb.AppendLine("                  ----------------");
            sb.AppendLine();
            sb.AppendLine("Modifying Commands:");
            sb.AppendLine("1. Assign Task  ====  assign task / ");
            sb.AppendLine("2. Unassign Task  ====  unassign task / ");
            sb.AppendLine("3. Change Bug Priority/Severity/Status Command  ====  change bug priority/severity/status / Bug's Id / New Priority/Severity/Status");
            sb.AppendLine("4. Change Story Priority/Size/Status Command  ====  change story priority/size/status / Story's Id / New Priority/Size/Status");
            sb.AppendLine("5. Change Feedback Rating/Status Command  ====  change feedback rating/status / Feedback's Id / New Rating/Status");
            



            return sb.ToString();

        }
    }
}
