using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dtwo.API
{
    public static class PluginsHelper
    {
        public static List<Assembly> PluginsAssemblies { get; set; } = new List<Assembly>();

        public static List<Plugin> Plugins { get; set; } = new List<Plugin>();

        public static Assembly? ApiAssembly { get; set; }

        public static T? GetPlugin<T>() where T : Plugin
        {
            for (int i = 0; i < Plugins.Count; i++)
            {
                var plugin = Plugins[i];   

                T? parsed = plugin as T;

                if (parsed != null)
                {
                    return parsed;
                }
            }

            return null;
        }
    }
}
