using RserveCli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RDemoViaCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverAddress = IPAddress.Parse("192.168.1.106");
            var serverPort = 6311;

            using (var s = new RConnection(serverAddress, serverPort, "david", "123456"))
            {
                s.VoidEval("library(forecast)");
				s.VoidEval("a<-2");
				s.VoidEval("b<-6");
				s.VoidEval("c<-a+b");
				var result = s["c"];
				Console.WriteLine(result);
				Console.ReadKey();
            }

            }

    }
}
