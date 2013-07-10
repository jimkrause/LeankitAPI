using Leankit.Entities;
using System.Collections.Generic;

namespace Leankit.EntitiesWrapper
{
    public class CardHistoryWrapper
    {
        public string ReplyCode { get; set; }
        public string ReplyText { get; set; }
        public List<List<CardHistory>> ReplyData { get; set; }
    }
}
