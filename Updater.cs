using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeyValue3Updater
{
    internal abstract class Updater
    {
        protected Regex findRegex;

        public string Process(ref string input)
        {
            string edited = input;
            var matches = findRegex.Matches(input);
            string classname = GetType().Name;
            foreach (Capture match in matches)
            {
                Console.WriteLine($"[{classname}] Found match.");
                var replacement = GetReplacement(match.Value);
                edited = edited.Replace(match.Value, replacement);
            }

            return edited;
        }

        /// <summary>
        /// Get a single line based on a given key
        /// </summary>
        public static string GetLine(ref string input, string key)
        {
            return Regex.Match(input, $"{key} = .+").Value ?? "";
        }

        /// <summary>
        /// Get Regular expression to find a keyvalue block with a classname.
        /// </summary>
        public static Regex GetBlockRegex(string blockClassName)
        {
            return new Regex(@"{\n?(_class = """ + blockClassName + @"""\n)([^}]+\n?){1,}},?", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        public static float GetLineValue(string line)
        {
            var match = Regex.Match(line, @"(?<=\= ?)[0-9]{1,}.?[0-9]{1,}", RegexOptions.IgnoreCase);
            return match != null ? Convert.ToSingle(match.Value) : 0.0f;
        }

        /// <summary>
        /// Get 3 array values from a line
        /// </summary>
        public static int[] GetLineArrayValues(string line)
        {
            var matches = Regex.Match(line, @"(?<=\[) ?(\d+), ?(\d+), ?(\d+) ?(?=\])").Groups;
            try
            {
                int[] values = new int[3];
                values[0] = Convert.ToInt32(matches[1].Value);
                values[1] = Convert.ToInt32(matches[2].Value);
                values[2] = Convert.ToInt32(matches[3].Value);
                return values;
            }
            catch(Exception)
            {
                Console.WriteLine($"[GetLineArrayValues] Could not get array value for '{line}'");
                return new int[]{ 0, 0, 0 };
            }
        }

        protected abstract string GetReplacement(string input);
    }
}
