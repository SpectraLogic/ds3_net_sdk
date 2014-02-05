using Ds3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ds3ClassRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Ds3Client client = new Ds3Client("http://hostname:8080", new Credentials("accessId", "key"));

                System.Console.WriteLine(Ds3Client.FormatXml(client.GetService()));
            }
            catch(Exception e) {
                System.Console.WriteLine("Failed with exception:" + e.ToString());
            }      

        }
    }
}
