using System;
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
                try
                {
                    return int.Parse(GetSettingValue(MaxTaskCountSettingKey));
                }
                catch (FormatException)
                {
                    throw new BadArgumentValueException("The maximum number of tasks must be a positive integer.");
                }
                catch (OverflowException)
                {
                    throw new BadArgumentValueException("The maximum number of tasks must be a positive integer.");
                }
            }
        }

        public string ClassesNamespace => GetSettingValue(ClassesNamespaceSettingKey);

        public string PluginsDirectory => GetSettingValue(PluginsDirectorySettingKey);

        public string GetSettingValue(string settingKey)
        {
            if (settingKey == null) throw new ArgumentNullException(nameof(settingKey));

            try
            {
                return ConfigurationManager.AppSettings[settingKey];
            }
            catch (ConfigurationErrorsException)
            {
                throw new SettingsNotFoundException(nameof(settingKey));
            }
        }
    }
}