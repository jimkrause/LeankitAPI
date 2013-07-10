using System.Collections.Generic;

namespace Leankit.EntitiesCustom
{
    public class InputData
    {
        public Dictionary<string, string> InputParam { get; set; }
        public Dictionary<string, string> JsonParam { get; set; }
        public string TempPath { get; set; }
    }
}
