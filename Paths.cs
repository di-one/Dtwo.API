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
        public static readonly string ConfigDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration");
        public static readonly string ConfigPathsPath = Path.Combine(ConfigDirectoryPath, "paths.json");
        public static readonly string DtwoBasePath = AppDomain.CurrentDomain.BaseDirectory;
        
        public static PathsConfiguration Config { get; private set; } = new PathsConfiguration();

        /// <summary>
        /// Path to the Dtwo data folder
        /// </summary>
        public static string? DtwoDataPath { get; private set; }

        /// <summary>
        /// Path to the retro binding file
        /// </summary>
        public static string? RetroBindingPath { get; private set; }

        /// <summary>
        /// Path to the dofus2 binding file
        /// </summary>
        public static string? Dofus2BindingPath { get; private set; }

        /// <summary>
        /// Path to the hybride binding file
        /// </summary>
        public static string? HybrideBindingPath { get; private set; }

        /// <summary>
        /// Path to the dofus2 binding types file
        /// </summary>
		public static string? Dofus2BindingTypesPath { get; private set; }
        public static string? Dofus2BindingInfosPath { get; private set; }
        public static string? HybrideBindingInfosPath { get; private set; }

        public static string? NoServerIpsPath { get; private set;}

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
            if (Directory.Exists(ConfigDirectoryPath) == false)
            {
                Directory.CreateDirectory(ConfigDirectoryPath);
            }

            if (File.Exists(ConfigPathsPath) == false)
            {
                Config = new PathsConfiguration();
                SaveConfig();

                return false;
            }
            else
            {
                Config = Newtonsoft.Json.JsonConvert.DeserializeObject<PathsConfiguration>(File.ReadAllText(ConfigPathsPath));

                return true;
            } 
        }

        public static void SaveConfig()
        {
            File.WriteAllText(ConfigPathsPath, Newtonsoft.Json.JsonConvert.SerializeObject(Config));
            InitPaths();
        }

        private static void InitPaths()
        {
            if (Config?.DtwoDataBasePath == null)
            {
                LogManager.LogError("Error on init paths : DtwoDataBasePath is null", 1);
                return;
            }

            DtwoDataPath = Path.Combine(DtwoBasePath, Config.DtwoDataBasePath);
            RetroBindingPath = Path.Combine(DtwoDataPath, "retro_binding.json");
            Dofus2BindingPath = Path.Combine(DtwoDataPath, "dofus2_binding.json");
            HybrideBindingPath = Path.Combine(DtwoDataPath, "hybride_binding.json");
			Dofus2BindingTypesPath = Path.Combine(DtwoDataPath, "dofus2_binding_types.json");
            Dofus2BindingInfosPath = Path.Combine(DtwoDataPath, "dofus2_binding_infos.json");
            HybrideBindingInfosPath = Path.Combine(DtwoDataPath, "hybride_binding_infos.json");
            NoServerIpsPath = Path.Combine(DtwoDataPath, "no_server_ips.json");
        }

        public static void UpdateConfig(PathsConfiguration config)
        {
            Config = config;
            SaveConfig();
        }
    }
}
