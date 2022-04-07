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

        private const float DegToRad = (float)(Math.PI / 180);

        protected override string GetReplacement(ref string input)
        {
            var radMin = GetLineValue(GetLine(ref input, "m_flDegreesMin")) * DegToRad;
            var radMax = GetLineValue(GetLine(ref input, "m_flDegreesMax")) * DegToRad;

            return GetInitFloatString(ref input, radMin, radMax, outputField);
        }
    }
}
