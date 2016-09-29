using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using DtoGenerator;

namespace Test
{
    class Program
    {
        private static string _jsonClassInfo = String.Empty;

        private static void ReadJsonDataFromFile(string fileName)
        {           
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            try
            {
                _jsonClassInfo = sr.ReadToEnd();
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
        }

        private static DtoClassDescription[] ParseJsonData()
        {
            return JsonConvert.DeserializeObject<DtoClassDescription[]>(_jsonClassInfo);
        }

        static void Main(string[] args)
        {
            ReadJsonDataFromFile(args[0]);
            IList<DtoClassDescription> dtoClassDescriptions = ParseJsonData();
            
            Console.Read();
        }
    }
}
