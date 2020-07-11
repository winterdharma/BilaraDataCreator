using System.Collections.Generic;
using System.IO;

namespace BilaraDataCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            // define default directories 
            // (ideally this should be user-defined parameter - future feature)
            string workDir = @"C:\Users\cdpat\Desktop\dev\bilara-workspace\";
            string outputDir = workDir + @"output\";

            // fetch the input files present in work directory
            string[] files = Directory.GetFiles(workDir);

            // initialize data processors for Chinese texts and English translations
            List<Source> sourceFiles = new List<Source>();
            List<Translation> transFiles = new List<Translation>();

            foreach(string file in files)
            {
                if (file.Contains("eng"))
                    transFiles.Add(new Translation(file));
                else
                    sourceFiles.Add(new Source(file));
            }

            // write the output files
            foreach(Source source in sourceFiles)
            {
                source.WriteBilaraFiles(outputDir);
            }

            foreach(Translation trans in transFiles)
            {
                trans.WriteBilaraFiles(outputDir);
            }
        }
    }
}
