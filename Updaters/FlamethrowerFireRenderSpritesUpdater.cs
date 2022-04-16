using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KeyValue3Updater.Updaters
{
    /// <summary>
    /// Special updater to specifically insert overbright factor for a certain used texture 
    /// </summary>
    internal class FlamethrowerFireRenderSpritesUpdater : Updater
    {
        protected override string BlockClassName => "C_OP_RenderSprites";
        private static string overbrightInsert = @"m_flOverbrightFactor = 
{
m_flLiteralValue = 5.0
}
";

        public override string Process(ref string input)
        {
            string classname = GetType().Name;
            List<Match> matches = new List<Match>(findRegex.Matches(input));
            if (matches == null || matches.Count == 0)
            {
                Log($"[{classname}] Found 0 matches and did not update.");
                return input;
            }

            //Itterate backwards so we can update all matches in one loop and not need to re find matches (and get stuck)
            for(int i = matches.Count - 1; i >= 0; i--)
            {
                Match m = matches[i];
                if(m.Value.Contains(@"m_hTexture = resource:""materials/particle/flamethrowerfire/flamethrowerfire102.vtex""") && !m.Value.Contains("m_flOverbrightFactor"))
                {
                    int offset = m.Value.LastIndexOf("}");
                    input = input.Insert(m.Index + offset, overbrightInsert);
                    Log($"[{classname}] Processed match with m_hTexture of 'flamethrowerfire102'.");
                }
                else
                {
                    Log($"[{classname}] Ignored match.");
                }
            }
            return input;
        }
    }
}
