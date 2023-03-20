using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API
{
    public class LogMessage
    {
        public readonly DateTime Date;
        public readonly string Title;
        public readonly string Text;
        public readonly MessageType Type;
        public readonly int Priority; // Used actually in app for toast (1)

        public LogMessage(string title, string text, MessageType type, int priority = 0)
        {
            Date = DateTime.Now;
            Title = title;
            Text = text;
            Type = type;

            Priority = priority;
        }

        public LogMessage(string text, int priority = 0)
		{
            Date = DateTime.Now;
            Text = text;

            Priority = priority;
		}

        public LogMessage(string text, MessageType type, int priority = 0)
        {
            Date = DateTime.Now;
            Text = text;
            Type = type;

            Priority = priority;
        }

        public LogMessage(string title, string text, int priority = 0)
		{
            Date = DateTime.Now;
            Title = title;
            Text = text;

            Priority = priority;
        }

        public static LogMessage LogDefault(string title, string text, int priority = 0)
		{
            return new LogMessage(title, text, MessageType.Default, priority);
		}

        public static LogMessage LogDefault(string text, int priority = 0)
        {
            return new LogMessage(text, MessageType.Default, priority);
        }

        public static LogMessage LogWarning(string title, string text, int priority = 0)
		{
            return new LogMessage(title, text, MessageType.Warning, priority);
        }

        public static LogMessage LogWarning(string text, int priority = 0)
        {
            return new LogMessage(text, MessageType.Warning, priority);
        }

        public static LogMessage LogError(string title, string text, int priority = 0)
		{
            return new LogMessage(title, text, MessageType.Error, priority);
        }

        public static LogMessage LogError(string text, int priority = 0)
        {
            return new LogMessage(text, MessageType.Error, priority);
        }

        public override string ToString()
        {
            string str = $"[{Date}] ";
            if (string.IsNullOrEmpty(Title) == false)
            {
                str += $"[{Title}] : ";
            }

            str += Text;

            return str;
        }
    }
}
