using System.Text;
using System.Text.RegularExpressions;

namespace KeyValue3Updater
{
    internal abstract class Updater
    {
        public const float DegToRad = (float)(Math.PI / 180);
        public const float RadToDeg = (float)(180 / Math.PI);

        protected virtual string BlockClassName { get; }
        protected Regex findRegex;

        private StringBuilder logOutputBuilder;

        public Updater()
        {
            logOutputBuilder = new StringBuilder();
            if (findRegex == null)
            {
                findRegex = GetBlockRegex(BlockClassName);
            }
        }

        /// <summary>
        /// Process and look for matches in the input string
        /// </summary>
        public abstract string Process(ref string input);

        protected void Log(string text)
        {
            logOutputBuilder.AppendLine(text);
        }

        public void SetLogBuilder(StringBuilder builder)
        {
            logOutputBuilder = builder;
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
            return new Regex(GetBlockRegexString(blockClassName), RegexOptions.Compiled | RegexOptions.Singleline);
        }

        public static string GetBlockRegexString(string blockClassName)
        {
            return @"({\n?_class = """ + blockClassName + @"""\n)[^}]+((.[^}])(.[^,]))+},";
            //return @"{\n?(_class = """ + blockClassName + @"""\n)(.?\n?[^},]){1,}(},){1}";
        }

        public static float GetLineValueFloat(string line)
        {
            var match = GetLineValue(line);
            return !String.IsNullOrEmpty(match) ? Convert.ToSingle(match) : 0.0f;
        }

        public static string GetLineValue(string line)
        {
            return Regex.Match(line, @"(?<=\= ?""?)-?[0-9]{1,}.?[0-9]*(?=""?)", RegexOptions.IgnoreCase).Value;
        }

        /// <summary>
        /// Get 3 array values from a line
        /// </summary>
        public int[] GetLineArrayValues(string line)
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
                Log($"[GetLineArrayValues] Could not get array value for '{line}'");
                return new int[]{ 0, 0, 0 };
            }
        }

        protected string GetInitFloatString(ref string input, float randomMin, float randomMax, int outputField)
        {
            var m_flOpStartFadeInTime = GetLineValueFloat(GetLine(ref input, "m_flOpStartFadeInTime"));
            var m_flOpEndFadeInTime = GetLineValueFloat(GetLine(ref input, "m_flOpEndFadeInTime"));
            var m_flOpStartFadeOutTime = GetLineValueFloat(GetLine(ref input, "m_flOpStartFadeOutTime"));
            var m_flOpEndFadeOutTime = GetLineValueFloat(GetLine(ref input, "m_flOpEndFadeOutTime"));
            var m_flOpFadeOscillatePeriod = GetLineValueFloat(GetLine(ref input, "m_flOpFadeOscillatePeriod"));

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
}},";

            return string.Format(replacement, randomMin, randomMax, m_flOpStartFadeInTime, m_flOpEndFadeInTime, m_flOpStartFadeOutTime, m_flOpEndFadeOutTime, m_flOpFadeOscillatePeriod, outputField);
        }
    }
}
