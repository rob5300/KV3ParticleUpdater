using System.Text.RegularExpressions;

namespace KeyValue3Updater
{
    internal abstract class ReplacementUpdater : Updater
    {
        public override string Process(ref string input)
        {
            var matches = findRegex.Matches(input);
            string classname = GetType().Name;
            foreach (Capture match in matches)
            {
                Log.WriteLine($"[{classname}] Found match.");
                return ProcessMatch(ref input, match);
            }
            if (matches.Count == 0)
            {
                Log.WriteLine($"[{classname}] Found 0 matches and did not update.");
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
