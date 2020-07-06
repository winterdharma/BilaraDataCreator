using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Utilities.IOFunctions;
using Utilities.Strings;

namespace BilaraDataCreator
{
    internal class Translation
    {
        private string inputFilePath;
        private string outputDirPath;
        private Dictionary<string, Dictionary<string, string>> datasets;
        private const string transName = "_translation-en-patton.json";
        private const string htmlName = "_html.json";

        public Translation(string input, string output)
        {
            inputFilePath = input;
            outputDirPath = output;

            var textLines = LoadFiles.TextFile(inputFilePath);
            datasets = ParseInputData(textLines);
        }

        private Dictionary<string, Dictionary<string, string>> ParseInputData(List<string> textLines)
        {
            var output = new Dictionary<string, Dictionary<string, string>>();
            string agamaRef = textLines[0];
            string title = textLines[1];
            string sutraHead = textLines[2];

            string transKey = agamaRef + transName;
            string htmlKey = agamaRef + htmlName;

            output.Add(transKey, new Dictionary<string, string>());
            output.Add(htmlKey, new Dictionary<string, string>());

            output[htmlKey].Add(agamaRef + ":0.1", "<article id='" + agamaRef +
                "'><header><ul><li class='division'>{}</li>");
            output[htmlKey].Add(agamaRef + ":0.2", "<h1 class='sutta-title'>{}</h1></header>");
            output[transKey].Add(agamaRef + ":0.1", title + " ");
            output[transKey].Add(agamaRef + ":0.2", sutraHead + " ");

            int paraNo = 0;
            int segmentNo = 0;

            textLines.RemoveRange(0, 3);

            foreach(string line in textLines)
            {
                paraNo++;
                string[] segmentSplit = line.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                foreach(string segment in segmentSplit)
                {
                    segmentNo++;
                    string content = segment;
                    string segmentRef = agamaRef + ":" + paraNo + "." + segmentNo;
                    if (segmentSplit.Length == 1)
                        output[htmlKey].Add(segmentRef, "<p>{}</p>");
                    else if(segmentNo == 1)
                        output[htmlKey].Add(segmentRef, "<p>{}");
                    else if (segmentNo == segmentSplit.Length)
                        output[htmlKey].Add(segmentRef, "{}</p>");
                    else
                        output[htmlKey].Add(segmentRef, "{}");

                    if (!segment.Substring(segment.Length - 1).Equals(" "))
                        content += " ";
                    output[transKey].Add(segmentRef, content);
                }
                segmentNo = 0;
            }

            return output;
        }

        internal void WriteBilaraFiles()
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> file in datasets)
            {
                string filename = outputDirPath + file.Key;
                var lines = new List<string>();
                lines.Add("{");
                foreach (KeyValuePair<string, string> line in file.Value)
                {
                    string text = "\t\"" + line.Key + "\" : \"" + line.Value + "\",";
                    lines.Add(text);
                }
                lines[lines.Count - 1] = lines[lines.Count - 1].Truncate(1);
                lines.Add("}");
                File.WriteAllLines(filename, lines);
            }
        }
    }
}