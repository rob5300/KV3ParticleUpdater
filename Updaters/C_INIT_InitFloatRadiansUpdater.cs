using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeyValue3Updater.Updaters
{
    internal class C_INIT_InitFloatRadiansUpdater : ReplacementUpdater
    {
        protected override string BlockClassName => "C_INIT_InitFloat";

        public override string Process(ref string input)
        {
            string classname = GetType().Name;
            List<Match> matches = findRegex.Matches(input).ToList();
            if (matches == null || matches.Count == 0)
            {
                Log($"[{classname}] Found 0 matches and did not update.");
            }
            
            for(int i = matches.Count - 1; i >= 0; i--)
            {
                Log($"[{classname}] Found match.");
                var match = matches[i];
                input = ProcessMatch(ref input, match);
            }
            return input;
        }

        protected override string GetReplacement(ref string input)
        {
            string outputField = GetLine(ref input, "m_nOutputField");
            if(!string.IsNullOrEmpty(outputField) && GetLineValue(outputField).Replace("\"", "") == "4")
            {
                Log("[C_INIT_InitFloatRadiansUpdater] m_nOutputField type 4 found. Will convert rand min and max to degrees");

                string randomMinLine = GetLine(ref input, "m_flRandomMin");
                string randomMinLineValue = GetLineValueComplex(randomMinLine);
                
                string randomMaxLine = GetLine(ref input, "m_flRandomMax");
                string randomMaxLineValue = GetLineValueComplex(randomMaxLine);
                
                try
                {
                    float randomMin = float.Parse(randomMinLineValue);
                    float randomMax = float.Parse(randomMaxLineValue);

                    input = input.Replace(randomMinLine, ReplaceLineValue(randomMinLine, (randomMin * RadToDeg).ToString()));
                    input = input.Replace(randomMaxLine, ReplaceLineValue(randomMaxLine, (randomMax * RadToDeg).ToString()));
                }
                catch(FormatException)
                {
                    Log($"Failed to update block due to an invalid number value. RandomMin: {randomMinLineValue}, RandomMax: {randomMaxLineValue}");
                    return input;
                }
            }
            
            return input;
        }
    }
}
