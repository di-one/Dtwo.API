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
		/// <summary>
		/// Absolute path of Application directory
		/// </summary>
		public static readonly string DtwoBasePath = AppDomain.CurrentDomain.BaseDirectory;
        
        /// <summary>
        /// Aboslute path of plugins folder
        /// </summary>
		public static readonly string PLUGINS__PATH = Path.Combine(DtwoBasePath, "plugins");
		private static readonly string ConfigFilePath = Path.Combine(DtwoBasePath, "paths.json");
        
        /// <summary>
        /// Absolute path of Application data directory
        /// </summary>
        /// 
        public static string DtwoDataPath { get; private set; }
		/// <summary>
		/// Absolute path of retro_binding.json
		/// </summary>
        /// 
		public static string RetroBindingPath { get; private set; }
		/// <summary>
		/// Absolute path of dofus2_binding.json
		/// </summary>
        /// 
		public static string Dofus2BindingPath { get; private set; }
		/// <summary>
		/// Absolute path of hybride_binding.json
		/// </summary>
		public static string HybrideBindingPath { get; private set; }

		/// <summary>
		/// Paths configuration
		/// </summary>
		public static PathsConfiguration Config { get; private set; }

        /// <summary>
        /// Load configuration and initialize paths
        /// </summary>
        public static void Init()
        {
            if (LoadConfig())
            {
                InitPaths();
            }
        }

		/// <summary>
		/// Save the paths configuration
		/// </summary>
		public static void SaveConfig()
		{
			File.WriteAllText(ConfigFilePath, Json.JSonSerializer<PathsConfiguration>.Serialize(Config));
			InitPaths();
		}


		private static bool LoadConfig()
        {
            if (File.Exists(ConfigFilePath) == false)
            {
                Config = new PathsConfiguration();
                SaveConfig();

                return false;
            }
            else
            {
                Config = Json.JSonSerializer<PathsConfiguration>.DeSerialize(File.ReadAllText(ConfigFilePath));

                return true;
            } 
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
