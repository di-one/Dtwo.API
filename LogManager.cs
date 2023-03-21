using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API
{
    public static class LogManager
    {
        public static Action<LogMessage>? OnLog;

		/// <summary>
		/// Log a message with a title and a text
		/// </summary>
		public static void Log(string title, string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogDefault(title, text, priority);
            LogInternal(log);
        }

		/// <summary>
		/// Log a message with a text
		/// </summary>
		public static void Log(string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogDefault(text, priority);
            LogInternal(log);
        }

		/// <summary>
		/// Log a warning message with a title and a text
		/// </summary>
		public static void LogWarning(string title, string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogWarning(title, text, priority);
            LogInternal(log);
        }

		/// <summary>
		/// Log a warning message with a text
		/// </summary>
		public static void LogWarning(string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogWarning(text, priority);
            LogInternal(log);
        }

		/// <summary>
		/// Log a error message with a title and a text
		/// </summary>
		public static void LogError(string title, string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogError(title, text, priority);
            LogInternal(log);
        }

		/// <summary>
		/// Log a error message with a text
		/// </summary>
		public static void LogError(string text, int priority = 0)
        {
            LogMessage log = LogMessage.LogError(text, priority);
            LogInternal(log);
        }

        /// <summary>
        /// Log a message with a LogMessage
        /// </summary>
        /// <param name="message"></param>
        public static void Log(LogMessage message)
		{
            LogInternal(message);
		}

        private static void LogInternal(LogMessage message)
        {
            OnLog?.Invoke(message);
        }
    }
}
