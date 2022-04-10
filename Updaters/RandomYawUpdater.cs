using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValue3Updater.Updaters
{
    internal class RandomYawUpdater : InitFloatUpdaterBase
    {
        protected override string BlockClassName => "C_INIT_RandomYaw";
        protected override int outputField => 12;

        protected override string GetReplacement(ref string input)
        {
            var radMin = GetLineValueFloat(GetLine(ref input, "m_flDegreesMin")) * DegToRad;
            var radMax = GetLineValueFloat(GetLine(ref input, "m_flDegreesMax")) * DegToRad;

            return GetInitFloatString(ref input, radMin, radMax, outputField);
        }
    }
}
