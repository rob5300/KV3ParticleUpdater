using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValue3Updater.Updaters
{
    internal class RandomRotationUpdater : InitFloatUpdaterBase
    {
        protected override string BlockClassName => "C_INIT_RandomRotation";
        protected override int outputField => 4;

        protected override string GetReplacement(ref string input)
        {
            var radMin = GetLineValueFloat(GetLine(ref input, "m_flDegreesMin"));
            var radMax = GetLineValueFloat(GetLine(ref input, "m_flDegreesMax"));

            return GetInitFloatString(ref input, radMin, radMax, outputField);
        }
    }
}
