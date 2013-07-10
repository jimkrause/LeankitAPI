using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leankit;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Diagnostics;

namespace Leankit
{
    class KanbanProxy
    {
        #region Public

        public static string DoWebRequest(string address, string method)
        {
            return DoWebRequest(address, method, string.Empty);
        }

        public static string DoWebRequest(string address, string method, string body)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://laserfiche.leankit.com/" + address);
            request.Method = method;
            request.Credentials = new NetworkCredential(@"IOR@laserfiche.com", @"L@53rf1ch3");
            request.PreAuthenticate = true;

            if (method == "POST")
            {
                if (!string.IsNullOrEmpty(body))
                {
                    Byte[] requestBody = Encoding.UTF8.GetBytes(body);
                    request.ContentLength = requestBody.Length;
                    request.ContentType = "application/json";
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(requestBody, 0, requestBody.Length);
                    }
                }
                else
                {
                    request.ContentLength = 0;
                }
            }

            request.Timeout = 15000;
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);

            string output = string.Empty;
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(1252)))
                    {
                        output = stream.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    using (StreamReader stream = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        output = stream.ReadToEnd();
                    }
                }
                else if (ex.Status == WebExceptionStatus.Timeout)
                {
                    output = "Request timeout is expired.";
                }
                else
                {
                    Helper.Logging.WriteLog("Exception Error", ex.Message, TraceEventType.Error, false);
                }
            }
            return output;
        }
        
        // Merge Httprequest GET item under APICaller with this POST item ?

        #endregion
    }
}
