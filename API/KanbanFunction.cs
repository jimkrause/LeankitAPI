using System;
using System.Net;
using System.Diagnostics;

using Leankit.Entities;
using Leankit.EntitiesWrapper;
using Leankit.EntitiesCustom;
using Leankit.Interfaces;

using EasyHttp.Http;
using System.Collections.Generic;

namespace Leankit
{
    public class KanbanFunction
    {
        #region Private 
        
        // Input Data
        private Settings inputSettings = new Settings();
        private InputData inputData  = new InputData();
        private FunctionAttributes functionAttributes = new FunctionAttributes();
        
        // Identifier Wrapper
        private IdentifierWrapper idWrapper = null;
        private IdentifierWrapper IdWrapper
        {
            get
            {
                if (this.idWrapper == null)
                {
                    this.idWrapper = this.ApiCaller.GetBoardIdentifiers(functionAttributes.BoardId);
                }
                return this.idWrapper;
            }
        }
        
        // Attributes Wrapper
        private AttributesWrapper attWrapper = null;
        private AttributesWrapper AttWrapper
        {
            get
            {
                if (this.attWrapper == null)
                {
                    this.attWrapper = this.ApiCaller.GetBoardAttributes(functionAttributes.BoardId);
                }
                return this.attWrapper;
            }
        }

        // Card Wrapper
        private CardWrapper cardWrapper = null;
        private CardWrapper CardWrapper
        {
            get
            {
                if (this.cardWrapper == null)
                {
                    this.cardWrapper = this.ApiCaller.GetCardIdentifiers(functionAttributes.BoardId, functionAttributes.CardId);
                }
                return this.cardWrapper;
            }
        }
        
        // Card History Wrapper
        private CardHistoryWrapper historyWrapper = null;
        private CardHistoryWrapper HistoryWrapper
        {
            get
            {
                if (this.historyWrapper == null)
                {
                    this.historyWrapper = this.ApiCaller.GetCardHistory(functionAttributes.BoardId, functionAttributes.CardId);
                }
                return this.historyWrapper;
            }
        }

        // Api Caller
        private ApiCaller apiCaller = null;
        private ApiCaller ApiCaller
        {
            get
            {
                if (this.apiCaller == null)
                {
                    this.apiCaller = this.GetCaller();
                }
                return this.apiCaller;
            }
        }
        private ApiCaller GetCaller()
        {
            var caller = new ApiCaller("http://laserfiche.leankit.com");
            caller.Authenticate(@"IOR@laserfiche.com", @"L@53rf1ch3");

            return caller;
        }

        #endregion

        #region Public

        // Constructor
        public KanbanFunction(InputData inputData, Settings inputSettings)
        {
            this.functionAttributes.BoardId = Helper.Dictionary.RetrieveFromDict(inputData.InputParam, "BoardId");
            this.functionAttributes.Priority = Helper.Dictionary.RetrieveFromDict(inputData.InputParam, "Priority");
            this.functionAttributes.CardId = Helper.Dictionary.RetrieveFromDict(inputData.InputParam, "CardId");
            this.functionAttributes.BoardLane = Helper.Dictionary.RetrieveFromDict(inputData.InputParam, "BoardLane");
            this.functionAttributes.CardType = Helper.Dictionary.RetrieveFromDict(inputData.InputParam, "CardType");
            this.functionAttributes.UserName = Helper.Dictionary.RetrieveFromDict(inputData.InputParam, "Approver");
            this.functionAttributes.Title = Helper.Dictionary.RetrieveFromDict(inputData.InputParam, "Title");
        }
        
