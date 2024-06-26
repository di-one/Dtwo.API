﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API
{
    public static class LogManager
    {
        public static Action<LogMessage>? OnLog;

        //public static List<LogMessage> _notReadedMessages = new();

        public static void Log(string title, string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogDefault(title, text, priority);
            LogInternal(log);
        }

        public static void Log(string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogDefault(text, priority);
            LogInternal(log);
        }

        public static void LogWarning(string title, string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogWarning(title, text, priority);
            LogInternal(log);
        }

        public static void LogWarning(string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogWarning(text, priority);
            LogInternal(log);
        }

        public static void LogError(string title, string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogError(title, text, priority);
            LogInternal(log);
        }

        public static void LogError(string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogError(text, priority);
            LogInternal(log);
        }

        public static void Log(LogMessage message)
		{
            LogInternal(message);
		}

        private static void LogInternal(LogMessage message)
        {
            //_notReadedMessages.Add(message);

            OnLog?.Invoke(message);
        }
    }
}
