using Leankit.Entities;
using System;
using System.Collections.Generic;

namespace Leankit.EntitiesWrapper
{
    public class AttributesWrapper
    {
        public string ReplyCode { get; set; }
        public string ReplyText { get; set; }
        public List<AttributesReplyData> ReplyData { get; set; }
    }
}