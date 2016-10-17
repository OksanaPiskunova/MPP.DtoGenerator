using System.Configuration;

namespace Test.ApplicationSettings
{
    internal sealed class ApplicationSettingsReader : IApplicationSettingsReader
    {
        private const string MaxTaskCountSettingKey = "maxTaskCount";
        private const string ClassesNamespaceSettingKey = "classesNamespace";
        private const string PluginsDirectorySettingKey = "pluginsDirectoryPath";

        public int MaxTaskCount
        {
            get
            {
                return int.Parse(GetSettingValue(MaxTaskCountSettingKey));
            }
        }

        public string ClassesNamespace
        {
            get
            {
                return GetSettingValue(ClassesNamespaceSettingKey);
            }
        }

        public string PluginsDirectory
        {
            get
            {
                return GetSettingValue(PluginsDirectorySettingKey);
            }
        }

        public string GetSettingValue(string settingKey)
        {
            return ConfigurationManager.AppSettings[settingKey];
        }
    }
}