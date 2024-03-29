﻿using System.Text;
using System.Text.RegularExpressions;

namespace KeyValue3Updater
{
    /// <summary>
    /// Base class for all Updaters
    /// </summary>
    internal abstract class Updater
    {
        private const string LineValueRegex = @"(?<=\= ?""?)-?[0-9]{1,}.?[0-9]*(?=""?)";
        private const string LineValueComplexRegex = @"(?<=\= ?""?)-?[0-9E+.]+(?=""?)";
        private const string LineArrayValueRegex = @"(?<=\[) ?(\d+), ?(\d+), ?(\d+) ?(?=\])";

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
            var match = Regex.Match(input, $"{key} = .+", RegexOptions.None, TimeSpan.FromSeconds(1));
            return match == null ? "" : match.Value;
        }

        /// <summary>
        /// Get Regular expression to find a keyvalue block with a classname.
        /// </summary>
        public static Regex GetBlockRegex(string blockClassName)
        {
            return new Regex(GetBlockRegexString(blockClassName), RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Get regex to find a whole class block with the given class name.
        /// </summary>
        /// <param name="blockClassName">The '_class' name</param>
        /// <returns></returns>
        public static string GetBlockRegexString(string blockClassName)
        {
            return @"({\n?_class = """ + blockClassName + @"""\n)([^}]|([^}][^,]))+},";
        }

        public static float GetLineValueFloat(string line)
        {
            var match = GetLineValue(line);
            return !String.IsNullOrEmpty(match) ? Convert.ToSingle(match) : 0.0f;
        }

        public static int GetLineValueInt(string line)
        {
            var match = GetLineValue(line);
            return !String.IsNullOrEmpty(match) ? Convert.ToInt32(match) : 0;
        }

        public static string GetLineValue(string line)
        {
            return Regex.Match(line, LineValueRegex, RegexOptions.IgnoreCase).Value;
        }

        public static string GetLineValueComplex(string line)
        {
            return Regex.Match(line, LineValueComplexRegex, RegexOptions.IgnoreCase).Value;
        }

        public static string ReplaceLineValue(string line, string newValue)
        {
            return Regex.Replace(line, LineValueRegex, newValue, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Get 3 array values from a line
        /// </summary>
        public int[] GetLineArrayValues(string line)
        {
            var matches = Regex.Match(line, LineArrayValueRegex, RegexOptions.None, TimeSpan.FromSeconds(1)).Groups;
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
