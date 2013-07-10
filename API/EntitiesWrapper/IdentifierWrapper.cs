using Leankit.Entities;
using System.Collections.Generic;

namespace Leankit.EntitiesWrapper
{
    public class IdentifierWrapper
    {
        public string ReplyCode { get; set; }
        public string ReplyText { get; set; }
        public List<IdentifierReplyData> ReplyData { get; set; }
    }
}
