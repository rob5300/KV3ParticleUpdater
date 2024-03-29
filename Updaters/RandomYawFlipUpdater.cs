﻿using System.Text.RegularExpressions;

namespace KeyValue3Updater.Updaters
{
    internal class RandomYawFlipUpdater : ReplacementUpdater
    {
        public RandomYawFlipUpdater()
        {
            //Add capture of comma as we want to remove this as well if present.
            string expression = GetBlockRegexString("C_INIT_RandomYawFlip") + @"\n";
            findRegex = new Regex(expression, RegexOptions.Compiled);
        }

        protected override string GetReplacement(ref string input)
        {
            return string.Empty;
        }
    }
}
