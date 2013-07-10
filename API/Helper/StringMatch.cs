using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leankit.Helper
{
    class stringMatch
    {
        public static string GetLaneStatus(string _laneName)
        {
            string cardStatus = null;

            switch (_laneName)
            {
                case "Backlog:Infrastructure Backlog":
                    cardStatus = "Backlog";
                    break;
                case "Backlog:Requirements being Determined":
                    cardStatus = "Backlog";
                    break;
                case "Backlog:IOR Backlog:IOR Features":
                    cardStatus = "Backlog";
                    break;
                case "Defined and Ready for Work":
                    cardStatus = "Queue";
                    break;
                case "Implementation in Progress:Process Improvement":
                    cardStatus = "Implementation";
                    break;
                case "Implementation in Progress:Analytics and CRM":
                    cardStatus = "Implementation";
                    break;
                case "Implementation in Progress:Systems Portfolio":
                    cardStatus = "Implementation";
                    break;
                case "Implementation in Progress:Timesheet":
                    cardStatus = "Implementation";
                    break;
                case "Ready for Testing":
                    cardStatus = "Implementation";
                    break;
                case "Testing in Progress":
                    cardStatus = "Testing";
                    break;
                case "Fix and Verfication in Progress:Analytics and CRM":
                    cardStatus = "Testing";
                    break;
                case "Fix and Verfication in Progress:Systems Portfolio":
                    cardStatus = "Testing";
                    break;
                case "Fix and Verfication in Progress:Timesheet":
                    cardStatus = "Testing";
                    break;
                case "UAT":
                    cardStatus = "UAT";
                    break;
                case "Ready to Deploy":
                    cardStatus = "Deployment";
                    break;
                case "Deployment and Smoke Test in Progress":
                    cardStatus = "Deployment";
                    break;
                case "Done":
                    cardStatus = "Done";
                    break;
                case "Archive":
                    cardStatus = "Archive";
                    break;
                default:
                    break;

            }

            return cardStatus;
        }
    }
}
