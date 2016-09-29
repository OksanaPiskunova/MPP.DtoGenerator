using System;
using System.Collections.Generic;
using System.Configuration;
using DtoGenerator;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonFileName = args[0];

            JsonParser jsonParser = new JsonParser();
            string jsonClassDescriptions = 
                JsonParser.ReadJsonDataFromFile(jsonFileName);
            IList<DtoClassDescription> dtoClassDescriptions = 
                JsonParser.ParseJsonData(jsonClassDescriptions);

            int maxTaskCount = Int32.Parse(ConfigurationManager.AppSettings["maxTaskCount"]);
            string dtoNamespace = ConfigurationManager.AppSettings["dtoNamespace"];

            Console.WriteLine(maxTaskCount);
            Console.WriteLine(dtoNamespace);

            Console.Read();
        }
    }
}
