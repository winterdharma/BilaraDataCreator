using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilaraDataCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            string workDir = @"C:\Users\cdpat\Desktop\dev\bilara-workspace\";
            string inputSource = workDir + "T99_803.txt";
            //string inputTrans = "someEnglishFile.txt";
            string outputDir = workDir + @"output\";

            Source source = new Source(inputSource, outputDir);
            //Translation trans = new Translation(inputTrans, outputDir);

            source.WriteBilaraFiles();
            //trans.WriteBilaraFiles();
        }
    }
}
