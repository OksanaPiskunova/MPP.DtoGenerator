namespace Test.ApplicationSettings
{
    internal interface IApplicationSettingsReader
    {
        string GetSettingValue(string settingKey);
    }
}