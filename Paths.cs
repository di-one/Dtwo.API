using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtwo.API.Configuration;

namespace Dtwo.API
{
    public static class Paths
    {
        public static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "paths.json");
        public static readonly string DtwoBasePath = AppDomain.CurrentDomain.BaseDirectory;
        
        public static PathsConfiguration Config { get; private set; }
        public static string DtwoDataPath { get; private set; }
        public static string RetroBindingPath { get; private set; }
        public static string Dofus2BindingPath { get; private set; }
        public static string HybrideBindingPath { get; private set; }

        public const string BASE_PATH = @".\";
        public const string TEMP_PATH = BASE_PATH + "temp";
        public const string PLUGINS_PATH = BASE_PATH + "plugins";

        public static readonly string PLUGINS_ABOSLUTE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");

        public static void Init()
        {
            if (LoadConfig())
            {
                InitPaths();
            }
        }

        private static bool LoadConfig()
        {
            if (File.Exists(ConfigPath) == false)
            {
                Config = new PathsConfiguration();
                SaveConfig();

                return false;
            }
            else
            {
                Config = Json.JSonSerializer<PathsConfiguration>.DeSerialize(File.ReadAllText(ConfigPath));

                return true;
            } 
        }

        public static void SaveConfig()
        {
            File.WriteAllText(ConfigPath, Json.JSonSerializer<PathsConfiguration>.Serialize(Config));
            InitPaths();
        }

        private static void InitPaths()
        {
            DtwoDataPath = Path.Combine(DtwoBasePath, Config.DtwoDataBasePath);
            RetroBindingPath = Path.Combine(DtwoDataPath, "retro_binding.json");
            Dofus2BindingPath = Path.Combine(DtwoDataPath, "dofus2_binding.json");
            HybrideBindingPath = Path.Combine(DtwoDataPath, "hybride_binding.json");
        }
    }
}
