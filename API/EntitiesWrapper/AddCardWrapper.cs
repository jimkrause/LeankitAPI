using Leankit.Entities;
using System.Collections.Generic;

namespace Leankit.EntitiesWrapper
{
    public class AddCardWrapper
    {
        public string ReplyCode { get; set; }
        public string ReplyText { get; set; }
        public List<AddCardReplyData> ReplyData { get; set; }
    }
}
