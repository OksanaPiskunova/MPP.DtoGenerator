using System;
using System.IO;
using DtoGenerator;
using Newtonsoft.Json;

namespace Test
{
    internal class JsonParser
    {
        internal static string ReadJsonDataFromFile(string fileName)
        {
            string jsonData;

            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            try
            {
                jsonData = sr.ReadToEnd();
            }
            catch (Exception e)
            {
                return String.Empty;
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return jsonData;
        }

        internal static DtoClassDescription[] ParseJsonData(string jsonData)
        {
            return JsonConvert.DeserializeObject<DtoClassDescription[]>(jsonData);
        }
    }
}
