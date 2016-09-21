using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonText = "{ \"Date\": { \"lala\" : \"21/11/2010\"} }";

            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<ConcurrentDictionary<string, dynamic>>(jsonText);
        }
    }
}
