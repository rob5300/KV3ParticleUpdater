using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValue3Updater.Updaters
{
    internal class CreateAlongPathUpdater : ReplacementUpdater
    {
        protected override string BlockClassName => "C_INIT_CreateAlongPath";

        protected override string GetReplacement(ref string input)
        {
            var maxDistance = GetLineValueFloat(GetLine(ref input, "m_fMaxDistance"));
            var m_flOpStartFadeInTime = GetLineValueFloat(GetLine(ref input, "m_flOpStartFadeInTime"));
            var m_flOpEndFadeInTime = GetLineValueFloat(GetLine(ref input, "m_flOpEndFadeInTime"));
            var m_flOpStartFadeOutTime = GetLineValueFloat(GetLine(ref input, "m_flOpStartFadeOutTime"));
            var m_flOpEndFadeOutTime = GetLineValueFloat(GetLine(ref input, "m_flOpEndFadeOutTime"));
            var m_flOpFadeOscillatePeriod = GetLineValueFloat(GetLine(ref input, "m_flOpFadeOscillatePeriod"));

            string replacement = @"_class = ""C_INIT_CreateSequentialPathV2""
m_fMaxDistance = {0}
m_flOpStartFadeInTime = {1}
m_flOpEndFadeInTime = {2}
m_flOpStartFadeOutTime = {3}
m_flOpEndFadeOutTime = {4}
m_flOpFadeOscillatePeriod = {5}
m_Notes = ""TEMP since C_INIT_CreateAlongPath doesn\'t exist anymore""
m_PathParams =
{{
m_nStartControlPointNumber = 1
m_nEndControlPointNumber = 1
m_nBulgeControl = 1
m_flBulge = 1.0
m_flMidPoint = 1.0
m_vStartPointOffset = [1.0, 0.0, 0.0]
m_vMidPointOffset = [1.0, 0.0, 0.0]
m_vEndOffset = [1.0, 0.0, 0.0]
}}";
            return string.Format(replacement, maxDistance, m_flOpStartFadeInTime, m_flOpEndFadeInTime, m_flOpStartFadeOutTime, m_flOpEndFadeOutTime, m_flOpFadeOscillatePeriod);
        }
    }
}
