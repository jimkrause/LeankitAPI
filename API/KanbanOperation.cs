using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Leankit.Entities;
using Leankit.EntitiesWrapper;
using Leankit.EntitiesCustom;


namespace Leankit
{
    public class KanbanOperation
    {
        #region Private

        private Settings inputSettings = new Settings();
        private InputData inputData = new InputData();
        private FunctionAttributes functionAttributes = new FunctionAttributes();
        private KanbanFunction kanbanFunction = null;
        private KanbanFunction KanbanFunction
        {
            get
            {
                if (this.kanbanFunction == null)
                {
                    this.kanbanFunction = new KanbanFunction(inputData, inputSettings);
                }
                return this.kanbanFunction;
            }
        }

        #endregion

        #region Public

        // Constructor
        public KanbanOperation(Dictionary<string, string> inputParam, Dictionary<string, string> jsonParam, string tempPath)
        {
            this.inputSettings.LogInfo = Properties.Settings.Default.LogInfo;
            this.inputSettings.LogDetails = Properties.Settings.Default.LogDetails;
            this.inputData.InputParam = inputParam;
            this.inputData.TempPath = tempPath;
            this.inputData.JsonParam = jsonParam;
            this.functionAttributes.BoardId = Helper.Dictionary.RetrieveFromDict(this.inputData.InputParam, "BoardId");
            this.functionAttributes.CardId = Helper.Dictionary.RetrieveFromDict(this.inputData.InputParam, "CardId");
        }

