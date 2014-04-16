using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Ds3ClientInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Ds3ClientInstaller.Ds3Client.zip"))
            using (var zip = new ZipArchive(stream))
            {
                var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var moduleDirectory = Path.Combine(myDocs, @"WindowsPowerShell\Modules\Ds3Client");
                if (Directory.Exists(moduleDirectory))
                {
                    Directory.Delete(moduleDirectory, true);
                }
                zip.ExtractToDirectory(moduleDirectory);
            }
        }
    }
}
