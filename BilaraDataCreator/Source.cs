using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utilities.IOFunctions;
using Utilities.Strings;

namespace BilaraDataCreator
{
    internal class Source
    {
        private readonly string inputFilePath;
        private Dictionary<string, Dictionary<string, string>> datasets;

        private string taishoRef = "";
        private const string refName = "_reference.json";
        private const string varName = "_variant-lzh-sct.json";
        private const string rootName = "_root-lzh-sct.json";
        private string agamaRef = "";
        private string rootKey = "";
        private string refKey = "";
        private string varKey = "";

        public Source(string input)
        {
            inputFilePath = input;

            var textLines = LoadFiles.TextFile(inputFilePath);
            
            datasets = HandleHeaderData(textLines, out textLines);

            datasets = ParseInputData(textLines);
        }

        private Dictionary<string, Dictionary<string, string>> ParseInputData(List<string> textLines)
        {
            var output = datasets;

            // remove line breaks from source text
            string rawData = ConcatTextLines(textLines);
            // divide the source text into paragraphs
            string[] paraSplit = rawData.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
            
            int paraNo = 0;
            foreach(string paragraph in paraSplit)
            {
                paraNo++;
                output = ProcessParagraph(output, paraNo, paragraph);
            }

            return output;
        }

        private Dictionary<string, Dictionary<string, string>> ProcessParagraph(Dictionary<string, Dictionary<string, string>> output, 
            int paraNo, string paragraph)
        {
            string[] segmentSplit = paragraph.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            
            int segmentNo = 0;
            foreach (string segment in segmentSplit)
            {
                segmentNo++;
                string segmentRef = agamaRef + ":" + paraNo + "." + segmentNo;
                string content = segment;
                string reference = HandleTaishoRefs(content, out content);
                string variant = HandleVariants(content, out content);

                if (reference != "")
                    output[refKey].Add(segmentRef, reference);
                if (variant != "")
                    output[varKey].Add(segmentRef, variant);
                output[rootKey].Add(segmentRef, content + " ");
            }
            return output;
        }

        private Dictionary<string, Dictionary<string, string>> HandleHeaderData(List<string> lines, out List<string> textLines)
        {
            var output = new Dictionary<string, Dictionary<string, string>>();
            agamaRef = lines[0];
            taishoRef = lines[1];
            string sutraNo = lines[2];
            string title = lines[3];
            string division = lines[4];
            string sutraHead = lines[5];
            refKey = agamaRef + refName;
            varKey = agamaRef + varName;
            rootKey = agamaRef + rootName;

            output.Add(rootKey, new Dictionary<string, string>());
            output.Add(refKey, new Dictionary<string, string>());
            output.Add(varKey, new Dictionary<string, string>());

            // add headings to root file
            output[rootKey].Add(agamaRef + ":0.1", title + " ");
            output[rootKey].Add(agamaRef + ":0.2", division + " ");
            output[rootKey].Add(agamaRef + ":0.3", sutraHead + " ");
            // add heading to reference file
            output[refKey].Add(agamaRef + ":0.1", taishoRef + "." + sutraNo);
            // discard the header data
            lines.RemoveRange(0, 6);
            
            textLines = lines;
            return output;
        }

        private string HandleVariants(string str, out string content)
        {
            content = str;
            string variant = "";
            if(str.Contains("["))
            {
                string[] items = str.Split('[');
                content = items[0];
                variant = items[1];
            }
            return variant.Trim(']');
        }

        private string HandleTaishoRefs(string str, out string content)
        {
            content = str;
            string refs = "";
            content = content.Replace("CBETA", "cbeta");
            if (str.Contains("_."))
            {
                string[] split = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                foreach(string part in split)
                {
                    if(part.Contains("_."))
                    {
                        refs += taishoRef + "." + GetPageColLineRef(part) + ", ";
                    }
                }
                if (refs.Length > 0)
                    refs = refs.Truncate(2);
            }

            content = content.Scrub('T', ' ', 19);
            
            return refs;
        }

        internal void WriteBilaraFiles(string outputDir)
        {
            foreach(KeyValuePair<string, Dictionary<string, string>> file in datasets)
            {
                string filename = outputDir + file.Key;
                var lines = new List<string> {"{"};

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

        #region Helper Methods
        private string ConcatTextLines(List<string> textLines)
        {
            var builder = new StringBuilder();
            foreach(string line in textLines)
            {
                builder.Append(line.Trim());
            }
            return builder.ToString();
        }

        private string GetPageColLineRef(string part)
        {
            string refs = part.Substring(part.Length - 7);
            refs = refs.Truncate(1);
            
            if (refs.Substring(refs.Length - 2, 1).Equals("0"))
            {
                refs = refs.Remove(refs.Length - 2, 1);
            }

            if (refs.Substring(0,2).Equals("00"))
            {
                refs = refs.TruncateStart(2);
                return refs;
            }
            else if(refs.Substring(0,1).Equals("0"))
            {
                refs = refs.TruncateStart(1);
                return refs;
            }
            else
            {
                return refs;
            }
        }
        #endregion
    }
}