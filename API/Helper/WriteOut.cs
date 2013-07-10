using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Leankit.Helper
{
    class WriteOut
    {
        #region Output

        public static void WriteOutputRequest(string _parameter, string _value, string _tempPath)
        {
            // To XML
            Dictionary<string, string> outputDict = new Dictionary<string, string>();
            string OutputXmlFile = _tempPath + "_" + _parameter + ".xml";
            outputDict[_parameter] = _value.ToString();
            Helper.Serializer.SerializeDictionaryToFile(outputDict, OutputXmlFile);
        }

        public static void WriteOutputList(List<string> _listcards, string _tempPath)
        {
            // To Output Text File
            string OutputTextFile = _tempPath + "_listCards.txt";
            using (StreamWriter outfile = new StreamWriter(OutputTextFile))
            {
                foreach (string cardid in _listcards)
                {
                    outfile.WriteLine(cardid);

                    // To Standard Output
                    Console.Out.WriteLine(cardid);
                }
            }

            // To Standard Output
            Console.Out.Close();
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);
        }

        public static void WriteOutputDict(Dictionary<string, string> _dict, string _tempPath, string _name)
        {
            // To XML
            string OutputXmlFile = _tempPath + _name + ".xml";
            Helper.Serializer.SerializeDictionaryToFile(_dict, OutputXmlFile);
        }

        #endregion
    }
}
