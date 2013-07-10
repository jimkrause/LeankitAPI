using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Leankit.Helper
{
    class Logging
    {
        // Microsoft Enterprise Library logging
        public static void WriteLog(string logMessageParameter, string logMessageValue, TraceEventType logType, bool methodSuccess)
        {
            LogEntry logEntry = new LogEntry();
             
            logEntry.Message = "Operation Success" + ": "
                                + methodSuccess.ToString() + "\n" + "\n"
                                + logMessageParameter + ": " 
                                + logMessageValue;
            logEntry.Severity = logType;
            logEntry.Title = GetMethodName();

            Logger.Write(logEntry);
        }

        public static void WriteLog(List<string> logMessageList, TraceEventType logType, bool methodSuccess)
        {
            LogEntry logEntry = new LogEntry();

            string logMessage = "Operation Success" + ": "
                                + methodSuccess.ToString() + "\n" + "\n";
            foreach (string message in logMessageList)
            {
                logMessage += message + "\n";
            }

            logEntry.Message = logMessage ;
            logEntry.Severity = logType;
            logEntry.Title = GetMethodName();

            Logger.Write(logEntry);
        }

        public static void WriteLog(Dictionary<string, string> logDictionary, TraceEventType logType, bool methodSuccess)
        {
            LogEntry logEntry = new LogEntry();

            string logMessage = "Operation Success" + ": "
                                + methodSuccess.ToString() + "\n" + "\n";
            foreach (KeyValuePair<string, string> pair in logDictionary)
            {
                logMessage += pair.Key + ": " 
                            + pair.Value + "\n";
            }

            logEntry.Message = logMessage;
            logEntry.Severity = logType;
            logEntry.Title = GetMethodName();

            Logger.Write(logEntry);
        }

        public static string GetMethodName()
        {
            StackTrace stacktrace = new StackTrace();
            return stacktrace.GetFrame(2).GetMethod().Name; // normally, frame layer would be 0. need to add 2 for the 2 frames between the original call and here.
        }
    }
}
