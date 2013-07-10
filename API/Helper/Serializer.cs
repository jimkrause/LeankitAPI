using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace Leankit.Helper
{
    class Serializer
    {
        #region JSON Serializer

        public static string ConvertFromJSON(Dictionary<string, string> _dict)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string _JSONstring = serializer.Serialize(_dict);
            return _JSONstring;
        }

        //public static EntitiesWrapper.AddCardWrapper ConvertToJSON(string response)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    EntitiesWrapper.AddCardWrapper JSONresponse = serializer.Deserialize<EntitiesWrapper.AddCardWrapper>(response);

        //    return JSONresponse;
        //    //return "";
        //}

        public static T ConvertToJSON<T>(string response)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            T JSONresponse = serializer.Deserialize<T>(response);

            return JSONresponse;
            //return "";
        }

        #endregion

        #region XML Serializer

        public static void SerializeDictionaryToFile(Dictionary<string, string> keyToValues, string path)
        {
            string xmlDictionary =  Serializer.Serialize(keyToValues);
            File.WriteAllText(path, xmlDictionary);
        }

        public static Dictionary<string, string> DeserializeDictionaryFromFile(string path)
        {
            string xmlDictionary = File.ReadAllText(path);
            Dictionary<string, string> keyToValues = Serializer.Deserialize<Dictionary<string, string>>(xmlDictionary);

            return keyToValues;
        }

        public static string Serialize<T>(T obj)
        {
            string s = string.Empty;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(memoryStream, obj);
                s = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            return "<?xml version=\"1.0\"?>" + s;
        }

        public static T Deserialize<T>(string xml)
        {
            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                XmlDictionaryReader reader =
                    XmlDictionaryReader.CreateTextReader(memoryStream, Encoding.UTF8, new XmlDictionaryReaderQuotas(), null);
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                return (T)serializer.ReadObject(reader);
            }
        }

        #endregion
    }
}
