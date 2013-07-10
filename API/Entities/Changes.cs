using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leankit.Entities
{
    public class Changes
    {
        public string FieldName { get; set; }
        public string NewDueDate { get; set; }
        public string NewValue { get; set; }
        public string OldDueDate { get; set; }
        public string OldValue { get; set; }
    }
}
