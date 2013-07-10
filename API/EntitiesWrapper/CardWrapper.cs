using Leankit.Entities;
using System.Collections.Generic;

namespace Leankit.EntitiesWrapper
{
    public class CardWrapper
    {
        public string ReplyCode { get; set; }
        public string ReplyText { get; set; }
        public List<Card> ReplyData { get; set; }
    }
}
