using System.Text.RegularExpressions;

namespace KeyValue3Updater
{
    internal abstract class ReplacementUpdater : Updater
    {
        public override string Process(ref string input)
        {
            string classname = GetType().Name;
            Match match = findRegex.Match(input);
            if (match == null || !match.Success)
            {
                Log($"[{classname}] Found 0 matches and did not update.");
            }
            while(match != null && match.Success)
            {
                Log($"[{classname}] Found match.");
                input = ProcessMatch(ref input, match);
                match = findRegex.Match(input);
            }
            return input;
        }

        /// <summary>
        /// Process an individual match
        /// </summary>
        protected virtual string ProcessMatch(ref string input, Capture match)
        {
            string matchString = match.Value;
            var replacement = GetReplacement(ref matchString);
            return input.Replace(match.Value, replacement);
        }

        protected abstract string GetReplacement(ref string input);
    }
}
