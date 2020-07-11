using System;
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

            // fetch the input files and initialize data processors
            var dataCollection = InitializeDataProcessors(Directory.GetFiles(workDir));

            WriteBilaraFiles(dataCollection, outputDir);
        }

        #region Helper Methods
        static Tuple<List<Source>, List<Translation>> InitializeDataProcessors(string[] filePaths)
        {
            List<Source> sourceFiles = new List<Source>();
            List<Translation> transFiles = new List<Translation>();
            
            foreach (string file in filePaths)
            {
                if (file.Contains("eng"))
                    transFiles.Add(new Translation(file));
                else
                    sourceFiles.Add(new Source(file));
            }

            return new Tuple<List<Source>, List<Translation>>(sourceFiles, transFiles);
        }

        private static void WriteBilaraFiles(Tuple<List<Source>, List<Translation>> dataCollection, string outputDir)
        {
            foreach (Source source in dataCollection.Item1)
            {
                source.WriteBilaraFiles(outputDir);
            }

            foreach (Translation trans in dataCollection.Item2)
            {
                trans.WriteBilaraFiles(outputDir);
            }
        }
        #endregion
    }
}