        public void AddCard()
        {
            try
            {
                // Calculate parameters
                string cardLaneId = this.KanbanFunction.FindLaneIdLaneName();
                string cardPriorityId = this.KanbanFunction.FindPriorityId();
                string cardTypeId = this.KanbanFunction.FindCardTypeId();
                string positionId = "0";
                //string userId = KanbanFunction.FindUserId();

                // Populate Input JSON
                this.inputData.JsonParam = Helper.Dictionary.AddToDict(this.inputData.JsonParam, "Priority", cardPriorityId);
                this.inputData.JsonParam = Helper.Dictionary.AddToDict(this.inputData.JsonParam, "TypeId", cardTypeId);
                this.inputData.JsonParam = Helper.Dictionary.AddToDict(this.inputData.JsonParam, "UserWipOverrideComment", "Created by IOR Workflow");
                //inputData.JsonParam = Helper.Serializer.AddToDict(inputData.JsonParam, "AssignedUserId", userId);

                // Post request
                string cardMethod = "POST";
                string cardAddress = string.Format("Kanban/Api/Board/{0}/AddCardWithWipOverride/Lane/{1}/Position/{2}", this.functionAttributes.BoardId, cardLaneId, positionId);
                string requestBody = Helper.Serializer.ConvertFromJSON(this.inputData.JsonParam);
                string requestReturn = KanbanProxy.DoWebRequest(cardAddress, cardMethod, requestBody);

                // AddCardWrapper
                AddCardWrapper addWrapper = Helper.Serializer.ConvertToJSON<AddCardWrapper>(requestReturn);
                string cardId = addWrapper.ReplyData[0].CardId;

                // Write Output
                Helper.WriteOut.WriteOutputRequest("KanbanCardId", cardId, this.inputData.TempPath);

                if (this.inputSettings.LogInfo)
                {
                    // Write to Event Viewer
                    Dictionary<string, string> logDictionay = new Dictionary<string, string>();
                    logDictionay.Add("BoardId", this.functionAttributes.BoardId);
                    logDictionay.Add("CardId", cardId);
                    logDictionay.Add("LaneId", cardLaneId);
                    logDictionay.Add("TypeId", cardTypeId);

                    Helper.Logging.WriteLog(logDictionay, TraceEventType.Information, true);
                    Helper.Logging.WriteLog(this.inputData.JsonParam, TraceEventType.Information, true);
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
                Helper.Logging.WriteLog(this.inputData.JsonParam, TraceEventType.Information, false);
            }
        }

        public void UpdateCard()
        {
            try
            {
                // Calculate parameters
                string cardLaneId = this.KanbanFunction.FindLaneIdLaneName();
                string cardPriorityId = this.KanbanFunction.FindPriorityId();
                string cardTypeId = this.KanbanFunction.FindCardTypeId();
                //string userId = KanbanFunction.FindUserId();

                // Populate Input JSON
                this.inputData.JsonParam = Helper.Dictionary.AddToDict(this.inputData.JsonParam, "Priority", cardPriorityId);
                this.inputData.JsonParam = Helper.Dictionary.AddToDict(this.inputData.JsonParam, "TypeId", cardTypeId);
                this.inputData.JsonParam = Helper.Dictionary.AddToDict(this.inputData.JsonParam, "UserWipOverrideComment", "Created by IOR Workflow");
                this.inputData.JsonParam = Helper.Dictionary.AddToDict(this.inputData.JsonParam, "LaneId", cardLaneId);
                //this.inputData.JsonParam = Helper.Serializer.AddToDict(this.inputData.JsonParam, "AssignedUserId", userId);

                // Post request
                string cardMethod = "POST";
                string cardAddress = string.Format("Kanban/Api/Board/{0}/UpdateCardWithWipOverride", this.functionAttributes.BoardId);
                string requestBody = Helper.Serializer.ConvertFromJSON(this.inputData.JsonParam);
                string requestReturn = KanbanProxy.DoWebRequest(cardAddress, cardMethod, requestBody);

                // UpdateCardWrapper
                UpdateCardWrapper updateWrapper = Helper.Serializer.ConvertToJSON<UpdateCardWrapper>(requestReturn);

                if (this.inputSettings.LogInfo)
                {
                    // Write to Event Viewer
                    Dictionary<string, string> logDictionay = new Dictionary<string, string>();
                    logDictionay.Add("BoardId", this.functionAttributes.BoardId);
                    logDictionay.Add("CardId", this.functionAttributes.CardId);
                    logDictionay.Add("LaneId", cardLaneId);
                    logDictionay.Add("TypeId", cardTypeId);

                    Helper.Logging.WriteLog(logDictionay, TraceEventType.Information, true);
                    Helper.Logging.WriteLog(this.inputData.JsonParam, TraceEventType.Information, true);
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
                Helper.Logging.WriteLog(this.inputData.JsonParam, TraceEventType.Information, false);
            }
        }

        public void MoveCard()
        {
            try
            {
                // Calculate parameters
                string cardLaneId = this.KanbanFunction.FindLaneIdLaneName();
                string positionId = "0";

                // Populate Input JSON
                this.inputData.JsonParam = Helper.Dictionary.AddToDict(this.inputData.JsonParam, "Comment", "Created by IOR Workflow.");

                // Post request
                string cardmethod = "POST";
                string cardaddress = string.Format("Kanban/Api/Board/{0}/MoveCardWithWipOverride/{1}/Lane/{2}/Position/{3}", this.functionAttributes.BoardId, this.functionAttributes.CardId, cardLaneId, positionId);
                string requestbody = Helper.Serializer.ConvertFromJSON(this.inputData.JsonParam);
                string requestReturn = KanbanProxy.DoWebRequest(cardaddress, cardmethod, requestbody);

                // MoveCardWrapper
                MoveCardWrapper moveWrapper = Helper.Serializer.ConvertToJSON<MoveCardWrapper>(requestReturn);

                if (this.inputSettings.LogInfo)
                {
                    // Write to Event Viewer
                    Dictionary<string, string> logDictionay = new Dictionary<string, string>();
                    logDictionay.Add("BoardId", this.functionAttributes.BoardId);
                    logDictionay.Add("CardId", this.functionAttributes.CardId);
                    logDictionay.Add("LaneId", cardLaneId);

                    Helper.Logging.WriteLog(logDictionay, TraceEventType.Information, true);
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }
        }

        public void DeleteCard()
        {
            try
            {
                // Post request
                string cardmethod = "POST";
                string cardaddress = string.Format("Kanban/Api/Board/{0}/DeleteCard/{1}", functionAttributes.BoardId, functionAttributes.CardId);
                string requestReturn = KanbanProxy.DoWebRequest(cardaddress, cardmethod);

                // DeleteCardWrapper
                DeleteCardWrapper deleteWrapper = Helper.Serializer.ConvertToJSON<DeleteCardWrapper>(requestReturn);

                if (this.inputSettings.LogInfo)
                {
                    // Write to Event Viewer
                    Dictionary<string, string> logDictionay = new Dictionary<string, string>();
                    logDictionay.Add("BoardId", this.functionAttributes.BoardId);
                    logDictionay.Add("CardId", this.functionAttributes.CardId);

                    Helper.Logging.WriteLog(logDictionay, TraceEventType.Information, true);
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }
        }

        public void GetCardList()
        {
            try
            {
                // Calculate parameters
                string cardLaneId = this.KanbanFunction.FindLaneIdLaneName();
                int laneIndex = this.KanbanFunction.FindLaneIndex(cardLaneId);
                List<string> cardIdList = this.KanbanFunction.FindCardList(laneIndex);

                // Write Output
                Helper.WriteOut.WriteOutputList(cardIdList, this.inputData.TempPath);

                if (this.inputSettings.LogInfo)
                {
                    // Write to Event Viewer
                    Helper.Logging.WriteLog(cardIdList, TraceEventType.Information, true);
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }
        }

        public void GetCardStatus()
        {
            try
            {
                // Calculate parameters
                string laneId = this.KanbanFunction.FindLaneIdCardId();
                string laneName = this.KanbanFunction.FindLaneName(laneId);
                string laneStatus = Helper.stringMatch.GetLaneStatus(laneName);

                // Write Output
                Helper.WriteOut.WriteOutputRequest("CardStatus", laneStatus, this.inputData.TempPath);

                if (this.inputSettings.LogInfo)
                {
                    // Write to Event Viewer
                    Helper.Logging.WriteLog("Cardstatus", laneStatus, TraceEventType.Information, true);
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }
        }

        public void GetCardHistory()
        {
            try
            {
                // CardHistoryWrapper
                CardHistoryWrapper wrapper = this.KanbanFunction.FindCardHistory();

                // Write Output

                if (this.inputSettings.LogInfo)
                {
                    // Write to Event Viewer
                }

            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }
        }
         
        #endregion

    }
}
