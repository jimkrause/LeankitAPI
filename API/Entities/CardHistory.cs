using System.Collections.Generic;

namespace Leankit.Entities
{
    public class CardHistory
    {
        public string CardId { get; set; }
        public string CardTitle { get; set; }
        public string FromLaneId { get; set; }
        public string FromLaneTitle { get; set; }
        public string ToLaneId { get; set; }
        public string ToLaneTitle { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string LastDate { get; set; }
        public string DateTime { get; set; }
        public string TimeDifference { get; set; }
        public string CommentText { get; set; }
        public string Comment { get; set; }
        public string IsBlocked { get; set; }
        public List<Changes> Changes { get; set; }
        public string AssignedUserId { get; set; }
        public string AssignedUserFullName { get; set; }
        public string AssignedUserEmailAddres { get; set; }
        public string IsUnassigning { get; set; }
        public string IsDelete { get; set; }
        public string FileName { get; set; }
        public string GravatarLink { get; set; }
        public string WipOverrideComment { get; set; }
        //public string UserToOverrideWipId { get; set; }
        //public string UserToOverrideWipName { get; set; }
        //public string UserToOverrideWipEmail { get; set; }
        //public string LaneToOverrideWipId { get; set; }
        //public string LaneToOverrideWipTitle { get; set; }
    }
}