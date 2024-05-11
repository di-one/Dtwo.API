using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API
{
    /* ExportedMethodAttribute was initially designed for Dtwo.Plugins.GPT,
     * which allows registering functions in a dictionary and calling them from anywhere. */

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class ExportedMethod : System.Attribute
    {
        private class ExportedMethodValue
        {
            public Type? MethodClassType;
            public string? MethodName;
            public List<Type>? ParametersTypes;
        }

        private static readonly Dictionary<Plugin, Dictionary<string, ExportedMethodValue>> m_exportedMethods = new Dictionary<Plugin, Dictionary<string, ExportedMethodValue>>();

        public static void RegisterExportedMethod(Plugin plugin, string methodName, Type classType, List<Type> parametersTypes)
        {
            if (m_exportedMethods.ContainsKey(plugin) == false)
            {
                m_exportedMethods.Add(plugin, new Dictionary<string, ExportedMethodValue>());
            }

            Dictionary<string, ExportedMethodValue>? events;
            if (m_exportedMethods.TryGetValue(plugin, out events))
            {
                if (events.ContainsKey(methodName) == false)
                {
                    events.Add(methodName, new ExportedMethodValue()
                    {
                        MethodClassType = classType,
                        MethodName = methodName,
                        ParametersTypes = parametersTypes
                    });
                }
            }
        }

        public static void UnRegisterExportedEvent(Plugin plugin, string methodName)
        {
            Dictionary<string, ExportedMethodValue>? events;
            if (m_exportedMethods.TryGetValue(plugin, out events))
            {
                if (events.ContainsKey(methodName) == false)
                {
                    events.Remove(methodName);
                }
            }
        }

        public static void CallExportedMethod(DofusWindow window, string methodName, string[] parameters, Action action)
        {
            for (int i = 0; i < m_exportedMethods.Count(); i++)
            {
                var evnt = m_exportedMethods.ElementAt(i);

                if (evnt.Value == null)
                    continue;

                for (int j = 0; j < evnt.Value.Count; j++)
                {
                    var methodAndParams = evnt.Value.ElementAt(j);

                    if (methodAndParams.Value == null)
                    {
                        LogManager.LogWarning($"Error on call exported method : methodAndParams.Value is null");
                        continue;
                    }

                    if (methodAndParams.Value.MethodName == null)
                    {
                        LogManager.LogWarning($"Error on call exported method : methodAndParams.Value.MethodName is null");
                        continue;
                    }

                    if (methodAndParams.Key.ToLower() == methodName.ToLower())
                    {
                        Type? pluginType = methodAndParams.Value.MethodClassType;

                        if (pluginType == null)
                        {
                            LogManager.LogWarning($"Error on call exported method : pluginType is null");
                            continue;
                        }

                        MethodInfo? method = pluginType.GetMethod(methodAndParams.Value.MethodName);

                        if (method != null)
                        {
                            dynamic[] firstarameters = new dynamic[] { window, action };
                            dynamic[] allParameters = firstarameters.Concat(StringsToDynamics(parameters)).ToArray();
                            var result = method.Invoke(evnt.Key, allParameters); // Cast automatique ??

                            return; // One method by name // todo : first key is name ?
                        }
                    }
                }
            }
        }

        private static dynamic[] StringsToDynamics(string[] parameters)
        {
            dynamic[] dParameters = new dynamic[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var val = StringToDynamic(parameters[i]);

                if (val == null)
                    continue;

                dParameters[i] = val;
            }

            return dParameters;
        }

        private static dynamic? StringToDynamic(string str)
        {
            string value = str;

            if (str.Contains("["))
            {
                string[]? arrayValues = GetStringArray(str);

                if (arrayValues == null)
                {
                    return null;
                }

                string firstValue = arrayValues[0];

                int rInt = 0;
                if (int.TryParse(firstValue, out rInt))
                {
                    List<int> ret = new List<int>();
                    foreach (var p in arrayValues)
                    {
                        ret.Add(int.Parse(p));
                    }

                    return ret;
                }

                bool rBool = false;
                if (bool.TryParse(firstValue, out rBool))
                {
                    List<bool> ret = new List<bool>();
                    foreach (var p in arrayValues)
                    {
                        ret.Add(bool.Parse(p));
                    }

                    return ret;
                }

                return arrayValues.ToList();
            }
            else
            {
                int rInt = 0;
                if (int.TryParse(value, out rInt))
                {
                    return rInt;
                }

                bool rBool = false;
                if (bool.TryParse(value, out rBool))
                {
                    return rBool;
                }

                return value;
            }
        }

        private static string[]? GetStringArray(string str)
        {
            str = str.Replace("[", "").Replace("]", "");
            var split = str.Split(';');

            if (split == null || split.Length == 0)
            {
                return null;
            }

            return split;
        }

        public static void CallExportedMethod(DofusWindow window, string methodName, dynamic[] parameters)
        {
            for (int i = 0; i < m_exportedMethods.Count(); i++)
            {
                var evnt = m_exportedMethods.ElementAt(i);

                if (evnt.Value == null)
                    continue;

                for (int j = 0; j < evnt.Value.Count; j++)
                {
                    var methodAndParams = evnt.Value.ElementAt(j);

                    if (methodAndParams.Value == null)
                    {
                        LogManager.LogWarning($"Error on call exported method : methodAndParams.Value is null");
                        continue;
                    }

                    if (methodAndParams.Value.MethodName == null)
                    {
                        LogManager.LogWarning($"Error on call exported method : methodAndParams.Value.MethodName is null");
                        continue;
                    }

                    if (methodAndParams.Key.ToLower() == methodName.ToLower())
                    {
                        Type? pluginType = methodAndParams.Value.MethodClassType;

                        if (pluginType == null)
                        {
                            LogManager.LogWarning($"Error on call exported method : pluginType is null");
                            continue;
                        }

                        MethodInfo? method = pluginType.GetMethod(methodAndParams.Value.MethodName);

                        if (method != null)
                        {
                            dynamic[] firstarameters = new dynamic[] { window };
                            dynamic[] allParameters = firstarameters.Concat(parameters).ToArray();
                            method.Invoke(evnt.Key, allParameters); // Cast automatique ??
                        }
                    }
                }
            }
        }
    }
}
