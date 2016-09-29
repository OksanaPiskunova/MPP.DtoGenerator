using System;
using System.Collections.Generic;
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


            
            Console.Read();
        }
    }
}
