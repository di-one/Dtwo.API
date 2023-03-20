using System.Reflection;
using Dtwo.Plugins;
using Dtwo.API.DofusBase.Network.Messages;

namespace Dtwo.API
{
    public class Plugin : PluginController
    {
        public Plugin(PluginInfos infos, Assembly assembly) : base(infos, assembly)
        {
        }

        #region Public
        /// <summary>
        /// Register event to the event playlist
        /// </summary>
        /// <param name="name">Event name</param>
        public void RegisterEvent<T>(string methodName) where T : DofusMessage
        {
            OnRegisterEvent?.Invoke(this, methodName, typeof(T));
        }

        /// <summary>
        /// Unregister event to the event playlist
        /// </summary>
        /// <param name="name">Event name</param>
        public void UnRegisterEvent<T>(string methodName) where T : DofusMessage
        {
            string name = typeof(T).Name;
            OnUnRegisterEvent?.Invoke(this, methodName, typeof(T));
        }

        public void SaveFile(string fileName, string content)
        {
            var basePath = GetDataPath();
            if (Directory.Exists(basePath) == false)
            {
                Directory.CreateDirectory(basePath);
            }

            File.WriteAllText(Path.Combine(basePath, fileName), content);
        }

        public string LoadFile(string fileName)
        {
            var basePath = GetDataPath();
            if (Directory.Exists(basePath) == false)
            {
                Console.WriteLine($"Directory {basePath} not found");
                return null;
            }

            string fullPath = Path.Combine(basePath, fileName);

            if (File.Exists(fullPath) == false)
            {
                Console.WriteLine($"File {fullPath} not found");
                return null;
            }

            return File.ReadAllText(fullPath);
        }

        public T LoadFile<T>(string fileName) where T : class
        {
            string content = LoadFile(fileName + ".json");
            if (content == null)
            {
                return null;
            }

            return Json.JSonSerializer<T>.DeSerialize(content);
        }

        public void SaveFile<T>(string fileName, T obj) where T : class
        {
            string str = Json.JSonSerializer<T>.Serialize(obj);

            if (str != null)
            {
                SaveFile(fileName + ".json", str);
            }
        }

        public virtual void OnStart()
        {

        }

        public virtual void OnAllPluginsLoaded()
        {

        }
        #endregion

        #region Core
        // Plugin, MethodName, MessageType
        public Action<Plugin, string, Type> OnRegisterEvent;
        public Action<Plugin, string, Type> OnUnRegisterEvent;

        protected string GetDataPath() => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "Data", Infos.Name.Replace(".", "_"));
        #endregion
    }
}