        public string FindPriorityId()
        {
            try
            {
                foreach (Priority priority in this.IdWrapper.ReplyData[0].Priorities)
                {
                    if (this.functionAttributes.Priority == priority.Name)
                    {
                        if (inputSettings.LogDetails)
                        {
                            Helper.Logging.WriteLog("PriorityId", priority.Id, TraceEventType.Information, true);
                        }
                        return priority.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }
            
            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find Priority Id", TraceEventType.Warning, true);

            return null;
        }

        public string FindLaneName(string boardLaneId)
        {
            try
            {
                foreach (Lane lane in this.IdWrapper.ReplyData[0].Lanes)
                {
                    if (lane.Id == boardLaneId)
                    {
                        if (inputSettings.LogDetails)
                        {
                            Helper.Logging.WriteLog("LaneName", lane.Name, TraceEventType.Information, true);
                        }
                        return lane.Name;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }

            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find Lane Name", TraceEventType.Warning, true);

            return null;
        }

        public string FindLaneIdCardId()
        {
            try
            {
                string laneId = this.CardWrapper.ReplyData[0].LaneId;

                if (inputSettings.LogDetails)
                {
                    Helper.Logging.WriteLog("LaneId", laneId, TraceEventType.Information, true);
                }

                return laneId;
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }

            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find Lane Id", TraceEventType.Warning, true);

            return null;
        }

        public string FindLaneIdLaneName()
        {
            try
            {
                foreach (Lane lane in this.IdWrapper.ReplyData[0].Lanes)
                {
                    if (lane.Name == this.functionAttributes.BoardLane)
                    {
                        if (inputSettings.LogDetails)
                        {
                            Helper.Logging.WriteLog("LaneId", lane.Id, TraceEventType.Information, true);
                        }
                        return lane.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }

            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find Lane Id", TraceEventType.Warning, true);

            return null;
        }

        public int FindLaneIndex(string boardLaneId)
        {
            try
            {
                int index = 0;

                foreach (Lane lane in this.AttWrapper.ReplyData[0].Lanes)
                {
                    if (lane.Id == boardLaneId)
                    {
                        if (inputSettings.LogDetails)
                        {
                            Helper.Logging.WriteLog("LaneIndex", index.ToString(), TraceEventType.Information, true);
                        }
                        return index;
                    }
                    index++;
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }

            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find Lane Index", TraceEventType.Warning, true);

            return -1;
        }

        public string FindCardTypeId()
        {
            try
            {
                foreach (CardType cardType in this.IdWrapper.ReplyData[0].CardTypes)
                {
                    if (cardType.Name == this.functionAttributes.CardType)
                    {
                        if (inputSettings.LogDetails)
                        {
                            Helper.Logging.WriteLog("CardTypeId", cardType.Id, TraceEventType.Information, true);
                        }
                        return cardType.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }

            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find CardType Id", TraceEventType.Warning, true);

            return null;
        }

        public string FindUserId()
        {
            try
            {
                foreach (BoardUser boardUser in this.AttWrapper.ReplyData[0].BoardUsers)
                {
                    if (boardUser.FullName.ToLower() == this.functionAttributes.UserName.ToLower())
                    {
                        if (inputSettings.LogDetails)
                        {
                            Helper.Logging.WriteLog("UserId", boardUser.Id, TraceEventType.Information, true);
                        }
                        return boardUser.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }

            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find User Id", TraceEventType.Warning, true);

            return null;
        }

        public string FindCardId()
        {
            try
            {
                foreach (Lane lane in this.AttWrapper.ReplyData[0].Lanes)
                {
                    foreach (Card card in lane.Cards)
                    {
                        if (card.Title == this.functionAttributes.Title)
                        {
                            if (inputSettings.LogDetails)
                            {
                                Helper.Logging.WriteLog("CardId", card.Id, TraceEventType.Information, true);
                            }
                            return card.Id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }

            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find Card Id", TraceEventType.Warning, true);

            return null;
        }

        public List<string> FindCardList(int laneIndex)
        {
            try
            {
                List<string> cardIdList = new List<string>();
                Lane lane = this.AttWrapper.ReplyData[0].Lanes[laneIndex];

                foreach (Card card in lane.Cards)
                {
                    cardIdList.Add(card.Id);
                }

                if (inputSettings.LogDetails)
                {
                    Helper.Logging.WriteLog(cardIdList, TraceEventType.Information, true);
                }

                return cardIdList;
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }

            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find Card List", TraceEventType.Warning, true);

            return null;
        }

        public CardHistoryWrapper FindCardHistory()
        {
            try
            {
                if (inputSettings.LogDetails)
                {
                    //Helper.Logging.WriteLog( , TraceEventType.Information, true);
                }
                return this.HistoryWrapper;
            }
            catch (Exception ex)
            {
                Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
            }

            // Write to Event Viewer 
            Helper.Logging.WriteLog("Error", "Cannot find Card History", TraceEventType.Warning, true);

            return null;
        }

        #endregion
    }
}
