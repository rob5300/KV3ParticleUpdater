using System.Text.RegularExpressions;

namespace KeyValue3Updater
{
    internal abstract class Updater
    {
        protected virtual string BlockClassName { get; }

        protected Regex findRegex;

        public Updater()
        {
            findRegex = GetBlockRegex(BlockClassName);
        }

        public string Process(ref string input)
        {
            string edited = input;
            var matches = findRegex.Matches(input);
            string classname = GetType().Name;
            foreach (Capture match in matches)
            {
                Log.WriteLine($"[{classname}] Found match.");
                string matchString = match.Value;
                var replacement = GetReplacement(ref matchString);
                edited = edited.Replace(match.Value, replacement);
            }
            if(matches.Count == 0)
            {
                Log.WriteLine($"[{classname}] Found 0 matches and did not update.");
            }

            return edited;
        }

        /// <summary>
        /// Get a single line based on a given key
        /// </summary>
        public static string GetLine(ref string input, string key)
        {
            var match = Regex.Match(input, $"{key} = .+");
            return match == null ? "" : match.Value;
        }

        /// <summary>
        /// Get Regular expression to find a keyvalue block with a classname.
        /// </summary>
        public static Regex GetBlockRegex(string blockClassName)
        {
            return new Regex(GetBlockRegexString(blockClassName), RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }

        public static string GetBlockRegexString(string blockClassName)
        {
            return @"{\n?(_class = """ + blockClassName + @"""\n)([^}]+\n?){1,}}";
        }

        public static float GetLineValue(string line)
        {
            var match = Regex.Match(line, @"(?<=\= ?""?)-?[0-9]{1,}.?[0-9]*(?=""?)", RegexOptions.IgnoreCase);
            return !String.IsNullOrEmpty(match.Value) ? Convert.ToSingle(match.Value) : 0.0f;
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
                Log.WriteLine($"[GetLineArrayValues] Could not get array value for '{line}'");
                return new int[]{ 0, 0, 0 };
            }
        }

        protected string GetInitFloatString(ref string input, float randomMin, float randomMax, int outputField)
        {
            var m_flOpStartFadeInTime = GetLineValue(GetLine(ref input, "m_flOpStartFadeInTime"));
            var m_flOpEndFadeInTime = GetLineValue(GetLine(ref input, "m_flOpEndFadeInTime"));
            var m_flOpStartFadeOutTime = GetLineValue(GetLine(ref input, "m_flOpStartFadeOutTime"));
            var m_flOpEndFadeOutTime = GetLineValue(GetLine(ref input, "m_flOpEndFadeOutTime"));
            var m_flOpFadeOscillatePeriod = GetLineValue(GetLine(ref input, "m_flOpFadeOscillatePeriod"));

            string replacement = @"{{
_class = ""C_INIT_InitFloat""
m_InputValue = 
{{
m_nType = ""PF_TYPE_RANDOM_UNIFORM""
m_flRandomMin = {0}
m_flRandomMax = {1}
}}
m_nOutputField = ""{7}""
m_flOpStartFadeInTime = {2}
m_flOpEndFadeInTime = {3}
m_flOpStartFadeOutTime = {4}
m_flOpEndFadeOutTime = {5}
m_flOpFadeOscillatePeriod = {6}
}}";

            return string.Format(replacement, randomMin, randomMax, m_flOpStartFadeInTime, m_flOpEndFadeInTime, m_flOpStartFadeOutTime, m_flOpEndFadeOutTime, m_flOpFadeOscillatePeriod, outputField);
        }

        protected abstract string GetReplacement(ref string input);
    }
}
