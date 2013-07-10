using System;
using System.Net;
using Leankit.Entities;
using Leankit.EntitiesWrapper;
using Leankit.Interfaces;
using EasyHttp.Http;

using System.Text;
using System.Net.Cache;
using System.IO;

namespace Leankit
{
    // This class does all of the communication with Leankit API
    public class ApiCaller
    {
        #region Private

        private BoardAuthenticationDetails data = new BoardAuthenticationDetails();

        private T Get<T>(string address)
        {
            HttpClient request = CreateHttpRequest(data.Username, data.Password);
            string url = data.Address + address;
            try
            {
                HttpResponse response = request.Get(url);
                T staticBody = response.StaticBody<T>();
                return staticBody;
            }
            catch (WebException ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Public

        // Constructor
        public ApiCaller(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentNullException("baseUrl");

            this.data.Address = baseUrl;
        }

        public void Authenticate(string userName, string password)
        {
            this.data.Username = userName;
            this.data.Password = password;
        }

        public CardHistoryWrapper GetCardHistory(string boardId, string cardId)
        {
            //Example of output
            //{"ReplyData":[[{"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39312338,"ToLaneTitle":"Ready for Analysis","Type":"CardCreationEventDTO","UserName":"IOR@laserfiche.com","UserFullName":"IOR Bot","GravatarLink":"a055d1557fec5d70cbde11ec8b976d6f","DateTime":"05/15/2013 at 05:28:53 PM","TimeDifference":"","LastDate":635042609330000000},{"AssignedUserId":39035049,"AssignedUserFullName":"Kurt Rapelje","AssignedUserEmailAddres":"kurt.rapelje@laserfiche.com","IsUnassigning":false,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39312338,"ToLaneTitle":"Ready for Analysis","Type":"UserAssignmentEventDTO","UserName":"IOR@laserfiche.com","UserFullName":"IOR Bot","GravatarLink":"a055d1557fec5d70cbde11ec8b976d6f","DateTime":"05/15/2013 at 05:28:55 PM","TimeDifference":"Two seconds later","LastDate":635042609350000000},{"FromLaneId":39312338,"FromLaneTitle":"Ready for Analysis","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065299,"ToLaneTitle":"Requirements Being Determined","Type":"CardMoveEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/16/2013 at 07:15:05 AM","TimeDifference":"Thirteen hours later","LastDate":635043105050000000},{"FromLaneId":39065299,"FromLaneTitle":"Requirements Being Determined","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39312338,"ToLaneTitle":"Ready for Analysis","Type":"CardMoveEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/16/2013 at 07:15:13 AM","TimeDifference":"Eight seconds later","LastDate":635043105130000000},{"Changes":[{"FieldName":"Description","OldValue":"-\tMeta tags on the following pages should be changed:\no\tHomepage: \n§\tCurrent Title: Laserfiche ECM Systems | Enterprise Content Management System | Document Management Software | Document Imaging Solutions\n§\tProposed Title: Enterprise Content Management (ECM) Software | Laserfiche\n§\tCurrent Description: Laserfiche: Enterprise Content Management Systems │ECM │ Document Management Software │ Document Imaging Solutions │ DoD 5015.2-Certified Records Management │ Business Process Management │ Capture\n§\tProposed Description: Laserfiche provides industry leading Enterprise Content Management (ECM) software that helps organizations securely manage content and automate business processes.\n\no\tProducts:\n§\tCurrent Title: Products Overview: Enterprise Content Management | ECM | Document Management | Business Process Management | BPM\n§\tProposed Title: ECM Products Overview | Laserfiche\n§\tCurrent Description: Laserfiche gives IT central control over information infrastructure, while offering business units the flexibility to react quickly to changing conditions.\n§\tProposed Description: Evaluate ECM solutions for your organization. Featuring document imaging, document management, business process management and records management baked into the core system architecture. [Submitter: \"Simon Poulton\"]","NewValue":"<p>- Meta tags on the following pages should be changed: o Homepage: § Current Title: Laserfiche ECM Systems | Enterprise Content Management System | Document Management Software | Document Imaging Solutions § Proposed Title: Enterprise Content Management (ECM) Software | Laserfiche § Current Description: Laserfiche: Enterprise Content Management Systems │ECM │ Document Management Software │ Document Imaging Solutions │ DoD 5015.2-Certified Records Management │ Business Process Management │ Capture § Proposed Description: Laserfiche provides industry leading Enterprise Content Management (ECM) software that helps organizations securely manage content and automate business processes. o Products: § Current Title: Products Overview: Enterprise Content Management | ECM | Document Management | Business Process Management | BPM § Proposed Title: ECM Products Overview | Laserfiche § Current Description: Laserfiche gives IT central control over information infrastructure, while offering business units the flexibility to react quickly to changing conditions. § Proposed Description: Evaluate ECM solutions for your organization. Featuring document imaging, document management, business process management and records management baked into the core system architecture. [Submitter: \"Simon Poulton\"]</p>","OldDueDate":null,"NewDueDate":null}],"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39312338,"ToLaneTitle":"Ready for Analysis","Type":"CardFieldsChangedEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/16/2013 at 07:15:13 AM","TimeDifference":"","LastDate":635043105130000000},{"AssignedUserId":39035049,"AssignedUserFullName":"Kurt Rapelje","AssignedUserEmailAddres":"kurt.rapelje@laserfiche.com","IsUnassigning":true,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39312338,"ToLaneTitle":"Ready for Analysis","Type":"UserAssignmentEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/16/2013 at 07:15:14 AM","TimeDifference":"One second later","LastDate":635043105140000000},{"AssignedUserId":39817694,"AssignedUserFullName":"Randall Weis","AssignedUserEmailAddres":"randall.weis@laserfiche.com","IsUnassigning":false,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39312338,"ToLaneTitle":"Ready for Analysis","Type":"UserAssignmentEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/16/2013 at 07:15:14 AM","TimeDifference":"","LastDate":635043105140000000},{"FromLaneId":39312338,"FromLaneTitle":"Ready for Analysis","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065299,"ToLaneTitle":"Requirements Being Determined","Type":"CardMoveEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/16/2013 at 07:15:18 AM","TimeDifference":"Four seconds later","LastDate":635043105180000000},{"FromLaneId":39065299,"FromLaneTitle":"Requirements Being Determined","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":41659688,"ToLaneTitle":"Defined and Ready for Work: System Portfolio","Type":"CardMoveEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/20/2013 at 10:10:59 AM","TimeDifference":"Four days later","LastDate":635046666590000000},{"FromLaneId":41659688,"FromLaneTitle":"Defined and Ready for Work: System Portfolio","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065298,"ToLaneTitle":"Ready for Testing","Type":"CardMoveEventDTO","UserName":"randall.weis@laserfiche.com","UserFullName":"Randall Weis","GravatarLink":"d2ef7256d405c7fc963ac782ff1dd9fa","DateTime":"05/20/2013 at 02:02:10 PM","TimeDifference":"Three hours later","LastDate":635046805300000000},{"AssignedUserId":39817694,"AssignedUserFullName":"Randall Weis","AssignedUserEmailAddres":"randall.weis@laserfiche.com","IsUnassigning":true,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065298,"ToLaneTitle":"Ready for Testing","Type":"UserAssignmentEventDTO","UserName":"kurt.rapelje@laserfiche.com","UserFullName":"Kurt Rapelje","GravatarLink":"61228251d1b887d3790d90f3c0f67b91","DateTime":"05/21/2013 at 09:55:42 AM","TimeDifference":"Nineteen hours later","LastDate":635047521420000000},{"AssignedUserId":39817666,"AssignedUserFullName":"Ben Geng","AssignedUserEmailAddres":"ben.geng@laserfiche.com","IsUnassigning":false,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065298,"ToLaneTitle":"Ready for Testing","Type":"UserAssignmentEventDTO","UserName":"ben.geng@laserfiche.com","UserFullName":"Ben Geng","GravatarLink":"d306acb5ef75ff9b1f0a6e5b5094bde6","DateTime":"05/23/2013 at 10:16:10 AM","TimeDifference":"Two days later","LastDate":635049261700000000},{"FromLaneId":39065298,"FromLaneTitle":"Ready for Testing","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065297,"ToLaneTitle":"Testing in Progress","Type":"CardMoveEventDTO","UserName":"ben.geng@laserfiche.com","UserFullName":"Ben Geng","GravatarLink":"d306acb5ef75ff9b1f0a6e5b5094bde6","DateTime":"05/23/2013 at 10:16:32 AM","TimeDifference":"Twenty Two seconds later","LastDate":635049261920000000},{"AssignedUserId":39817666,"AssignedUserFullName":"Ben Geng","AssignedUserEmailAddres":"ben.geng@laserfiche.com","IsUnassigning":true,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065297,"ToLaneTitle":"Testing in Progress","Type":"UserAssignmentEventDTO","UserName":"ben.geng@laserfiche.com","UserFullName":"Ben Geng","GravatarLink":"d306acb5ef75ff9b1f0a6e5b5094bde6","DateTime":"05/24/2013 at 08:00:36 AM","TimeDifference":"Twenty One hours later","LastDate":635050044360000000},{"AssignedUserId":39817694,"AssignedUserFullName":"Randall Weis","AssignedUserEmailAddres":"randall.weis@laserfiche.com","IsUnassigning":false,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065297,"ToLaneTitle":"Testing in Progress","Type":"UserAssignmentEventDTO","UserName":"ben.geng@laserfiche.com","UserFullName":"Ben Geng","GravatarLink":"d306acb5ef75ff9b1f0a6e5b5094bde6","DateTime":"05/24/2013 at 08:00:36 AM","TimeDifference":"","LastDate":635050044360000000},{"CommentText":"\r\n<p>Title of Home page is not updated.</p>\r\n","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":0,"ToLaneTitle":null,"Type":"CommentPostEventDTO","UserName":"ben.geng@laserfiche.com","UserFullName":"Ben Geng","GravatarLink":"d306acb5ef75ff9b1f0a6e5b5094bde6","DateTime":"05/24/2013 at 08:00:53 AM","TimeDifference":"Seventeen seconds later","LastDate":635050044530000000},{"FromLaneId":39065297,"FromLaneTitle":"Testing in Progress","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":41962169,"ToLaneTitle":"Fix and Verification in Progress: Systems Portfolio","Type":"CardMoveEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/24/2013 at 10:04:10 AM","TimeDifference":"Two hours later","LastDate":635050118500000000},{"FromLaneId":41962169,"FromLaneTitle":"Fix and Verification in Progress: Systems Portfolio","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065297,"ToLaneTitle":"Testing in Progress","Type":"CardMoveEventDTO","UserName":"randall.weis@laserfiche.com","UserFullName":"Randall Weis","GravatarLink":"d2ef7256d405c7fc963ac782ff1dd9fa","DateTime":"05/24/2013 at 11:24:24 AM","TimeDifference":"One hour later","LastDate":635050166640000000},{"AssignedUserId":39817694,"AssignedUserFullName":"Randall Weis","AssignedUserEmailAddres":"randall.weis@laserfiche.com","IsUnassigning":true,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065297,"ToLaneTitle":"Testing in Progress","Type":"UserAssignmentEventDTO","UserName":"randall.weis@laserfiche.com","UserFullName":"Randall Weis","GravatarLink":"d2ef7256d405c7fc963ac782ff1dd9fa","DateTime":"05/24/2013 at 11:24:25 AM","TimeDifference":"One second later","LastDate":635050166650000000},{"AssignedUserId":39817666,"AssignedUserFullName":"Ben Geng","AssignedUserEmailAddres":"ben.geng@laserfiche.com","IsUnassigning":false,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065297,"ToLaneTitle":"Testing in Progress","Type":"UserAssignmentEventDTO","UserName":"randall.weis@laserfiche.com","UserFullName":"Randall Weis","GravatarLink":"d2ef7256d405c7fc963ac782ff1dd9fa","DateTime":"05/24/2013 at 11:24:25 AM","TimeDifference":"","LastDate":635050166650000000},{"FromLaneId":39065297,"FromLaneTitle":"Testing in Progress","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":41962169,"ToLaneTitle":"Fix and Verification in Progress: Systems Portfolio","Type":"CardMoveEventDTO","UserName":"ben.geng@laserfiche.com","UserFullName":"Ben Geng","GravatarLink":"d306acb5ef75ff9b1f0a6e5b5094bde6","DateTime":"05/24/2013 at 11:35:55 AM","TimeDifference":"Eleven minutes later","LastDate":635050173550000000},{"AssignedUserId":39817666,"AssignedUserFullName":"Ben Geng","AssignedUserEmailAddres":"ben.geng@laserfiche.com","IsUnassigning":true,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":41962169,"ToLaneTitle":"Fix and Verification in Progress: Systems Portfolio","Type":"UserAssignmentEventDTO","UserName":"ben.geng@laserfiche.com","UserFullName":"Ben Geng","GravatarLink":"d306acb5ef75ff9b1f0a6e5b5094bde6","DateTime":"05/24/2013 at 11:35:58 AM","TimeDifference":"Three seconds later","LastDate":635050173580000000},{"AssignedUserId":39817694,"AssignedUserFullName":"Randall Weis","AssignedUserEmailAddres":"randall.weis@laserfiche.com","IsUnassigning":false,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":41962169,"ToLaneTitle":"Fix and Verification in Progress: Systems Portfolio","Type":"UserAssignmentEventDTO","UserName":"ben.geng@laserfiche.com","UserFullName":"Ben Geng","GravatarLink":"d306acb5ef75ff9b1f0a6e5b5094bde6","DateTime":"05/24/2013 at 11:36:18 AM","TimeDifference":"Twenty seconds later","LastDate":635050173780000000},{"CommentText":"\r\n<p>fixed</p>\r\n","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":0,"ToLaneTitle":null,"Type":"CommentPostEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/28/2013 at 10:06:53 AM","TimeDifference":"Three days later","LastDate":635053576130000000},{"CommentText":"\r\n<p>fixed and ready for testing</p>\r\n","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":0,"ToLaneTitle":null,"Type":"CommentPostEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/28/2013 at 10:06:53 AM","TimeDifference":"","LastDate":635053576130000000},{"AssignedUserId":39817694,"AssignedUserFullName":"Randall Weis","AssignedUserEmailAddres":"randall.weis@laserfiche.com","IsUnassigning":true,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":41962169,"ToLaneTitle":"Fix and Verification in Progress: Systems Portfolio","Type":"UserAssignmentEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"05/28/2013 at 10:07:14 AM","TimeDifference":"Twenty One seconds later","LastDate":635053576340000000},{"FromLaneId":41962169,"FromLaneTitle":"Fix and Verification in Progress: Systems Portfolio","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39179295,"ToLaneTitle":"Ready to Deploy","Type":"CardMoveEventDTO","UserName":"ben.geng@laserfiche.com","UserFullName":"Ben Geng","GravatarLink":"d306acb5ef75ff9b1f0a6e5b5094bde6","DateTime":"05/31/2013 at 09:52:57 AM","TimeDifference":"Two days later","LastDate":635056159770000000},{"AssignedUserId":39817694,"AssignedUserFullName":"Randall Weis","AssignedUserEmailAddres":"randall.weis@laserfiche.com","IsUnassigning":false,"CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39179295,"ToLaneTitle":"Ready to Deploy","Type":"UserAssignmentEventDTO","UserName":"winfong.lee@laserfiche.com","UserFullName":"Winfong Lee","GravatarLink":"0599720fa876197bdd92e61903718959","DateTime":"05/31/2013 at 10:13:11 AM","TimeDifference":"Twenty minutes later","LastDate":635056171910000000},{"FromLaneId":39179295,"FromLaneTitle":"Ready to Deploy","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39179294,"ToLaneTitle":"Deployment and Smoke Test in Progress","Type":"CardMoveEventDTO","UserName":"randall.weis@laserfiche.com","UserFullName":"Randall Weis","GravatarLink":"d2ef7256d405c7fc963ac782ff1dd9fa","DateTime":"05/31/2013 at 02:51:20 PM","TimeDifference":"Four hours later","LastDate":635056338800000000},{"FromLaneId":39179294,"FromLaneTitle":"Deployment and Smoke Test in Progress","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065292,"ToLaneTitle":"Done","Type":"CardMoveEventDTO","UserName":"randall.weis@laserfiche.com","UserFullName":"Randall Weis","GravatarLink":"d2ef7256d405c7fc963ac782ff1dd9fa","DateTime":"06/03/2013 at 09:54:11 AM","TimeDifference":"Two days later","LastDate":635058752510000000},{"FromLaneId":39065292,"FromLaneTitle":"Done","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065289,"ToLaneTitle":"Archive","Type":"CardMoveEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"06/05/2013 at 07:26:26 AM","TimeDifference":"One day later","LastDate":635060391860000000},{"FromLaneId":39065289,"FromLaneTitle":"Archive","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065292,"ToLaneTitle":"Done","Type":"CardMoveEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"06/05/2013 at 07:26:44 AM","TimeDifference":"Eighteen seconds later","LastDate":635060392040000000},{"FromLaneId":39065292,"FromLaneTitle":"Done","CardId":51668439,"CardTitle":"Update Meta Tags","ToLaneId":39065289,"ToLaneTitle":"Archive","Type":"CardMoveEventDTO","UserName":"mike.wu@laserfiche.com","UserFullName":"Mike Wu","GravatarLink":"57ff1f86af900deb22911239b161d8bd","DateTime":"07/01/2013 at 06:56:50 AM","TimeDifference":"Twenty Five days later","LastDate":635082838100000000}]],"ReplyCode":200,"ReplyText":""}

            return this.Get<CardHistoryWrapper>(string.Format("/Kanban/Api/Card/History/{0}/{1}", boardId, cardId));
        }

        public CardWrapper GetCardIdentifiers(string boardId, string cardId)
        {
            //Example of output
            // This function currently isn't being used so i don't have a sample output.

            return this.Get<CardWrapper>(string.Format("/Kanban/Api/Board/{0}/GetCard/{1}", boardId, cardId));
        }

        public IdentifierWrapper GetBoardIdentifiers(string boardId)
        {
            //This method is used to get all the necessary Identifiers for a Board used within the other API calls.
            //This method takes the Id of the Board which can be derived from the URL when viewing the Board or by calling using the GetBoards method
            //Ex. - http://test.leankitkanban.com/Boards/Show/12345 - BoardId = 12345
            //Example of output
            //{"ReplyCode":200,"ReplyText":"The Board Identifiers were retrieved successfully.","ReplyData":[{"BoardId":101,"CardTypes":[{"Id":1,"Name":"Defect"},{"Id":2,"Name":"Improvement"},{"Id":3,"Name":"Feature"},{"Id":4,"Name":"Task"}],"BoardUsers":[{"Id":101,"Name":"testuser@test.com"},{"Id":102,"Name":"testmanager@test.com"},{"Id":103,"Name":"testreadonly@test.com"},{"Id":1,"Name":"info@bandit-software.com"}],"Lanes":[{"Id":303,"Name":"Backlog"},{"Id":304,"Name":"Archive"},{"Id":305,"Name":"Lane 1"},{"Id":306,"Name":"Lane 2"},{"Id":307,"Name":"Lane 3"}],"ClassesOfService":[{"Id":104,"Name":"Date Dependent"},{"Id":102,"Name":"Expedite"},{"Id":103,"Name":"Regulatory"},{"Id":101,"Name":"Standard"}]}]}

            return this.Get<IdentifierWrapper>(string.Format("/Kanban/Api/Board/{0}/GetBoardIdentifiers", boardId));
        }

        public AttributesWrapper GetBoardAttributes(string boardId)
        {
            //Returns a listing of the Board attributes, the Lanes within the Board and all the Cards within those lanes.  Does not include the Cards within the Archive. 
            //Example of output
            //{"ReplyCode":200,"ReplyText":"Board successfully retrieved.","ReplyData":[{"Id":1,"Title":"Realistic Board","Description":"","Version":8,"Active":false,"OrganizationId":1,"Lanes":[{"Id":3,"Active":true,"Title":"Work Queue","Description":"","CardLimit":15,"ClassType":0,"Width":3,"Version":20,"Cards":[{"Id":1,"LaneId":0,"Title":"aaa","Description":"","ClassType":0,"TypeName":"Feature","Size":0,"Active":false,"Color":"Green","Version":8,"AssignedUserId":1,"IsBlocked":false}],"UserSubscription":0},{"Id":4,"Active":true,"Title":"Design","Description":"","CardLimit":3,"ClassType":0,"Width":1,"Version":20,"Cards":[{"Id":2,"LaneId":0,"Title":"bbb","Description":"","ClassType":2,"TypeName":"Defect","Size":0,"Active":false,"Color":"Red","Version":9,"AssignedUserId":1,"IsBlocked":false}],"UserSubscription":0},{"Id":5,"Active":true,"Title":"Doing","Description":"","CardLimit":2,"ClassType":0,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},{"Id":6,"Active":true,"Title":"Code Review","Description":"","CardLimit":4,"ClassType":0,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},{"Id":7,"Active":true,"Title":"QA Test","Description":"","CardLimit":3,"ClassType":0,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},{"Id":8,"Active":true,"Title":"Deploy Queue","Description":"","CardLimit":6,"ClassType":0,"Width":2,"Version":20,"Cards":[],"UserSubscription":0},{"Id":9,"Active":true,"Title":"Done","Description":"","CardLimit":15,"ClassType":0,"Width":3,"Version":20,"Cards":[],"UserSubscription":0}],"BoardUsers":[{"Id":1,"FullName":"root root","EmailAddress":"chris.hefley@imihealth.com"}],"Backlog":{"Id":1,"Active":false,"Title":"Backlog","Description":"","CardLimit":0,"ClassType":1,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},"Archive":{"Id":2,"Active":false,"Title":"Archive","Description":"","CardLimit":0,"ClassType":2,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},"UserSubscription":0}]}

            return this.Get<AttributesWrapper>(string.Format("/Kanban/Api/Boards/{0}", boardId));
        }

        public string GetListOfItemsInBackLog(string boardId)
        {
            //Gets a listing of all the Cards contained in the Backlog
            //This method takes the Id of the Board which can be derived from the URL when viewing the Board or by calling using the GetBoards method
            //Ex. - http://test.leankitkanban.com/Boards/Show/12345 - BoardId = 12345
            //Example of output
            //{"ReplyCode":200,"ReplyText":"Backlog successfully retrieved.","ReplyData":[{"Id":1,"Active":false,"Title":"Backlog","Description":"","CardLimit":0,"ClassType":1,"Width":1,"Version":18,"Cards":[],"UserSubscription":0}]}

            return this.Get<string>(string.Format("/Kanban/Api/Board/{0}/Backlog", boardId));
        }

        #region old code
        //// <summary>
        ///// Gets a List of all the Items currently in the Archive
        ///// </summary>
        ///// <param name="boardId">string boardId</param>
        ///// <returns>string of JSON data</returns>
        //public string GetListOfItemsInArchive(string boardId)
        //{
        //    //Gets a listing of all the Cards contained in the Archive
        //    //This method takes the Id of the Board which can be derived from the URL when viewing the Board or by calling using the GetBoards method
        //    //Ex. - http://test.leankitkanban.com/Boards/Show/12345 - BoardId = 12345
        //    //Example of output
        //    //{"ReplyCode":200,"ReplyText":"Archive successfully retrieved.","ReplyData":[{"Id":2,"Active":false,"Title":"Archive","Description":"","CardLimit":0,"ClassType":2,"Width":1,"Version":19,"Cards":[],"UserSubscription":0}]}

        //    var response = Requester(string.Format("/Kanban/Api/Board/{0}/Archive", boardId), "GET");

        //    return response;
        //}

        ///// <summary>
        ///// Gets all the Boards for a Specified Account - if a user has access to any boards. Admin returns ALL boards
        ///// </summary>
        ///// <returns>string of JSON data</returns>
        //public string GetListOfAllBoardsForAnAccount()
        //{
        //    //Lists all the Boards for the Organization
        //    //Example of output
        //    //{"ReplyCode":200,"ReplyText":"Board(s) successfully retrieved.","ReplyData":[[{"Id":1,"Title":"Realistic Board","Description":"","Version":8,"Active":false,"OrganizationId":1,"Lanes":[{"Id":3,"Active":true,"Title":"Work Queue","Description":"","CardLimit":15,"ClassType":0,"Width":3,"Version":20,"Cards":[{"Id":1,"LaneId":0,"Title":"aaa","Description":"","ClassType":0,"TypeName":"Feature","Size":0,"Active":false,"Color":"Green","Version":8,"AssignedUserId":1,"IsBlocked":false}],"UserSubscription":0},{"Id":4,"Active":true,"Title":"Design","Description":"","CardLimit":3,"ClassType":0,"Width":1,"Version":20,"Cards":[{"Id":2,"LaneId":0,"Title":"bbb","Description":"","ClassType":2,"TypeName":"Defect","Size":0,"Active":false,"Color":"Red","Version":9,"AssignedUserId":1,"IsBlocked":false}],"UserSubscription":0},{"Id":5,"Active":true,"Title":"Doing","Description":"","CardLimit":2,"ClassType":0,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},{"Id":6,"Active":true,"Title":"Code Review","Description":"","CardLimit":4,"ClassType":0,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},{"Id":7,"Active":true,"Title":"QA Test","Description":"","CardLimit":3,"ClassType":0,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},{"Id":8,"Active":true,"Title":"Deploy Queue","Description":"","CardLimit":6,"ClassType":0,"Width":2,"Version":20,"Cards":[],"UserSubscription":0},{"Id":9,"Active":true,"Title":"Done","Description":"","CardLimit":15,"ClassType":0,"Width":3,"Version":20,"Cards":[],"UserSubscription":0}],"BoardUsers":[{"Id":1,"FullName":"root root","EmailAddress":"chris.hefley@imihealth.com"}],"Backlog":{"Id":1,"Active":false,"Title":"Backlog","Description":"","CardLimit":0,"ClassType":1,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},"Archive":{"Id":2,"Active":false,"Title":"Archive","Description":"","CardLimit":0,"ClassType":2,"Width":1,"Version":20,"Cards":[],"UserSubscription":0},"UserSubscription":0}]]}

        //    var response = Requester(string.Format("/Kanban/Api/Boards"), "GET");

        //    return response;
        //}

        ///// <summary>
        ///// Adds a New Ticket for given data
        ///// </summary>
        ///// <param name="boardId"></param>
        ///// <param name="laneId"></param>
        ///// <param name="card"></param>
        ///// <returns></returns>
        //public string AddCard(string boardId, string laneId, API.DTO.CardItem card)
        //{
        //    //Example of output (output would be quite long, since it returns all board infrastructure for re-rendering)
        //    //{"ReplyCode":201,"ReplyText":"The Card was successfully added.","ReplyData":[{"Id":1,"Title":"Realistic Board","Description":"","Version":11,"Active":false,"OrganizationId":1,"Lanes":[{"Id":3,"Active":true,"Title":"Work Queue","Description":"","CardLimit":15,"ClassType":0,"Width":3,"Version":23,"Cards":[{"Id":1,"LaneId":0,"Title":"aaa","Description":"","ClassType":0,"TypeName":"Feature","Size":0,"Active":false,"Color":"Green","Version":9,"AssignedUserId":1,"IsBlocked":false}],"UserSubscription":0},{"Id":4,"Active":true,"Title":"Design","Description":"","CardLimit":3,"ClassType":0,"Width":1,"Version":23,"Cards":[{"Id":4,"LaneId":0,"Title":"Test Card","Description":"","ClassType":0,"TypeName":"Feature","Size":12,"Active":false,"Color":"Green","Version":1,"AssignedUserId":0,"IsBlocked":false},{"Id":2,"LaneId":0,"Title":"bbb","Description":"","ClassType":2,"TypeName":"Defect","Size":0,"Active":false,"Color":"Red","Version":11,"AssignedUserId":1,"IsBlocked":false}],"UserSubscription":0},{"Id":5,"Active":true,"Title":"Doing","Description":"","CardLimit":2,"ClassType":0,"Width":1,"Version":23,"Cards":[{"Id":3,"LaneId":0,"Title":"cccc","Description":"<p>asdasd</p>","ClassType":0,"TypeName":"Feature","Size":12,"Active":false,"Color":"Green","Version":1,"AssignedUserId":0,"IsBlocked":false}],"UserSubscription":0},{"Id":6,"Active":true,"Title":"Code Review","Description":"","CardLimit":4,"ClassType":0,"Width":1,"Version":23,"Cards":[],"UserSubscription":0},{"Id":7,"Active":true,"Title":"QA Test","Description":"","CardLimit":3,"ClassType":0,"Width":1,"Version":23,"Cards":[],"UserSubscription":0},{"Id":8,"Active":true,"Title":"Deploy Queue","Description":"","CardLimit":6,"ClassType":0,"Width":2,"Version":23,"Cards":[],"UserSubscription":0},{"Id":9,"Active":true,"Title":"Done","Description":"","CardLimit":15,"ClassType":0,"Width":3,"Version":23,"Cards":[],"UserSubscription":0}],"BoardUsers":[{"Id":1,"FullName":"root root","EmailAddress":"chris.hefley@imihealth.com"}],"Backlog":{"Id":1,"Active":false,"Title":"Backlog","Description":"","CardLimit":0,"ClassType":1,"Width":1,"Version":23,"Cards":[],"UserSubscription":0},"Archive":{"Id":2,"Active":false,"Title":"Archive","Description":"","CardLimit":0,"ClassType":2,"Width":1,"Version":23,"Cards":[],"UserSubscription":0},"UserSubscription":0}]}

        //    var response = Get<string>(string.Format("/Kanban/Api/Board/{0}/AddCard/Lane/{1}/Position/0", boardId, laneId), "POST", JsonConvert.SerializeObject(card));

        //    return response;
        //}

        ///// <summary>
        ///// Moves a given card to a new lane
        ///// </summary>
        ///// <param name="boardId"></param>
        ///// <param name="cardId"></param>
        ///// <param name="laneId"></param>
        ///// <returns></returns>
        //public string movecard(string boardid, string cardid, string laneid)
        //{
        //    //example of output
        //    //{"replycode":202,"replytext":"the card was moved successfully.","replydata":[11]} 
        //    //replydata is a new board version

        //    //var response = requester(string.format("/kanban/api/board/{0}/movecard/{1}/lane/{2}/position/0", boardid, cardid, laneid),"post");
        //    var response = Get<string>(string.format("/kanban/api/board/{0}/movecard/{1}/lane/{2}/position/0", boardid, cardid, laneid), "post");

        //    return response;
        //}

        ///// <summary>
        ///// Updates a specified card
        ///// </summary>
        ///// <param name="boardId"></param>
        ///// <param name="laneId"></param>
        ///// <param name="card"></param>
        ///// <returns></returns>
        //public string UpdateCard(string boardId, string laneId, CardItem card)
        //{
        //    //Example of output (output would be quite long, since it returns all board infrastructure for re-rendering)
        //    //{"ReplyCode":202,"ReplyText":"The Card was successfully updated.","ReplyData":[{"Id":1,"Title":"RealisticBoard","Description":"","Version":16,"Active":false,"OrganizationId":1,"Lanes":[{"Id":3,"Active":true,"Title":"Work Queue","Description":"","CardLimit":15,"ClassType":0,"Width":3,"Version":27,"Cards":[{"Id":1,"LaneId":0,"Title":"aaa","Description":"","ClassType":0,"TypeName":"Feature","Size":0,"Active":false,"Color":"Green","Version":15,"AssignedUserId":1,"IsBlocked":false}],"UserSubscription":0},{"Id":4,"Active":true,"Title":"Design","Description":"","CardLimit":3,"ClassType":0,"Width":1,"Version":27,"Cards":[{"Id":4,"LaneId":0,"Title":"Test Card","Description":"","ClassType":0,"TypeName":"Feature","Size":12,"Active":false,"Color":"Green","Version":5,"AssignedUserId":0,"IsBlocked":false},{"Id":2,"LaneId":0,"Title":"bbb","Description":"","ClassType":2,"TypeName":"Defect","Size":0,"Active":false,"Color":"Red","Version":16,"AssignedUserId":1,"IsBlocked":false}],"UserSubscription":0},{"Id":5,"Active":true,"Title":"Doing","Description":"","CardLimit":2,"ClassType":0,"Width":1,"Version":27,"Cards":[{"Id":5,"LaneId":0,"Title":"Test Card With WIP Override","Description":"","ClassType":0,"TypeName":"Feature","Size":12,"Active":false,"Color":"Green","Version":4,"AssignedUserId":0,"IsBlocked":false},{"Id":3,"LaneId":0,"Title":"cccc","Description":"<p>asdasd</p>","ClassType":0,"TypeName":"Feature","Size":12,"Active":false,"Color":"Green","Version":6,"AssignedUserId":0,"IsBlocked":false}],"UserSubscription":0},{"Id":6,"Active":true,"Title":"Code Review","Description":"","CardLimit":4,"ClassType":0,"Width":1,"Version":27,"Cards":[],"UserSubscription":0},{"Id":7,"Active":true,"Title":"QA Test","Description":"","CardLimit":3,"ClassType":0,"Width":1,"Version":27,"Cards":[],"UserSubscription":0},{"Id":8,"Active":true,"Title":"Deploy Queue","Description":"","CardLimit":6,"ClassType":0,"Width":2,"Version":27,"Cards":[],"UserSubscription":0},{"Id":9,"Active":true,"Title":"Done","Description":"","CardLimit":15,"ClassType":0,"Width":3,"Version":27,"Cards":[],"UserSubscription":0}],"BoardUsers":[{"Id":1,"FullName":"root root","EmailAddress":"chris.hefley@imihealth.com"}],"Backlog":{"Id":1,"Active":false,"Title":"Backlog","Description":"","CardLimit":0,"ClassType":1,"Width":1,"Version":27,"Cards":[],"UserSubscription":0},"Archive":{"Id":2,"Active":false,"Title":"Archive","Description":"","CardLimit":0,"ClassType":2,"Width":1,"Version":27,"Cards":[],"UserSubscription":0},"UserSubscription":0}]}

        //    var response = Requester(string.Format("/Kanban/Api/Board/{0}/AddCard/Lane/{1}/Position/0", boardId, laneId), "POST", JsonConvert.SerializeObject(card));

        //    return response;
        //}

        #endregion

        #endregion

        #region Static

        static HttpClient CreateHttpRequest(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            HttpClient httpClient = new HttpClient();
            httpClient.Request.ContentType = HttpContentTypes.ApplicationJson;
            httpClient.Request.Accept = HttpContentTypes.ApplicationJson;
            httpClient.Request.SetBasicAuthentication(userName, password);

            return httpClient;
        }

        #endregion
    }
}
