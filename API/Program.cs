using Leankit;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Cache;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Web.Script.Serialization;
//using System.Windows.Forms;

namespace Leankit
{
    class Program
    {
        static void Initialization(string[] arg, ref Dictionary<string, string> inputParameters, ref Dictionary<string, string> jsonParameters, ref string inputPath, ref string cardOperation)
        {
            // Input Path
            inputPath = arg[0];
            string InputXmlFile = inputPath + "_arguments.xml";
            inputParameters = Helper.Serializer.DeserializeDictionaryFromFile(InputXmlFile);
            
            // Kanban Operation
            cardOperation = null;
            inputParameters.TryGetValue("Operation", out cardOperation);

            // JSON Parameters
            if (cardOperation == "AddCard" || cardOperation == "UpdateCard")
            {
                string JSONXmlFile = inputPath + "_json.xml";
                jsonParameters = Helper.Serializer.DeserializeDictionaryFromFile(JSONXmlFile);
            }
        }

        static void Main(string[] arg)
        {
            // Input Parameters
            Dictionary<string, string> inputParameters = new Dictionary<string, string>();
            Dictionary<string, string> jsonParameters = new Dictionary<string, string>();
            string inputPath = null;
            string cardOperation = null;

            // Initialization
            Initialization(arg, ref inputParameters, ref jsonParameters, ref inputPath, ref cardOperation);

            // Instantiate Board
            KanbanOperation KanbanBoard = new KanbanOperation(inputParameters, jsonParameters, inputPath);

            //----------[ IOR Operations ]----------//
            switch (cardOperation)
            {
                //----------[ Create Card ]----------//
                case "AddCard":
                    KanbanBoard.AddCard();
                    break;
                //----------[ Move Card ]----------//
                case "MoveCard":
                    KanbanBoard.MoveCard();
                    break;
                //----------[ Delete Card ]----------//
                case "DeleteCard":
                    KanbanBoard.DeleteCard();
                    break;
                //----------[ Update Card ]----------//
                case "UpdateCard":
                    KanbanBoard.UpdateCard();
                    break;
                //----------[ Get Card List ]----------//
                case "GetCardList":
                    KanbanBoard.GetCardList();
                    break;
                //----------[ Get Card Status ]----------//
                case "GetCardStatus":
                    KanbanBoard.GetCardStatus();
                    break;
                //----------[ Get Card History ]----------//
                case "GetCardHistory":
                    KanbanBoard.GetCardHistory();
                    break;
                //----------[ Default ]----------//
                default:
                    break;
            }
        }
    }
}
