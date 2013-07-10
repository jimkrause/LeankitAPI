using System.Collections.Generic;

namespace Leankit.Entities
{
    public class CardDTO
    {
        public bool Active { get; set; }
        public string AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }
        public List<BoardUser> AssignedUsers { get; set; }
        public string AttachmentsCount { get; set; }
        public string BlockReason { get; set; }
        public string BlockStateChangeDate { get; set; }
        public string ClassOfServiceId { get; set; }
        public string ClassOfServiceTitle { get; set; }
        public string ClassOfServiceIconPath { get; set; }
        public string ClassOfServiceColorHex { get; set; }
        public string Color{ get; set; }
        public string CommentsCount { get; set; }
        public string CountOfOldCards { get; set; }
        public string CurrentContext { get; set; }
        public string CurrentTaskBoardId { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public string ExternalSystemName { get; set; }
        public string ExternalSystemUrl { get; set; }
        public string ExternalCardID { get; set; }
        public string GravatarLink { get; set; }
        public bool HasDrillThroughBoard { get; set; }
        public string Id { get; set; }
        public string Index { get; set; }
        public bool IsBlocked { get; set; }
        public string LaneId { get; set; }
        public string LastAttachment { get; set; }
        public string LastComment { get; set; }
        public string LastMove { get; set; }
        public string ParentCardId { get; set; }
        public string Priority { get; set; }
        public string PriorityText { get; set; }
        public string Size { get; set; }
        public string SmallGravatarLink { get; set; }
        public string SystemType { get; set; }
        public string Tags { get; set; }
        public string TaskBoardCompletionPercent { get; set; }
        public string TaskBoardTotalCards { get; set; }
        public string Title { get; set; }
        public Type Type { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeIconPath { get; set; }
        public string TypeColorHex { get; set; }
        public string Version { get; set; }
    }
}